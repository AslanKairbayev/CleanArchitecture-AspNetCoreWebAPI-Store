﻿using Core.Dto.UseCaseResponses;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Presenters
{
    public class GetCategoriesPresenter : IOutputPort<GetCategoriesResponse>
    {
        public IEnumerable<string> Categories { get; private set; }

        public void Handle(GetCategoriesResponse response)
        {
            Categories = response.Categories;
        }
    }
}