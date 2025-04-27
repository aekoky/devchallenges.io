import { Injectable } from '@angular/core';
import { map, Observable, tap } from 'rxjs';
import { ToastType } from 'app/shared/enums/taost-type.enum';
import { ToastService } from 'app/shared/services/toast.service';
import { BoardDto, BoardsClient, CreateBoardCommand, UpdateBoardCommand } from 'app/web-api-client';
import { Store } from '@ngrx/store';
import { saveBoardAction } from './state-management/boards.actions';

@Injectable({
    providedIn: 'root'
})
export class BoardService {

    constructor(
        private readonly _boardsClient: BoardsClient,
        private readonly _toastService: ToastService,
        private readonly _store: Store
    ) {
    }

    saveBoard(boardId: number, board: BoardDto): Observable<BoardDto> {
        if (boardId > 0)
            return this._boardsClient.update(boardId, UpdateBoardCommand.fromJS(board)).pipe(
                tap(
                    {
                        next: () => {
                            this._store.dispatch(saveBoardAction(board));
                            this._toastService.openToast('The board has been updated', ToastType.Success, 3000);
                        },
                        error: error => this._toastService.openToast(error, ToastType.Error)
                    }
                ),
                map(() => board));
        else
            return this._boardsClient.create(CreateBoardCommand.fromJS(board)).pipe(
                map((id) => {
                    board.id = id;
                    return board;
                }),
                tap({
                    next: (id) => {
                        this._store.dispatch(saveBoardAction(board));
                        this._toastService.openToast('The board has been created', ToastType.Success, 3000);
                    },
                    error: error => this._toastService.openToast(error, ToastType.Error),
                }),
            );
    }

    getBoard():Observable<BoardDto>{
        return this._boardsClient.getBoard();
    }
}