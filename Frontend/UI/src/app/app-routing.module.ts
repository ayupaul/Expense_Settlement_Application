import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { CreateGroupComponent } from './create-group/create-group.component';
import { GroupComponent } from './group/group.component';
import { ViewMembersComponent } from './view-members/view-members.component';
import { AddExpenseComponent } from './add-expense/add-expense.component';
import { MyExpensesComponent } from './my-expenses/my-expenses.component';
import { OweExpenseComponent } from './owe-expense/owe-expense.component';
import { PaidExpenseComponent } from './paid-expense/paid-expense.component';
import { AllUsersComponent } from './all-users/all-users.component';
import { UpdateUserComponent } from './update-user/update-user.component';
import { UserGuard } from './guards/user.guard';
import { AdminGuard } from './guards/admin.guard';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  { path: 'dashboard', component: DashboardComponent ,canActivate:[UserGuard]},
  { path: 'add new group', component: CreateGroupComponent,canActivate:[UserGuard] },
  { path: 'myGroup', component: GroupComponent ,canActivate:[UserGuard]},
  { path: 'viewMember/:id', component: ViewMembersComponent,canActivate:[UserGuard] },
  { path: 'addExpense/:groupId', component: AddExpenseComponent ,canActivate:[UserGuard]},
  { path: 'viewExpense/:groupId', component: MyExpensesComponent,canActivate:[UserGuard] },
  { path: 'oweExpense/:expenseId', component: OweExpenseComponent ,canActivate:[UserGuard]},
  { path: 'paidExpense/:expenseId', component: PaidExpenseComponent,canActivate:[UserGuard] },
  {path:'viewAllUsers',component:AllUsersComponent,canActivate:[AdminGuard]},
  {path:'updateUser/:id',component:UpdateUserComponent,canActivate:[AdminGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
