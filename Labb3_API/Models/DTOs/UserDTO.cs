namespace Labb3_API.Models.DTOs
{
    public record GetUserResponse(int id, string fullName, string email, string phone);
    public record UpdateUserRequest(int id);

    public record CreateLinkRequest(int interestId);

}
