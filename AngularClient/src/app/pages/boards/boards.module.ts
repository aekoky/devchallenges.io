import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Route, RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { DirectivesModule } from 'app/shared/directives/directives.module';
import { MatSidenavModule } from '@angular/material/sidenav';
import { TaskComponent } from './task/task.component';
import { BoardsComponent } from './boards.component';
import { IconsModule } from 'app/shared/icons/icons.module';

const problemsRoutes: Route[] = [
  {
    path: '',
    title: "MyTaskBoard - Board",
    component: BoardsComponent
  }
];


@NgModule({
  declarations: [
    BoardsComponent,
    TaskComponent,
  ],
  imports: [
    RouterModule.forChild(problemsRoutes),
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    DirectivesModule,
    MatSidenavModule,
    IconsModule
  ]
})
export class ProblemsModule {

}
