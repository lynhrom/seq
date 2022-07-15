using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries
{
    public class GetByIdQuery : IRequest<ApplicationUser>
    {
        public string Id { get; set; }
        public class GetTokenQueryHandler : IRequestHandler<GetByIdQuery, ApplicationUser>
        {
            private readonly IUserService _userService;

            public GetTokenQueryHandler(IUserService userService)
            {
                _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            }

            public async Task<ApplicationUser> Handle(GetByIdQuery request, CancellationToken cancellationToken)
            {
                return await _userService.GetByIdAsync(request.Id);
            }
        }
    }
}
