## Running locally
To start the application locally run the following commands

```bash
# start the mysql and adminer containers
docker compose up -d --build

# start the kestrel server
dotnet watch
```

- `-d` flag is used to run the containers in the background.
- `--build` flag is used to rebuild the containers. This is useful when the Dockerfile or docker-compose.yml file is updated
- you can stop the containers by running the following command

```bash
docker compose down
```

to stop the containers and delete the volumes run the following command

```bash
docker compose down -v
```
## Debugging
- Use the run & debug feature in Visual Studio Code to debug the application

## Running in Production
- to run in production, you need to use the `docker-compose.prod.yml` file

```bash
docker-compose -f docker-compose.prod.yml up -d --build
```

## About the .env file
- The .env file contains two environment variables
  - `MYSQL_ROOT_PASSWORD` - the root password for the MySQL database. The default value is `secret`