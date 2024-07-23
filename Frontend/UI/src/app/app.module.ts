import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { ReactiveFormsModule } from '@angular/forms';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import { DashboardComponent } from './dashboard/dashboard.component';
import { CreateGroupComponent } from './create-group/create-group.component';
import { GroupComponent } from './group/group.component';
import { ViewMembersComponent } from './view-members/view-members.component';
import { AddExpenseComponent } from './add-expense/add-expense.component';
import { MyExpensesComponent } from './my-expenses/my-expenses.component';
import { OweExpenseComponent } from './owe-expense/owe-expense.component';
import { PaidExpenseComponent } from './paid-expense/paid-expense.component';
import { AllUsersComponent } from './all-users/all-users.component';
import { UpdateUserComponent } from './update-user/update-user.component'
import { TokenInterceptor } from './interceptor/token.interceptor';
@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    HomeComponent,
    LoginComponent,
    DashboardComponent,
    CreateGroupComponent,
    GroupComponent,
    ViewMembersComponent,
    AddExpenseComponent,
    MyExpensesComponent,
    OweExpenseComponent,
    PaidExpenseComponent,
    AllUsersComponent,
    UpdateUserComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [
    {
    provide:HTTP_INTERCEPTORS,
    useClass:TokenInterceptor,
    multi:true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
