import { Injectable } from '@angular/core';
import { Http } from "@angular/http";
import 'rxjs/add/operator/map';

@Injectable()
export class ConsultatieService {

    constructor(private http: Http) { }

    getConsultatie(id: number) {
        return this.http.get('/api/Consultatie/' + id).map(res => res.json());
    }

    getConsultatiiMedic(medicId: number) {
        return this.http.get('/api/ConsultatieMedic/' + medicId).map(res => res.json());
    }

    getConsultatiiVechiMedic(medicId: number) {
        return this.http.get('/api/ConsultatieMedicVeche/' + medicId).map(res => res.json());
    }

    getConsultatiiNoiMedic(medicId: number) {
        return this.http.get('/api/ConsultatieMedicNoua/' + medicId).map(res => res.json());
    }

    getNumeAnimaleWithCNPProprietar(cnp: string) {
        return this.http.get('/api/numeAnimal/' + cnp).map(res => res.json());
    }

    getConsultatii() {
        return this.http.get('/api/Consultatie').map(res => res.json());
    }

    createConsultatie(consultatie: any) {
        return this.http.post('/api/Consultatie', consultatie);
    }

    updateConsultatie(id: number, consultatie: any) {
        return this.http.put('/api/Consultatie/' + id, consultatie);
    }

    deleteConsultatie(id: number) {
        return this.http.delete('/api/Consultatie/' + id);
    }

}
