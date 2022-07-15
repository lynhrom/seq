using Application.Interfaces;
using Application.ViewModels;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ProductFeatures.Commands
{
    public class AddRoleCommand : IRequest<string>
    {
        public AddRoleModel Request { get; set; }
        public class AddRoleCommandHandler : IRequestHandler<AddRoleCommand, string>
        {
            private readonly IUserService _userService;

            public AddRoleCommandHandler(IUserService userService)
            {
                _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            }

            public async Task<string> Handle(AddRoleCommand command, CancellationToken cancellationToken)
            {
                return await _userService.AddRole(command.Request.Email, command.Request.Role, command.Request.Password);
            }
        }
    }
}
