import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { LocaleModel } from '../models/locale.model';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TranslationService {

  private languages:any[]=[];

  constructor(private translateService:TranslateService) {
    this.translateService.addLangs(['en']);
    this.translateService.setDefaultLang('en');
   }

   loadTranslations(...args:LocaleModel[]){
    const locales=[...args];

    locales.forEach(locale=>{
      this.translateService.setTranslation(locale.language, locale.data, true);
      this.languages.push(locale.language);
    });

    this.translateService.addLangs(this.languages);
   }

   setLanguage(lang){
     if(lang){
       this.translateService.use(this.translateService.getDefaultLang());
       this.translateService.use(lang);
       localStorage.setItem('language', lang);
     }
   }

   getSelectedLanguage():Observable<any>{
     return of(localStorage.getItem('language || this.translateService.getDefaultLang()'));
   }
}
