import { Component, OnInit } from '@angular/core';
import { MedicService } from "../../services/medic.service";
import { ActivatedRoute, Router } from "@angular/router";

@Component({
  selector: 'medic',
  templateUrl: './medic.component.html',
  styleUrls: ['./medic.component.css']
})
export class MedicComponent implements OnInit {

    medic: any = {};



    constructor(private medicService: MedicService, private route: ActivatedRoute,
        private router: Router) {
        route.params.subscribe(p => {
            if (p.id != null)
                this.medic.id = +p['id'];

        });

    }

    ngOnInit() {

        if (this.medic.id)
            this.medicService.getMedic(this.medic.id).subscribe(x => {
                this.medic = x;

            });

    }

    submit() {
        if (this.medic.id) {
            this.medicService.updateMedic(this.medic.id, this.medic)
                .subscribe(
                () => {

                    this.router.navigate(['/medic-list']);

                },
                err => {
                    console.log(err);
                }
                );



        }
        else {
            this.medicService.createMedic(this.medic)
                .subscribe(
                () => {

                    this.router.navigate(['/medic-list']);
                },
                err => {
                    console.log(err);
                }
                );

        }
    }
}
