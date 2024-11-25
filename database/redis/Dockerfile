# Arguments: REDIS_VERSION
ARG REDIS_VERSION=latest

# Use the official Redis image
FROM redis:$REDIS_VERSION

# Expose default port of Redis
EXPOSE 6379

# Start Redis server
CMD ["redis-server"]
