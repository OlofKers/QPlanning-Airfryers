using QPlanning.Business.Interfaces.Base;

public class BoekingResponse : UseCaseResponseMessage
{
    public BoekingResponse(int id, bool succes, string message) : base(succes, message)
    {
        Id = id;
    }
    public int Id { get; }
}