import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-group',
  templateUrl: './group.component.html',
  styleUrls: ['./group.component.css']
})
export class GroupComponent  {
  group:any
  constructor(private router:Router){
    const navigation=this.router.getCurrentNavigation();
    this.group=navigation?.extras?.state?.['groupDetails'];
    console.log(this.group);
  }

}
