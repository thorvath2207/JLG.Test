
## Run Locally

Clone the project

```bash
  git clone https://github.com/thorvath2207/JLG.Test
```

Go to the project directory

```bash
  cd JLG.Test
```

Run integration tests with docker compose

```bash
  docker compose -f docker-compose.test.yml up --build backend-integration-test
```

Run unit tests with docker compose

```bash
  docker compose -f docker-compose.test.yml up --build backend-unit-test
```

Build and start the application with docker compose

```bash
docker compose up --build
```

To access swagger ui for the backend go to: https://localhost:8081/swagger/index.html

To access fronted go to: http://localhost:8083