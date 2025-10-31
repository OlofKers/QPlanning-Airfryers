import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { Moment } from 'moment';
import * as moment from 'moment';
import { AuthenticationService, RepositoryService } from '../../_services';
import { PersonalBooking, User } from '../../_models';

@Component({
  selector: 'app-planning-list',
  templateUrl: './planning-list.component.html',
  styleUrls: ['./planning-list.component.css']
})
export class PlanningListComponent implements OnInit, AfterViewInit {
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;

  currentUser: User;
  startDate: Moment;
  endDate: Moment;

  isLoading = false;
  hasError = false;
  areAllCollapsed = true;

  personalPlanningViewModel: any;

  constructor(
    private repoService: RepositoryService,
    private authenticationService: AuthenticationService
  ) {
    this.authenticationService.currentUser.subscribe(
      (x) => (this.currentUser = x)
    );
    this.startDate = moment().startOf('week');
    this.endDate = moment(this.startDate).add(3, 'month').startOf('week');
  }

  ngOnInit(): void {
    this.getMyCurrentPlanning();
  }

  ngAfterViewInit(): void {}

  public getMyCurrentPlanning(): void {
    if (this.isLoading) return;

    this.isLoading = true;
    this.hasError = false;

    this.repoService
      .post('api/boeking/getPersonalBoekingWithinPeriod', {
        startDate: this.startDate,
        endDate: this.endDate,
      })
      .subscribe({
        next: (res: any) => {
          this.personalPlanningViewModel = res.personalPlanningViewModel;
          this.isLoading = false;
        },
        error: () => {
          this.hasError = true;
          this.isLoading = false;
        },
      });
  }

  public collapseAll(): void {
    if (!this.personalPlanningViewModel) return;

    const expand = this.areAllCollapsed;
    this.personalPlanningViewModel.topRows.forEach(
      (klant: any) => (klant.expanded = expand)
    );
    this.areAllCollapsed = !expand;
  }

  public toggleExpand(klant: any): void {
    klant.expanded = !klant.expanded;
  }
}
