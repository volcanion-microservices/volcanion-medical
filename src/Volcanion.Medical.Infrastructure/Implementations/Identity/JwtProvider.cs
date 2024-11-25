using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using Volcanion.Core.Common.Abstractions;
using Volcanion.Core.Models.Enums;
using Volcanion.Core.Models.Jwt;
using Volcanion.Core.Presentation.Middlewares.Exceptions;
using Volcanion.Medical.Infrastructure.Abstractions.Identity;
using Volcanion.Medical.Models.Entities.Identity;
using Volcanion.Medical.Models.Setting;

namespace Volcanion.Medical.Infrastructure.Implementations.Identity;

/// <inheritdoc/>
internal class JwtProvider : IJwtProvider
{
    /// <summary>
    /// IStringProvider instance
    /// </summary>
    private readonly IStringProvider _stringProvider;

    /// <summary>
    /// IRedisCacheProvider instance
    /// </summary>
    private readonly IRedisCacheProvider _redisCacheProvider;

    /// <summary>
    /// IHahsProvider instance
    /// </summary>
    private readonly IHashProvider _hashProvider;

    /// <summary>
    /// PrivateKeyFilePath
    /// </summary>
    private string PrivateKeyFilePath { get; set; }

    /// <summary>
    /// PublicKeyFilePath
    /// </summary>
    private string PublicKeyFilePath { get; set; }

    /// <summary>
    /// AccessTokenExpiredTime
    /// </summary>
    private string AccessTokenExpiredTime { get; set; }

    /// <summary>
    /// RefreshTokenExpiredTime
    /// </summary>
    private string RefreshTokenExpiredTime { get; set; }

    /// <summary>
    /// JwtSettings
    /// </summary>
    private readonly JwtSettings _jwtSettings;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="stringProvider"></param>
    /// <param name="redisCacheProvider"></param>
    /// <param name="hashProvider"></param>
    /// <param name="configProvider"></param>
    /// <param name="logger"></param>
    public JwtProvider(IStringProvider stringProvider, IRedisCacheProvider redisCacheProvider, IHashProvider hashProvider, IOptions<JwtSettings> options)
    {
        _stringProvider = stringProvider;
        _redisCacheProvider = redisCacheProvider;
        _hashProvider = hashProvider;
        _jwtSettings = options.Value;
        PrivateKeyFilePath = _jwtSettings.PrivateKeyFilePath;
        PublicKeyFilePath = _jwtSettings.PublicKeyFilePath;
        AccessTokenExpiredTime = _jwtSettings.AccessTokenExpiredTime;
        RefreshTokenExpiredTime = _jwtSettings.RefreshTokenExpiredTime;
    }

    /// <inheritdoc/>
    public (VolcanionJwtHeader? header, VolcanionJwtPayload? payload) DecodeJwt(string token)
    {
        var payloadDictonary = DecodeJWTToDictionary(token);
        var payload = ConvertToVolcanionJwtPayload(payloadDictonary);
        // Return the header and payload
        return (null, payload);
    }

    /// <inheritdoc/>
    public string GenerateJwt(Account account, string audience, string issuer, List<string> allowedOrigins, List<string> groupAccess, ResourceAccess resourceAccess, JwtType type, string sessionId)
    {
        // Determine expiration time
        var expirationTimeStr = type == JwtType.AccessToken ? AccessTokenExpiredTime ?? "10m" : RefreshTokenExpiredTime ?? "30d";
        var expirationUnixTime = _stringProvider.GenerateDateTimeOffsetFromString(expirationTimeStr).ToUnixTimeSeconds();

        // Generate payload
        var payload = new VolcanionJwtPayload
        {
            Audience = audience,
            Issuer = issuer,
            AllowedOrigins = allowedOrigins,
            GroupAccess = groupAccess,
            Expiration = expirationUnixTime,
            ResourceAccess = resourceAccess,
            TokenId = account.Id.ToString(),
            Email = account.Email,
            Name = account.Fullname,
            SessionId = sessionId
        };

        // Serialize header and payload
        var jwtPayload = JsonConvert.SerializeObject(payload);

        // Generate signature
        return _hashProvider.HashSHA512(jwtPayload, PrivateKeyFilePath);
    }

    /// <inheritdoc/>
    public bool ValidateJwt(string jwt, JwtType? type)
    {
        // Check for empty JWT and public key
        if (string.IsNullOrEmpty(jwt)) throw new VolcanionAuthException("Jwt is empty!");
        if (string.IsNullOrEmpty(PublicKeyFilePath)) throw new VolcanionAuthException("Public key file path is not set!");

        // Split JWT into components and verify signature
        if (!_hashProvider.VerifySignature(jwt, PublicKeyFilePath)) throw new VolcanionAuthException("Jwt is invalid!");

        // Decode the jwt
        var payload = DecodeJwt(jwt).payload ?? throw new VolcanionAuthException("Jwt is invalid!");
        if (payload.Expiration < DateTimeOffset.Now.ToUnixTimeSeconds()) throw new VolcanionAuthException("Jwt is expired!");

        // Validate session ID from Redis cache
        var sessionId = payload!.SessionId;
        var cacheSessionId = _redisCacheProvider.GetStringAsync(sessionId).Result;

        if (cacheSessionId == null)
        {
            if (type == JwtType.RefreshToken)
            {
                _ = _redisCacheProvider.SetStringAsync(sessionId, "Valid");
                return true;
            }

            throw new VolcanionAuthException("Session is expired!");
        }

        if (!cacheSessionId.Equals("Valid"))
        {
            throw new VolcanionAuthException("Session is expired!");
        }

        return true;
    }

