using Application.Interfaces;
using Application.ViewModels;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ProductFeatures.Queries
{
    public class GetRefreshTokenQuery : IRequest<AuthenticationModel>
    {
        public string Token { get; set; }
        public class GetRefreshTokenQueryHandler : IRequestHandler<GetRefreshTokenQuery, AuthenticationModel>
        {
            private readonly ITokenClaimsService _userService;

            public GetRefreshTokenQueryHandler(ITokenClaimsService userService)
            {
                _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            }
            public async Task<AuthenticationModel> Handle(GetRefreshTokenQuery query, CancellationToken cancellationToken)
            {
                return await _userService.GetRefreshTokenAsync(query.Token);
            }
        }
    }
}
