﻿using Core.Dto;
using Core.Dto.UseCaseRequests;
using Core.Dto.UseCaseResponses;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Interfaces.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.UseCases
{
    public sealed class RemoveFromCartUseCase : IRemoveFromCartUseCase
    {
        private readonly IProductRepository productRepository;
        private readonly ICartService _cartService;

        public RemoveFromCartUseCase(IProductRepository repo, ICartService cartService)
        {
            productRepository = repo;
            _cartService = cartService;
        }

        public async Task<bool> Handle(RemoveFromCartRequest request, IOutputPort<RemoveFromCartResponse> outputPort)
        {
            var linesDto = _cartService.Lines;
            if (!linesDto.Any())
            {
                outputPort.Handle(new RemoveFromCartResponse(false, "Your Cart is Empty"));
                return false;
            }
            var product = await productRepository.GetProductById(request.ProductId);
            if (product != null)
            {
                await _cartService.RemoveLine(new ProductDto(product.Id, product.Name, product.Description, product.Price, product.Category));
                outputPort.Handle(new RemoveFromCartResponse(true));
                return true;
            }
            outputPort.Handle(new RemoveFromCartResponse(false, $"ProductId - {request.ProductId} was not found"));
            return false;
        }
    }
}
