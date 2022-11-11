import { AuthService } from './auth-service';
import { NotFoundError } from '../common/not-found-error';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError,map } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { AppError } from '../common/app-error';
import { BadInput } from '../common/bad-input';



@Injectable({
  providedIn: 'root'
})
export class DataService {
  baseURl='https://localhost:7204/api/Prioritys';

 
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
       Authorization: `Bearer ${this.Auth.tokens}`
    })
  }

  constructor(@Inject(String) private baseURL:string,private HttpClient:HttpClient,private Auth:AuthService) {
   }

   GetAll():Observable<any[]>{
    return this.HttpClient.get<any[]>(this.baseURL,this.httpOptions);
    }
    GetPriority():Observable<any[]>{
      return this.HttpClient.get<any[]>(this.baseURl);
      }

    GetSubTaskOfTask(taskId:string):Observable<any[]>{
      return this.HttpClient.get<any[]>(this.baseURL +'/GetSubTasksOfTask/' + taskId,this.httpOptions).pipe(
        catchError(this.handleError)
      );
      }
   

   create(resource:any): Observable<any>{
    
    return this.HttpClient.post<any>(this.baseURL + '/Create',JSON.stringify(resource),this.httpOptions)
    .pipe(
      catchError(this.handleError)
    );
   }


   update(resource:any):Observable<any>{
    return this.HttpClient.put<any>(this.baseURL +'/Update',JSON.stringify(resource),this.httpOptions).pipe(
      catchError(this.handleError)
    );
   }


   delete(id:string){
    return this.HttpClient.delete<any>(this.baseURL +'/'+ id,this.httpOptions).pipe(
        catchError(this.handleError)

    );
    
  }
  get(id:string): Observable<any> {
    return this.HttpClient.get<any>(this.baseURL +'/'+ id);
    
  }


  private handleError(error:Response){
    if(error.status === 400)
      return throwError(new BadInput(error.json()));
    if(error.status === 404)
      return throwError(new NotFoundError());
    return throwError(new AppError(error.json())); 
  }
}
