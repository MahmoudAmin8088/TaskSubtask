import { AuthService } from './auth-service';
import { DataService } from './data.service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';



@Injectable()
export class TaskService extends DataService {

  constructor(HttpClient:HttpClient ,Auth:AuthService) {
    super('https://localhost:7204/api/Tasks',HttpClient,Auth);
   }

}
