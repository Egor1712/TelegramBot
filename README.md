# TelegramBot
Бот для вывода актульной информации о студенте УрФУ.
----------------------------------------------------
Day #1
Сделал описание модели студента и начал писать парсеры для сайта.
Делаю парсинг и разбор HTML кода через AngleSharp.
Нужно будет рефакторить все))
----------------------------------------------------
Day #2
Доделал парсинг всех предметов и оценок за них,  а также финансовых сервисов - долг, и начисления по каждому месяцу.
Как-то странно работает парсинг, захожу на сайт https://ubu.urfu.ru/fse/communal_charges/, а он заходит на https://ubu.urfu.ru/fse/ скорее всего можно поправить и переделать все на Request'ы, но пока не хочется разбираться
Сделал привязку к боту, получил токен, все работает))
Узнал, что можно написать на Asp.Net Core - хороший повод изучить и постараться поставить на какой-нибудь сервак, Azure например, все-таки УрФУ дает возможность))
----------------------------------------------------
 Day #3
 Добавил команд и "нормальную авторизацию". Исправил парсеры, чтобы не кидали исключений где не надо, надо бы еще тестов написать)) Затестил на друзьях, вроде работает, теперь еще и можно паралелльно юзать бота всем, и проблем с авторизацией не будет))
Надо будет скрыть токен бота как-то, а то своруют еще)))  В целом - работает, но качество кода мне не нравится, нужно добавлить DI container и всяких прочих штук. Но сейчас больше хочется заставить это работать без моего участия, например на Azure. 
-------------------------------------------
Day #4
Azure меня подвел и потребовал подписочку, но Heroku меня спас. К сожжалению запуска консольных приложений там нет, поэтому пришлось создать пустое  приложение на  Asp.Net, в котором просто инициализируется бот и начинает принимать сообщения.
Добавил разлогрование, надо бы еще попробовать как-то хранить данные пользователей, возможно нужна база данных, но как она будет на серваке работать и какую нужно выбрать я не знаю)
Как работать с вебхуками я не понял, нужно больше примеров. Heroku вроде сбрасывает все состояние приложения каждые 20-30 минут, поэтому приходится снова логиниться, надо думать как это исправить)
 