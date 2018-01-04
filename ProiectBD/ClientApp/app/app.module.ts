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
import { MedicComponent } from './components/medic/medic.component';
import { MedicListComponent } from './components/medic/medic-list/medic-list.component';
import { MedicDetailsComponent } from './components/medic/medic-details/medic-details.component';
import { ConsultatieComponent } from './components/consultatie/consultatie.component';
import { InterventieComponent } from './components/interventie/interventie.component';
import { RetetaComponent } from './components/reteta/reteta.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { DashboardService } from "./services/dashboard.service";
import { AllAnimalsComponent } from './components/all-animals/all-animals.component';

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
        AnimalDetailsComponent,
        MedicComponent,
        MedicListComponent,
        MedicDetailsComponent,
        ConsultatieComponent,
        InterventieComponent,
        RetetaComponent,
        DashboardComponent,
        AllAnimalsComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'dashboard', component: DashboardComponent },
            { path: 'allAnimals', component: AllAnimalsComponent },

            { path: 'proprietar', component: ProprietarComponent },
            { path: 'proprietar/:id', component: ProprietarComponent },
            { path: 'proprietar/details/:id', component: ProprietarDetailsComponent },
            { path: 'proprietari-list', component: ProprietarListComponent },

            { path: 'animal/:proprietarId', component: AnimalComponent },
            { path: 'animal/edit/:id', component: AnimalComponent },
            { path: 'animal/details/:id', component: AnimalDetailsComponent },
           // { path: 'animal-list/:proprietarId', component: AnimalListComponent },

            { path: 'medic', component: MedicComponent },
            { path: 'medic/:id', component: MedicComponent },
            { path: 'medic/details/:id', component: MedicDetailsComponent },
            { path: 'medic-list', component: MedicListComponent },

            { path: 'consultatie/:medicID', component: ConsultatieComponent },
            { path: 'consultatie/edit/:id', component: ConsultatieComponent },

            { path: 'interventie/:consultatieID', component: InterventieComponent },
            { path: 'interventie/edit/:id', component: InterventieComponent },

            { path: 'reteta/:consultatieID', component: RetetaComponent },
            { path: 'reteta/edit/:id', component: RetetaComponent },


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
        RetetaService,
        DashboardService
    ]
    
})
export class AppModuleShared {
}
