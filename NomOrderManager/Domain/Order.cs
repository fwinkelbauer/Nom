namespace NomOrderManager.Domain
{
    public class Order
    {
        public Order(int id, Item item, string comment, string host, string username)
        {
            Id = id;
            Item = item;
            Comment = comment;
            Host = host;
            Username = username;
            HasComment = !string.IsNullOrEmpty(Comment);
        }

        public int Id { get; }

        public Item Item { get; }

        public string Comment { get; }

        public string Host { get; }

        public string Username { get; }

        public bool HasComment { get; }
    }
}
