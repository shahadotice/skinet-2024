using System;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using Core.Entity;

namespace Core.Specifications;

public class ProductSpecification : BaseSpecification<Product>
{
    public ProductSpecification(ProductSpecParam specParam) : base(x=>
    (string.IsNullOrEmpty(specParam.Search) || x.Name.ToLower().Contains(specParam.Search)) &&
    (!specParam.Brands.Any()||specParam.Brands.Contains(x.Brand))&&
    (!specParam.Types.Any()||specParam.Types.Contains(x.Type))
    )
    {
        ApplyPaging(specParam.PageSize * (specParam.PageIndex-1),specParam.PageSize);
        switch (specParam.Sort)
        {
            case "priceAsc":
                AddOrderBy(x=>x.Price);
                break;
            case "priceDesc" :   
                AddOrderByDescending(x=>x.Price);
                break;
            default:
                AddOrderBy(x=>x.Name);
                break;

        }
    }
}
