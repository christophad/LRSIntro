import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subject, throwError } from 'rxjs';
import { IUser } from '../shared/Entities/IUser';
import { catchError, map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IUserTitle } from '../shared/Entities/IUserTitle';
import { IUserType } from '../shared/Entities/IUserType';
import { IUserAddOrUpdate } from '../shared/Entities/IUserAddOrUpdate';

@Injectable()
export class UserService {
  constructor(private http: HttpClient) {}

  private users: IUser[] = [];
  usersChanged = new Subject<IUser[]>();
  error = new Subject<string>();

  setUsers = (users: IUser[]) => {
    this.users = users;
    this.usersChanged.next(this.users.slice());
  };

  fetchUsers = () => {
    return this.http.get<IUser[]>(environment.apiUrl + 'User').pipe(
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
    return this.http.get<IUser>(environment.apiUrl + 'User/' + id).pipe(
      map((response) => {
        return response;
      }),
      catchError((errorRes) => {
        return throwError(errorRes);
      })
    );
  };

  getUserTitles = () => {
    return this.http.get<IUserTitle[]>(environment.apiUrl + 'User/Titles').pipe(
      map((response) => {
        return response;
      }),
      catchError((errorRes) => {
        return throwError(errorRes);
      })
    );
  };

  getUserTypes = () => {
    return this.http.get<IUserType[]>(environment.apiUrl + 'User/Types').pipe(
      map((response) => {
        return response;
      }),
      catchError((errorRes) => {
        return throwError(errorRes);
      })
    );
  };

  updateUser = (user: IUserAddOrUpdate) => {
    return this.http.put<IUser>(environment.apiUrl + 'User', user).pipe(
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
    debugger;
    return this.http.post<IUser>(environment.apiUrl + 'User', user).pipe(
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
      .delete(environment.apiUrl + 'User', { params: params })
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

  searchUsers = (searchTerm: string) => {
    let params = new HttpParams().set('searchTerm', searchTerm?.toString());
    return this.http.get<IUser[]>(environment.apiUrl + 'User/Search', {params: params}).pipe(
      map((response) => {
        this.users = response;
        return this.users.slice();
      }),
      catchError((errorRes) => {
        return throwError(errorRes);
      })
    );
  };

}
