using Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace WebApi.Features.Sources
{
    public class GetAllPriceSourceQuery : IRequest<ListPriceSourceResponse>
    {

        public class GetAllPriceSourceQueryHandler : IRequestHandler<GetAllPriceSourceQuery, ListPriceSourceResponse>
        {
            private readonly IReadRepository<PriceSource> _repository;
            private readonly IMapper _mapper;

            public GetAllPriceSourceQueryHandler(IReadRepository<PriceSource> repository, IMapper mapper)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }

            public async Task<ListPriceSourceResponse> Handle(GetAllPriceSourceQuery query, CancellationToken cancellationToken)
            {
                var result = new ListPriceSourceResponse();
                var items = await _repository.ListAsync();
                result.Items.AddRange(items.Select(_mapper.Map<PriceSourceDto>));
                return result;

            }
        }


    }
}
