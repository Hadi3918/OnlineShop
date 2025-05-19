namespace OnlineShop.DTOs;

public sealed record CreateOrUpdateUserDTO(
    string Name,
    string LastName,
    string PhoneNumber,
    string NationalCode,
    bool IsActive
    );