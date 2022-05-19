# Tatargram

Как установить Tatargram локально:

1. Склонировать репо

Для запуска серверной части:
1. Зайти в папку ..\Tatargram\backend\src\Tatargram.API;
2. Создать директорию wwwroot\images;
3. В файле appsettings.json вместо "Password=postgres" вписать "Password=(ваш пароль от БД). Примечание: на компьютере должен быть установлен PostgreSQL;
4. В терминале прописать "dotnet run".

Для запуска клиентской части:
1. Зайти в папку ..\Tatargram\frontend\tatargram\src;
2. В терминале прописать "npm start". Примечание: предварительно должен быть установлен React. "npm install react".