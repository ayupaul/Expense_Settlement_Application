import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserServiceService } from './user-service.service';

@Injectable({
  providedIn: 'root',
})
export class ExpenseService {
  BackendUrl: string = 'http://localhost:5006/api/Expense';
  constructor(
    private http: HttpClient,
    private userService: UserServiceService
  ) {}

  addNewExpense(expenseDetails: any, groupId: any): Observable<any> {
    return this.http.post(`${this.BackendUrl}/${groupId}`, expenseDetails);
  }

  getExpensesPaidByMe(groupId: number): Observable<any> {
    const userId = this.userService.getUserIdFromToken();
    return this.http.get(
      `${this.BackendUrl}/getMyExpensesPaidByMe/${groupId}/${userId}`
    );
  }
  getExpensesOwe(groupId: number): Observable<any> {
    const userId = this.userService.getUserIdFromToken();
    return this.http.get(
      `${this.BackendUrl}/getMyExpensesSplitForMe/${groupId}/${userId}`
    );
  }
  viewMyOweExpenseDetails(expenseId: number): Observable<any> {
    const userId = this.userService.getUserIdFromToken();
    return this.http.get(
      `${this.BackendUrl}/getMyOwedExpense/${expenseId}/${userId}`
    );
  }
  settleExpense(expenseId: number, amountOwe: number): Observable<any> {
    const userId = this.userService.getUserIdFromToken();
    return this.http.post(
      `${this.BackendUrl}/settleExpense/${expenseId}/${userId}`,
      amountOwe
    );
  }
  getExpenseById(expenseId: number): Observable<any> {
    return this.http.get(`${this.BackendUrl}/getExpenseById/${expenseId}`);
  }
  checkExpenseSettled(expenseId:number):Observable<any>{
    return this.http.get(`${this.BackendUrl}/checkExpenseSettled/${expenseId}`);
  }
  closeExpense(expenseId:number):Observable<any>{
    return this.http.delete(`${this.BackendUrl}/closeExpense/${expenseId}`);
  }
}
