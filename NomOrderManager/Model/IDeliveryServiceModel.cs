namespace NomOrderManager.Model
{
    public interface IDeliveryServiceModel : IModel
    {
        string ServiceName { get; }

        string PhoneNumber { get; }
    }
}
