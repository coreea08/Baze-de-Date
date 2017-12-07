import { Component, OnInit } from '@angular/core';
import { ProprietarService } from "../../../services/proprietar.service";
import { ActivatedRoute, Router } from "@angular/router";
import { AnimalService } from "../../../services/animal.service";

@Component({
  selector: 'proprietar-details',
  templateUrl: './proprietar-details.component.html',
  styleUrls: ['./proprietar-details.component.css']
})
export class ProprietarDetailsComponent implements OnInit {

    proprietar: any = {};
    animale: any;

    constructor(private proprietarService: ProprietarService, private animalService: AnimalService, private route: ActivatedRoute,
        private router: Router) {

        route.params.subscribe(p => {
            if (p.id != null)
                this.proprietar.id = +p['id'];

        });

    }

    ngOnInit() {
        this.populateAnimale();
        this.proprietarService.getProprietar(this.proprietar.id).subscribe(x => {
            this.proprietar = x;

        });

    }

    populateAnimale() {
        this.animalService.getAnimaleProprietar(this.proprietar.id).subscribe(x => {
            this.animale = x;
            console.log(this.proprietar.id);
            console.log(x);
        });
    }

    deleteAnimal(id: number) {

        this.animalService.deleteAnimal(id)
            .subscribe(
            () => {
                this.populateAnimale();
            });

    }

}
