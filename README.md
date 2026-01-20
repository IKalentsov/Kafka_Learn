## Kafka_Learn

# Базовый проект для изучения кафки.

# Для запуска проекта 
1) Необходимо запустить Docker Desktop
2) В каждом из проектов producer или consumer есть папка Files.
   В этой папке лежит два файла. 
   - `docker-compose-kafka.yml` - конфиг для контейнера кафки
   - `start-command.txt` - команда для запуска. 
3) Заходим в папку Files(любую) и запускаем из папки PowerShell.
4) Вставляем команду из start-command.txt: `docker-compose -f docker-compose-kafka.yml up`
5) Ждем инициализхация и загрузки
6) Запускаем проект Producer
7) Переходим на страницу сваггера: https://localhost:xxxx/swagger/index.html
8) Выполняем пост запрос
9) Открываем Offset Explorer
10) Страница должна быть такой, не забываем выбрать string-поля и нажать Update
<img width="2028" height="438" alt="image" src="https://github.com/user-attachments/assets/ac7c1826-dd8b-4d97-9ec6-a039a17eb3fb" />

11) В зависимости, сколько сообщений отправили - будет такой результат
<img width="2028" height="454" alt="image" src="https://github.com/user-attachments/assets/4a8181e8-2e95-4f3b-b119-4352c72fe3c6" />

12) Запускаем проект Consumer
13) После запуска должны увидеть в консоли
14) Увидим сообщения, у меня 1, т.к. остальные я уже прочитал
<img width="947" height="152" alt="image" src="https://github.com/user-attachments/assets/672b46ee-f2aa-4401-a770-2ffe4de1a5b0" />

