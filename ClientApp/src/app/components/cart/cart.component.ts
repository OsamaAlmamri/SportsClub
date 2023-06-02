// cart.component.ts
import { Component } from '@angular/core';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent {
  cartItems: any[] = [];

  constructor() {
    const storedCartItems = localStorage.getItem('cartItems');
    if (storedCartItems) {
      this.cartItems = JSON.parse(storedCartItems);
    }
  }

  removeFromCart(item: any) {
    const itemIndex = this.cartItems.findIndex(cartItem => cartItem.id === item.id);
    if (itemIndex !== -1) {
      this.cartItems.splice(itemIndex, 1);
      localStorage.setItem('cartItems', JSON.stringify(this.cartItems));
    }
  }

  getTotalPrice() {
    let totalPrice = 0;
    for (const item of this.cartItems) {
      totalPrice += item.price * item.quantity;
    }
    return totalPrice;
  }
}
