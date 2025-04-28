namespace DeadlocksAndDragons.Data;

public static class TasksService
{
    private static readonly List<CodingTask> tasks = new()
    {
        new CodingTask
        {
            Id = 1,
            Title = "Урок 1: Комната с ловушкой",
            Intro = "Ты — Мастер Подземелий. Перед тобой две отважные партии приключенцев. Их путь пересекается в зале с нажимной плитой. Что случится, если обе войдут туда одновременно?",
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
            Explanation = "Первая партия идёт без остановки. Вторая проверяет флаг 'готовности', но устанавливает его только после проверки. Это создаёт риск гонки, если обе группы стартуют одновременно.",
            Hint = "Обрати внимание на момент установки флага и порядок действий.",
            IsDeadlock = true
        },

        new CodingTask
        {
            Id = 2,
            Title = "Урок 2: Алтарь Жадности",
            Intro = "Древний алтарь дарует благословение только первой партии, что увеличит счётчик. Но что будет, если обе попытаются сделать это одновременно?",
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
            Explanation = "Прибавление +1 к переменной — это не единый шаг. Чтение, вычисление и запись происходят отдельно. При одновременном доступе результат может быть неожиданным.",
            Hint = "Разложи 'a = a + 1' на части и подумай, что может пойти не так.",
            IsDeadlock = true
        },

        new CodingTask
        {
            Id = 3,
            Title = "Урок 3: Зеркальные Залы",
            Intro = "Две партии входят в зеркальные копии одного подземелья. Их действия должны быть асинхронными, чтобы не нарушить магический баланс. Справятся ли они?",
            CodeSnippet = """
package main

import "fmt"

var crystalIsRed = false

func partyA() {
    for {
        for crystalIsRed {
            // ждём освобождения
        }
        crystalIsRed = true
        enterTrapRoom("A")
        crystalIsRed = false
    }
}

func partyB() {
    for {
        for crystalIsRed {
            // ждём освобождения
        }
        crystalIsRed = true
        enterTrapRoom("B")
        crystalIsRed = false
    }
}

func enterTrapRoom(party string) {
    fmt.Printf("Партия %s вошла в зал ловушек\n", party)
}

func main() {
    go partyA()
    go partyB()
    select {} // вечное ожидание
}
""",
            Explanation = "Обе партии смотрят на один магический индикатор — цвет кристалла. Если обе увидят свободный кристалл одновременно, баланс разрушится.",
            Hint = "Является ли чтение кристалла и установка его цвета одной операцией?",
            IsDeadlock = true
        },

        new CodingTask
        {
            Id = 4,
            Title = "Урок 4: Испытание Сфинкса",
            Intro = "Партии решают загадку Сфинкса. Но порядок их действий различается. Где именно возникает магическая ошибка, если они действуют слишком быстро?",
            CodeSnippet = """
// Партия A
solvePuzzle()
approachAltar()
claimReward()

// Партия B
solvePuzzle()
claimReward()
approachAltar()
""",
            Explanation = "Ошибка происходит при попытке получить награду. Обе партии считают, что выиграли первыми — и нарушают порядок ритуала.",
            Hint = "Когда партия должна подходить к алтарю: до или после получения награды?",
            AnswerOptions = new List<string> { "solvePuzzle()", "approachAltar()", "claimReward()" },
            CorrectOptionIndex = 2
        },

        new CodingTask
        {
            Id = 5,
            Title = "Урок 5: Гильдия Магов",
            Intro = "Две партии проводят сложный ритуал в магической башне. В какой момент ритуал выходит из-под контроля?",
            CodeSnippet = """
// Партия A
prepareScroll()
castSpell()
finalize()

// Партия B
prepareScroll()
finalize()
castSpell()
""",
            Explanation = "Критический момент — произнесение заклинания. Если партии наложат его одновременно, последствия будут непредсказуемы.",
            Hint = "Что важнее: закончить подготовку или контролировать начало ритуала?",
            AnswerOptions = new List<string> { "prepareScroll()", "castSpell()", "finalize()" },
            CorrectOptionIndex = 1
        }
    };

    public static List<CodingTask> GetAll() => tasks;

    public static CodingTask? GetById(int id) => tasks.FirstOrDefault(t => t.Id == id);
}