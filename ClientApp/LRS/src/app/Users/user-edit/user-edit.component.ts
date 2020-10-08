import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { error } from 'protractor';
import { combineLatest, forkJoin, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AlertService } from 'src/app/alert/alert.service';
import { LoaderService } from 'src/app/loader/loader.service';
import { IUserEdit } from 'src/app/shared/Entities/IUserEdit';
import { IUserTitle } from 'src/app/shared/Entities/IUserTitle';
import { IUserType } from 'src/app/shared/Entities/IUserType';
import { UtilityServices } from 'src/app/shared/utils/utilityServices';
import { UserService } from '../user.service';

interface IUserEditModel {
  user: IUserEdit;
  titles: IUserTitle[];
  types: IUserType[];
}

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.css'],
})
export class UserEditComponent implements OnInit {
  id: number;
  editMode = false;
  userForm: FormGroup;
  userEditModel: IUserEditModel;
  user: IUserEdit;
  isFetching = false;
  error = null;

  constructor(
    private route: ActivatedRoute,
    private userService: UserService,
    private router: Router,
    private loaderService: LoaderService,
    private utilityServices: UtilityServices,
    private alertService: AlertService
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      this.isFetching = true;
      this.loaderService.showLoader();
      this.id = +params['id'];
      this.editMode = params['id'] != null;

      const userDataObs = this.userService.getUserDataForEdit(this.id);
      const userTitlesObs = this.userService.getUserTitles();
      const userTypesObs = this.userService.getUserTypes();
      let combinedResult: Observable<IUserEditModel>;

      if (this.editMode) {
        combinedResult = forkJoin([
          userDataObs,
          userTitlesObs,
          userTypesObs,
        ]).pipe(
          map(([x, y, z]) => {
            return <IUserEditModel>{
              user: x,
              titles: y,
              types: z,
            };
          })
        );
      } else {
        combinedResult = forkJoin([userTitlesObs, userTypesObs]).pipe(
          map(([y, z]) => {
            return <IUserEditModel>{
              user: {
                id: null,
                name: '',
                surname: '',
                birthDate: null,
                userType: null,
                userTitle: null,
                emailAddress: null,
                isActive: true,
              },
              titles: y,
              types: z,
            };
          })
        );
      }

      combinedResult.subscribe(
        (response) => {
          this.userEditModel = response;
          this.initForm(this.userEditModel);
          this.loaderService.hideLoader();
          this.isFetching = false;
        },
        (error) => {
          this.loaderService.hideLoader();
          this.initForm({
            user: {
              id: null,
              name: '',
              surname: '',
              birthDate: null,
              userType: null,
              userTitle: null,
              emailAddress: null,
              isActive: true,
            },
            titles: [],
            types: [],
          });
          this.alertService.showAlert(error.message);
          this.error = error.message;
          this.isFetching = false;
        }
      );
    });
  }

  private initForm(userModel: IUserEditModel) {
    this.userForm = new FormGroup({
      id: new FormControl(userModel.user.id),
      name: new FormControl(userModel.user.name),
      surname: new FormControl(userModel.user.surname),
      birthDate: new FormControl(
        this.utilityServices.formatDateForReactiveForm(
          userModel.user?.birthDate
        )
      ),
      email: new FormControl(userModel.user.emailAddress, Validators.email),
      userType: new FormControl(
        userModel.user.userType?.id,
        Validators.required
      ),
      userTitle: new FormControl(
        userModel.user.userTitle?.id,
        Validators.required
      ),
    });
  }

  onSubmit() {
    this.loaderService.showLoader();
    if (this.editMode) {
      this.userService
        .updateUser({
          id: this.userForm.value.id,
          name: this.userForm.value.name,
          surname: this.userForm.value.surname,
          birthDate: this.userForm.value.birthDate,
          userType: this.userForm.value.userType,
          userTitle: this.userForm.value.userTitle,
          emailAddress: this.userForm.value.email,
          isActive: true,
        })
        .subscribe(
          (response) => {
            this.loaderService.hideLoader();
            this.router.navigate(['../'], { relativeTo: this.route });
          },
          (error) => {
            console.log(error);
            this.loaderService.hideLoader();
            this.alertService.showAlert(error.message);
            this.error = error.message;
          }
        );
    } else {
      this.userService
        .addUser({
          name: this.userForm.value.name,
          surname: this.userForm.value.surname,
          birthDate: this.userForm.value.birthDate,
          userType: this.userForm.value.userType,
          userTitle: this.userForm.value.userTitle,
          emailAddress: this.userForm.value.email,
          isActive: true,
        })
        .subscribe(
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

  onCancel() {
    this.router.navigate(['../'], { relativeTo: this.route });
  }
}
