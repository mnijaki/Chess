// Class that implements manage of Queen piece.
public class Queen:Piece
{
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

    // Iterators.
    i = this.X;
    int j = this.Z;

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

} // End of Queen