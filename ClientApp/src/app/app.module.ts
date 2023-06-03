import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { PaymentComponent } from './payment/payment.component';
import { ProductsComponent } from './components/products/products.component';
import { CartComponent } from './components/cart/cart.component';
import { UserSearchComponent } from './components/user-search/user-search.component';
import { UserComponent } from './components/user/user.component';
import { UserServicesComponent } from './components/user-services/user-services.component';
import { ServiceListComponent } from './components/services/service-list/service-list.component';
import { ServiceFormComponent } from './components/services/service-form/service-form.component';
import {LoginComponent} from "./components/auth/login/login.component";
import {RegisterComponent} from "./components/auth/register/register.component";
import {LogoutComponent} from "./components/auth/logout/logout.component";
import {ProfileComponent} from "./components/auth/profile/profile.component";
import {AuthGuard} from "./components/auth/auth.guard";

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    PaymentComponent,
    CounterComponent,
    ProductsComponent,
    FetchDataComponent,
    UserSearchComponent,
    UserComponent,
    UserServicesComponent,

    ServiceListComponent,
    ServiceFormComponent,
    LoginComponent,
    RegisterComponent,
    LogoutComponent,
    CartComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard] },
      { path: 'payment/:type', component: PaymentComponent },
      { path: 'payment', component: PaymentComponent },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'services', component: ProductsComponent },
      { path: 'cart', component: CartComponent },
      { path: 'search', component: UserSearchComponent },
      { path: 'services-manage', component: ServiceListComponent },
      { path: 'add-service', component: ServiceFormComponent },
      { path: 'login', component: LoginComponent },
      { path: 'logout', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
