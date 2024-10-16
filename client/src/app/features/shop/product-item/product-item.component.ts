import { Component, Input } from '@angular/core';
import { Product } from '../../../shared/models/Product';
import { MaterialModule } from '../../../shared/material/material.module';
import { CurrencyPipe } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-product-item',
  standalone: true,
  imports: [MaterialModule,CurrencyPipe,RouterLink],
  templateUrl: './product-item.component.html',
  styleUrl: './product-item.component.scss'
})
export class ProductItemComponent {
  @Input() product?:Product

}
