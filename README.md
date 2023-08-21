## Running locally
To start the kestral server use the following command

```
dotnet watch
```

to run with https enabled

```
dotnet watch --launch-profile https
```

## Trusting dev certs
To trust the dev certs run the following command

```
dotnet dev-certs https --trust
```

## Docker container
to start the docker container run the following command

```
docker-compose up -d
```

This will start the mysql and adminer containers. The adminer container will be available at http://localhost:8080