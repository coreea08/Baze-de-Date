import { Component, OnInit } from '@angular/core';
import { AnimalService } from "../../../services/animal.service";
import { ActivatedRoute, Router } from "@angular/router";

@Component({
  selector: 'animal-details',
  templateUrl: './animal-details.component.html',
  styleUrls: ['./animal-details.component.css']
})
export class AnimalDetailsComponent implements OnInit {

    animal: any = {};

    constructor(private animalService: AnimalService, private route: ActivatedRoute,
        private router: Router) {

        route.params.subscribe(p => {
            if (p.id != null)
                this.animal.id = +p['id'];

        });

    }

    ngOnInit() {
        this.animalService.getAnimal(this.animal.id).subscribe(x => {
            this.animal = x;

        });

    }

}
