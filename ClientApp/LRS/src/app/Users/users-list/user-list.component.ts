import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AlertService } from 'src/app/alert/alert.service';
import { LoaderService } from 'src/app/loader/loader.service';
import { IUser } from 'src/app/shared/Entities/IUser';
import { UtilityServices } from 'src/app/shared/utils/utilityServices';
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

  constructor(
    private userService: UserService,
    private loaderService: LoaderService,
    public utilityServices: UtilityServices,
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
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  // TODO not used
  onHandleError() {
    this.error = null;
  }

  onAddUser = () => {
    this.router.navigate(['new'], { relativeTo: this.route });
  };
}
