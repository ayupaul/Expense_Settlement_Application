import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ExpenseService } from '../services/expense.service';

@Component({
  selector: 'app-owe-expense',
  templateUrl: './owe-expense.component.html',
  styleUrls: ['./owe-expense.component.css'],
})
export class OweExpenseComponent implements OnInit {
  expenseId!: number;
  expenseDetail: any;
  constructor(
    private route: ActivatedRoute,
    private expenseService: ExpenseService,
    private router: Router
  ) {}
  ngOnInit(): void {
    this.route.params.subscribe(
      (param) => {
        this.expenseId = param['expenseId'];
      },
      (err) => {
        alert('Something went wrong');
      }
    );
    this.expenseService.viewMyOweExpenseDetails(this.expenseId).subscribe(
      (res) => {
        this.expenseDetail = res;
        console.log(res);
      },
      (err) => {
        alert('Something went wrong');
      }
    );
  }
  onPay(amountOwe: number) {
    this.expenseService.settleExpense(this.expenseId, amountOwe).subscribe(
      (res) => {
        console.log(res);
        this.router.navigateByUrl('/dashboard');
      },
      (err) => {
        console.log(err);
      }
    );
  }
}
