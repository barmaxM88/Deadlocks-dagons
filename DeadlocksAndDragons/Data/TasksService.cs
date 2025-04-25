namespace DeadlocksAndDragons.Data;

public static class TasksService
{
    private static readonly List<CodingTask> tasks = new()
    {
        new CodingTask
        {
            Id = 1,
            Title = "Урок 1: Интерфейс",
            Intro = "Ты — Гейм-мастер. Перед тобой две группы игроков. Их маршрут включает одну и ту же 'комнату с ловушкой'. Попадут ли они туда одновременно?",
            CodeSnippet = """
// Партия A
prepareForAdventure()
criticalSection()
rest()

// Партия B
if ready {
    criticalSection()
}
ready = true
""",
            Explanation = "Партия A входит без условий. Партия B проверяет флаг — но устанавливает его после входа. Если обе начнут одновременно — может возникнуть гонка.",
            Hint = "Подумай, в каком порядке исполняются команды.",
            IsDeadlock = true
        },

        new CodingTask
        {
            Id = 2,
            Title = "Урок 2: Счётчики жадности",
            Intro = "Две партии пытаются первыми получить благословение алтаря, прибавляя +1 к древнему счётчику. Побеждает тот, кто делает это первым. Но магия не всегда честна...",
            CodeSnippet = """
// Партия A
a = a + 1
if a == 1 {
  enterChamber()
}

// Партия B
a = a + 1
if a == 1 {
  enterChamber()
}
""",
            Explanation = "Прибавление +1 к переменной — неатомарная операция. Если обе группы делают это одновременно, они могут получить одинаковый результат и войти в комнату вместе.",
            Hint = "Раздели операцию на чтение, вычисление, запись. Где возникает проблема?",
            IsDeadlock = true
        },

        new CodingTask
        {
            Id = 3,
            Title = "Урок 3: Зеркальные подземелья",
            Intro = "Ты — мастер подземелий. Две группы приключенцев входят в зеркальные копии одного и того же зала. Их действия должны различаться, иначе магическая структура рушится.",
            CodeSnippet = """
package main

import "fmt"

var crystalIsRed = false

func partyA() {
    for {
        for crystalIsRed {
            // ждём, пока другая партия в ловушке
        }
        crystalIsRed = true
        enterTrapRoom("A")
        crystalIsRed = false
    }
}

func partyB() {
    for {
        for crystalIsRed {
            // ждём, пока другая партия в ловушке
        }
        crystalIsRed = true
        enterTrapRoom("B")
        crystalIsRed = false
    }
}

func enterTrapRoom(party string) {
    fmt.Printf("Партия %s вошла в комнату с ловушкой\n", party)
}

func main() {
    go partyA()
    go partyB()
    select {} // бесконечное ожидание
}
""",
            Explanation = "Обе партии ориентируются на один магический индикатор (флаг). Но чтение и установка его состояния происходят в разное время. Если обе группы проверят кристалл до того, как кто-то изменит его цвет, они войдут одновременно.",
            Hint = "Сколько шагов включает проверка кристалла? Что, если обе группы делают это в один момент?",
            IsDeadlock = true
        }
    };

    public static List<CodingTask> GetAll() => tasks;

    public static CodingTask? GetById(int id) => tasks.FirstOrDefault(t => t.Id == id);
}
