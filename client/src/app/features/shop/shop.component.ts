import { Component, inject, OnInit } from '@angular/core';
import { ShopService } from '../../core/services/shop.service';
import { Product } from '../../shared/models/Product';
import { MaterialModule } from '../../shared/material/material.module';
import { ProductItemComponent } from "./product-item/product-item.component";
import { MatDialog } from '@angular/material/dialog';
import { FiltersDialogComponent } from './filters-dialog/filters-dialog.component';
import { MatSelectionListChange } from '@angular/material/list';
import { ShopParams } from '../../shared/models/shopParams';
import { Pagination } from '../../shared/models/Pagination';
import { PageEvent } from '@angular/material/paginator';
import { FormsModule } from '@angular/forms';


@Component({
  selector: 'app-shop',
  standalone: true,
  imports: [FormsModule,MaterialModule, ProductItemComponent],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss'
})
export class ShopComponent implements OnInit {
  private shopServices=inject(ShopService);
  private dialogService=inject(MatDialog);

  sortOptions=[
    {name:'Alphabetical', value:'name'},
    {name:'Price: Low-High', value:'priceAsc'},
    {name:'price: High-Low', value:'priceDesc'},
  ]
  shopParams=new ShopParams();
  products?:Pagination<Product>;
  pageSizeOptions=[5,10,15,20];
  


  ngOnInit(): void {
    this.initializeShop();
  }

  initializeShop(){
    this.shopServices.getBrands();
    this.shopServices.getTypes();
    this.getProduct();
    
  }

  getProduct(){
    this.shopServices.getProducts(this.shopParams).subscribe({
      next:response=>this.products=response,
      error:error=>console.log(error),
      
    })
  }
  onSearchChange(){
    debugger;
  
    this.shopParams.pageNumber=1;
    this.getProduct();
  }
  handlePageEvent(event:PageEvent){
    this.shopParams.pageNumber=event.pageIndex+1;
    this.shopParams.pageSize=event.pageSize;
    this.getProduct();

  }
  onSortChange(event:MatSelectionListChange){
    const selectedOption=event.options[0];
    if(selectedOption){
      this.shopParams.sort=selectedOption.value;
      this.shopParams.pageNumber=1;
      this.getProduct();
    }
  }

  openFiltersDialog(){
    const dialogRef=this.dialogService.open(FiltersDialogComponent,{
      minWidth:'500px',
      data:{
        selectedBrands: this.shopParams.brands,
        selectedTypes:this.shopParams.types
      }
    });
    dialogRef.afterClosed().subscribe({
      next:result=>{
        if(result){
          console.log(result);
          debugger;
          this.shopParams.brands=result.selectedBrands;
          this.shopParams.types=result.selectedTypes;
          this.shopParams.pageNumber=1;
          this.getProduct();
        }
      }
    })
  }

}
