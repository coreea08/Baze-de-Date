import { Injectable } from '@angular/core';
import { Http } from "@angular/http";
import 'rxjs/add/operator/map';

@Injectable()
export class ProprietarService {

    constructor(private http: Http) { }

    getProprietar(id: number) {
        return this.http.get('/api/Proprietar/' + id).map(res => res.json());
    }

    getProprietari() {
        return this.http.get('/api/Proprietar').map(res => res.json());
    }

    createProprietar(proprietar: any) {
        return this.http.post('/api/Proprietar', proprietar);
    }

    updateProprietar(id: number, proprietar: any) {
        return this.http.put('/api/Proprietar/' + id, proprietar);
    }

    deleteProprietar(id: number) {
        return this.http.delete('/api/Proprietar/' + id);
    }

}
