namespace PersonAPI.Models;

// Model e uma representação do Banco de Dados
public class PersonModel
{
    public PersonModel(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }

    public Guid Id { get; init; }
    public string Name { get; private set; }

    public void ChangeNome(string name)
    {
        Name = name;
    }

    public void SetInvalidName()
    {
        Name = "Invalid";
    }
}
