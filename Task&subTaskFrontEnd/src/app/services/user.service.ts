import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrI='https://localhost:7204/api/Users';


  constructor(private httpClient:HttpClient) { }

  GetEmployees():Observable<any[]>{
    return this.httpClient.get<any[]>(this.baseUrI);
  }

  GetEmployee(id:string):Observable<any>{
    return this.httpClient.get<any>(this.baseUrI+'/'+id);
  }
  DeleteEmployee(id:string){

    return this.httpClient.delete(this.baseUrI+'/'+id);

  }
}
