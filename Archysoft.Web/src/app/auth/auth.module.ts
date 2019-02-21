import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthRoutingModule } from './auth-routing.module';
import { AuthComponent } from './auth.component';
import { AuthNotificationComponent } from './components/auth-notification/auth-notification.component';
import { LoginComponent } from './components/login/login.component';

import {
  AuthModule as AuthenticationModule,
  AUTH_SERVICE,
  PUBLIC_FALLBACK_PAGE_URI,
  PROTECTED_FALLBACK_PAGE_URI
 } from 'ngx-auth';
import { AuthService } from './services/auth.service';
import { SharedModule } from '../shared/shared.module';
import { TranslateModule } from '@ngx-translate/core';
import { TokenService } from './services/token.service';

 export function factory(authenticationService: AuthService) {
  return authenticationService;
}

@NgModule({
  declarations: [AuthComponent, AuthNotificationComponent, LoginComponent],
  imports: [
    CommonModule,
    AuthRoutingModule,
    SharedModule, 
    TranslateModule
  ],
  exports:[
    AuthenticationModule,    
  ],
  providers: [
    TokenService,
    AuthService,
    { provide: PROTECTED_FALLBACK_PAGE_URI, useValue: '/' },
    { provide: PUBLIC_FALLBACK_PAGE_URI, useValue: '/auth/login' },
    {
      provide: AUTH_SERVICE,
      deps: [ AuthService ],
      useFactory: factory
    }
  ]
})
export class AuthModule { }
