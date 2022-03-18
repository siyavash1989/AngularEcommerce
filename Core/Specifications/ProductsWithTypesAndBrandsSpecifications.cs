using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecifications:BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecifications()
        {
            AddIncludes(p=>p.ProductBrand);
            AddIncludes(p=>p.ProductType);
        }

        public ProductsWithTypesAndBrandsSpecifications(int id) : base(x=>x.Id == id)
        {
            AddIncludes(p=>p.ProductBrand);
            AddIncludes(p=>p.ProductType);
        }
    }
}