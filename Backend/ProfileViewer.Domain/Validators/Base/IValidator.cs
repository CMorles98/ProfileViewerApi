namespace ProfileViewer.Domain.Validators.Base
{
    public interface IValidator<TEntity>
    {
        Task Validate(TEntity dto);
    }

    public interface IValidator<TIn, TOut>
    {
        Task<TOut> Validate(TIn dto);
    }

    public interface IValidator<TIn, TOut, TIdentifier>
    {
        Task<TOut> Validate(TIn dto, TIdentifier id);
    }
}
