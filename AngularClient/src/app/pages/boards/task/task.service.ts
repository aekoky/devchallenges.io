import { Injectable } from '@angular/core';
import { map, Observable, tap } from 'rxjs';
import { ToastType } from 'app/shared/enums/taost-type.enum';
import { ToastService } from 'app/shared/services/toast.service';
import { BoardTaskDto, BoardTasksClient, CreateBoardTaskCommand, UpdateBoardTaskCommand } from 'app/web-api-client';
import { Store } from '@ngrx/store';
import { createTaskAction, updateTaskAction } from '../state-management/boards.actions';

@Injectable({
    providedIn: 'root'
})
export class TaskService {

    constructor(
        private readonly _tasksClient: BoardTasksClient,
        private readonly _toastService: ToastService,
        private readonly _store: Store
    ) {
    }

    saveTask(taskId: number, task: BoardTaskDto): Observable<BoardTaskDto> {
        if (taskId > 0)
            return this._tasksClient.update(taskId, UpdateBoardTaskCommand.fromJS(task)).pipe(
                tap(
                    {
                        next: () => {
                            this._store.dispatch(updateTaskAction(task));
                            this._toastService.openToast('The task has been updated', ToastType.Success, 3000);
                        },
                        error: error => this._toastService.openToast(error, ToastType.Error)
                    }
                ),
                map(() => task));
        else
            return this._tasksClient.create(CreateBoardTaskCommand.fromJS(task)).pipe(
                map((id) => {
                    task.id = id;
                    return task;
                }),
                tap({
                    next: (id) => {
                        this._store.dispatch(createTaskAction(task));
                        this._toastService.openToast('The task has been created', ToastType.Success, 3000);
                    },
                    error: error => this._toastService.openToast(error, ToastType.Error),
                }),
            );
    }

    deleteTask(id: number): Observable<void> {
        return this._tasksClient.delete(id).pipe(
            tap({
                next: () => this._toastService.openToast('The task has been deleted', ToastType.Success, 3000),
                error: error => this._toastService.openToast(error, ToastType.Error),
            })
        );
    }
}