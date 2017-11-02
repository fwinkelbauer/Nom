namespace NomOrderManager.Model
{
    public class RegisterModel : IModel
    {
        public RegisterModel(string currentUsername)
        {
            Name = "Registrierung";
            CurrentUsername = currentUsername;
            HasUsername = !string.IsNullOrEmpty(currentUsername);
        }

        public string Name { get; }

        public string CurrentUsername { get; }

        public bool HasUsername { get; }
    }
}
