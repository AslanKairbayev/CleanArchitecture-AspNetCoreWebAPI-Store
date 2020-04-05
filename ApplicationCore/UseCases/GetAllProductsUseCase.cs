﻿using ApplicationCore.Dto;
using ApplicationCore.Dto.UseCaseRequests;
using ApplicationCore.Dto.UseCaseResponses;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Interfaces.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationCore.Interactors
{
    public class GetAllProductsUseCase : IGetAllProductsUseCase
    {
        private IProductRepository repository;

        public GetAllProductsUseCase(IProductRepository repo)
        {
            repository = repo;
        }
        public bool Handle(GetAllProductsRequest request, IOutputPort<GetAllProductsResponse> outputPort)
        {
            var products = repository.Products;

            if (products.Count() != 0)
            {
                var productsDto = new List<ProductDto>();

                foreach (var p in products)
                {
                    productsDto.Add(new ProductDto(p.Id, p.Name, p.Description, p.Price));
                }

                outputPort.Handle(new GetAllProductsResponse(productsDto));

                return true;
            }

            return false;
        }
    }
}