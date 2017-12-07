import { Component, OnInit } from '@angular/core';
import { AnimalService } from "../../services/animal.service";
import { ActivatedRoute, Router } from "@angular/router";

@Component({
  selector: 'animal',
  templateUrl: './animal.component.html',
  styleUrls: ['./animal.component.css']
})
export class AnimalComponent implements OnInit {

    animal: any = {};



    constructor(private animalService: AnimalService, private route: ActivatedRoute,
        private router: Router) {
        route.params.subscribe(p => {
            if (p.id != null)
                this.animal.id = +p['id'];
            if (p.proprietarId != null)
                this.animal.proprietarID = +p['proprietarId'];

        });

    }

    ngOnInit() {

        if (this.animal.id)
            this.animalService.getAnimal(this.animal.id).subscribe(x => {
                this.animal = x;


            });

        console.log(this.animal.proprietarId);
    }

    submit() {
        if (this.animal.id) {
            this.animalService.updateAnimal(this.animal.id, this.animal)
                .subscribe(
                () => {

                    this.router.navigate(['/proprietar/details/' + this.animal.proprietarID]);

                },
                err => {
                    console.log(err);
                }
                );



        }
        else {
            this.animalService.createAnimal(this.animal)
                .subscribe(
                () => {

                    this.router.navigate(['/proprietar/details/' + this.animal.proprietarID]);
                },
                err => {
                    console.log(err);
                }
                );

        }
    }

}
