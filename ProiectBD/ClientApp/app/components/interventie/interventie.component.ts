import { Component, OnInit } from '@angular/core';
import { InterventieService } from "../../services/interventie.service";
import { ActivatedRoute, Router } from "@angular/router";
import { ConsultatieService } from "../../services/consultatie.service";

@Component({
  selector: 'interventie',
  templateUrl: './interventie.component.html',
  styleUrls: ['./interventie.component.css']
})
export class InterventieComponent implements OnInit {

    interventie: any = {};
    consultatie: any = {};


    constructor(private interventieService: InterventieService, private consultatieService: ConsultatieService, private route: ActivatedRoute,
        private router: Router ) {
        route.params.subscribe(p => {
            if (p.id != null)
                this.interventie.id = +p['id'];
            if (p.consultatieID != null)
                this.interventie.consultatieID = +p['consultatieID'];

        });

    }

    ngOnInit() {
        this.consultatieService.getConsultatie(this.interventie.consultatieID).subscribe(x => {
            this.consultatie = x;
            console.log(this.consultatie);
        });

        if (this.interventie.id)
            this.interventieService.getInterventie(this.interventie.id).subscribe(x => {
                this.interventie = x;

                this.interventie.data = this.interventie.data.slice(0, 10);

                this.consultatieService.getConsultatie(this.interventie.consultatieID).subscribe(x => {
                    this.consultatie = x;
                    console.log(this.consultatie);
                });
            });

    }

    submit() {
        if (this.interventie.id) {
            this.interventieService.updateInterventie(this.interventie.id, this.interventie)
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
            this.interventieService.createInterventie(this.interventie)
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
