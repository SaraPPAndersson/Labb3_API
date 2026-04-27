namespace Labb3_API.Models.DTOs
{
    public record GetUserResponse(int id, string? fullName, string? email, string? phone);

    public record GetInterestResponse(int id, string? title, string? description);
    public record GetLinkResponse(int id, string? url);
    public record AddInterestToUserRequest(int interestId);

   public record AddLinkRequest(int userId, int interestId, string url);
   

}
