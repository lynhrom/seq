using System;

namespace Infrastructure.Handlers
{
    /// <summary>
    /// Base class used by API responses
    /// </summary>
    public abstract class BaseResponse : BaseMessage
    {
        public BaseResponse(Guid correlationId) : base()
        {
            _correlationId = correlationId;
        }

        public BaseResponse()
        {
        }
    }
}
