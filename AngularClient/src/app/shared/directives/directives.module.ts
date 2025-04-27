import { NgModule } from '@angular/core';
import { CloseDialogDirective } from './close-dialog.directive';
import { ScrollToInvalideDirective } from './scroll-to-invalide.directive';
import { OnlyDialogDirective } from './only-dialog.directive';

@NgModule({
    imports: [],
    declarations: [
        ScrollToInvalideDirective,
        CloseDialogDirective,
        OnlyDialogDirective
    ],
    exports: [
        ScrollToInvalideDirective,
        CloseDialogDirective,
        OnlyDialogDirective
    ]
})
export class DirectivesModule { }