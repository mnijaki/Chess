// Class that implements manage of Pawn piece.
public class Pawn:Piece
{
  // Function that return allowed moves.
  public override bool[,] MovesGet(Piece[,] pieces, bool verify)
  {
    // Array of allowed moves.
    bool[,] moves=new bool[8,8];

    // Chess piece.
    Piece piece;

    // If white pawn.
    if(this.Is_white)
    {
      // If pawn is not on the top edge of the board and there is no piece one step forward.
      if((this.Z<7)&&(pieces[this.X,this.Z+1]==null))
      {
        // Add move as allowed.
        moves[this.X,this.Z+1]=true;
      }
      // If pawn didn't move.
      if(this.Z==1)
      {
        // If there is no piece one step forward and two steps forward.
        if((pieces[this.X,this.Z+1]==null)&&(pieces[this.X,this.Z+2]==null))
        {
          // Add move as allowed.
          moves[this.X,this.Z+2]=true;
        }        
      }
      // If pawn is not on the left edge of the board and pawn is not on the top edge of the board.
      if((this.X>0)&&(this.Z<7))
      {
        // Get piece on the left side one step forward.
        piece=pieces[this.X-1,this.Z+1];
        // If there is opossit piece.
        if((piece!=null)&&(!piece.Is_white))
        {
          // Add move as allowed.
          moves[this.X-1,this.Z+1]=true;
        }
      }
      // If pawn is not on the right edge of the board and pawn is not on the top edge of the board.
      if((this.X<7)&&(this.Z<7))
      {
        // Get piece on the right side one step forward.
        piece=pieces[this.X+1,this.Z+1];
        // If there is opossit piece.
        if((piece!=null)&&(!piece.Is_white))
        {
          // Add move as allowed.
          moves[this.X+1,this.Z+1]=true;
        }
      }
      // If left en passant.
      if((Pieces.Instance.enpassant_x==this.X-1)&&(Pieces.Instance.enpassant_z==this.Z+1))
      {
        // Add move as allowed.
        moves[Pieces.Instance.enpassant_x,Pieces.Instance.enpassant_z]=true;
      }
      // If right en passant.
      if((Pieces.Instance.enpassant_x==this.X+1)&&(Pieces.Instance.enpassant_z==this.Z+1))
      {
        // Add move as allowed.
        moves[Pieces.Instance.enpassant_x,Pieces.Instance.enpassant_z]=true;
      }
    }
    // If black pawn.
    else
    {
      // If pawn is not on the bottom edge of the board and there is no piece one step forward.
      if((this.Z>0)&&(pieces[this.X,this.Z-1]==null))
      {
        // Add move as allowed.
        moves[this.X,this.Z-1]=true;
      }
      // If pawn didn't move.
      if(this.Z==6)
      {
        // If there is no piece one step forward and two steps forward.
        if((pieces[this.X,this.Z-1]==null)&&(pieces[this.X,this.Z-2]==null))
        {
          // Add move as allowed.
          moves[this.X,this.Z-2]=true;
        }        
      }
      // If pawn is not on the left edge of the board and pawn is not on the bottom edge of the board.
      if((this.X>0)&&(this.Z>0))
      {
        // Get piece on the left side one step forward.
        piece=pieces[this.X-1,this.Z-1];
        // If there is opossit piece.
        if((piece!=null)&&(piece.Is_white))
        {
          // Add move as allowed.
          moves[this.X-1,this.Z-1]=true;
        }
      }
      // If pawn is not on the right edge of the board and pawn is not on the top edge of the board.
      if((this.X<7)&&(this.Z>0))
      {
        // Get piece on the right side one step forward.
        piece=pieces[this.X+1,this.Z-1];
        // If there is opossit piece.
        if((piece!=null)&&(piece.Is_white))
        {
          // Add move as allowed.
          moves[this.X+1,this.Z-1]=true;
        }
      }
      // If left en passant.
      if((Pieces.Instance.enpassant_x==this.X-1)&&(Pieces.Instance.enpassant_z==this.Z-1))
      {
        // Add move as allowed.
        moves[Pieces.Instance.enpassant_x,Pieces.Instance.enpassant_z]=true;
      }
      // If right en passant.
      if((Pieces.Instance.enpassant_x==this.X+1)&&(Pieces.Instance.enpassant_z==this.Z-1))
      {
        // Add move as allowed.
        moves[Pieces.Instance.enpassant_x,Pieces.Instance.enpassant_z]=true;
      }
    }

    // If piece should verify enemy moves.
    if(verify)
    {
      // Verify allowed moves vs enemy allowed moves. 
      MovesVerify(pieces,moves);
    }

    // Return array of allowed moves.
    return moves;
  } // End of MovesGet

  // Function that move a chess piece.
  public override bool Move(int x,int z,bool[,] moves,ref bool promote)
  {
    // If user clicked again on the same piece.
    if((this.X==x)&&(this.Z==z))
    {
      // Unselect of chess piece.
      UnSel();
      // Exit from function.
      return false;
    }
    // If given move is not allowed.
    if(!moves[x,z])
    {
      // Unselect of chess piece.
      UnSel();
      // Exit from function.
      return false;
    }
    // Get piece from new position. 
    Piece piece1 = Pieces.Instance[x,z];
    // If white Pawn.
    if(this.Is_white)
    {
      // If there is no piece on a new position.
      if(piece1==null)
      {
        // If it is en passant move.
        if((Pieces.Instance.enpassant_x==x)&&(Pieces.Instance.enpassant_z==z))
        {
          // Get en passant piece.
          piece1=Pieces.Instance[x,z-1];
          // Remove piece from chess board.
          piece1.Remove();
        }
      }
      // If there is a piece on a new position.
      else
      {
        // Remove piece from chess board.
        piece1.Remove();
      }
      // Clear last allowed en passant.
      Pieces.Instance.enpassant_x=-1;
      Pieces.Instance.enpassant_z=-1;
      // If white promotion.
      if(z==7)
      {
        // Change info about promotion.
        promote=true;
      }
      // If not white promotion.
      else
      {
        // If it is first move of white pawn two steps forward.
        if((this.Z==1)&&(z==3))
        {
          // Actualize information about allowed en passant.
          Pieces.Instance.enpassant_x=x;
          Pieces.Instance.enpassant_z=2;
        }
      }
    }
    // If black pawn.
    else
    {
      // If there is no piece on a new position.
      if(piece1==null)
      {
        // If it is en passant move.
        if((Pieces.Instance.enpassant_x==x)&&(Pieces.Instance.enpassant_z==z))
        {
          // Get en passant piece.
          piece1=Pieces.Instance[x,z+1];
          // Remove piece from chess board.
          piece1.Remove();
        }
      }
      // If there is a piece on a new position.
      else
      {
        // Remove piece from chess board.
        piece1.Remove();
      }
      // Clear last allowed en passant.
      Pieces.Instance.enpassant_x=-1;
      Pieces.Instance.enpassant_z=-1;
      // If black promotion.
      if(z==0)
      {
        // Change info about promotion.
        promote=true;
      }
      // If not black promotion.
      else
      {
        // If it is first move of black pawn two steps forward.
        if((this.Z==6)&&(z==4))
        {
          // Actualize information about allowed en passant.
          Pieces.Instance.enpassant_x=x;
          Pieces.Instance.enpassant_z=z+1;
        }
      }
    }
    // Actualize piece position.
    PosSet(x,z);
    // Exit from function.
    return true;
  } // End of move

} // End of Pawn