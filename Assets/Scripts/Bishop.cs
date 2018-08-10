// Class that implements manage of Bishop piece.
public class Bishop:Piece
{
  // Function that return allowed moves.
  public override bool[,] MovesGet(Piece[,] pieces,bool verify)
  {
    // Array of allowed moves.
    bool[,] moves = new bool[8,8];

    // Chess piece.
    Piece piece;

    // Iterators.
    int i= this.X;
    int j= this.Z;

    // Loop on the up left side.
    while(true)
    {
      // Actualize iterators.
      i--;
      j++;
      // If out from ofthen break from loop.
      if((i<0)||(j>7))
      {
        break;
      }
      // Get piece on that position.
      piece=pieces[i,j];
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
          moves[i,j]=true;
          // Exit from loop.
          break;
        }
      }
      // Add move as allowed.
      moves[i,j]=true;
    }

    // Actualize iterators.
    i=this.X;
    j=this.Z;

    // Loop on the up right side.
    while(true)
    {
      // Actualize iterators.
      i++;
      j++;
      // If out from ofthen break from loop.
      if((i>7)||(j>7))
      {
        break;
      }
      // Get piece on that position.
      piece=pieces[i,j];
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
          moves[i,j]=true;
          // Exit from loop.
          break;
        }
      }
      // Add move as allowed.
      moves[i,j]=true;
    }

    // Actualize iterators.
    i=this.X;
    j=this.Z;

    // Loop on the down left side.
    while(true)
    {
      // Actualize iterators.
      i--;
      j--;
      // If out from ofthen break from loop.
      if((i<0)||(j<0))
      {
        break;
      }
      // Get piece on that position.
      piece=pieces[i,j];
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
          moves[i,j]=true;
          // Exit from loop.
          break;
        }
      }
      // Add move as allowed.
      moves[i,j]=true;
    }

    // Actualize iterators.
    i=this.X;
    j=this.Z;

    // Loop on the down right side.
    while(true)
    {
      // Actualize iterators.
      i++;
      j--;
      // If out from ofthen break from loop.
      if((i>7)||(j<0))
      {
        break;
      }
      // Get piece on that position.
      piece=pieces[i,j];
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
          moves[i,j]=true;
          // Exit from loop.
          break;
        }
      }
      // Add move as allowed.
      moves[i,j]=true;
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

} // End of Bishop