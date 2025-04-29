import { createReducer, on } from '@ngrx/store';
import * as BoardPageActions from './boards.actions';
import { BoardDto, BoardTaskDto } from 'app/web-api-client';

//Board Reducers
export const boardReducer = createReducer(
    <BoardDto>undefined,
    on(BoardPageActions.saveBoardAction, (state, entityState) => {
        return entityState;
    }),
    on(BoardPageActions.loadBoardAction, (state, entityState) => {
        return entityState;
    }),
);

export const boardTasksReducer = createReducer(
    <BoardTaskDto[]>[],
    on(BoardPageActions.loadBoardAction, (state, entityState) => {
        return entityState.tasks ?? [];
    }),
    on(BoardPageActions.createTaskAction, (state, entityState) => {
        state = [...state, entityState];
        return state;
    }),
    on(BoardPageActions.updateTaskAction, (state, entityState) => {
        const index = state.findIndex(task => task.id == entityState.id);
        if (index !== -1) {
            state = [...state];
            state[index] = entityState
        }
        return state;
    }),
    on(BoardPageActions.deleteTaskAction, (state, entityState) => {
        const index = state.findIndex(task => task.id == entityState.id);
        if (index !== -1) {
            state = [...state];
            state.splice(index, 1);
        }
        return state;
    }),
);

//Task Reducers
export const taskReducer = createReducer(
    <BoardTaskDto>undefined,
    on(BoardPageActions.createTaskAction, (state, entityState) => {
        return undefined;
    }),
    on(BoardPageActions.updateTaskAction, (state, entityState) => {
        return undefined;
    }),
    on(BoardPageActions.deleteTaskAction, (state, entityState) => {
        return undefined;
    }),
    on(BoardPageActions.resetTaskAction, (state) => {
        return undefined;
    }),
    on(BoardPageActions.openTaskAction, (state, entityState) => {
        return entityState;
    }),
);
