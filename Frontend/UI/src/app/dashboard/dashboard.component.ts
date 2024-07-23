import { Component, OnInit } from '@angular/core';
import { UserServiceService } from '../services/user-service.service';
import { UserStoreService } from '../services/user-store.service';
import { GroupService } from '../services/group.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit{
  myGroups:any
  userDetail:any
  constructor(private userService:UserServiceService,private userStoreService:UserStoreService,private groupService:GroupService){}
  ngOnInit(): void {
    const email=this.userService.getEmailFromToken();
    const amount=this.userService.getAmountFromToken();
    const role=this.userService.getRoleFromToken();
    this.userStoreService.setEmailToStore(email);
    this.userStoreService.setAmountToStore(amount);
    this.userStoreService.setRoleToStore(role);
    this.groupService.getMyGroups().subscribe((res)=>{
      console.log(res);
      this.myGroups=res;
    },(error)=>{
      console.log(error);
    })
    this.userService.getUserById().subscribe((res)=>{
      this.userDetail=res;
      this.userStoreService.setUserDetailToStore(this.userDetail);
    },(err)=>{
      alert("Something went wrong while fetching details");
    })
  }
}
