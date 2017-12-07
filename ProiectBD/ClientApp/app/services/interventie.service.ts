import { Injectable } from '@angular/core';
import { Http } from "@angular/http";
import 'rxjs/add/operator/map';

@Injectable()
export class InterventieService {

    constructor(private http: Http) { }

    getInterventie(id: number) {
        return this.http.get('/api/Interventie/' + id).map(res => res.json());
    }

    getInterventii() {
        return this.http.get('/api/Interventie').map(res => res.json());
    }

    createInterventie(interventie: any) {
        return this.http.post('/api/Interventie', interventie).map(res => res.json());
    }

    updateInterventie(id: number, interventie: any) {
        return this.http.put('/api/Interventie/' + id, interventie).map(res => res.json());
    }

    deleteInterventie(id: number) {
        return this.http.delete('/api/Interventie/' + id).map(res => res.json());
    }
}
