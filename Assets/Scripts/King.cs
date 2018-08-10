using System;

// Class that implements manage of King piece.
public class King:Piece
{
  // Information if king has moved.
  private bool has_moved;

  // Initialization of properties.
  public override void Init(bool is_white,int x,int y)
  {
    // Base class initialization.
    base.Init(is_white,x,y);
    // Set information that king has not moved.
    this.has_moved=false;
  } // End of Init

  // Function that return allowed moves.
  public override bool[,] MovesGet(Piece[,] pieces,bool verify)
  {
    // Array of allowed moves.
    bool[,] moves = new bool[8,8];

    // Chess piece.
    Piece piece;

    // If king didn't move.
    if(!this.has_moved)
    {
      // Left side castling.
      if((pieces[this.X-1,this.Z]==null)&&(pieces[this.X-2,this.Z]==null)&&(pieces[this.X-3,this.Z]==null))
      {
        // Get piece.
        piece=pieces[this.X-4,this.Z];
        // If piece is Rook of the same color.
        if((piece!=null)&&(piece.GetType()==typeof(Rook))&&(piece.Is_white==this.Is_white))
        {
          // If Rook didn't move.
          if(!((Rook)piece).has_moved)
          {
            // King can do castling.
            moves[this.X-2,this.Z]=true;
          }
        }
      }
      // Right side castling.
      if((pieces[this.X+1,this.Z]==null)&&(pieces[this.X+2,this.Z]==null))
      {
        // Get piece.
        piece=pieces[this.X+3,this.Z];
        // If piece is Rook of the same color.
        if((piece!=null)&&(piece.GetType()==typeof(Rook))&&(piece.Is_white==this.Is_white))
        {
          // If Rook didn't move.
          if(!((Rook)piece).has_moved)
          {
            // King can do castling.
            moves[this.X+2,this.Z]=true;
          }
        }
      }
    }

    // Iterators.
    int k;
    int i = this.X-1;
    int j = this.Z+1;

    // If not out of board.
    if(j<8)
    {
      // Loop on the upper side.
      for(k=0; k<3; k++)
      {
        // If not out of board.
        if((i>-1)&&(i<8))
        {
          // Get piece on that position.
          piece=pieces[i,j];
          // If there is no piece.
          if(piece==null)
          {
            // Add move as allowed.
            moves[i,j]=true;
          }
          // If there is piece.
          else
          {
            // If it is not user own piece.
            if(piece.Is_white!=this.Is_white)
            {
              // Add move as allowed.
              moves[i,j]=true;
            }
          }
        }
        // Actualize of iterator.
        i++;
      }
    }

    // Actualize of iterators.
    k=0;
    i=this.X-1;
    j=this.Z-1;

    // If not out of board.
    if(j>-1)
    {
      // Loop on the bottom side.
      for(k=0; k<3; k++)
      {
        // If king is not out of board.
        if((i>-1)&&(i<8))
        {
          // Get piece on that position.
          piece=pieces[i,j];
          // If there is no piece.
          if(piece==null)
          {
            // Add move as allowed.
            moves[i,j]=true;
          }
          // If there is piece.
          else
          {
            // If it is not user own piece.
            if(piece.Is_white!=this.Is_white)
            {
              // Add move as allowed.
              moves[i,j]=true;
            }
          }
        }
        // Actualize of iterator.
        i++;
      }
    }

    // Actualize of iterator.
    i=this.X-1;

    // If not out of board.
    if(i>-1)
    {
      // Get piece on that position.
      piece=pieces[i,this.Z];
      // If there is no piece.
      if(piece==null)
      {
        // Add move as allowed.
        moves[i,this.Z]=true;
      }
      // If there is piece.
      else
      {
        // If it is not user own piece.
        if(piece.Is_white!=this.Is_white)
        {
          // Add move as allowed.
          moves[i,this.Z]=true;
        }
      }
    }

    // Actualize of iterator.
    i=this.X+1;

    // If not out of board.
    if(i<8)
    {
      // Get piece on that position.
      piece=pieces[i,this.Z];
      // If there is no piece.
      if(piece==null)
      {
        // Add move as allowed.
        moves[i,this.Z]=true;
      }
      // If there is piece.
      else
      {
        // If it is not user own piece.
        if(piece.Is_white!=this.Is_white)
        {
          // Add move as allowed.
          moves[i,this.Z]=true;
        }
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

  // Function that verify allowed moves vs enemy allowed moves.
  protected override void MovesVerify(Piece[,] pieces,bool[,] moves)
  {
    // If king didn't move and king can do castling.
    if((!this.has_moved)&&((moves[this.X-2,this.Z])||(moves[this.X+2,this.Z])))
    {
      // Loop of all enemy chess pieces.
      for(int k=0; k<8; k++)
      {
        for(int l = 0; l<8; l++)
        {
          // Get piece from position.
          Piece piece=pieces[k,l];
          // If there is no piece on that position or if it is player piece then skip loop.
          if((piece==null)||(piece.Is_white==this.Is_white))
          {
            continue;
          }
          // Get enemy chess piece allowed moves.
          bool[,] moves_tmp = piece.MovesGet(pieces,false);
          // If enemy chess piece can move to player king position.
          if(moves_tmp[this.X,this.Z])
          {
            // Mark both castling as not allowed.
            moves[this.X-2,this.Z]=false;
            moves[this.X+2,this.Z]=false;
          }
          // If enemy piece interrupt with left side castling.
          if((moves_tmp[this.X-1,this.Z])||(moves_tmp[this.X-2,this.Z])||(moves_tmp[this.X-3,this.Z])||(moves_tmp[this.X-4,this.Z]))
          {
            // Mark left side castling as not allowed.
            moves[this.X-2,this.Z]=false;
          }
          // If enemy piece interrupt with right side castling.
          if((moves_tmp[this.X+1,this.Z])||(moves_tmp[this.X+2,this.Z])||(moves_tmp[this.X+3,this.Z]))
          {
            // Mark right side castling as not allowed.
            moves[this.X+2,this.Z]=false;
          }
          // If king now can't do castling then break from loop.
          if((!moves[this.X-2,this.Z])&&(!moves[this.X+2,this.Z]))
          {
            // Break from loop.
            k=8;
            l=8;
          }
        }
      }
    }

    // Loop of chess piece moves.
    for(int i = 0; i<8; i++)
    {
      for(int j = 0; j<8; j++)
      {
        // If chess piece can't move on that position then skip loop.
        if(!moves[i,j])
        {
          continue;
        }
        // Create new temporary array of chess pieces.
        Piece[,] pieces_tmp = new Piece[8,8];
        // Copy all chess pieces to temporary array.
        Array.Copy(pieces,0,pieces_tmp,0,pieces.Length);
        // Temporary move chess piece.
        pieces_tmp[i,j]=pieces_tmp[this.X,this.Z];
        // Clear old position of chess piece in temporary array.
        pieces_tmp[this.X,this.Z]=null;
        // Loop of all chess pieces from temporary array.
        for(int k = 0; k<8; k++)
        {
          for(int l = 0; l<8; l++)
          {
            // Get piece from position.
            Piece piece_tmp = pieces_tmp[k,l];
            // If there is no piece on that position or if it is player piece then skip loop.
            if((piece_tmp==null)||(piece_tmp.Is_white==this.Is_white))
            {
              continue;
            }
            // Get enemy chess piece allowed moves.
            bool[,] moves_tmp = piece_tmp.MovesGet(pieces_tmp,false);
            // If enemy chess piece can move to player king position.
            if(moves_tmp[i,j])
            {
              // Mark move as not allowed.
              moves[i,j]=false;
              // Exit from all chess piece loop.
              k=8;
              l=8;
            }
          }
        }
      }
    }
  } // End of MoevsVerify

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
    // Clear last allowed en passant.
    Pieces.Instance.enpassant_x=-1;
    Pieces.Instance.enpassant_z=-1;
    // Get piece from new position. 
    Piece piece = Pieces.Instance[x,z];
    // If there is a piece on a new position.
    if(piece!=null)
    {
      // Remove piece from chess board.
      piece.Remove();
    }
    // Castling rook.
    Rook castling_rook=null; 
    // If king didn't move.
    if(this.has_moved==false)
    {
      // If left side castling.
      if(x==this.X-2)
      {
        // Actualize information about castling rook.
        castling_rook=(Rook)Pieces.Instance[this.X-4,this.Z];
      }
      // If not left side castling.
      else
      {
        // If right side castling.
        if(x==this.X+2)
        {
          // Actualize information about castling rook.
          castling_rook=(Rook)Pieces.Instance[this.X+3,this.Z];
        }
      }
    }
    // Actualize piece position.
    PosSet(x,z);
    // Actualize infromation about King movment.
    this.has_moved=true;
    // If white king.
    if(this.Is_white)
    {
      // Actualize information about king position.
      Pieces.Instance.white_king_x=x;
      Pieces.Instance.white_king_z=z;
    }
    // If black king.
    else
    {
      // Actualize information about king position.
      Pieces.Instance.black_king_x=x;
      Pieces.Instance.black_king_z=z;
    }
    // If castling.
    if(castling_rook!=null)
    {
      // Calculate new Rook X position.
      int castling_rook_x=(castling_rook.X==0?3:5);
      // Actualize piece position.
      castling_rook.PosSet(castling_rook_x,z);
      // Actualize infromation about Rook movment.
      castling_rook.has_moved=true;
    }
    // Exit from function.
    return true;
  } // End of move

} // End of King