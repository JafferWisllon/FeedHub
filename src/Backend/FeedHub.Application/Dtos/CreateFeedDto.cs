using System.ComponentModel.DataAnnotations;

namespace FeedHub.Application.Dtos;

public class CreateFeedDto
{
    [Required]
    [StringLength(2048)]
    public string Url { get; set; }

    [StringLength(100)]
    public string Name { get; set; }
}
