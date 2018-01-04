import { Component, OnInit } from '@angular/core';
import { RetetaService } from "../../services/reteta.service";
import { ActivatedRoute, Router } from "@angular/router";
import { ConsultatieService } from "../../services/consultatie.service";

@Component({
  selector: 'reteta',
  templateUrl: './reteta.component.html',
  styleUrls: ['./reteta.component.css']
})
export class RetetaComponent implements OnInit {

    reteta: any = {};
    consultatie: any = {};


    constructor(private retetaService: RetetaService, private consultatieService: ConsultatieService, private route: ActivatedRoute,
        private router: Router) {
        route.params.subscribe(p => {
            if (p.id != null)
                this.reteta.id = +p['id'];
            if (p.consultatieID != null)
                this.reteta.consultatieID = +p['consultatieID'];

        });

    }

    ngOnInit() {
        this.consultatieService.getConsultatie(this.reteta.consultatieID).subscribe(x => {
            this.consultatie = x;

        });

        if (this.reteta.id)
            this.retetaService.getReteta(this.reteta.id).subscribe(x => {
                this.reteta = x;

                this.consultatieService.getConsultatie(this.reteta.consultatieID).subscribe(x => {
                    this.consultatie = x;

                });

            });

    }

    submit() {
        if (this.reteta.id) {
            this.retetaService.updateRetete(this.reteta.id, this.reteta)
                .subscribe(
                () => {

                    this.router.navigate(['/medic/details/' + this.consultatie.medicID]);

                },
                err => {
                    console.log(err);
                }
                );



        }
        else {
            this.retetaService.createRetete(this.reteta)
                .subscribe(
                () => {

                    this.router.navigate(['/medic/details/' + this.consultatie.medicID]);
                },
                err => {
                    console.log(err);
                }
                );

        }
    }
}
