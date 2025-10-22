import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormGroup} from '@angular/forms';
import {DropDown} from '../../_models';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material';
import {RepositoryService} from '../../_services';
import * as moment from 'moment';
import {Moment} from 'moment';
import DateTimeFormat = Intl.DateTimeFormat;

@Component({
  selector: 'app-klant-details',
  templateUrl: './klant-details.component.html',
  styleUrls: ['./klant-details.component.css']
})
export class KlantDetailsComponent implements OnInit {
  form: FormGroup;
  title: string;
  saveButtonText: string;

  //added variables for seperate name fields
  voornaam: string;
  achternaam:string;

  naam: string;
  startdatum: string;
  einddatum: string;
  verantwoordelijkTeamId: number;
  boekjaar: number;
  budget: number;
  planbaarDoorTeamIds: [];
  medewerkerId: number;
  teamDropDown: DropDown[];
  medewerkersDropDown: DropDown[];
  isEdit: boolean;


  constructor(private fb: FormBuilder,
              private dialogRef: MatDialogRef<KlantDetailsComponent>,
              private repoService: RepositoryService,
              @Inject(MAT_DIALOG_DATA) data) {
    this.title = data.title;
    this.saveButtonText = data.saveButtonText;
    this.teamDropDown = data.teamDropDown;
    this.medewerkersDropDown = data.medewerkersDropDown;
    this.medewerkerId = data.medewerkerId;

    //added variables for seperate name fields
    this.voornaam = data.voornaam;
    this.achternaam = data.achternaam;

    this.naam = data.naam;
    this.startdatum = data.startdatum;
    this.einddatum = data.einddatum;
    this.verantwoordelijkTeamId = data.verantwoordelijkTeamId;
    this.boekjaar = moment().utc(true).year();
    this.budget = data.budget;
    this.planbaarDoorTeamIds = data.planbaarDoorTeamIds;
    if (data.medewerkerId) {
      this.isEdit = true;
    }
  }

  ngOnInit() {
    this.form = this.fb.group({
      budget: [this.budget, []],
      boekjaar: [this.boekjaar, []],
      medewerkerId: [this.medewerkerId, []],

      //added variables for seperate name fields
      voornaam: [this.voornaam, []],
      achternaam: [this.achternaam, []],

      startdatum: [this.startdatum, []],
      einddatum: [this.einddatum, []],
      verantwoordelijkTeamId: [this.verantwoordelijkTeamId, []],
      planbaarDoorTeamIds: [this.planbaarDoorTeamIds, []]
    });
  }

  // merge seperate name field into one
  save() {
  const formValue = this.form.getRawValue();
  formValue.naam = `${formValue.voornaam || ''} ${formValue.achternaam || ''}`.trim();
  this.dialogRef.close(formValue);    
  }

  close() {
    this.dialogRef.close();
  }

}
