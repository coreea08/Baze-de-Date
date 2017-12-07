import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
import { ProprietarComponent } from './components/proprietar/proprietar.component';
import { ProprietarService } from "./services/proprietar.service";
import { MedicService } from "./services/medic.service";
import { AnimalService } from "./services/animal.service";
import { ConsultatieService } from "./services/consultatie.service";
import { InterventieService } from "./services/interventie.service";
import { RetetaService } from "./services/reteta.service";
import { ProprietarListComponent } from './components/proprietar-list/proprietar-list.component';
import { ProprietarDetailsComponent } from './components/proprietar/proprietar-details/proprietar-details.component';
import { AnimalComponent } from './components/animal/animal.component';
import { AnimalListComponent } from './components/animal/animal-list/animal-list.component';
import { AnimalDetailsComponent } from './components/animal/animal-details/animal-details.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent,
        ProprietarComponent,
        ProprietarListComponent,
        ProprietarDetailsComponent,
        AnimalComponent,
        AnimalListComponent,
        AnimalDetailsComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },

            { path: 'proprietar', component: ProprietarComponent },
            { path: 'proprietar/:id', component: ProprietarComponent },
            { path: 'proprietar/details/:id', component: ProprietarDetailsComponent },
            { path: 'proprietari-list', component: ProprietarListComponent },

            { path: 'animal/:proprietarId', component: AnimalComponent },
            { path: 'animal/edit/:id', component: AnimalComponent },
            { path: 'animal/details/:id', component: ProprietarDetailsComponent },
           // { path: 'animal-list/:proprietarId', component: AnimalListComponent },

            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: '**', redirectTo: 'home' }

        ])
    ],
    providers: [
        ProprietarService,
        MedicService,
        AnimalService,
        ConsultatieService,
        InterventieService,
        RetetaService
    ]
    
})
export class AppModuleShared {
}
