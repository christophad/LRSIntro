import { DatePipe } from '@angular/common';
import { Injectable } from '@angular/core';

// TODO use pipes
@Injectable()
export class UtilityServices {
  constructor(private datePipe: DatePipe) {}

  public formatDate = (date: Date) => {
    return this.datePipe.transform(date, 'dd-MM-yyyy');
  };

  public formatDateForReactiveForm = (date: Date) => {
    return this.datePipe.transform(date, 'yyyy-MM-dd');
  };
}
