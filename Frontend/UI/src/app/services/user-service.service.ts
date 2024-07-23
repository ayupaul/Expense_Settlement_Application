import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UserServiceService {
  BackendUrl: string = 'http://localhost:5006/api/Account';
  constructor(private http: HttpClient) {}
  loginService(userDetails: any): Observable<any> {
    return this.http.post(`${this.BackendUrl}`, userDetails);
  }
  storeToken(token: string) {
    localStorage.setItem('token', token);
  }
  getToken() {
    return localStorage.getItem('token');
  }
  decodeToken() {
    const jwt = new JwtHelperService();
    const token = this.getToken() ?? '';
    return jwt.decodeToken(token);
  }
  getEmailFromToken() {
    const token = this.decodeToken();
    if (token) {
      const email = token.name;
      return email;
    }
    return '';
  }
  getUserIdFromToken() {
    const token = this.decodeToken();
    if (token) {
      const userId = token.UserId;
      return userId;
    }
    return 0;
  }
  getAllUsers(): Observable<any> {
    return this.http.get(`${this.BackendUrl}/getAllUsers`);
  }
  getAmountFromToken() {
    const token = this.decodeToken();
    if (token) {
      const amount = token.Amount;
      return amount;
    }
    return NaN;
  }
  getUserById(): Observable<any> {
    const userId = this.getUserIdFromToken();
    return this.http.get(`${this.BackendUrl}/getUserById/${userId}`);
  }
  getAllUsersForAdmin(): Observable<any> {
    return this.http.get(`${this.BackendUrl}/getAllUserForAdmin`);
  }
  getRoleFromToken() {
    const token = this.decodeToken();
    if (token) {
      const role = token.role;
      return role;
    }
    return '';
  }
  getUserIdForAdmin(userId: any) {
    return this.http.get(`${this.BackendUrl}/getUserById/${userId}`);
  }
  updateUser(user: any,userId:any): Observable<any> {
    return this.http.put(`${this.BackendUrl}/updateUser/${userId}`, user);
  }
}
