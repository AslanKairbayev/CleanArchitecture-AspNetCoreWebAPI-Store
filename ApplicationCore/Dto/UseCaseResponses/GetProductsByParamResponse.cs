﻿using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Dto.UseCaseResponses
{
    public class GetProductsByParamResponse : ResponseMessage
    {
        public IEnumerable<ProductDto> Products { get; set; }

        public GetProductsByParamResponse(IEnumerable<ProductDto> products, bool success = false, string message = null) : base(success, message)
        {
            Products = products;
        }
    }
}