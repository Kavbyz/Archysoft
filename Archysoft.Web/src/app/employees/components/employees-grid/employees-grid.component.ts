import { Component, OnInit, ViewChild } from '@angular/core';
import { EmployeesDataSource } from '../../services/employees.datasource';
import { MatPaginator } from '@angular/material';
import { EmployeeService } from '../../services/employee.service';

@Component({
  selector: 'employees-grid',
  templateUrl: './employees-grid.component.html',
  styleUrls: ['./employees-grid.component.scss']
})
export class EmployeesGridComponent implements OnInit {

  displayedColumns:string[]=['id', 'userName', 'firstName', 'email', 'lastName'];
  dataSource: EmployeesDataSource;

  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(private employeeService:EmployeeService) { }

  ngOnInit() {
    this.dataSource=new EmployeesDataSource(this.employeeService);
    this.dataSource.loadEmployees({search:'', orderBy:'', pageIndex:0, pageSize:10});
  }

}
