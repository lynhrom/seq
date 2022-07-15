using Application.Interfaces;
using Application.ViewModels;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ProductFeatures.Queries
{
    public class GetTokenQuery : IRequest<AuthenticationModel>
    {
        public TokenRequestModel Request { get; set; }
        public class GetTokenQueryHandler : IRequestHandler<GetTokenQuery, AuthenticationModel>
        {
            private readonly ITokenClaimsService _userService;

            public GetTokenQueryHandler(ITokenClaimsService userService)
            {
                _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            }

            public async Task<AuthenticationModel> Handle(GetTokenQuery query, CancellationToken cancellationToken)
            {
                return await _userService.GetTokenAsync(query.Request.Email, query.Request.Password);
            }
        }
    }
}
