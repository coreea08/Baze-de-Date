import { Injectable } from '@angular/core';
import { Http } from "@angular/http";
import 'rxjs/add/operator/map';

@Injectable()
export class ConsultatieService {

    constructor(private http: Http) { }

    getConsultatie(id: number) {
        return this.http.get('/api/Consultatie/' + id).map(res => res.json());
    }

    getConsultatii() {
        return this.http.get('/api/Consultatie').map(res => res.json());
    }

    createConsultatie(consultatie: any) {
        return this.http.post('/api/Consultatie', consultatie).map(res => res.json());
    }

    updateConsultatie(id: number, consultatie: any) {
        return this.http.put('/api/Consultatie/' + id, consultatie).map(res => res.json());
    }

    deleteConsultatie(id: number) {
        return this.http.delete('/api/Consultatie/' + id).map(res => res.json());
    }

}
