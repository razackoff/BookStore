# Bookstore Automation System

Проект представляет собой приложение для автоматизации книжного магазина. Система позволяет выполнять операции CRUD (создание, чтение, обновление, удаление) данных о книгах, а также их категориях и авторах.

## Содержание

- [Описание](#описание)
- [Технические требования](#технические-требования)
- [Установка и запуск](#установка-и-запуск-из-репозитория)
- [Запуск с использованием Docker Compose](#запуск-с-использованием-docker-compose)
- [Использование](#использование)
- [Структура проекта](#структура-проекта)

## Описание

Проект создан для обеспечения удобства управления данными книжного магазина. Пользователи могут выполнять операции CRUD для книг, категорий и авторов. Каждая книга может иметь уникальные свойства, такие как название, цена, изображение и файлы в формате PDF.

## Технические требования

Приложение создано на ASP.NET Web API с использованием языка программирования C#. Для хранения данных используется PostgreSQL.

## Установка и запуск из репозитория

1. Клонируйте репозиторий:

    ```bash
    git clone https://github.com/yourusername/bookstore.git
    ```

2. Откройте проект в вашей IDE (например, Visual Studio).

3. Установите необходимые пакеты NuGet.

4. Настройте подключение к базе данных в файле `appsettings.json`.

5. Запустите проект.

## Запуск с использованием Docker Compose

1. Создайте файл docker-compose.yml и скопируйте в него следующий код:
   ```bash
    version: '3.9'

    services:
      book_store_database_container:
        image: postgres:latest
        container_name: book_store_database_container
        environment:
          POSTGRES_DB: bs_db
          POSTGRES_USER: user
          POSTGRES_PASSWORD: passwordpassword
        volumes:
          - lona_database_data:/var/lib/postgresql/data
        expose:
          - 8080
        networks:
          - bs_network
    
      book_store_api_container:
        image: razackoff/book-store-api-arm:1.0
        container_name: book_store_api_container
        environment:
          - UPLOADS_FOLDER=/app/static-files
          - DATABASE_HOST=book_store_database_container
          - DATABASE_NAME=bs_db
          - DATABASE_USER=user
          - DATABASE_PASSWORD=passwordpassword
        volumes:
          - ./static-files:/app/static-files
        ports:
          - "8080:8080"
        networks:
          - bs_network
    
    networks:
      bs_network:
        driver: bridge
    
    volumes:
      lona_database_data:
    ```
   Примечание: Используйте образ razackoff/book-store-api-amd64 для архитектуры X86 и razackoff/book-store-api-arm для ARM.
   
2. Запустите приложение с помощью команды:
    ```bash
    docker-compose up -d
    ```
Теперь ваше приложение должно быть доступно по адресу http://localhost:8080.

🚨 Примечание: При первом запуске может возникнуть ошибка, поскольку веб-приложение будет пытаться подключиться к базе данных до ее инициализации. В этом случае подождите несколько секунд и повторно запустите контейнеры с помощью команды docker-compose up -d.

## Использование

После запуска приложения вы можете использовать следующие эндпоинты для управления данными:

- `GET /api/Books/GetAllBooks` - получить список всех книг
- `GET /api/Books/GetBookById/{id}` - получить информацию о книге по ее ID
- `POST /api/Books/AddBook` - создать новую книгу
- `POST /api/Books/UploadPdf` - загрузить файл PDF для книги
- `POST /api/Books/UploadImage` - загрузить изображение для книги
- `PUT /api/Books/UpdateBook/{id}` - обновить информацию о книге по ее ID
- `DELETE /api/Books/DeleteBook/{id}` - удалить книгу по ее ID

- `GET /api/Category/GetAllCategories` - получить список всех категорий
- `GET /api/Category/GetCategoryById/{id}` - получить информацию о категории по ее ID
- `POST /api/Category/AddCategory` - создать новую категорию
- `PUT /api/Category/UpdateCategory/{id}` - обновить информацию о категории по ее ID
- `DELETE /api/Category/DeleteCategory/{id}` - удалить категорию по ее ID

Также, для доступа к файлам и изображениям, вы можете использовать следующие адреса:

- `GET /static-files/files/{FileName}` - доступ к загруженным файлам
- `GET /static-files/images/{ImageName}` - доступ к загруженным изображениям

Примечание: При использовании этих эндпоинтов, убедитесь, что ваш сервер запущен и работает на порту 8080 на локальной машине (`http://localhost:8080`).

## Структура проекта

```plaintext
bookstore/
├── Controllers/         # Контроллеры API
├── Data/                # Классы и контекст данных
├── DTOs/                # Объекты передачи данных
├── Exceptions/          # Обработка исключений
├── Migration/           # Файлы миграции базы данных
├── Models/              # Модели данных
├── Repositories/        # Репозитории для работы с данными
├── Services/            # Сервисы для работы с данными
├── Utilities/           # Вспомогательные утилиты
├── wwwroot/             # Статические файлы (например, изображения, скрипты)
├── appsettings.json     # Файл конфигурации
├── Program.cs           # Точка входа в приложение
└── README.md            # Документация проекта
