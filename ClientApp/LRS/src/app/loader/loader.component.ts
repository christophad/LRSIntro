import { Component, Input, OnInit } from '@angular/core';
import { ILoader } from '../shared/Entities/ILoader';
import { LoaderService } from './loader.service';

@Component({
  selector: 'app-loader',
  templateUrl: './loader.component.html',
  styleUrls: ['./loader.component.css'],
})
export class LoaderComponent implements OnInit {
  public show: boolean;

  constructor(private loaderService: LoaderService) {}

  ngOnInit(): void {
    this.loaderService.loaderStatus.subscribe((response: ILoader) => {
      this.show = response.status;
    });
  }
}
