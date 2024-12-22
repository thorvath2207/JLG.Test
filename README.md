
## Run Locally

Clone the project

```bash
  git clone https://link-to-project
```

Go to the project directory

```bash
  cd my-project
```

Run integration tests with docker compose

```bash
  docker compose -f docker-compose.test.yml up --build backend-integration-test
```

Run unit tests with docker compose

```bash
  docker compose -f docker-compose.test.yml up --build backend-integration-test
```

Start the application with docker compose

```bash
docker compose up
```