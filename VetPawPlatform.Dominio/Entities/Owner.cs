using VetPawPlatform.Domain.Exceptions;

namespace VetPawPlatform.Domain.Entities;

public class Owner
{
    public Guid Id { get; set; }
    public string Document { get; private set; } = string.Empty;    
    public string FullName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    public DateTime BirthDate { get; private set; }
    
    private readonly List<Pet> _pets = [];
    public IReadOnlyCollection<Pet> Pets => _pets;

    public Owner(string document, string fullName, string email, string phoneNumber, DateTime birthDate)
    {
        Validate(document, fullName, email, phoneNumber, birthDate);

        Id = Guid.NewGuid();
        Document = document;
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
        BirthDate = birthDate;
    }

    public void UpdateDetails(string document, string fullName, string email, string phoneNumber, DateTime birthDate)
    {
        Validate(document, fullName, email, phoneNumber, birthDate);

        FullName = fullName;
        Email = email;        
        PhoneNumber = phoneNumber;
        BirthDate = birthDate;
    }

    public void AddPet(Pet pet)
    {
        if (pet == null) 
            throw new DomainException("O pet não pode ser nulo.");

        _pets.Add(pet);
    }

    public static Owner Rehydrate(Guid id, string document, string fullName, string email, string phoneNumber, DateTime birthDate)
    {
        return new Owner
        {
            Id = id,
            Document = document,
            FullName = fullName,
            Email = email,            
            PhoneNumber = phoneNumber,
            BirthDate = birthDate
        };
    }

    private static void Validate(string document, string fullName, string email, string phoneNumber, DateTime birthDate)
    {
        if (string.IsNullOrWhiteSpace(document))
            throw new DomainException("O documento (CPF) é obrigatório.");

        if (string.IsNullOrWhiteSpace(fullName) || fullName == "string")
            throw new DomainException("O nome completo é obrigatório.");

        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            throw new DomainException("E-mail inválido.");

        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new DomainException("O telefone de contato é obrigatório.");

        if (birthDate > DateTime.UtcNow.AddYears(-18))
            throw new DomainException("O tutor deve ser maior de 18 anos.");
    }

    private Owner() { }
}