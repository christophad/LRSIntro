import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { HttpClientModule } from '@angular/common/http';
import { UsersComponent } from './Users/users.component';
import { UserListComponent } from './Users/users-list/user-list.component';
import { UserService } from './Users/user.service';
import { AlertComponent } from './alert/alert.component';
import { UserDetailComponent } from './Users/UserDetail/userDetail.component';
import { LoaderComponent } from './loader/loader.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoaderService } from './loader/loader.service';
import { DatePipe } from '@angular/common';
import { UserEditComponent } from './Users/user-edit/user-edit.component';
import { AlertService } from './alert/alert.service';
import { MapComponent } from './map/map.component';
import { FilterPipe } from './pipes/filter.pipe';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    UsersComponent,
    UserListComponent,
    UserDetailComponent,
    AlertComponent,
    LoaderComponent,
    UserEditComponent,
    MapComponent,
    FilterPipe,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  providers: [UserService, LoaderService, DatePipe, AlertService],
  bootstrap: [AppComponent],
})
export class AppModule {}
