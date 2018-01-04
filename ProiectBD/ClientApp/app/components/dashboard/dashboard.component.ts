import { Component, OnInit } from '@angular/core';
import { DashboardService } from "../../services/dashboard.service";


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {


    animalProprietar: any;
    medicNrConsult: any;
    castigConsultatii: any;
    reteteMedic: any;
    AVGinterventiiMedic: any;

    constructor(private dashboardService: DashboardService) {


    }

    ngOnInit() {
        this.MedicWithNrConsultatii();
        this.CatigTotalConsultatii();
        this.ReteteMedic();
        this.AVGInterventiiMedic();
    }

    MedicWithNrConsultatii() {
        this.dashboardService.getMedicWithNrConsultatii().subscribe(x => {
            this.medicNrConsult = x;
            console.log(x);
        })

    }

    CatigTotalConsultatii() {
        this.dashboardService.getCastigTotalConsultatii().subscribe(x => {
            this.castigConsultatii = x;
            console.log(x);
        })

    }

    ReteteMedic() {
        this.dashboardService.getNrReteteMedic().subscribe(x => {
            this.reteteMedic = x;
            console.log(x);
        })

    }

    AVGInterventiiMedic() {
        this.dashboardService.getAVGInterventiiMedic().subscribe(x => {
            this.AVGinterventiiMedic = x;
            console.log(x);
        })

    }
}
