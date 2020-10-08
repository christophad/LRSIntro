import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { AlertService } from 'src/app/alert/alert.service';
import { LoaderService } from 'src/app/loader/loader.service';
import { IUser } from 'src/app/shared/Entities/IUser';
import { UtilityServices } from 'src/app/shared/utils/utilityServices';
import { UserService } from '../user.service';

@Component({
  selector: 'app-user-detail',
  templateUrl: './userDetail.component.html',
  styleUrls: ['./userDetail.component.css'],
})
export class UserDetailComponent implements OnInit {
  user: IUser;
  id: number;
  isFetching = false;
  error = null;

  constructor(
    private userService: UserService,
    private route: ActivatedRoute,
    private router: Router,
    private loaderService: LoaderService,
    public utilityServices: UtilityServices,
    private alertService: AlertService
  ) {}

  ngOnInit() {
    this.route.params.subscribe((params: Params) => {
      this.loaderService.showLoader();
      this.id = +params['id'];
      this.userService.getUser(this.id).subscribe(
        (response) => {
          this.user = response;
          this.loaderService.hideLoader();
        },
        (error) => {
          this.loaderService.hideLoader();
          this.alertService.showAlert(error.message);
          this.error = error.message;
        }
      );
    });
  }

  onEditUser() {
    this.router.navigate(['edit'], { relativeTo: this.route });
  }

  onDelete() {
    this.loaderService.showLoader();
    this.userService.deleteUser(this.id).subscribe(
      (response) => {
        this.loaderService.hideLoader();
        this.router.navigate(['../'], { relativeTo: this.route });
      },
      (error) => {
        this.loaderService.hideLoader();
        this.alertService.showAlert(error.message);
        this.error = error.message;
      }
    );
  }
}
