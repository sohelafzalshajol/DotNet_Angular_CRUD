
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Product } from 'src/app/models/product.model';
import { ProductService } from 'src/app/services/product.service';

@Component({ 
  selector: 'app-product-list', 
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];

  constructor(private service: ProductService, private router: Router) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts() {
    debugger;
    this.service.getAll().subscribe(res => (this.products = res));
    console.log(this.products);
  }

  edit(id: number) {
    this.router.navigate(['/edit', id]);
  }

  delete(id: number) {
    if (confirm('Are you sure?')) {
      //this.service.delete(id).subscribe(() => this.loadProducts());

      this.service.delete(id).subscribe(response => {
        if (response.status) {
          alert(response.message);
        } else {
          console.error('Delete failed:', response.message);
        }
        this.loadProducts();
      });

    }
  }
}
