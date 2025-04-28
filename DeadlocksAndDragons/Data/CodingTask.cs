// Data/CodingTask.cs
namespace DeadlocksAndDragons.Data;

public class CodingTask
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Intro { get; set; } = "";
    public string CodeSnippet { get; set; } = "";
    public string Explanation { get; set; } = "";
    public string Hint { get; set; } = "";
    public bool IsDeadlock { get; set; }

    // Поля для заданий с выбором строки
    public List<string>? AnswerOptions { get; set; }
    public int CorrectOptionIndex { get; set; }
}
