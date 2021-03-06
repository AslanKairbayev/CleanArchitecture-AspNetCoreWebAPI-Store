﻿using Core.Dto;
using Core.Dto.UseCaseRequests;
using Core.Dto.UseCaseResponses;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Core.Interfaces.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.UseCases
{
    public sealed class GetUnshippedOrdersUseCase : IGetUnshippedOrdersUseCase
    {
        private readonly IOrderRepository repository;

        public GetUnshippedOrdersUseCase(IOrderRepository repo)
        {
            repository = repo;
        }

        public async Task<bool> Handle(GetUnshippedOrdersRequest request, IOutputPort<GetUnshippedOrdersResponse> outputPort)
        {
            var orders = await repository.UnshippedOrdersWithLines();         
            if (!orders.Any())
            {
                outputPort.Handle(new GetUnshippedOrdersResponse(null, false, "No Unshipped Orders"));

                return false;
            }
            var ordersDto = new List<OrderDto>();
            foreach (var o in orders)
            {
                var linesDto = new List<CartLineDto>();
                foreach (var l in o.Lines)
                {
                    linesDto.Add(new CartLineDto(new ProductDto(0, l.Product.Name, null, 0, null), l.Quantity));
                }
                ordersDto.Add(new OrderDto(o.Id, o.Name, o.Line1, o.Line2, o.Line3, o.City, o.State, o.Country, o.Zip, o.GiftWrap, o.Shipped, linesDto));
            }
            outputPort.Handle(new GetUnshippedOrdersResponse(ordersDto, true));
            return true;
        }
    }
}
