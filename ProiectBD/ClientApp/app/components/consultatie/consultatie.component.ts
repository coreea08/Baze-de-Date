import { Component, OnInit, ElementRef } from '@angular/core';
import { ConsultatieService } from "../../services/consultatie.service";
import { ActivatedRoute, Router } from "@angular/router";
import { ProprietarService } from "../../services/proprietar.service";

@Component({
  selector: 'consultatie',
  templateUrl: './consultatie.component.html',
  styleUrls: ['./consultatie.component.css'],
  host: {
      '(document:click)': 'handleClick($event)'
  },
})
export class ConsultatieComponent implements OnInit {

    consultatie: any = {};

    // autocomplete data
    public animaleList: any = [];
    public CNPList: any = [];
    public filteredList: any = [];
    public filteredListCNP: any = [];
    public elementRef: any;
    public query: any = {};
    public animale: any[];
    public proprietari: any[];


    constructor(private consultatieService: ConsultatieService, private proprietarService: ProprietarService, private route: ActivatedRoute,
        private router: Router, private myElement: ElementRef,) {
        route.params.subscribe(p => {
            if (p.id != null)
                this.consultatie.id = +p['id'];
            if (p.medicID != null)
                this.consultatie.medicID = +p['medicID'];

        });
        this.elementRef = myElement;
    }

    ngOnInit() {

        if (!this.consultatie.id)
            this.populateCNP();

        if (this.consultatie.id)
            this.consultatieService.getConsultatie(this.consultatie.id).subscribe(x => {
                this.consultatie = x;

                this.consultatie.data = this.consultatie.data.slice(0, 10);
                console.log(this.consultatie.data);
            });

    }

    submit() {
        let t = this.animale.find(f => f.nume == this.query.nume);
        this.consultatie.animalID = t.id;
        if (this.consultatie.id) {
            this.consultatieService.updateConsultatie(this.consultatie.id, this.consultatie)
                .subscribe(
                () => {

                    this.router.navigate(['/medic/details/' + this.consultatie.medicID]);

                },
                err => {
                    console.log(err);
                }
                );



        }
        else {
            this.consultatieService.createConsultatie(this.consultatie)
                .subscribe(
                () => {

                    this.router.navigate(['/medic/details/' + this.consultatie.medicID]);
                },
                err => {
                    console.log(err);
                }
                );

        }
    }

    populateAnimale()
    {

        this.consultatieService.getNumeAnimaleWithCNPProprietar(this.query.cnp).subscribe(x => {
            this.animale = x;
            
            this.animaleList = this.animale.map(a => a.nume);

            console.log(this.animaleList);
        })
    }

    populateCNP() {
        this.proprietarService.getProprietari().subscribe(x => {
            this.proprietari = x;

            this.CNPList = this.proprietari.map(a => a.cnp);
            console.log(this.CNPList);

        });
    }

    filter() {
        console.log(this.query);
        let q = this.query;
        if (this.query.nume !== "") {
            this.filteredList = this.animaleList.filter(function (el: any) {
                return (el.toLowerCase().substr(0, q.nume.length) === q.nume.toLowerCase()) == true;
            }.bind(this));
        } else {
            this.filteredList = [];
        }
    }

    select(item: any) {
        this.query.nume = item;
        this.filteredList = [];
    }

    filterCNP() {
        console.log(this.query);
        let q = this.query;
        if (this.query.cnp !== "") {
            this.filteredListCNP = this.CNPList.filter(function (el: any) {
                return (el.toLowerCase().substr(0, q.cnp.length) === q.cnp.toLowerCase()) == true;
            }.bind(this));
        } else {
            this.filteredListCNP = [];
            this.animaleList = [];
            this.query.nume = "";
        }
    }

    selectCNP(item: any) {
        this.query.cnp = item;
        this.filteredListCNP = [];

        this.populateAnimale();
    }

    handleClick(event: any) {
        var clickedComponent = event.target;
        var inside = false;
        do {
            if (clickedComponent === this.elementRef.nativeElement) {
                inside = true;
            }
            clickedComponent = clickedComponent.parentNode;
        } while (clickedComponent);
        if (!inside) {
            this.filteredList = [];
        }
    }

}
