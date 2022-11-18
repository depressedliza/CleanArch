using Domain.Common;

namespace Domain.Entities;

public class Profile : BaseEntity
{
    public string NickName { get; set; } = string.Empty;
    public string Avatar { get; set; } = string.Empty;

    public Guid UserId { get; set; }
}
