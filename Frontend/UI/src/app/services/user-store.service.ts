import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UserStoreService {
  dataSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  amountSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  userDetailSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  roleDetailSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  constructor() {}
  setEmailToStore(email: string) {
    this.dataSubject.next(email);
  }
  getEmailFromStore() {
    return this.dataSubject.asObservable();
  }
  setAmountToStore(amount: string) {
    this.amountSubject.next(amount);
  }
  getAmountSubjectFromStore() {
    return this.amountSubject.asObservable();
  }
  setUserDetailToStore(userDetail: any) {
    this.userDetailSubject.next(userDetail);
  }
  getUserDetailFromStore() {
    return this.userDetailSubject.asObservable();
  }
  setRoleToStore(role: any) {
    this.roleDetailSubject.next(role);
  }
  getRoleFromToken() {
    return this.roleDetailSubject.asObservable();
  }
}
