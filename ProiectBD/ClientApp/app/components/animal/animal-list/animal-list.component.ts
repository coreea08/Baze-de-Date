import { Component, OnInit } from '@angular/core';
import { AnimalService } from "../../../services/animal.service";
import { ActivatedRoute, Router } from "@angular/router";

@Component({
  selector: 'animal-list',
  templateUrl: './animal-list.component.html',
  styleUrls: ['./animal-list.component.css']
})
export class AnimalListComponent implements OnInit {

    animale: any;
    proprietarID: number;

    constructor(private animalService: AnimalService, private route: ActivatedRoute,
        private router: Router) {

        route.params.subscribe(p => {
            if (p.proprietarId != null)
                this.proprietarID = +p['proprietarId'];

        });

    }

    ngOnInit() {
        this.populateAnimale();
    }

    populateAnimale() {
        this.animalService.getAnimaleProprietar(this.proprietarID).subscribe(x => {
            this.animale = x;

            console.log(this.proprietarID);
            console.log(x);
        });
    }

    delete(id: number) {

        this.animalService.deleteAnimal(id)
            .subscribe(
            () => {
                this.populateAnimale();
            });

    }

}
