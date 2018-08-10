using UnityEngine;

public class Pieces : MonoBehaviour
{
  // Chess pieces elements indexes.
  private const int KING_ELEM_IDX = 0;
  private const int QUEEN_ELEM_IDX = 1;
  private const int ROOK_ELEM_IDX = 2;
  private const int BISHOP_ELEM_IDX = 3;
  private const int KNIGHT_ELEM_IDX = 4;
  private const int PAWN_ELEM_IDX = 5;

  // Information if piece animation is doing.
  public bool is_piece_animating;

  // Currently selected piece.
  public Piece sel_piece;

  // Kings positions.
  public int white_king_x;
  public int white_king_z;
  public int black_king_x;
  public int black_king_z;

  // Allowed en passant.
  public int enpassant_x;
  public int enpassant_z;

  // Instance of 'Pieces'.
  private static Pieces _instance;
  public static Pieces Instance
  {
    get
    {
      return Pieces._instance;
    }
  }

  // Array of chess pieces.
  private Piece[,] pieces;

  // Indexer of chess pieces array.
  public Piece this[int i,int j]
  {
    get
    {
      return this.pieces[i,j];
    }

    set
    {
      this.pieces[i,j]=value;
    }
  }

  // Function that initialize object.
  private void Awake()
  {
    // Initialize 'Pieces' class.
    Pieces._instance=this;
  } // End of Awake

  // Function that return array of chess pieces.
  public Piece[,] Get()
  {
    return this.pieces;
  } // End of Get

  // King initialization.
  public void KingInit(bool is_white,int x,int z)
  {
    this.PieceInit(is_white,KING_ELEM_IDX,x,z);
  } // End of KingInit

  // Queen initialization.
  public void QueenInit(bool is_white,int x,int z)
  {
    this.PieceInit(is_white,QUEEN_ELEM_IDX,x,z);
  } // End of QueenInit

  // Rook initialization.
  public void RookInit(bool is_white,int x,int z)
  {
    this.PieceInit(is_white,ROOK_ELEM_IDX,x,z);
  } // End of RookInit

  // Bishop initialization.
  public void BishopInit(bool is_white,int x,int z)
  {
    this.PieceInit(is_white,BISHOP_ELEM_IDX,x,z);
  } // End of BishopInit

  // Knight initialization.
  public void KnightInit(bool is_white,int x,int z)
  {
    this.PieceInit(is_white,KNIGHT_ELEM_IDX,x,z);
  } // End of KnightInit

  // Pawn initialization.
  public void PawnInit(bool is_white,int x,int z)
  {
    this.PieceInit(is_white,PAWN_ELEM_IDX,x,z);
  } // End of PawnInit

  // Pawn promotion.
  public void PawnPromote(bool is_white,int x,int z)
  {
    // Remove selected piece from chess board.
    this.sel_piece.Remove();
    // Deactivate highlights of chess piece allowed moves.
    BoardHighlights.Instance.HighlightsDeact();
    // Spawn Queen. TO_DO: giva a dialog with piece to choose for promotion(except king).
    this.QueenInit(is_white,x,z);
    // Set information that piece is not moving.
    this.is_piece_animating=false;
  } // End of PawnPromote

  // Pieces initialization.
  public void Init(int mode)
  {
    // Set information that piece is not animating.
    this.is_piece_animating=false;
    // Set information about chess piece selection.
    this.sel_piece=null;
    // Initialization of chess pieces array.
    this.pieces=new Piece[8,8];  
    // Depending on mode.
    switch(mode)
    {
      // Standard chess pieces initialization.
      case 1:
      {
        // Standard chess pieces initialization. 
        PiecesInit();
        // Break.
        break;
      }
      // Special chess pieces initialization (initialize pieces at specific tiles).
      case 2:
      {
        // Special chess pieces initialization (initialize pieces at specific tiles).
        SpecPiecesInit();
        // Break.
        break;
      }
      // User chess pieces initialization (initialize pieces at tiles specified by user).
      case 3:
      {
        // User chess pieces initialization (initialize pieces at tiles specified by user).
        UsrPiecesInit();
        // Break.
        break;
      }
    }
  } // End of Init

