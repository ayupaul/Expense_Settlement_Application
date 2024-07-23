import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ExpenseService } from '../services/expense.service';

@Component({
  selector: 'app-my-expenses',
  templateUrl: './my-expenses.component.html',
  styleUrls: ['./my-expenses.component.css']
})
export class MyExpensesComponent implements OnInit {
  groupId!:number
  expensesPaidByMe:any
  expensesThatIOwe:any
  constructor(private route:ActivatedRoute,private expenseService:ExpenseService,private router:Router){}
  ngOnInit(): void {
    this.route.params.subscribe((param)=>{
      this.groupId=param['groupId'];
    },(err)=>{
      alert("Something went wrong");
    })
    this.expenseService.getExpensesPaidByMe(this.groupId).subscribe((res)=>{
      this.expensesPaidByMe=res;
    },(err)=>{
      console.log(err);
    })
    this.expenseService.getExpensesOwe(this.groupId).subscribe((res)=>{
      this.expensesThatIOwe=res;
    },(err)=>{
      console.log(err);
    })
  }

  addNewExpense(){
    this.router.navigateByUrl(`/addExpense/${this.groupId}`);
  }
}
