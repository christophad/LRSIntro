import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { fromEvent, Subscription } from 'rxjs';
import { debounceTime, distinctUntilChanged, filter, map } from 'rxjs/operators';
import { AlertService } from 'src/app/alert/alert.service';
import { LoaderService } from 'src/app/loader/loader.service';
import { IUser } from 'src/app/shared/Entities/IUser';
import { UserService } from '../user.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css'],
})
export class UserListComponent implements OnInit, OnDestroy {
  error = null;
  users: IUser[];
  subscription: Subscription;
  @ViewChild('searchInput', { static: true }) searchInput: ElementRef;

  constructor(
    private userService: UserService,
    private loaderService: LoaderService,
    private alertService: AlertService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.loaderService.showLoader();
    this.userService.fetchUsers().subscribe(
      (users: IUser[]) => {
        this.users = users;
        this.loaderService.hideLoader();
      },
      (error) => {
        this.loaderService.hideLoader();
        this.alertService.showAlert(error.message);
        this.error = error.message;
      }
    );

    this.subscription = this.userService.usersChanged.subscribe(
      (users: IUser[]) => {
        this.users = users;
      }
    );

    fromEvent(this.searchInput.nativeElement,'keyup').pipe(
      map((event: any) => {
        return event.target.value;
      }),debounceTime(500)
    ).subscribe((text: string) =>{
      this.loaderService.showLoader();
      this.userService.searchUsers(text).subscribe(
      (users: IUser[]) => {
        this.users = users;
        this.loaderService.hideLoader();
      },
      (error) => {
        this.loaderService.hideLoader();
        this.alertService.showAlert(error.message);
        this.error = error.message;
      }
    );
    })
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  onAddUser = () => {
    this.router.navigate(['new'], { relativeTo: this.route });
  };
}
