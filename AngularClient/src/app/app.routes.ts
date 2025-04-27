import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: '',
        loadChildren: () => import('app/pages/boards/boards.module').then(m => m.ProblemsModule),
    },
];
