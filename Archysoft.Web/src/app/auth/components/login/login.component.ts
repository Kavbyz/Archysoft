import { Component, OnInit } from '@angular/core';
import { LoginModel } from '../../models/login.model';
import { TranslateService } from '@ngx-translate/core';
import { AuthService } from '../../services/auth.service';
import { AuthNotificationService } from '../../services/auth-notification.service';
import { Router } from '@angular/router';
import { ApiResponse } from 'src/app/shared/models/api-response.model';
import { TokenModel } from '../../models/token.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  model:LoginModel={login:'admin@d1.archysoft.com', password:'admin', rememberMe:false};
  loading=false;

  constructor(
    private translateService:TranslateService,
    private authService:AuthService,
    private authNotificationService:AuthNotificationService,
    private router:Router
  ) { }

  ngOnInit() {
  }

  login(){
    this.loading=true;
    this.authService.login(this.model).subscribe((response:ApiResponse<TokenModel>)=>{
      this.loading=false;
      if(response&&response.status===1){
        this.router.navigate(['/']);
      }
      else{
        this.authNotificationService.notify(this.translateService.instant('AUTH.INVALID_LOGIN_OR_PASSWORD'), 'error');
      }
    },
    (error:any)=>{
      this.loading=false;
      this.authNotificationService.notify(this.translateService.instant('AUTH.SERVER_ERROR'), 'error');
    })
  }

}
