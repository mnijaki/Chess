// Class that implements manage of Rook piece.
public class Rook:Piece
{
  // Information if rook has moved.
  public bool has_moved;

  // Initialization of properties.
  public override void Init(bool is_white,int x,int y) 
  {
    // Base class initialization.
    base.Init(is_white,x,y);
    // Set information that rook has not moved.
    this.has_moved=false;
  } // End of Init

  // Function that return allowed moves.
  public override bool[,] MovesGet(Piece[,] pieces,bool verify)
  {
    // Array of allowed moves.
    bool[,] moves = new bool[8,8];

    // Chess piece.
    Piece piece;

    // Iterator.
    int i = this.X;

    // Loop on the left side.
    while(true)
    {
      // Actualize iterator.
      i--;
      // If out of board then break from loop.
      if(i<0)
      {
        break;
      }
      // Get piece on that position.
      piece=pieces[i,this.Z];
      // If there is a piece.
      if(piece!=null)
      {
        // If it is user own piece.
        if(piece.Is_white==this.Is_white)
        {
          // Exit from the loop.
          break;
        }
        // If it is user enemy piece. 
        else
        {
          // Add move as allowed.
          moves[i,this.Z]=true;
          // Exit from loop.
          break;
        }
      }
      // Add move as allowed.
      moves[i,this.Z]=true;
    }

    // Actualize iterator.
    i=this.X;

    // Loop on the right side.
    while(true)
    {
      // Actualize iterator.
      i++;
      // If out of board then break from loop.
      if(i>7)
      {
        break;
      }
      // Get piece on that position.
      piece=pieces[i,this.Z];
      // If there is a piece.
      if(piece!=null)
      {
        // If it is user own piece.
        if(piece.Is_white==this.Is_white)
        {
          // Exit from the loop.
          break;
        }
        // If it is user enemy piece. 
        else
        {
          // Add move as allowed.
          moves[i,this.Z]=true;
          // Exit from loop.
          break;
        }
      }
      // Add move as allowed.
      moves[i,this.Z]=true;
    }

    // Actualize iterator.
    i=this.Z;

    // Loop on the down side.
    while(true)
    {
      // Actualize iterator.
      i--;
      // If out of board then break from loop.
      if(i<0)
      {
        break;
      }
      // Get piece on that position.
      piece=pieces[this.X,i];
      // If there is a piece.
      if(piece!=null)
      {
        // If it is user own piece.
        if(piece.Is_white==this.Is_white)
        {
          // Exit from the loop.
          break;
        }
        // If it is user enemy piece. 
        else
        {
          // Add move as allowed.
          moves[this.X,i]=true;
          // Exit from loop.
          break;
        }
      }
      // Add move as allowed.
      moves[this.X,i]=true;
    }

    // Actualize iterator.
    i=this.Z;

    // Loop on the up side.
    while(true)
    {
      // Actualize iterator.
      i++;
      // If out of board then break from loop.
      if(i>7)
      {
        break;
      }
      // Get piece on that position.
      piece=pieces[this.X,i];
      // If there is a piece.
      if(piece!=null)
      {
        // If it is user own piece.
        if(piece.Is_white==this.Is_white)
        {
          // Exit from the loop.
          break;
        }
        // If it is user enemy piece. 
        else
        {
          // Add move as allowed.
          moves[this.X,i]=true;
          // Exit from loop.
          break;
        }
      }
      // Add move as allowed.
      moves[this.X,i]=true;
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
    // Actualize piece position.
    PosSet(x,z);
    // Actualize infromation about Rook movment.
    this.has_moved=true;
    // Exit from function.
    return true;
  } // End of Move

} // End of Rook