import { Component, OnInit } from '@angular/core';
import { DashboardService } from "../../services/dashboard.service";

@Component({
  selector: 'all-animals',
  templateUrl: './all-animals.component.html',
  styleUrls: ['./all-animals.component.css']
})
export class AllAnimalsComponent implements OnInit {

    animalProprietar: any;

    constructor(private dashboardService: DashboardService) {

    }

    ngOnInit() {
        this.populateAnimals();

    }

    populateAnimals() {
        this.dashboardService.getProprietariAnimale().subscribe(x => {
            this.animalProprietar = x;
            console.log(x);
        })

    }

}
