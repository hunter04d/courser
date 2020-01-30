# Courser

## Docker images

The main app image is fine as is.

However to build the database image you will need to run the following commands:

1. `dotnet tool restore`

2. `dotnet ef migrations script -p src/Persistence -s src/WebApp -o database/init.sql`

After this sequence of commands `docker-compose up --build` will work as intended.

This should probably be handled by the build system.
****
