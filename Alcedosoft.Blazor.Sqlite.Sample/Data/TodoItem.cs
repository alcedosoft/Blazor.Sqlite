namespace Alcedosoft.Blazor.Sqlite.Sample;

public class TodoItem
{
    public int Id { get; set; }
    public string Title { get; set; } = String.Empty;
    public bool IsDone { get; set; }
}
