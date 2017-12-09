import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import { ConsultatieService } from "../../../services/consultatie.service";

@Component({
  selector: 'medic-details',
  templateUrl: './medic-details.component.html',
  styleUrls: ['./medic-details.component.css']
})
export class MedicDetailsComponent implements OnInit {

    medic: any = {};
    consultatiiVechi: any;
    consultatiiNoi: any;


    constructor(/*private proprietarService: ProprietarService,*/ private consultatieService: ConsultatieService, private route: ActivatedRoute,
        private router: Router) {

        route.params.subscribe(p => {
            if (p.id != null)
                this.medic.id = +p['id'];

        });

    }

    ngOnInit() {
        this.populateConsultatii();
        //this.proprietarService.getProprietar(this.proprietar.id).subscribe(x => {
        //    this.proprietar = x;

        //});

    }

    populateConsultatii() {
        this.consultatieService.getConsultatiiNoiMedic(this.medic.id).subscribe(x => {
            this.consultatiiNoi = x;

            console.log(this.medic.id);
            console.log(x);
        });
        this.consultatieService.getConsultatiiVechiMedic(this.medic.id).subscribe(x => {
            this.consultatiiVechi = x;

            console.log(this.medic.id);
            console.log(x);
        });
    }

    deleteConsultatie(id: number) {

        this.consultatieService.deleteConsultatie(id)
            .subscribe(
            () => {
                this.populateConsultatii();
            });

    }

}
