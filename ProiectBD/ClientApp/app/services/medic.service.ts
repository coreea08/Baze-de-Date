import { Injectable } from '@angular/core';
import { Http } from "@angular/http";
import 'rxjs/add/operator/map';

@Injectable()
export class MedicService {

    constructor(private http: Http) { }

    getMedic(id: number) {
        return this.http.get('/api/Medic/' + id).map(res => res.json());
    }

    getMedici() {
        return this.http.get('/api/Medic').map(res => res.json());
    }

    createMedic(medic: any) {
        return this.http.post('/api/Medic', medic).map(res => res.json());
    }

    updateMedic(id: number, medic: any) {
        return this.http.put('/api/Medic/' + id, medic).map(res => res.json());
    }

    deleteMedic(id: number) {
        return this.http.delete('/api/Medic/' + id).map(res => res.json());
    }
}
