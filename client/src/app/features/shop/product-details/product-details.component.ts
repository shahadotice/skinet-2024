import { Component, inject, OnInit } from '@angular/core';
import { ShopService } from '../../../core/services/shop.service';
import { ActivatedRoute } from '@angular/router';
import { Product } from '../../../shared/models/Product';
import { CurrencyPipe } from '@angular/common';
import { MaterialModule } from '../../../shared/material/material.module';

@Component({
  selector: 'app-product-details',
  standalone: true,
  imports: [CurrencyPipe,MaterialModule],
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.scss'
})
export class ProductDetailsComponent implements OnInit {
  private shopService=inject(ShopService);
  private activatedRoute=inject(ActivatedRoute);
  product?:Product;
  ngOnInit(): void {
    this.loadProduct();
  }
  loadProduct(){
    const id=this.activatedRoute.snapshot.paramMap.get('id');
    if(!id) return;
    this.shopService.getproduct(+id).subscribe({
      next:product=>this.product=product,
      error:error=>console.log(error)
    })
  }

}
