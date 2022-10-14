import { FormGroup } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from '../services/auth-service';
import { AstMemoryEfficientTransformer } from '@angular/compiler';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent  {
  invalidLogin!: boolean; 
  myForm!:FormGroup;
  constructor(
    private authService: AuthService,
    private router:Router,
    private route:ActivatedRoute) { }

  signIn(credentials:any) {
    this.authService.login(credentials)
      .subscribe(result => { 
        if (result!){
          let returnUrl=this.route.snapshot.queryParamMap.get('returnUrl');
          this.router.navigate([returnUrl || '/home']);
        }
        else  
          this.invalidLogin = true; 
      });
  }
}
