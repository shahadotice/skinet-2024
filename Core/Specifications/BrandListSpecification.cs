using System;
using Core.Entity;

namespace Core.Specifications;

public class BrandListSpecification:BaseSpecification<Product,string>
{
    public BrandListSpecification()
    {
        AddSelect(x=>x.Brand);
        ApplyDistinct();
    }

}
