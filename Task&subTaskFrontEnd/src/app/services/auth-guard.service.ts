import { AuthService } from './auth-service';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Route, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(
    private router:Router,
    private authService:AuthService) { }
  canActivate(route:any,state:RouterStateSnapshot){
    if(this.authService.isLoggedIn()) return true;

    this.router.navigate(['/login'],{queryParams:{returnUrl:state.url}});
    return false;
  }
}
