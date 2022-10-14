import { AuthService } from './auth-service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DataService } from './data.service';

@Injectable({
  providedIn: 'root'
})
export class SubtaskService extends DataService{

  constructor(HttpClient:HttpClient, Auth:AuthService) {

    super('https://localhost:7204/api/Subtasks',HttpClient,Auth);
   }


   

}
