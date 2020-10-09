import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { IAlert } from '../shared/Entities/IAlert';
import { AlertService } from './alert.service';

@Component({
  selector: 'app-alert',
  templateUrl: './alert.component.html',
  styleUrls: ['./alert.component.css'],
})
export class AlertComponent implements OnInit {
  public show: boolean;
  message: string;

  constructor(private alertService: AlertService) {}

  ngOnInit(): void {
    this.alertService.alertStatus.subscribe((response: IAlert) => {
      this.show = response.status;
      this.message = response.message;
    });
  }

  onClose = () => {
    this.alertService.hideAlert();
  };
}
