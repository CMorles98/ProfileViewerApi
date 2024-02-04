namespace ProfileViewer.Domain.Validators.Common
{
    public static class ValidatorRegex
    {
        public static readonly string passwordRegex = @"^(?=.*[0-9])(?=.*[a-z]).+$";
        public static readonly string emailRegex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
    }
}
