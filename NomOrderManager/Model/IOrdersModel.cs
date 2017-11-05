namespace NomOrderManager.Model
{
    public interface IOrdersModel : IDeliveryServiceModel
    {
        bool HasComment { get; }
    }
}
