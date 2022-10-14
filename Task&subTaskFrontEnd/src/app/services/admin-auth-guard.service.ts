import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Injectable } from '@angular/core';
import { AuthService } from './auth-service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AdminAuthGuard implements CanActivate {

  constructor(
      private router:Router,
      private authService:AuthService
    ) { }


      canActivate() {

        if(this.authService.IsAdmin) return true;

        this.router.navigate(['/no-access']);
        return false;
        
      }         

}
