// products.component.ts
import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent {
  cartItems: any[] = [];

  products: any[];

  constructor(private http: HttpClient) {
    this.products = [];
  }

  ngOnInit() {
    this.fetchProducts();
    const storedCartItems = localStorage.getItem('cartItems');
    if (storedCartItems) {
      this.cartItems = JSON.parse(storedCartItems);
    }
  }

  fetchProducts() {
    this.http.get<any[]>('/api/service/all')
      .subscribe(
        (response) => {
          this.products = response;
        },
        (error) => {
          console.error('Error fetching products:', error);
        }
      );
  }

  handleCartAction(product: any) {
    if (this.isProductInCart(product)) {
      this.removeFromCart(product);
    } else {
      this.addToCart(product);
    }
  }

  addToCart(product: any) {
    const existingItem = this.cartItems.find(item => item.id === product.id);
    if (!existingItem) {
      this.cartItems.push({ ...product, quantity: 1 });

      // Update local storage with the updated cart items
      localStorage.setItem('cartItems', JSON.stringify(this.cartItems));
    }
  }

  removeFromCart(item: any) {
    const itemIndex = this.cartItems.findIndex(cartItem => cartItem.id === item.id);
    if (itemIndex !== -1) {
      this.cartItems.splice(itemIndex, 1);

      // Update local storage with the updated cart items
      localStorage.setItem('cartItems', JSON.stringify(this.cartItems));
    }
  }

  isProductInCart(product: any) {
    return this.cartItems.some(item => item.id === product.id);
  }

  getTotalPrice() {
    let totalPrice = 0;
    for (const item of this.cartItems) {
      totalPrice += item.price * item.quantity;
    }
    return totalPrice;
  }
}
