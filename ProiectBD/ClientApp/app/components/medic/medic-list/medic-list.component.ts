import { Component, OnInit } from '@angular/core';
import { MedicService } from "../../../services/medic.service";
import { ActivatedRoute, Router } from "@angular/router";

@Component({
  selector: 'medic-list',
  templateUrl: './medic-list.component.html',
  styleUrls: ['./medic-list.component.css']
})
export class MedicListComponent implements OnInit {

    medici: any;

    constructor(private medicService: MedicService, private route: ActivatedRoute,
        private router: Router) {

    }

    ngOnInit() {
        this.populateMedici();
    }

    populateMedici() {
        this.medicService.getMedici().subscribe(x => {
            this.medici = x;
            console.log(x);
        });
    }

    delete(id: number) {

        this.medicService.deleteMedic(id)
            .subscribe(
            () => {
                this.populateMedici();
            });

    }

}
