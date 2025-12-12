namespace BusinessLogic.DTO
{
    public record UserResponse(Guid UserId, string? Email, string? PersonName, string? Gender)
    {
        public UserResponse() : this(default, default, default, default)
        {

        }
    }
}
