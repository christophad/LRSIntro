import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MapComponent } from './map/map.component';
import { UserEditComponent } from './Users/user-edit/user-edit.component';
import { UserDetailComponent } from './Users/UserDetail/userDetail.component';
import { UsersComponent } from './Users/users.component';

const appRoutes: Routes = [
  { path: '', redirectTo: '/users', pathMatch: 'full' },
  {
    path: 'maps',
    component: MapComponent,
    data: { center: [22.97218, 40.61391], basemap: 'satellite', zoom: '18' },
  },
  {
    path: 'users',
    component: UsersComponent,
    children: [
      { path: 'new', component: UserEditComponent },
      {
        path: ':id',
        component: UserDetailComponent,
      },
      {
        path: ':id/edit',
        component: UserEditComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
