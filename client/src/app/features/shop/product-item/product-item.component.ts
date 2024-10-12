import { Component, Input } from '@angular/core';
import { Product } from '../../../shared/models/Product';
import { MaterialModule } from '../../../shared/material/material.module';
import { CurrencyPipe } from '@angular/common';

@Component({
  selector: 'app-product-item',
  standalone: true,
  imports: [MaterialModule,CurrencyPipe],
  templateUrl: './product-item.component.html',
  styleUrl: './product-item.component.scss'
})
export class ProductItemComponent {
  @Input() product?:Product

}
