import { Task } from './../task';
import { SubtaskService } from './../services/subtask.service';
import { AuthService } from './../services/auth-service';
import { BadInput } from './../common/bad-input';
import { NotFoundError } from './../common/not-found-error';
import { AppError } from './../common/app-error';
import { TaskService } from '../services/task.service';
import { Component, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SubTask } from '../subtask';

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.css']
})
export class TaskComponent implements OnInit {

  tasks: Task[] = [];
  subtasks: SubTask[] = [];
  @Output()task:any;

  myForm!: FormGroup;
  

  constructor(public service:TaskService,public subservice:SubtaskService,private fb:FormBuilder,public authService:AuthService) { }

  ngOnInit(): void {

    this.getAll();
    this.GForm();
    

  }

  GForm(){
    this.myForm=this.fb.group({
      taskId:[null],
      title: ['',Validators.required],
      description: ['',Validators.required],
      // date: [''],
      userId:['']
    });
  }

  getAll(){
    this.service.GetAll().subscribe(res=>{
      this.tasks=res;
    });

  }
  GetSubTasksOfTask(taskId:string){
    this.subservice.GetSubTaskOfTask(taskId).subscribe(res=>{
      this.subtasks=res;
    });    
  }

  Create(){
    this.myForm.controls['userId'].setValue(this.authService.currentUser.userId);
    this.service.create(this.myForm.value).subscribe(res=>{
      this.myForm.reset();
      this.getAll();
      
    },(error:AppError)=>{
      if(error instanceof BadInput)
        //this.myForm.setErrors(error.orignalError);
        alert('BadInput');

      else throw error;
    });
  }

  Update(){
    this.service.update(this.myForm.value).subscribe(res=>{
        this.myForm.reset();
        this.getAll();
        
    },(error:AppError)=>{
      if(error instanceof BadInput)
        alert('BadInput');
      else throw error;  
  }
    );

  }
  Delete(id:any) {
    
    this.service.delete(id).subscribe(res => {
      this.getAll();
  },
  (error:AppError)=>{

    if(error instanceof NotFoundError)
      alert('This Task has Already deleted');
    else throw error;
   
  });
  }

  edit(task:any){
    this.myForm.patchValue(task);
    

  }

  delete(task:any){
    let id = task.taskId;
    this.Delete(id);

  }
  


  CreateData(){
    if (this.myForm.value.taskId == null) {
      this.Create();
    }
    else {
      this.Update();
    }
  }





  get title(){
    return this.myForm.get('title');
   }
   get description(){
    return this.myForm.get('description');
   }
  


}
