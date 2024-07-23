import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserServiceService } from '../services/user-service.service';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  constructor(private userService:UserServiceService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const token=localStorage.getItem('token');
    if(token){
      request=request.clone({
        setHeaders:{Authorization:`Bearer ${token}`}
      })
    }
    return next.handle(request);
  }
}
