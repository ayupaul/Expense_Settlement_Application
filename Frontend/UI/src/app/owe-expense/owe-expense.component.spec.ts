import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OweExpenseComponent } from './owe-expense.component';

describe('OweExpenseComponent', () => {
  let component: OweExpenseComponent;
  let fixture: ComponentFixture<OweExpenseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OweExpenseComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OweExpenseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
