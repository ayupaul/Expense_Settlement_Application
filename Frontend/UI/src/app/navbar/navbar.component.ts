import { Component, OnInit } from '@angular/core';
import { UserStoreService } from '../services/user-store.service';
import { UserServiceService } from '../services/user-service.service';
import { Router } from '@angular/router';
import { switchMap } from 'rxjs';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  email!: any;
  role:any;
  updatedAmount:any
  AmountduringTraverse:any
  actualAmount:any
  constructor(
    private userStoreService: UserStoreService,
    private useService: UserServiceService,
    private router:Router
  ) {}
  ngOnInit(): void {
    this.userStoreService.getEmailFromStore().subscribe((res)=>{
      this.email=res || this.useService.getEmailFromToken();
    })
    this.useService.getUserById().subscribe((res)=>{
      this.updatedAmount=res.amount;
    })
    this.userStoreService.getUserDetailFromStore().subscribe((res)=>{
      this.actualAmount=res?.amount || this.updatedAmount;
    })
    this.userStoreService.getRoleFromToken().subscribe((res)=>{
      this.role=res || this.useService.getRoleFromToken();
    })
  }
  
  onLogout(){
    localStorage.clear();
    this.router.navigateByUrl("");
  }
}
