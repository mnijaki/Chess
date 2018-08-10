using System;
using System.Collections;
using UnityEngine;

// Class that has no objcets representation. Other chess piecess will extend from it.
public abstract class Piece:MonoBehaviour
{
  // Information if chess piece is white.
  private bool _is_white;
  public bool Is_white
  {
    get
    {
      return this._is_white;
    }
  }

  // Position of chess piece in x dimension. 
  private int _x;
  public int X
  {
    get
    {
      return this._x;
    }
  }

  // Position of chess piece in z dimension.
  private int _z;
  public int Z
  {
    get
    {
      return this._z;
    }
  }

  // Movment animation speed.
  private float move_anim_speed = 1.7f;

  // Initialization of properties.
  public virtual void Init(bool is_white,int x,int z)
  {
    // Actualize piece position.
    this._x=x;
    this._z=z;
    // Set side of piece.
    this._is_white=is_white;
    // Add chess piece to chess pieces array.
    Pieces.Instance[x,z]=this;
  } // End of Init

  // Function that set the position of chess piece.
  public void PosSet(int x,int z)
  {
    // Clear old position of chess piece.
    Pieces.Instance[this.X,this.Z]=null;
    // Actualize piece position.
    this._x=x;
    this._z=z;
    // Add piece to new position.
    Pieces.Instance[x,z]=this;
    // Actualize piece height and rotation.
    this.MoveAnim(new Vector3(0f,0f,0f),BoardManager.Instance.TileCenterGet(x,z));
    // Deactivate board highlights.
    BoardHighlights.Instance.HighlightsDeact();
  } // End of PosSet

  // Function that return array of allowed moves.
  public abstract bool[,] MovesGet(Piece[,] pieces,bool verify); // Enf of MovesGet

  // Function that verify allowed moves vs enemy allowed moves.
  protected virtual void MovesVerify(Piece[,] pieces,bool[,] moves)
  {
    // Get king position.
    int king_x = (this.Is_white) ? Pieces.Instance.white_king_x : Pieces.Instance.black_king_x;
    int king_z = (this.Is_white) ? Pieces.Instance.white_king_z : Pieces.Instance.black_king_z;

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
            if(moves_tmp[king_x,king_z])
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
  } // End of MovesVerify

  // Function that select a chess piece.
  public void Sel(bool[,] moves)
  {
    // Information if chess piece can move.
    bool can_move = false;
    // Loop of all chess piece allowed moves.
    for(int i = 0; i<8; i++)
    {
      for(int j = 0; j<8; j++)
      {
        // If chess piece can move on that position.
        if(moves[i,j])
        {
          // Actualize information if chess piece can move.
          can_move=true;
          // Exit from loop.
          i=8;
          j=8;
        }
      }
    }
    // If chess piece can't move then exit from function.
    if(!can_move)
    {
      return;
    }    
    // Highlight allowed moves of chess piece.
    BoardHighlights.Instance.MovesHighlight(moves);
    // Actualize piece height and rotation.
    this.MoveAnim(new Vector3((this.Is_white?40f:-40f),0f,0f),this.transform.position+new Vector3(0f,1f,0f));
    // Actualization of chess piece selection.
    Pieces.Instance.sel_piece=this;
  } // End of Sel

  // Function that unselect a chess piece.
  public void UnSel()
  {
    // Actualize piece height and rotation.
    this.MoveAnim(new Vector3(0f,0f,0f),this.transform.position-new Vector3(0f,this.transform.position.y,0f));
    // Deactivate highlights of chess piece allowed moves.
    BoardHighlights.Instance.HighlightsDeact();    
  } // End of UnSel

  // Function that move a chess piece.
  public virtual bool Move(int x,int z,bool[,] moves,ref bool promote)
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
    // Exit from function.
    return true;
  } // End of Move

  // Function that remove chess piece from board.
  public void Remove()
  {
    // Remove chess piece from board.
    Destroy(this.gameObject);
  } // End of Remove

  // Move animation.
  protected void MoveAnim(Vector3 end_angles,Vector3 end_pos)
  {
    // Set information that piece is moving.
    Pieces.Instance.is_piece_animating=true;
    // Calculate end rotation (made here to pass variable to coroutine, saves more calulation at ech coroutine).
    Quaternion end_rot = Quaternion.Euler(end_angles);
    // Animate move.
    StartCoroutine(this.MoveAnimStep(end_rot,end_pos));
  } // End of MoveAnim

  // Move animation step.
  protected IEnumerator MoveAnimStep(Quaternion end_rot, Vector3 end_pos)
  {
    // Start rotation.
    Quaternion start_rot=this.transform.rotation;
    // Start position.
    Vector3 start_pos=this.transform.position;
    // Non smoothed step.
    float step = 0.0f; 
    // Smoothed step.
    float smooth_step = 0.0f;
    // While step is < 1.0 (animation is still processing).
    while(step<1.0)
    {
      // Actualize step.
      step+=Time.deltaTime*this.move_anim_speed;
      // Actualize smoothed step.
      smooth_step=Mathf.SmoothStep(0.0f,1.0f,step);
      // Transform position.
      this.transform.position=Vector3.Slerp(start_pos,end_pos,smooth_step);
      // Transform rotation.
      this.transform.rotation=Quaternion.Slerp(start_rot,end_rot,smooth_step);
      // Return to coroutine.
      yield return null;
    }
    // Transform position.
    this.transform.position=end_pos;
    // Transform rotation.
    this.transform.rotation=end_rot;
    // Set information that piece is not moving.
    Pieces.Instance.is_piece_animating=false;
  } // End of MoveAnimStep

} // End of piece