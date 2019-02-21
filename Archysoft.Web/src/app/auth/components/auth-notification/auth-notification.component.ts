import { Component, OnInit, Output } from '@angular/core';
import { AuthNotificationService } from '../../services/auth-notification.service';
import { AuthNotice } from '../../models/auth-notice.model';

@Component({
  selector: 'auth-notification',
  templateUrl: './auth-notification.component.html',
  styleUrls: ['./auth-notification.component.scss']
})
export class AuthNotificationComponent implements OnInit {

  @Output() message:any='';
  @Output() type: any;

  constructor(private authNotificationService:AuthNotificationService) { }

  ngOnInit() {
    this.authNotificationService.onNoticeChanged$.subscribe((notice:AuthNotice)=>{
      this.message=notice?notice.message:'';
      this.type=notice?notice.type:null;
    })
  }

}
