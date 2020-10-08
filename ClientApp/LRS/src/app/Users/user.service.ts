import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subject, throwError } from 'rxjs';
import { IUser } from '../shared/Entities/IUser';
import { map, catchError, tap } from 'rxjs/operators';
import { IUserEdit } from '../shared/Entities/IUserEdit';
import { environment } from 'src/environments/environment';
import { IUserTitle } from '../shared/Entities/IUserTitle';
import { IUserType } from '../shared/Entities/IUserType';
import { IUserAddOrUpdate } from '../shared/Entities/IUserAddOrUpdate';
import { debug } from 'console';

@Injectable()
export class UserService {
  constructor(private http: HttpClient) {}

  private users: IUser[] = [];
  usersChanged = new Subject<IUser[]>();
  error = new Subject<string>();

  getUsers = () => {
    return this.users.slice();
  };

  setUsers = (users: IUser[]) => {
    this.users = users;
    this.usersChanged.next(this.users.slice());
  };

  fetchUsers = () => {
    return this.http.get<IUser[]>(environment.apiUrl + 'User/GetUsers').pipe(
      map((response) => {
        this.users = response;
        return this.users.slice();
      }),
      catchError((errorRes) => {
        return throwError(errorRes);
      })
    );
  };

  getUser = (id: number) => {
    let params = new HttpParams().set('id', id?.toString());
    return this.http
      .get<IUser>(environment.apiUrl + 'User/GetUserById', {
        params: params,
      })
      .pipe(
        map((response) => {
          return response;
        }),
        catchError((errorRes) => {
          return throwError(errorRes);
        })
      );
  };

  getUserDataForEdit = (id: number) => {
    let params = new HttpParams().set('id', id?.toString());
    return this.http
      .get<IUserEdit>(environment.apiUrl + 'User/GetUserEditById', {
        params: params,
      })
      .pipe(
        map((response) => {
          return response;
        }),
        catchError((errorRes) => {
          return throwError(errorRes);
        })
      );
  };

  getUserTitles = () => {
    return this.http
      .get<IUserTitle[]>(environment.apiUrl + 'User/GetUserTitles')
      .pipe(
        map((response) => {
          return response;
        }),
        catchError((errorRes) => {
          return throwError(errorRes);
        })
      );
  };

  getUserTypes = () => {
    return this.http
      .get<IUserType[]>(environment.apiUrl + 'User/GetUserTypes')
      .pipe(
        map((response) => {
          return response;
        }),
        catchError((errorRes) => {
          return throwError(errorRes);
        })
      );
  };

  updateUser = (user: IUserAddOrUpdate) => {
    return this.http
      .put<IUser>(environment.apiUrl + 'User/UpdateUser/', user)
      .pipe(
        map((response) => {
          let index = this.users.findIndex((x) => x.id === response.id);
          this.users[index] = response;
          this.usersChanged.next(this.users.slice());
          return response;
        }),
        catchError((errorRes) => {
          return throwError(errorRes);
        })
      );
  };

  addUser = (user: IUserAddOrUpdate) => {
    return this.http
      .post<IUser>(environment.apiUrl + 'User/AddUser/', user)
      .pipe(
        map((response) => {
          this.users.push(response);
          this.usersChanged.next(this.users.slice());
          return response;
        }),
        catchError((errorRes) => {
          return throwError(errorRes);
        })
      );
  };

  deleteUser = (id: number) => {
    let params = new HttpParams().set('id', id?.toString());
    return this.http
      .delete(environment.apiUrl + 'User/DeleteUser/', { params: params })
      .pipe(
        map((response) => {
          let index = this.users.findIndex((x) => x.id === id);
          this.users.splice(index, 1);
          this.usersChanged.next(this.users.slice());
          return response;
        }),
        catchError((errorRes) => {
          return throwError(errorRes);
        })
      );
  };
}
