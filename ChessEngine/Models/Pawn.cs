using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace ChessEngine.Models
{
    public class Pawn : Piece//пешка
    {
        private bool _moved;
        public bool Moved
        {
            get { return _moved; }
            set { _moved = value; }
        }

        private int _enPassantTurn;
        public int EnPassantTurn
        {
            get { return _enPassantTurn; }
            set { _enPassantTurn = value; }
        }

        public Pawn(PieceColorEnum Color) : base(Color)
        {
            _pieceType = PieceTypeEnum.Pawn;
            _moved = false;
            _enPassantTurn = 0;
            if (_pieceColor == PieceColorEnum.White)
            {
                PieceImage = "/ChessEngine;component/Resources/WhitePawn.png";
            }
            else
            {
                PieceImage = "/ChessEngine;component/Resources/BlackPawn.png";
            }
        }

        public override Dictionary<string, string> GenerateLegalMoves(int Turn, int CurrentX, int CurrentY, ObservableCollection<ObservableCollection<BoardSquare>> Board)
        {
            Dictionary<string, string> Moves = new Dictionary<string, string>();

            string MoveType;//определяем тип хода
            if (_moved == false)
            {
                MoveType = "PawnFirst";
            }
            else if ((CurrentX == 6 && _pieceColor == PieceColorEnum.Black)//Повышение пешки при достижении конца доски
                || (CurrentX == 1 && _pieceColor == PieceColorEnum.White))
            {
                MoveType = "Promotion";
            }
            else
            {
                MoveType = "Standard";
            }

            //белая пешка
            if (_pieceColor == PieceColorEnum.White)
            {
                if (Board[CurrentX - 1][CurrentY].SquarePiece.PieceType == PieceTypeEnum.Empty) 
                {
                    Moves.Add(MoveEncoder.EncodeMove(CurrentX, CurrentY, CurrentX+2, CurrentY), MoveType);
                    if (_moved == false)
                    {
                        if (Board[CurrentX - 2][CurrentY].SquarePiece.PieceType == PieceTypeEnum.Empty)
                        {
                            Moves.Add(MoveEncoder.EncodeMove(CurrentX, CurrentY, CurrentX - 2, CurrentY), "PawnDoubleFirst");//ход на 2 клетки
                        }
                    }
                }
                if (CurrentY > 0) //взятие фигуры по левой диагонали
                {
                    if (Board[CurrentX - 1][CurrentY - 1].SquarePiece.PieceColor != PieceColorEnum.Empty
                        && Board[CurrentX - 1][CurrentY - 1].SquarePiece.PieceColor != _pieceColor)
                    {
                        Moves.Add(MoveEncoder.EncodeMove(CurrentX, CurrentY, CurrentX - 1, CurrentY - 1), MoveType);
                    }
                    if (_moved == true
                        && Board[CurrentX][CurrentY - 1].SquarePiece.PieceType == PieceTypeEnum.Pawn
                        && Board[CurrentX][CurrentY - 1].SquarePiece.PieceColor != _pieceColor)
                    {
                        var Threat = (Pawn)Board[CurrentX][CurrentY - 1].SquarePiece;
                        if (Threat.EnPassantTurn == Turn)
                        {
                            Moves.Add(MoveEncoder.EncodeMove(CurrentX, CurrentY, CurrentX - 1, CurrentY-1), "EnPassant");//взятие на проходе по левой стороне
                        }
                    }
                }
                if (CurrentY < 7) //взятие фигуры по правой диагонали
                {
                    if (Board[CurrentX - 1][CurrentY + 1].SquarePiece.PieceColor != PieceColorEnum.Empty
                        && Board[CurrentX - 1][CurrentY + 1].SquarePiece.PieceColor != _pieceColor)
                    {
                        Moves.Add(MoveEncoder.EncodeMove(CurrentX, CurrentY, CurrentX - 1, CurrentY + 1), MoveType);
                    }
                    if (_moved == true
                        && Board[CurrentX][CurrentY + 1].SquarePiece.PieceType == PieceTypeEnum.Pawn
                        && Board[CurrentX][CurrentY + 1].SquarePiece.PieceColor != _pieceColor)
                    {
                        var Threat = (Pawn)Board[CurrentX][CurrentY + 1].SquarePiece;
                        if (Threat.EnPassantTurn == Turn)
                        {
                            Moves.Add(MoveEncoder.EncodeMove(CurrentX, CurrentY, CurrentX - 1, CurrentY + 1), "EnPassant");//взятие на проходе по правой стороне
                        }
                    }
                }
            }
           

         //Черная пешка
            if (_pieceColor == PieceColorEnum.Black)
            {
                if (Board[CurrentX + 1][CurrentY].SquarePiece.PieceType == PieceTypeEnum.Empty) 
                {
                    Moves.Add(MoveEncoder.EncodeMove(CurrentX, CurrentY, CurrentX + 1, CurrentY), MoveType);
                    if (_moved == false)
                    {
                        if (Board[CurrentX + 2][CurrentY].SquarePiece.PieceType == PieceTypeEnum.Empty)
                        {
                            Moves.Add(MoveEncoder.EncodeMove(CurrentX, CurrentY, CurrentX + 2, CurrentY), "PawnDoubleFirst");
                        }
                    }
                }
                if (CurrentY > 0) 
                {
                    if (Board[CurrentX + 1][CurrentY - 1].SquarePiece.PieceColor != PieceColorEnum.Empty
                        && Board[CurrentX + 1][CurrentY - 1].SquarePiece.PieceColor != _pieceColor)
                    {
                        Moves.Add(MoveEncoder.EncodeMove(CurrentX, CurrentY, CurrentX + 1, CurrentY - 1), MoveType);
                    }
                    if (_moved == true
                        && Board[CurrentX][CurrentY - 1].SquarePiece.PieceType == PieceTypeEnum.Pawn
                        && Board[CurrentX][CurrentY - 1].SquarePiece.PieceColor != _pieceColor)
                    {
                        var Threat = (Pawn)Board[CurrentX][CurrentY - 1].SquarePiece;
                        if (Threat.EnPassantTurn == Turn)
                        {
                            Moves.Add(MoveEncoder.EncodeMove(CurrentX, CurrentY, CurrentX + 1, CurrentY - 1), "EnPassant");
                        }
                    }
                }
                if (CurrentY < 7) 
                {
                    if (Board[CurrentX + 1][CurrentY + 1].SquarePiece.PieceColor != PieceColorEnum.Empty
                        && Board[CurrentX + 1][CurrentY + 1].SquarePiece.PieceColor != _pieceColor)
                    {
                        Moves.Add(MoveEncoder.EncodeMove(CurrentX, CurrentY, CurrentX + 1, CurrentY + 1), MoveType);
                    }
                    if (_moved == true
                        && Board[CurrentX][CurrentY + 1].SquarePiece.PieceType == PieceTypeEnum.Pawn
                        && Board[CurrentX][CurrentY + 1].SquarePiece.PieceColor != _pieceColor)
                    {
                        var Threat = (Pawn)Board[CurrentX][CurrentY + 1].SquarePiece;
                        if (Threat.EnPassantTurn == Turn)
                        {
                            Moves.Add(MoveEncoder.EncodeMove(CurrentX, CurrentY, CurrentX + 1, CurrentY + 1), "EnPassant");
                        }
                    }
                }
            }
            Moves = LegalMoveGenerator.GenerateLegalMoves(Moves, Board, _pieceColor);
           /* foreach (var mov in Moves)
            {
                Console.WriteLine($"key: {mov.Key}  value: {mov.Value}");
            }*/

            return Moves;
        }

    }
}