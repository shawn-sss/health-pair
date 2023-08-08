import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable } from 'rxjs';

import { AuthenticationService } from '../_services';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  constructor(private authenticationService: AuthenticationService) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    // add authorization header with jwt token if available
    let currentPatient = this.authenticationService.CurrentPatientValue;
    if (currentPatient && currentPatient.token) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${currentPatient.token}`,
        },
      });
    }

    return next.handle(request);
  }
}
