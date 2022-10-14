import { RegisterComponent } from './register/register.component';
import { AdminAuthGuard } from './services/admin-auth-guard.service';
import { AuthGuard } from './services/auth-guard.service';
import { NoAccessComponent } from './no-access/no-access.component';
import { LoginComponent } from './login/login.component';
import { AdminComponent } from './admin/admin.component';
import { SubtaskComponent } from './subtask/subtask.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { TaskComponent } from './task/task.component';
import { HomeComponent } from './home/home.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [];

@NgModule({
  imports: [RouterModule.forRoot([
    {
      path:'',
      component:HomeComponent
    },
    {
      path:'login',
      component:LoginComponent
    },
    { path:'home',
       component:HomeComponent
    },
    {
      path:'admin',
      component:AdminComponent,
      canActivate:[AuthGuard,AdminAuthGuard]
    },
    {
      path:'tasks',
      component:TaskComponent,
      canActivate:[AuthGuard]

    },
    {
      path:'subtasks/:taskId',
      component:SubtaskComponent,
      canActivate:[AuthGuard]

    },
    {
      path:'register',
      component:RegisterComponent
    },
    {
      path:'no-access',
      component:NoAccessComponent
    },
    {
      path:'**',
      component:NotFoundComponent
    }

  ])],
  exports: [RouterModule]
})
export class AppRoutingModule { }
