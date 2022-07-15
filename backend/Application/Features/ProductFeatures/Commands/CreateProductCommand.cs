using Application.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ProductFeatures.Commands
{
    public class CreateProductCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
        {
            private readonly IProductService _productService;
            public CreateProductCommandHandler(IProductService productService)
            {
                _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            }

            public async Task<int> Handle(CreateProductCommand command, CancellationToken cancellationToken)
            {
                return await _productService.CreateProduct(command.Name, command.Barcode, command.Description, command.Rate);
            }
        }
    }
}
