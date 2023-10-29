using System.Text.Json.Serialization;

namespace Domain.Models;

public class Post
{
    public int Id { get; set; }
    [JsonPropertyName("Author")]
    public User Author { get; set; }
    
    [JsonPropertyName("Title")]
    public string Title { get; set; }
    
    [JsonPropertyName("Content")]
    public string Content { get; set; }

    [JsonPropertyName("IsPrivate")] 
    public bool IsPrivate { get; init; }

    public Post(User author, string title, string content)
    {
        Author = author;
        Title = title;
        Content = content;
    }
    
    
}