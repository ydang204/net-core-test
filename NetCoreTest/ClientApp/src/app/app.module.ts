import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { AlertComponent } from './_components';
import { NgxLoadingModule } from 'ngx-loading';



const authModule = () => import('./auth/auth.module').then(x => x.AuthModule);
@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    AlertComponent,
    HomeComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    NgxLoadingModule.forRoot({}),
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'auth', loadChildren: authModule },
    ]),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
