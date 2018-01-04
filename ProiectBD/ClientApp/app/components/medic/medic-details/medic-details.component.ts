import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import { ConsultatieService } from "../../../services/consultatie.service";
import { InterventieService } from "../../../services/interventie.service";
import { RetetaService } from "../../../services/reteta.service";


@Component({
  selector: 'medic-details',
  templateUrl: './medic-details.component.html',
  styleUrls: ['./medic-details.component.css']
})
export class MedicDetailsComponent implements OnInit {

    medic: any = {};
    consultatiiVechi: any;
    consultatiiNoi: any;
    interventiiVechi: any;
    interventiiNoi: any;
    retete: any;

    constructor(private interventieService: InterventieService, private consultatieService: ConsultatieService, private retetaService: RetetaService, private route: ActivatedRoute,
        private router: Router) {

        route.params.subscribe(p => {
            if (p.id != null)
                this.medic.id = +p['id'];

        });

    }

    ngOnInit() {
        this.populateConsultatii();
        this.populateInterventii();
        this.populateRetete();

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

    populateInterventii() {
        this.interventieService.getInterventiiNoiMedic(this.medic.id).subscribe(x => {
            this.interventiiNoi = x;

            console.log(this.medic.id);
            console.log(x);
        });
        this.interventieService.getInterventiiVechiMedic(this.medic.id).subscribe(x => {
            this.interventiiVechi = x;

            console.log(this.medic.id);
            console.log(x);
        });
    }

    populateRetete() {
        this.retetaService.getReteteMedic(this.medic.id).subscribe(x => {
            this.retete = x;

            console.log(this.medic.id);
            console.log(x);
        });
    }

    deleteConsultatie(id: number) {

        this.consultatieService.deleteConsultatie(id)
            .subscribe(
            () => {
                this.populateConsultatii();
                this.populateInterventii();
                this.populateRetete();
            });

    }

    deleteInterventie(id: number) {

        this.interventieService.deleteInterventie(id)
            .subscribe(
            () => {
                this.populateInterventii();
            });

    }

    deleteReteta(id: number) {

        this.retetaService.deleteRetete(id)
            .subscribe(
            () => {
                this.populateRetete();
            });

    }


}
