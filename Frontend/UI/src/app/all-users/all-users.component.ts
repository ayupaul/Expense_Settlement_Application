import { Component, OnInit } from '@angular/core';
import { UserServiceService } from '../services/user-service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-all-users',
  templateUrl: './all-users.component.html',
  styleUrls: ['./all-users.component.css']
})
export class AllUsersComponent implements OnInit {
  users:any
  constructor(private userService:UserServiceService,private router:Router){}
  ngOnInit(): void {
    this.userService.getAllUsers().subscribe((res)=>{
      this.users=res;
    },(err)=>{
      alert("Something went wrong");
    })
  }
  updateUser(userId:any){
    this.router.navigateByUrl(`/updateUser/${userId}`);
  }
}
