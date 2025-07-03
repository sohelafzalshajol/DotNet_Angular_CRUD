export interface ProductDetails {
    id: number;
    details?: string;
    productId: number;
  }
  
  export interface Product {
    id: number;
    name: string;
    price: number;
    note?: string;
    isActive: boolean;
    productDetails: ProductDetails[];
  }