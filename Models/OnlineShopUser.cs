namespace OnlineShop.Models;

public class OnlineShopUser
{
    public int Id { get; set; }
    public string Name { get; private set; }
    public string LastName { get; private set; }
    public string PhoneNumber { get; private set; }
    public string NationalCode { get; private set; }
    public bool IsActive { get; private set; }
    private OnlineShopUser(
        string name,
        string lastName,
        string phoneNumber,
        string nationalCode
        )
    {
        SetName(name);
        SetLastName(lastName);
        SetPhoneNumber(phoneNumber);
        SetNationalCode(nationalCode);
    }

    public static OnlineShopUser Create(
        string name,
        string lastName,
        string phoneNumber,
        string nationalCode
        )
    {
        return new(
            name,
            lastName,
            phoneNumber,
            nationalCode
            );
    }

    public void Update(
        string name,
        string lastName,
        string phoneNumber,
        string nationalCode,
        bool isActive
        )
    {
        SetName(name);
        SetLastName(lastName);
        SetPhoneNumber(phoneNumber);
        SetNationalCode(nationalCode);
        SetActivation(isActive);
    }

    private void SetName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(Name));
        Name = value;
    }
    private void SetLastName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(LastName));
        LastName = value;
    }
    private void SetPhoneNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(PhoneNumber));
        PhoneNumber = value;
    }
    private void SetNationalCode(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(NationalCode));
        NationalCode = value;
    }
    private void SetActivation(bool value) => IsActive = value;
    public void ToggleActivation() => SetActivation(!IsActive);
}