  // Chess piece initialization.
  private void PieceInit(bool is_white,int idx,int x,int z)
  {
    // Create a GameObject variable.
    GameObject go = null;
    GameObject con = null;
    // Depending on piece type index.
    switch(idx)
    {
      case KING_ELEM_IDX:
      {
        // Create contener.
        con=new GameObject("king_con",typeof(King));
        // Create a copy of prefabricate element.
        go=Instantiate((GameObject)Resources.Load("GameObjects/Pieces/king"));
        // If white king.
        if(is_white)
        {
          // Actualize information about king position.
          this.white_king_x=x;
          this.white_king_z=z;
        }
        // If black king.
        else
        {
          // Actualize information about king position.
          this.black_king_x=x;
          this.black_king_z=z;
        }        
        // Break.
        break;
      }
      case QUEEN_ELEM_IDX:
      {
        // Create contener.
        con=new GameObject("queen_con",typeof(Queen));
        // Create a copy of prefabricate element.
        go=Instantiate((GameObject)Resources.Load("GameObjects/Pieces/queen"));
        // Break.
        break;
      }
      case ROOK_ELEM_IDX:
      {
        // Create contener.
        con=new GameObject("rook_con",typeof(Rook));
        // Create a copy of prefabricate element.
        go=Instantiate((GameObject)Resources.Load("GameObjects/Pieces/rook"));
        // Break.
        break;
      }
      case BISHOP_ELEM_IDX:
      {
        // Create contener.
        con=new GameObject("bishop_con",typeof(Bishop));
        // Create a copy of prefabricate element.
        go=Instantiate((GameObject)Resources.Load("GameObjects/Pieces/bishop"));
        // Break.
        break;
      }
      case KNIGHT_ELEM_IDX:
      {
        // Create contener.
        con=new GameObject("knight_con",typeof(Knight));        
        // Create a copy of prefabricate element.
        go=Instantiate((GameObject)Resources.Load("GameObjects/Pieces/knight"));
        // Rotate game object.
        go.transform.Rotate(0,(is_white)?-90:90,0);
        // Break.
        break;
      }
      case PAWN_ELEM_IDX:
      {
        // Create contener.
        con=new GameObject("pawn_con",typeof(Pawn));
        // Create a copy of prefabricate element.
        go=Instantiate((GameObject)Resources.Load("GameObjects/Pieces/Pawn"));
        // Break.
        break;
      }
    }
    // If white piece.
    if(is_white)
    {
      // Set new Material to a Piece (must be in folder 'Resources').
      go.GetComponent<MeshRenderer>().material=Resources.Load("Materials/gold",typeof(Material)) as Material;
    }
    // If black piece.
    else
    {
      // Set new Material to a Piece (must be in folder 'Resources').
      go.GetComponent<MeshRenderer>().material=Resources.Load("Materials/black_and_wood",typeof(Material)) as Material;
    }
    // Set transform parent of contener to board.
    con.transform.SetParent(this.transform);
    // Set position of contener.
    con.transform.position=BoardManager.Instance.TileCenterGet(x,z);
    // Initialize piece.
    con.GetComponent<Piece>().Init(is_white,x,z);
    // Set transform parent of piece to contener.
    go.transform.SetParent(con.transform,false);
  } // End of PieceInit

  // Standard chess pieces initialization.
  private void PiecesInit()
  {
    // White king.
    KingInit(true,4,0);
    // White queen.
    QueenInit(true,3,0);
    // White rook.
    RookInit(true,0,0);
    // White rook.
    RookInit(true,7,0);
    // White bishop.
    BishopInit(true,2,0);
    // White bishop.
    BishopInit(true,5,0);
    // White knight.
    KnightInit(true,1,0);
    // White knight.
    KnightInit(true,6,0);
    // White pawns.
    for(int i = 0; i<8; i++)
    {
      PawnInit(true,i,1);
    }
    // Black king.
    KingInit(false,4,7);
    // Black queen.
    QueenInit(false,3,7);
    // Black rook.
    RookInit(false,0,7);
    // Black rook.
    RookInit(false,7,7);
    // Black bishop.
    BishopInit(false,2,7);
    // Black bishop.
    BishopInit(false,5,7);
    // Black knight.
    KnightInit(false,1,7);
    // Black knight.
    KnightInit(false,6,7);
    // Black pawns.
    for(int i = 0; i<8; i++)
    {
      PawnInit(false,i,6);
    }
  } // End of PiecesInit

  // Special chess pieces initialization (initialize pieces at specific tiles).
  private void SpecPiecesInit()
  {
    // White king.
    KingInit(true,4,0);
    // White queen.
    QueenInit(true,3,0);
    // White rook.
    RookInit(true,0,0);
    // White rook.
    RookInit(true,7,0);
    // White bishop.
    BishopInit(true,2,0);
    // White bishop.
    BishopInit(true,5,0);
    // White knight.
    KnightInit(true,1,0);
    // White knight.
    KnightInit(true,6,0);
    // White pawns.
    for(int i = 0; i<8; i++)
    {
      //PawnInit(true,i,1);
    }
    // Black king.
    KingInit(false,4,7);
    // Black queen.
    QueenInit(false,3,7);
    // Black rook.
    RookInit(false,0,7);
    // Black rook.
    RookInit(false,7,7);
    // Black bishop.
    BishopInit(false,2,7);
    // Black bishop.
    BishopInit(false,5,7);
    // Black knight.
    KnightInit(false,1,7);
    // Black knight.
    KnightInit(false,6,7);
    // Black pawns.
    for(int i = 0; i<8; i++)
    {
      //PawnInit(false,i,6);
    }
  } // End of SpecPiecesInit

  // User chess pieces initialization (initialize pieces at tiles specified by user).
  private void UsrPiecesInit()
  {
    // TO_DO: create this function.
  } // End of UsrPiecesInit

  // Clear all game objects.
  public void Clear()
  {
    // Clear of all game objects.
    foreach(Piece piece in this.pieces)
    {
      // If there is no piece on that position then skip loop step.
      if(piece==null)
      {
        continue;
      }
      // Destroy GameObject.
      Destroy(piece.gameObject);
    }
  } // End of Clear

} // End of Pieces
