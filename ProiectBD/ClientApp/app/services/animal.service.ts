import { Injectable } from '@angular/core';
import { Http } from "@angular/http";
import 'rxjs/add/operator/map';

@Injectable()
export class AnimalService {

    constructor(private http: Http) { }

    getAnimal(id: number) {
        return this.http.get('/api/Animal/' + id).map(res => res.json());
    }

    getAnimaleProprietar(proprietarId: number) {
        return this.http.get('/api/Animale/' + proprietarId).map(res => res.json());
    }

    getConsultatiiAnimal(Id: number) {
        return this.http.get('/api/ConsultatiiAnimal/' + Id).map(res => res.json());
    }

    getInterventiiAnimal(Id: number) {
        return this.http.get('/api/InterventiiAnimal/' + Id).map(res => res.json());
    }

    getAnimale() {
        return this.http.get('/api/Animal').map(res => res.json());
    }

    createAnimal(animal: any) {
        return this.http.post('/api/Animal', animal);
    }

    updateAnimal(id: number, animal: any) {
        return this.http.put('/api/Animal/' + id, animal);
    }

    deleteAnimal(id: number) {
        return this.http.delete('/api/Animal/' + id);
    }

}
