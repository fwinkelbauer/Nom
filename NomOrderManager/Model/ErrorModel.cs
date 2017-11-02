namespace NomOrderManager.Model
{
    public class ErrorModel : IModel
    {
        public ErrorModel()
        {
            Name = "Fehler";
        }

        public string Name { get; }
    }
}
