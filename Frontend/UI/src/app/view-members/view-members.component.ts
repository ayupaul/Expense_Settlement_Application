import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { UserServiceService } from '../services/user-service.service';
import { GroupService } from '../services/group.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-view-members',
  templateUrl: './view-members.component.html',
  styleUrls: ['./view-members.component.css']
})
export class ViewMembersComponent implements OnInit {
  users:any
  usersInGroup:any
  groupId:any
  constructor(private userService:UserServiceService,private groupService:GroupService,private route:ActivatedRoute,private router:Router){}
  ngOnInit(): void {
    this.route.params.subscribe((param)=>{
      this.groupId=param['id'];
    })
   
    this.userService.getAllUsers().subscribe((res)=>{
      console.log(res);
      this.users=res;
    },(err)=>{
      console.log(err);
    })
    this.getUserInGroup();
  }
  onAdd(userId:any){
    this.groupService.addUserToGroup(userId,Number(this.groupId)).subscribe((res)=>{
      console.log(res);
      this.getUserInGroup();
    },(err)=>{
      alert("Something went wrong");
    })
  }
  isUserInGroup(user:any){
   const b= this.usersInGroup.some((usersData:any)=>usersData.id===user.id);
   return b;
  }
  getUserInGroup(){
    this.groupService.getUserInGroup(this.groupId).subscribe((res)=>{
      this.usersInGroup=res;
      console.log(this.usersInGroup);
    },(error)=>{
      console.log(error);
    })
  }
}
