import { UserService } from './services/user.service';
import { AdminAuthGuard } from './services/admin-auth-guard.service';
import { AuthGuard } from './services/auth-guard.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NotFoundComponent } from './not-found/not-found.component';
import { HomeComponent } from './home/home.component';
import { AuthService } from './services/auth-service';
import { NavbarComponent } from './navbar/navbar.component';
import { SubtaskService } from './services/subtask.service';
import { TaskService } from './services/task.service';
import { ErrorHandler } from '@angular/core';
import { AppErrorHandler } from './common/app-error-handler';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule} from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TaskComponent } from './task/task.component';
import { SubtaskComponent } from './subtask/subtask.component';
import { AdminComponent } from './admin/admin.component';
import { LoginComponent } from './login/login.component';
import { NoAccessComponent } from './no-access/no-access.component';
import { RouterModule } from '@angular/router';
import { JwtModule } from "@auth0/angular-jwt";
import { RegisterComponent } from './register/register.component';


@NgModule({
  declarations: [
    AppComponent,
    TaskComponent,
    SubtaskComponent,
    NavbarComponent,
    AdminComponent,
    LoginComponent,
    NoAccessComponent,
    HomeComponent,
    NotFoundComponent,
    RegisterComponent,
    

    
  
  ],
  imports: [
    BrowserModule,
    RouterModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,


  ],
  
  providers:
  [
    TaskService,
    SubtaskService,
    AuthService,
    UserService,
    {provide:ErrorHandler,useClass:AppErrorHandler},
    JwtHelperService,
    AuthGuard,
    AdminAuthGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
