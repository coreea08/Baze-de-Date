import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from "@angular/router";
import { ProprietarService } from "../../services/proprietar.service";

@Component({
  selector: 'proprietar',
  templateUrl: './proprietar.component.html',
  styleUrls: ['./proprietar.component.css']
})
export class ProprietarComponent implements OnInit {

    proprietar: any = {};



    constructor(private proprietarService: ProprietarService, private route: ActivatedRoute,
        private router: Router) {
        route.params.subscribe(p => {
            if (p.id != null)
                this.proprietar.id = +p['id'];

        });

    }

    ngOnInit() {

        if (this.proprietar.id)
            this.proprietarService.getProprietar(this.proprietar.id).subscribe(x => {
                this.proprietar = x;

            });

    }

    submit() {
        if (this.proprietar.id) {
            this.proprietarService.updateProprietar(this.proprietar.id, this.proprietar)
                .subscribe(
                () => {

                    this.router.navigate(['/proprietari-list']);

                },
                err => {
                    console.log(err);
                }
                );



        }
        else {
            this.proprietarService.createProprietar(this.proprietar)
                .subscribe(
                () => {

                    this.router.navigate(['/proprietari-list']);
                },
                err => {
                    console.log(err);
                }
                );

        }
    }

}
