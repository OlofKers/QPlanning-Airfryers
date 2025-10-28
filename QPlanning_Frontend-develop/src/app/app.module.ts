import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

// Material Imports
import { MatDialogModule } from '@angular/material/dialog';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { MAT_DATE_LOCALE } from '@angular/material/core';
import { MAT_MOMENT_DATE_ADAPTER_OPTIONS } from '@angular/material-moment-adapter';

// Core Imports
import { CustomMaterialModule } from './core/material.module';
import { routing } from './core/app.routing';
import { JwtInterceptor, ErrorInterceptor } from './_helpers';

// Component Imports
import { AppComponent } from './app.component';
import { LoginComponent } from './login';
import { HomeComponent } from './home';
import { PlanningListComponent } from './home/planning-list';
import { NonBillableListComponent } from './home/non-billable-list';

// Gebruiker Components
import { GebruikerResetpasswordComponent } from './gebruikers/gebruiker-resetpassword';
import { GebruikerOverzichtComponent } from './gebruikers/gebruiker-overzicht';
import { GebruikerDetailsComponent } from './gebruikers/gebruiker-details';
import { GebruikerRolesComponent } from './gebruikers/gebruiker-roles';

// Boeking Components
import { BoekingenOverzichtComponent } from './boeking/boekingen-overzicht';
import { BoekingDetailsComponent } from './boeking/boeking-details/boeking-details.component';
import { MedewerkerPlanningComponent } from './boeking/medewerker-planning/medewerker-planning.component';
import { KlantPlanningComponent } from './boeking/klant-planning/klant-planning.component';

// Medewerker Components
import { MedewerkerDetailsComponent } from './medewerker/medewerker-details/medewerker-details.component';
import { MedewerkerOverzichtComponent } from './medewerker/medewerker-overzicht/medewerker-overzicht.component';

// Klant Components
import { KlantOverzichtComponent } from './klant/klant-overzicht/klant-overzicht.component';
import { KlantDetailsComponent } from './klant/klant-details/klant-details.component';

// Other Components
import { ConfirmationComponent } from './modal';
import { BoekjaarDetailsComponent } from './boekjaar/boekjaar-details/boekjaar-details.component';

const COMPONENTS = [
  AppComponent,
  LoginComponent,
  HomeComponent,
  GebruikerResetpasswordComponent,
  GebruikerOverzichtComponent,
  GebruikerDetailsComponent,
  PlanningListComponent,
  NonBillableListComponent,
  BoekingenOverzichtComponent,
  BoekingDetailsComponent,
  ConfirmationComponent,
  GebruikerRolesComponent,
  MedewerkerDetailsComponent,
  MedewerkerOverzichtComponent,
  KlantOverzichtComponent,
  KlantDetailsComponent,
  MedewerkerPlanningComponent,
  KlantPlanningComponent,
  BoekjaarDetailsComponent
];

const ENTRY_COMPONENTS = [
  GebruikerResetpasswordComponent,
  GebruikerDetailsComponent,
  GebruikerRolesComponent,
  BoekingDetailsComponent,
  ConfirmationComponent,
  MedewerkerDetailsComponent,
  KlantDetailsComponent,
  BoekjaarDetailsComponent
];

const MATERIAL_MODULES = [
  MatDialogModule,
  MatToolbarModule,
  MatButtonModule,
  MatMenuModule,
  MatIconModule,
  MatDividerModule
];

@NgModule({
  declarations: [...COMPONENTS],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    CustomMaterialModule,
    ...MATERIAL_MODULES,
    routing
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: MAT_MOMENT_DATE_ADAPTER_OPTIONS, useValue: { useUtc: true }},
    { provide: MAT_DATE_LOCALE, useValue: 'nl-NL' }
  ],
  bootstrap: [AppComponent],
  entryComponents: [...ENTRY_COMPONENTS]
})
export class AppModule { }