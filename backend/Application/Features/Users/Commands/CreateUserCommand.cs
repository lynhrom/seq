using Application.Interfaces;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ProductFeatures.Commands
{
    public class CreateUserCommand : IRequest<string>
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
        {
            private readonly IUserService _userService;

            public CreateUserCommandHandler(IUserService userService)
            {
                _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            }

            public async Task<string> Handle(CreateUserCommand command, CancellationToken cancellationToken)
            {
                return await _userService.CreateUser(command.Username, command.Email, command.FirstName, command.LastName, command.Password);
            }
        }
    }
}
