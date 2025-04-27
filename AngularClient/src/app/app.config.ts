import { ApplicationConfig, provideZoneChangeDetection, isDevMode } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideServiceWorker } from '@angular/service-worker';
import { provideStore } from '@ngrx/store';
import { provideHttpClient } from '@angular/common/http';
import { API_BASE_URL } from './web-api-client';
import { boardReducer, boardTasksReducer, taskReducer } from './pages/boards/state-management/boards.reducer';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideServiceWorker('ngsw-worker.js', {
      enabled: !isDevMode(),
      registrationStrategy: 'registerWhenStable:30000'
    }),
    provideStore({ boardReducer, boardTasksReducer, taskReducer }),
    {
      provide: API_BASE_URL,
      useValue: "/api"
    },
    provideHttpClient()
  ]
};
