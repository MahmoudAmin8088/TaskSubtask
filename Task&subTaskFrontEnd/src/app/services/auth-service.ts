import { catchError, map, Observable, throwError } from 'rxjs';
import { HttpClient, HttpHeaders, HttpParams,HttpClientModule } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from "@auth0/angular-jwt";
import { BadInput } from '../common/bad-input';
import { AppError } from '../common/app-error';
import { NotFoundError } from '../common/not-found-error';


@Injectable({
  providedIn: 'root'
})
export class AuthService  {
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
       Authorization: `Bearer ${this.tokens}`

    })
  }
  //token:any;
  baseurl='https://localhost:7204/api/Auths';
  constructor(private HttpClient:HttpClient) { }

  login(credentials:any) { 
    return this.HttpClient.post(this.baseurl +'/login', 
       JSON.stringify(credentials),this.httpOptions).pipe(
        map(response=>{
          let result :any=response;
          if(result && result.token){
            localStorage.setItem('token',result.token);
            return true;
          }
          return false;
      }
    )
    );
   }
   signUp(credentials:any){
    return this.HttpClient.post(this.baseurl +'/register', 
    JSON.stringify(credentials),this.httpOptions).pipe(
      map(response=>{
        let result :any=response;
        if(result)
          return true;
        
        return false;
      }
      )
    );
   }

   MakeAdmin(credentials:any):Observable<any>{
    return this.HttpClient.post<any>(this.baseurl +'/AddAdmin',
     JSON.stringify(credentials),this.httpOptions)
   }
   

MakeEmployee(credentials:any):Observable<any>{
  return this.HttpClient.post<any>(this.baseurl +'/DeleteAdmin',
  JSON.stringify(credentials),this.httpOptions)
}
   
 
   logout() { 

    localStorage.removeItem('token');
   }
 
   isLoggedIn() { 
    let jwtHelper = new JwtHelperService();

     let token = localStorage.getItem('token');
     if(!token)
       return false;
     let expiratioDate =jwtHelper.getTokenExpirationDate(token!);
     let isExpird =jwtHelper.isTokenExpired(token!);
      return !isExpird;
   }
   get tokens(){
    let token = localStorage.getItem('token');
    return token;
   }
   get currentUser(){
    
    let token = localStorage.getItem('token');
    //if(!token) return null;
    
   return new JwtHelperService().decodeToken(token!);

   }

   get IsAdmin(){

    //  let token = localStorage.getItem('token');
    // let dec =  new JwtHelperService().decodeToken(token!);
    let dec = this.currentUser;
    
    var role = dec["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
    if(role ==='Admin')
    return true;
    else
    return false;
    
    
    

   }

  

    
   }


