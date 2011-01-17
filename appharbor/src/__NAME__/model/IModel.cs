namespace __NAME__.model
{
    using auditing;

    public interface IModel : Auditable
    {
        long id { get; }
    }
}