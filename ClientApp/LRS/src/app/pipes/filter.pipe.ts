import { Pipe, PipeTransform } from '@angular/core';
import { IUser } from '../shared/Entities/IUser';

@Pipe({
  name: 'filter',
  pure: false,
})
export class FilterPipe implements PipeTransform {
  transform(users: IUser[], filterString: string): any {
    if (users?.length === 0 || filterString === '') {
      return users;
    }
    const resultArray = [];
    for (const user of users) {
      if (
        user.name.toLowerCase().indexOf(filterString) !== -1 ||
        user.surname.toLowerCase().indexOf(filterString) !== -1
      ) {
        resultArray.push(user);
      }
    }
    return resultArray;
  }
}
