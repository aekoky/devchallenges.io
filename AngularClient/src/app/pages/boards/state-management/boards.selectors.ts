import { createSelector } from '@ngrx/store';


//Problem Entity State Selector
export const selectBoard = createSelector(
    (state: any) => state.boardReducer,
    state => state
);

export const selectBoardTasks = createSelector(
    (state: any) => state.boardTasksReducer,
    state => state
);

export const selectTask = createSelector(
    (state: any) => state.taskReducer,
    state => state
);
