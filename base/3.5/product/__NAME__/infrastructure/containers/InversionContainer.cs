namespace __NAME__.infrastructure.containers
{
    public interface InversionContainer
    {
        TypeToReturn Resolve<TypeToReturn>();
    }
}