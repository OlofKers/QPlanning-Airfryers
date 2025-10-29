import { Component } from '@angular/core';
import { AuthenticationService, RepositoryService } from './_services';
import { User } from './_models';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { GebruikerResetpasswordComponent } from './gebruikers/gebruiker-resetpassword';
import * as moment from 'moment';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'ZuydPlanning';
  username: string;
  isLoggedIn = false;
  isAdmin = false;
  isAtLeastPlanner = false;
  isManager = false;
  email: string;

  constructor(
    private authenticationService: AuthenticationService,
    private repoService: RepositoryService,
    private dialog: MatDialog
  ) {
    this.initializeApp();
  }

  private initializeApp(): void {
    moment.locale('nl');
    this.subscribeToUserChanges();
  }

  private subscribeToUserChanges(): void {
    this.authenticationService.currentUser.subscribe((currentUser: User) => {
      if (currentUser) {
        this.setUserData(currentUser);
      } else {
        this.resetUserState();
      }
    });
  }

  private setUserData(currentUser: User): void {
    this.username = `${currentUser.voornaam} ${currentUser.achternaam}`;
    this.email = currentUser.email;
    this.isLoggedIn = true;
    this.setUserRoles(currentUser.highestRole);
  }

  private setUserRoles(role: string): void {
    this.isAdmin = role === 'Admin';
    this.isManager = ['Planner', 'Manager', 'Admin'].includes(role);
    this.isAtLeastPlanner = ['Planner', 'Admin'].includes(role);
  }

  private resetUserState(): void {
    this.isLoggedIn = false;
    this.isAdmin = false;
    this.isManager = false;
    this.isAtLeastPlanner = false;
  }

  public redirectToResetPassword(): void {
    const dialogConfig: MatDialogConfig = {
      disableClose: true,
      autoFocus: true,
      data: {
        email: this.email,
        title: 'Wachtwoord resetten'
      }
    };

    const dialogRef = this.dialog.open(GebruikerResetpasswordComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(data => {
      if (data) {
        this.handlePasswordReset(data);
      }
    });
  }

  private handlePasswordReset(data: any): void {
    this.repoService.post('api/account/resetPassword', data)
      .subscribe({
        next: (res) => console.log('Password reset successful:', res),
        error: (err) => console.error('Password reset failed:', err)
      });
  }
}