    /// <inheritdoc/>
    public (string headerPayload, string signature) SplitJwt(string jwt)
    {
        // Split the jwt
        var jwtSplit = jwt.Split('.');
        // Check if the jwt is not valid
        if (jwtSplit.Length != 3) throw new Exception("Jwt is not valid.");
        // Return the signature and header payload
        return ($"{jwtSplit[0]}.{jwtSplit[1]}", jwtSplit[2]);
    }

    /// <summary>
    /// DecodeJWTToDictionary
    /// </summary>
    /// <param name="jwtToken"></param>
    /// <returns></returns>
    private static Dictionary<string, object> DecodeJWTToDictionary(string jwtToken)
    {
        // Giải mã JWT
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadJwtToken(jwtToken);

        // Lấy payload từ JWT
        var payload = jsonToken.Payload;

        // Chuyển Payload thành Dictionary<string, object>
        var result = new Dictionary<string, object>();

        foreach (var claim in payload)
        {
            // Kiểm tra nếu giá trị là chuỗi JSON (chứa dấu { hoặc [) và chuyển nó thành đối tượng hoặc mảng
            if (claim.Value is string value && (value.StartsWith("{") || value.StartsWith("[")))
            {
                try
                {
                    // Cố gắng giải mã chuỗi thành đối tượng hoặc mảng JSON
                    var jsonObject = JsonConvert.DeserializeObject(value) ?? throw new Exception("Json object is invalid!");
                    result[claim.Key] = jsonObject;
                }
                catch (JsonException)
                {
                    // Nếu không thể giải mã, giữ lại giá trị chuỗi ban đầu
                    result[claim.Key] = value;
                }
            }
            else
            {
                // Nếu không phải chuỗi JSON, giữ lại giá trị ban đầu
                result[claim.Key] = claim.Value;
            }
        }

        return result;
    }

    /// <summary>
    /// ConvertToVolcanionJwtPayload
    /// </summary>
    /// <param name="dictionary"></param>
    /// <returns></returns>
    private static VolcanionJwtPayload ConvertToVolcanionJwtPayload(Dictionary<string, object> dictionary)
    {
        var payload = new VolcanionJwtPayload();

        // Duyệt qua từng trường trong Dictionary và chuyển đổi vào đối tượng VolcanionJwtPayload
        foreach (var kvp in dictionary)
        {
            switch (kvp.Key)
            {
                case "Expiration":
                    payload.Expiration = Convert.ToInt64(kvp.Value);
                    break;

                case "IssuedAt":
                    payload.IssuedAt = Convert.ToInt64(kvp.Value);
                    break;

                case "TokenId":
                    payload.TokenId = Convert.ToString(kvp.Value) ?? string.Empty;
                    break;

                case "Issuer":
                    payload.Issuer = Convert.ToString(kvp.Value) ?? string.Empty;
                    break;

                case "Audience":
                    payload.Audience = Convert.ToString(kvp.Value) ?? string.Empty;
                    break;

                case "Type":
                    payload.Type = Convert.ToString(kvp.Value) ?? string.Empty;
                    break;

                case "SessionId":
                    payload.SessionId = Convert.ToString(kvp.Value) ?? string.Empty;
                    break;

                case "AllowedOrigins":
                    // Nếu AllowedOrigins là chuỗi JSON, chúng ta phải giải mã nó thành List<string>
                    payload.AllowedOrigins = JsonConvert.DeserializeObject<List<string>>(kvp.Value.ToString());
                    break;

                case "ResourceAccess":
                    // Nếu ResourceAccess là chuỗi JSON, chúng ta cần giải mã nó thành đối tượng ResourceAccess
                    payload.ResourceAccess = JsonConvert.DeserializeObject<ResourceAccess>(kvp.Value.ToString());
                    break;

                case "GroupAccess":
                    // Nếu GroupAccess là chuỗi JSON, giải mã nó thành List<string>
                    payload.GroupAccess = JsonConvert.DeserializeObject<List<string>>(kvp.Value.ToString());
                    break;

                case "Name":
                    payload.Name = Convert.ToString(kvp.Value) ?? string.Empty;
                    break;

                case "Email":
                    payload.Email = Convert.ToString(kvp.Value) ?? string.Empty;
                    break;

                default:
                    // Xử lý các trường khác (nếu có)
                    break;
            }
        }

        return payload;
    }
}
