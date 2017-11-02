using System.Collections.Generic;
using System.Linq;

namespace NomOrderManager.Model
{
    public class IndexModel : IModel
    {
        public IndexModel(IEnumerable<string> locations)
        {
            Name = "Lieferdienste";
            Locations = locations;
        }

        public string Name { get; }

        public IEnumerable<string> Locations { get; }

        public bool HasData { get { return Locations.Any(); } }
    }
}
