using Domain.Entities;
using System;

namespace Domain.Common
{
    public abstract class BaseEntity
    {
        public virtual long Id { get; protected set; }

    }
}
