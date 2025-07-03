
import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductDetails } from 'src/app/models/product.model';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.scss']
})
export class ProductFormComponent implements OnInit {
  productForm!: FormGroup;
  productId: number = 0;

  constructor(
    private fb: FormBuilder,
    private service: ProductService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.productId = +this.route.snapshot.paramMap.get('id')!;
    this.productForm = this.fb.group({
      Id: [this.productId],
      name: ['', Validators.required],
      price: [0, Validators.required],
      note: [''],
      isActive: [true],
      productDetails: this.fb.array([])
    });

    if (this.productId) {
      this.service.getById(this.productId).subscribe(res => {
        console.log(res);
        this.productForm.patchValue(res);
        res.productDetails.forEach(detail => this.populateDetail(detail));
      });
    }
  }

  get productDetails() {
    return this.productForm.get('productDetails') as FormArray;
  }

  addDetail(value: string = '') {
    this.productDetails.push(this.fb.group({
      id: [0],
      details: [value],
      productId: [this.productId]
    }));
  }

  populateDetail(data: ProductDetails) {
    this.productDetails.push(this.fb.group({
      id: [data.id],
      details: [data.details],
      productId: [this.productId]
    }));
  }
  removeDetail(index: number) {
    this.productDetails.removeAt(index);
  }

  onSubmit() {
    const product = this.productForm.value;
    if (this.productId === 0) {
     // this.service.create(product).subscribe(() => this.router.navigate(['/']));
      
      this.service.create(product).subscribe(response => {
        if (response.status) {
          alert(response.message);
        } else {
          console.error('Save failed:', response.message);
        }
        this.router.navigate(['/']);
      });

    } else {
      //this.service.update(this.productId, product).subscribe(() => this.router.navigate(['/']));

      this.service.update(this.productId, product).subscribe(response => {
        if (response.status) {
          alert(response.message);
        } else {
          console.error('Update failed:', response.message);
        }
        this.router.navigate(['/']);
      });


    }
  }
}
