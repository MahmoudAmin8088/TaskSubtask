import { User } from './../user';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth-service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  invalidSignUp!: boolean;
  myForm!: FormGroup;
    constructor(
      private fb:FormBuilder,
     private authService: AuthService,
    private router:Router,
    ) { }

  ngOnInit(): void {
    this.GForm();
  }
  GForm(){
    this.myForm=this.fb.group({
      firstName: ['',Validators.required],
      lastName: ['',Validators.required],
      userName:['',Validators.required],
      email:['',Validators.required],
      password:['',Validators.required]
    });
  }
  signUp(){
    
    this.authService.signUp(this.myForm.value)
    .subscribe(result=>{
      console.log(this.myForm);
      if (result!){
        this.router.navigate(['/login']);
      }
      else
      this.invalidSignUp = true;
    
    });
  }
  

  get firstName(){
    return this.myForm.get('firstName');
   }
   get lastName(){
    return this.myForm.get('lastName');
   }
   get userName(){
    return this.myForm.get('userName');
   }
   get email(){
    return this.myForm.get('email');
   }
   get password(){
    return this.myForm.get('password');
   }
   

}
