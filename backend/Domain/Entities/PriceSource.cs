using Ardalis.GuardClauses;
using Domain.Common;

namespace Domain.Entities
{
    public class PriceSource : BaseEntity
    {
        public string Code { get; private set; }
        public string Name { get; private set; }

        public PriceSource(string code, string name)
        {
            Guard.Against.NullOrEmpty(code, nameof(code));
            Guard.Against.NullOrEmpty(name, nameof(name));

            Code = code;
            Name = name;
        }
    }
}
