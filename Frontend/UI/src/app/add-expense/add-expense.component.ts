import { Component, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { GroupService } from '../services/group.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ExpenseService } from '../services/expense.service';

@Component({
  selector: 'app-add-expense',
  templateUrl: './add-expense.component.html',
  styleUrls: ['./add-expense.component.css']
})
export class AddExpenseComponent implements OnInit {
  expenseForm!:any
  usersInGroup!:any
  groupId!:number
  constructor(private groupService:GroupService,private route:ActivatedRoute,private expenseService:ExpenseService,private router:Router){}
  ngOnInit(): void {
    this.expenseForm=new FormGroup({
      expenseName:new FormControl(null,[Validators.required]),
      description:new FormControl(null,[Validators.required]),
      expenseAmount:new FormControl(null,[Validators.required]),
      EmailsPaidBy:new FormArray([]),
      EmailSplitAmongs:new FormArray([])
    });
    this.route.params.subscribe((param)=>{
      this.groupId=param['groupId'];
    });
    this.groupService.getUserInGroup(this.groupId).subscribe((data)=>{
      this.usersInGroup=data;
    },(err)=>{
      console.log(err);
    });
  }
  onAddPaid(){
    const emailControl=new FormControl(null);
    (this.expenseForm.controls['EmailsPaidBy'] as FormArray).push(emailControl);
  }
  onAddSplit(){
    const emailControl=new FormControl(null);
    (this.expenseForm.controls['EmailSplitAmongs'] as FormArray).push(emailControl);
  }
  onSubmit(){
    const date=new Date();
    const expenseDate=date.toLocaleDateString();
    const expenseDetails={...this.expenseForm.value,expenseDate:expenseDate};
    this.expenseService.addNewExpense(expenseDetails,this.groupId).subscribe((res)=>{
      console.log(res);
      this.router.navigateByUrl(`/dashboard}`);
    },(error)=>{
      console.log(error);
    });
  }
}
