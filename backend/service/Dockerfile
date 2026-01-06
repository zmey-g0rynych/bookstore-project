# 1. Используем базовый образ OpenJDK 17
FROM eclipse-temurin:17-jdk-alpine

# 2. Устанавливаем рабочую директорию внутри контейнера
WORKDIR /app

# 3. Копируем Gradle wrapper и сборочные файлы
COPY gradlew .
COPY gradle ./gradle
COPY build.gradle .
COPY settings.gradle .

# 4. Делаем gradlew исполняемым
RUN chmod +x gradlew

# 5. Копируем весь исходный код
COPY src ./src

# 6. Сборка проекта внутри контейнера
RUN ./gradlew clean build -x test

# 7. Указываем, что порт 8080 будет доступен
EXPOSE 8080

# 8. Команда для запуска jar
ENTRYPOINT ["java", "-jar", "build/libs/service-0.0.1-SNAPSHOT.jar"]
