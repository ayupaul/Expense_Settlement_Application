import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ExpenseService } from '../services/expense.service';

@Component({
  selector: 'app-paid-expense',
  templateUrl: './paid-expense.component.html',
  styleUrls: ['./paid-expense.component.css'],
})
export class PaidExpenseComponent implements OnInit {
  expenseDetail: any;
  expenseId!: number;
  expenseSettled!:boolean
  constructor(
    private route: ActivatedRoute,
    private expenseService: ExpenseService,
    private router:Router
  ) {}
  ngOnInit(): void {
    this.route.params.subscribe((param) => {
      this.expenseId = param['expenseId'];
    });
    this.expenseService.getExpenseById(this.expenseId).subscribe(
      (res) => {
        this.expenseDetail = res;
      },
      (err) => {
        console.log(err);
      }
    );
    this.expenseService.checkExpenseSettled(this.expenseId).subscribe((res)=>{
      console.log(res);
      this.expenseSettled=res;
    },(err)=>{
      console.log(err);
    })
  }
  closeExpense(){
    this.expenseService.closeExpense(this.expenseId).subscribe((res)=>{
      console.log(res);
      this.router.navigateByUrl("/dashboard");
    })
  }
}
