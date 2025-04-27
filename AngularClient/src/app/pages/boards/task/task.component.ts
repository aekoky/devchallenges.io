import { Component, OnDestroy } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Subject, takeUntil } from 'rxjs';
import { Store } from '@ngrx/store';
import { BoardTaskDto, BoardTaskIcon, BoardTaskStatus } from 'app/web-api-client';
import { TaskService } from './task.service';
import { selectTask } from '../state-management/boards.selectors';
import { deleteTaskAction } from '../state-management/boards.actions';

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.css'],
  standalone: false
})
export class TaskComponent implements OnDestroy {
  private readonly _unsubscribeAll: Subject<any> = new Subject<any>();
  task: BoardTaskDto;
  taskForm = new FormGroup({
    id: new FormControl<number>(0),
    name: new FormControl('', [Validators.required, Validators.maxLength(250)]),
    description: new FormControl('', [Validators.maxLength(1500)]),
    icon: new FormControl<BoardTaskIcon>(BoardTaskIcon.Icon1),
    status: new FormControl<BoardTaskStatus>(BoardTaskStatus.Pending),
  });

  constructor(
    private readonly _taskService: TaskService,
    private readonly _store: Store
  ) {
    this._store.select(selectTask).pipe(takeUntil(this._unsubscribeAll)).subscribe(task => {
      this.task = task;
      if (task)
        this.taskForm.patchValue(task);
      else
        this.taskForm.reset({ icon: BoardTaskIcon.Icon1, status: BoardTaskStatus.Pending });
    });
  }

  saveTask() {
    if (this.taskForm.invalid)
      return;
    this.taskForm.disable({ emitEvent: false });
    this.task = BoardTaskDto.fromJS(this.taskForm.getRawValue());
    this._taskService.saveTask(this.task?.id, this.task).subscribe((task) => {
      this.task = task;
      this.taskForm.enable();
    });
  }

  deleteTask() {
    if (!this.task?.id)
      return;
    this._taskService.deleteTask(this.task?.id).subscribe((task) => {
      this._store.dispatch(deleteTaskAction(this.task));
    });
  }

  ngOnDestroy(): void {
    // Unsubscribe from all subscriptions
    this._unsubscribeAll.next(null);
    this._unsubscribeAll.complete();
  }
}
