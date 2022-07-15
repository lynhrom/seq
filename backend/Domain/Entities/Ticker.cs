using Ardalis.GuardClauses;
using Domain.Common;

namespace Domain.Entities
{
    public class Ticker: BaseEntity
    {
        public string Code { get; private set; }
        public string Name { get; private set; }

        public Ticker(string code, string name)
        {
            Guard.Against.NullOrEmpty(code, nameof(code));
            Guard.Against.NullOrEmpty(name, nameof(name));

            Code = code;
            Name = name;
        }
    }
}
