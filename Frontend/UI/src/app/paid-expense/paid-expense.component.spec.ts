import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PaidExpenseComponent } from './paid-expense.component';

describe('PaidExpenseComponent', () => {
  let component: PaidExpenseComponent;
  let fixture: ComponentFixture<PaidExpenseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PaidExpenseComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PaidExpenseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
