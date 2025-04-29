import { Component, OnDestroy, ViewChild } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDrawer } from '@angular/material/sidenav';
import { Router, RouterEvent, NavigationEnd } from '@angular/router';
import { Store } from '@ngrx/store';
import { BoardDto, BoardTaskDto, BoardTaskIcon, BoardTaskStatus } from 'app/web-api-client';
import { takeUntil, filter, map, Subject, Observable, tap, take, switchMap, shareReplay } from 'rxjs';
import { loadBoardAction, openTaskAction } from './state-management/boards.actions';
import { selectBoard, selectBoardTasks, selectTask } from './state-management/boards.selectors';
import { BoardService } from './board.service';
import { TaskService } from './task/task.service';

@Component({
    selector: 'app-boards',
    templateUrl: './boards.component.html',
    styleUrls: ['./boards.component.scss'],
    host: {
        'class': 'h-full'
    },
    standalone: false
})
export class BoardsComponent implements OnDestroy {
    private readonly _unsubscribeAll: Subject<any> = new Subject<any>();
    @ViewChild('matDrawer', { static: true }) matDrawer: MatDrawer;
    boardId: number;
    boardEditMode: boolean;
    board: Observable<BoardDto>;
    selectedTask: Observable<BoardTaskDto>;
    boardTasks: Observable<BoardTaskDto[]>;
    BoardTaskIcon = BoardTaskIcon;
    BoardTaskStatus = BoardTaskStatus;
    boardForm = new FormGroup({
        id: new FormControl<number>(0),
        name: new FormControl('', [Validators.required, Validators.maxLength(250)]),
        description: new FormControl('', [Validators.maxLength(1500)]),
    });

    constructor(
        private readonly _boardService: BoardService,
        private readonly _taskService: TaskService,
        private readonly _store: Store
    ) {
        this.board = this._store.select(selectBoard).pipe(
            takeUntil(this._unsubscribeAll),
            tap(board => {
                debugger;
                if (board)
                    this.boardForm.patchValue(board);
                else
                    this.boardForm.reset();
            }),
            shareReplay(1));

        this.boardTasks = this._store.select(selectBoardTasks).pipe(
            takeUntil(this._unsubscribeAll));
        this.selectedTask = this._store.select(selectTask).pipe(
            takeUntil(this._unsubscribeAll),
            filter(() => !!this.matDrawer),
            tap(task => (task?.id) ? this.matDrawer.toggle(true) : this.matDrawer.toggle(false)));
        this._boardService.getBoard().subscribe(board => this._store.dispatch(loadBoardAction(board)));
    }

    saveBoard() {
        if (this.boardForm.invalid)
            return;
        this.boardForm.disable({ emitEvent: false });
        const board = BoardDto.fromJS(this.boardForm.getRawValue());
        this._boardService.saveBoard(board.id, board).subscribe(() => {
            this.boardForm.enable({ emitEvent: false });
            this.boardEditMode = false;
        });
    }

    openTask(task: BoardTaskDto) {
        if (!task?.id)
            return;
        this._store.dispatch(openTaskAction(task));
    }

    addTask() {
        this.board.pipe(
            take(1),
            map(board => board.id),
            filter(boardId => !!boardId),
            switchMap(boardId => this._taskService.saveTask(0, new BoardTaskDto({ name: "New task", icon: BoardTaskIcon.Icon1, status: BoardTaskStatus.Pending, boardId })))
        ).subscribe(
            task => this.openTask(task)
        )
    }

    ngOnDestroy(): void {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }
}
