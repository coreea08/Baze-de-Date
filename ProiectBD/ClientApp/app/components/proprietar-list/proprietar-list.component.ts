import { Component, OnInit } from '@angular/core';
import { ProprietarService } from "../../services/proprietar.service";
import { ActivatedRoute, Router } from "@angular/router";
declare var $: any;
declare var swal: any;

@Component({
  selector: 'proprietar-list',
  templateUrl: './proprietar-list.component.html',
  styleUrls: ['./proprietar-list.component.css']
})
export class ProprietarListComponent implements OnInit {

    proprietari: any;

    constructor(private proprietarService: ProprietarService, private route: ActivatedRoute,
        private router: Router) {

    }

    ngOnInit() {
        this.populateProprietari();
    }

    populateProprietari() {
        this.proprietarService.getProprietari().subscribe(x => {
            this.proprietari = x;
            console.log(x);
        });
    }

    delete(id: number) {

        this.proprietarService.deleteProprietar(id)
            .subscribe(
            () => {
                this.populateProprietari();
            });

    }

}
