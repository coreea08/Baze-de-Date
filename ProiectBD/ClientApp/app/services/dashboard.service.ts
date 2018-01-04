import { Injectable } from '@angular/core';
import { Http } from "@angular/http";
import 'rxjs/add/operator/map';

@Injectable()
export class DashboardService {

    constructor(private http: Http) { }
    
    //aux services
    getProprietariAnimale() {
        return this.http.get('/api/animalProprietar').map(res => res.json());
    }

    getMedicWithNrConsultatii() {
        return this.http.get('/api/medicNumarConsultatii').map(res => res.json());
    }

    getCastigTotalConsultatii() {
        return this.http.get('/api/castigConsultatiiMedic').map(res => res.json());
    }

    getNrReteteMedic() {
        return this.http.get('/api/NrReteteMedic').map(res => res.json());
    }

    getAVGInterventiiMedic() {
        return this.http.get('/api/medicAverageInterventii').map(res => res.json());
    }
}
