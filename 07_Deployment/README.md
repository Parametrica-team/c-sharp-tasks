# Deployment

Необходимо создать файл `deploy.exe` для автоматического копирования нужного содержимого в папку 00_DEPLOY. Список файлов, которые нужно скопировать будут содержаться в файле `.deploysettings`. Программа должна запускаться из командной строки и иметь 2 режима работы: 
1. **build** - собрать нужные файлы в папку 00_DEPLOY
2. **update** - обновить пути в файле .deploysettings

## Пример:
У нас есть такая структура папок в проекте
- 00_RELEASE
    - 01_RHINO
        - oldFile1.3dm
    - 02_GRASSHOPPER
        - oldScript1.gh
    - OLDFOLDER
        - some.doc
- 01_RHINO
    - file1.3dm
    - file2.3dm
- 02_GRASSHOPPER
    - script1.gh
    - stcript2.gh
- 03_SOME USELESS SHIT
    - even more useless shit.doc
- .deploysettings

Файл настроек `.deploysettings` содержит следующую информацию:
```
00_RELEASE //первая строка - это путь для сохранения файлов
01_RHINO\file1.3dm
02_GRASSHOPPER\script1.gh
```

### Режим build
В командной строке нужно написать `deploy build`
Файлы, находящиеся в папке 00_DEPLOY должны быть удалены и вместо них скопированы все файлы, указанные в `.deploysettings`. То есть мы должны получить такую структуры файлов:
- 00_RELEASE
    - 01_RHINO
        - file1.3dm
    - 02_GRASSHOPPER
        - script1.gh
- 01_RHINO
    - file1.3dm
    - file2.3dm
- 02_GRASSHOPPER
    - script1.gh
    - stcript2.gh
- 03_SOME USELESS SHIT
    - even more useless shit.doc
- .deploysettings

### Режим update
В командной строке нужно написать `deploy update`
Режим update наоборот обновляет пути в файле настроек согласно файлам, находящимся в папке 00_DEPLOY. В нашем примере после запуска в командной строке `deploy update` файл настроек .deploysettings должен обновиться до следующего вида:
```
00_RELEASE
01_RHINO\oldFile1.3dm
02_GRASSHOPPER\oldScript1.gh
OLDFOLDER\some.doc
```

## Комментарии
Чтобы запускать программу с консоли нужно добавить путь к нашему .exe файлу в переменную PATH операционной системы. Подробнее об этом тут: http://barancev.github.io/what-is-path-env-var/

Чтобы открыть консоль можно использовать правую кнопку мыши и в контекстном меню выбрать `Git Bash Here`.
Другой способ - написать в проводнике в строке, где указан путь, - *powershell*

Папку из которой запущена командная строка в программе можно получить из переменной `Environment.CurrentDirectory`

Аргументы (buid и update) попадают в программу через метод `Main(string[] args)`.
Чтобы понять какой аргумент был отправлен достаточно посмотреть что находится в `args[0]`
