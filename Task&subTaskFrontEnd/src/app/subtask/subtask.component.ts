import { ActivatedRoute } from '@angular/router';
import { AuthService } from './../services/auth-service';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AppError } from '../common/app-error';
import { BadInput } from '../common/bad-input';
import { NotFoundError } from '../common/not-found-error';
import { SubtaskService } from '../services/subtask.service';
import { SubTask } from '../subtask';

@Component({
  selector: 'subtask',
  templateUrl: './subtask.component.html',
  styleUrls: ['./subtask.component.css']
})
export class SubtaskComponent implements OnInit {
 id:any;
 subtasks: SubTask[] = [];
  myForm!: FormGroup;
  priority:any;
  

  constructor( private route :ActivatedRoute, public service:SubtaskService,private fb:FormBuilder,public authService: AuthService) { }

  ngOnInit(): void {

    this.route.paramMap.subscribe(param=>{
     this.id= param.get('taskId');
    });
    this.GetSubTasksOfTask();
    this.GForm();
    this.GetPriority();
  
  }

  GForm(){
    this.myForm=this.fb.group({
      subtaskId:[null],
      title: ['',Validators.required],
      description: ['',Validators.required],
      // date: [''],
      userId:[''],
      taskId:[''],
      priorityId:['',Validators.required]
    });
  }

  getAll(){
    this.service.GetAll().subscribe(res=>{
      this.subtasks=res;
    });

  }
  GetPriority(){
    this.service.GetPriority().subscribe(res=>{
      this.priority=res;
    });
  }


  GetSubTasksOfTask(){
    this.service.GetSubTaskOfTask(this.id).subscribe(res=>{
      this.subtasks=res;
    });    
  }


  Create(){
    this.myForm.controls['userId'].setValue(this.authService.currentUser.userId);
    this.myForm.controls['taskId'].setValue(this.id);

    this.service.create(this.myForm.value).subscribe(res=>{
      this.myForm.reset();
      this.GetSubTasksOfTask();
      
    },(error:AppError)=>{
      if(error instanceof BadInput)
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
      alert('This Subtask has Already deleted');
    else throw error;
   
  });
  }
  
  edit(subtask:any){
    this.myForm.controls['subtaskId'].setValue(subtask.subTaskId);
    this.myForm.patchValue(subtask);
    

  }

  delete(subtask:any){
    let id = subtask.subTaskId;
    this.Delete(id);

  }


  CreateData(){
    if (this.myForm.value.subtaskId == null) {
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
   get prioritys(){
    return this.myForm.get('priorityId');
   }


}
