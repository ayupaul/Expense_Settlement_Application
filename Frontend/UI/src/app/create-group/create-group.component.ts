import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { GroupService } from '../services/group.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-group',
  templateUrl: './create-group.component.html',
  styleUrls: ['./create-group.component.css']
})
export class CreateGroupComponent implements OnInit {
  groupForm!:FormGroup
  constructor(private groupService:GroupService,private router:Router){}
  ngOnInit(): void {
   this.groupForm=new FormGroup({
    groupName:new FormControl(null,Validators.required),
    description:new FormControl(null,Validators.required)
   })
  }

  onAdd(){
    const date=new Date();
    const createdDate=date.toLocaleDateString();
    const groupDetail={...this.groupForm.value,createdDate:createdDate};
    this.groupService.createGroup(groupDetail).subscribe((data)=>{
      console.log(data);
      this.router.navigateByUrl("/dashboard");
    },(error)=>{
      console.log(error);
    }
  )
  }
}
