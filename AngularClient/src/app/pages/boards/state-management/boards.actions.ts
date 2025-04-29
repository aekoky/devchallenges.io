import { createAction, props } from '@ngrx/store';
import { BoardDto, BoardTaskDto } from 'app/web-api-client';

//Task Actions

export const deleteTaskAction = createAction(
    '[Task] Delete Task',
    props<BoardTaskDto>()
);

export const createTaskAction = createAction(
    '[Task] Create Task',
    props<BoardTaskDto>()
);

export const updateTaskAction = createAction(
    '[Task] Update Task',
    props<BoardTaskDto>()
);

export const resetTaskAction = createAction(
    '[Task] Update Task'
);

//Board Actions

export const openTaskAction = createAction(
    '[Board] Open Task',
    props<BoardTaskDto>()
);

export const saveBoardAction = createAction(
    '[Board] Save Board',
    props<BoardDto>()
);

export const loadBoardAction = createAction(
    '[Board] Load Board',
    props<BoardDto>()
);