// Class that implements manage of Knight piece.
public class Knight:Piece
{
  // Function that return allowed moves.
  public override bool[,] MovesGet(Piece[,] pieces,bool verify)
  {
    // Array of allowed moves.
    bool[,] moves = new bool[8,8];

    // Up left 1.
    MoveSet(this.X-1,this.Z+2,pieces,moves);

    // Up left 2.
    MoveSet(this.X-2,this.Z+1,pieces,moves);

    // Up right 1.
    MoveSet(this.X+1,this.Z+2,pieces,moves);

    // Up right 2.
    MoveSet(this.X+2,this.Z+1,pieces,moves);

    // Down left 1.
    MoveSet(this.X-1,this.Z-2,pieces,moves);

    // Down left 2.
    MoveSet(this.X-2,this.Z-1,pieces,moves);

    // Down right 1.
    MoveSet(this.X+1,this.Z-2,pieces,moves);

    // Down right 2.
    MoveSet(this.X+2,this.Z-1,pieces,moves);

    // If piece should verify enemy moves.
    if(verify)
    {
      // Verify allowed moves vs enemy allowed moves. 
      MovesVerify(pieces,moves);
    }

    // Return array of allowed moves.
    return moves;
  } // End of MovesGet

  // Function that check if given knight move is alloowed.
  public void MoveSet(int x,int z,Piece[,] pieces, bool[,] moves)
  {
    // If move is on the board.
    if((x>-1)&&(x<8)&&(z>-1)&&(z<8))
    {
      // Get piece on that position.
      Piece piece=pieces[x,z];
      // If there is no piece.
      if(piece==null)
      {
        // Add move as allowed.
        moves[x,z]=true;
      }
      // If there is a piece.
      else
      {
        // If not user own piece.
        if(this.Is_white!=piece.Is_white)
        {
          // Add move as allowed.
          moves[x,z]=true;
        }
      }
    }
  } // End of MoveSet

} // End of Knight