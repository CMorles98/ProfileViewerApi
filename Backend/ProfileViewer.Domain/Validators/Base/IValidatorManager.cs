namespace ProfileViewer.Domain.Validators.Base
{
    public interface IValidatorManager
    {
        IUserValidator UserValidator { get; }
        IAuthValidator AuthValidator { get; }
    }
}
