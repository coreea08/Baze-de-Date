import { Injectable } from '@angular/core';
import { Http } from "@angular/http";
import 'rxjs/add/operator/map';

@Injectable()
export class RetetaService {

    constructor(private http: Http) { }

    getReteta(id: number) {
        return this.http.get('/api/Reteta/' + id).map(res => res.json());
    }

    getRetete() {
        return this.http.get('/api/Reteta').map(res => res.json());
    }

    getReteteMedic(medicID: number) {
        return this.http.get('/api/reteteMedic/' + medicID).map(res => res.json());
    }

    createRetete(reteta: any) {
        return this.http.post('/api/Reteta', reteta);
    }

    updateRetete(id: number, reteta: any) {
        return this.http.put('/api/Reteta/' + id, reteta);
    }

    deleteRetete(id: number) {
        return this.http.delete('/api/Reteta/' + id);
    }

}
