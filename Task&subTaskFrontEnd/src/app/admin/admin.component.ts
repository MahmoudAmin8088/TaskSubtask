import { BadInput } from './../common/bad-input';
import { AuthService } from './../services/auth-service';
import { NotFoundError } from './../common/not-found-error';
import { AppError } from './../common/app-error';
import { UserService } from './../services/user.service';
import { Component, OnInit } from '@angular/core';
import { User } from '../user';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
  employees: User[]=[];
  Employee!: User;

  constructor(private service:UserService ,public auth:AuthService) { }

  ngOnInit(): void {
    this.getAll();
  }

  getAll(){
    
    this.service.GetEmployees().subscribe(res=>{
      this.employees=res;
      console.log(res);
      
    });
  }
  getEmp(id:string){
    this.service.GetEmployee(id).subscribe(res=>{
      this.Employee=res;
    });
  }
  Delete(id:string){
    this.service.DeleteEmployee(id).subscribe(res=>{
      this.getAll();
    },
    (error:AppError)=>{
      if(error instanceof NotFoundError)
      alert('This Employee has Already deleted');
    else throw error;
    }
    );
  }
  MakeAdmin={UserId:'',Role:'Admin'}
  admin(employee:User){
    this.MakeAdmin.UserId=employee.id;
    
    this.auth.MakeAdmin(this.MakeAdmin).subscribe(res=>{
      this.getAll();
      
    },
    (error:AppError)=>{
      if(error instanceof BadInput)
      alert('Something WentRong');
    else throw error;
    }
    );
  }
MakeEmployee={UserId:'',Role:'Employee'}
  removeadmin(employee:User){
    this.MakeEmployee.UserId=employee.id;
    this.auth.MakeEmployee(this.MakeEmployee).subscribe(res=>{
      this.getAll();
    },
    (error:AppError)=>{
      if(error instanceof BadInput)
      alert('Something WentRong');
    else throw error;

    }
    );
  }
  

  delete(employee:any){
    debugger;
    let id = employee.id;
    this.Delete(id);
  }
 
}
