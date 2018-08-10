using System;
using System.Collections;          // Define various collections of objects, such as lists, queues, etc.
using UnityEngine;                 // Unity engine.

// Class that implements managing of the chessboard.
public class BoardManager:MonoBehaviour
{
  // ***************************************************************************************
  //                                      TO_DO:
  // # Dorobic dialog wyboru figury ktora chce sie awansowac (moze byc nawet 9 hetmanow)
  // # dodac ladny dialog na zakonczenie gry
  // # zrobic menu
  //   - zakonczenia gry (poddanie sie lub ogłoszenie remisu)
  //   - ustawienia (rozdzielczosc, kolor pionkow/szachownicy itp)
  //   - niestandardowe ustawienie pionków
  //   - włączenie/wyłączenie rotacji kamery
  //   - ustawienie szybkosci animacji kamery oraz ruchu pionków
  // # Dodatkowe opcje zakończenia gry:
  //   - jeżeli pozycja na szachownicy powtórzyła się trzy razy
  //   - jeżeli na szachownicy przez ostatnie 30 posunięć nie wykonano ani jednego posunięcia pionkiem oraz nie zbito żadnej bierki
  //   - jeden z partnerów szachuje cały czas króla, a przeciwnik nie ma możliwości uciec od szachów (tak zwany 'wieczny szach')
  // # dodaj oswietlenie z wielu stron z zoltym swiatlem
  // # podniesiona bierka mogla by sie lekko kolysac w gore w dol
  //
  // # bezparametrowy indexer?
  // # jak sie zazwyczaj robi odwolania do metod klasy z innej klasy (jezeli nie posiad ona zmiennej przechowujacej klasy z zadana metoda)?
  // # czy moglbym jakos odwolywac sie poprzez 'Pieces[i,j]' zamiast 'Pieces.Instance[i,j]'?
  // # piece.GetType()==typeof(Knight) w switchu? jakies pole w Piece ktore by przechowywalo indeks?
  // ***************************************************************************************

  // Tiles size and offset.
  private const float TILE_SIZE= 1.0f;
  private const float TILE_OFFSET=0.5f;

  // Array of cached allowed moves.
  private bool[,][,] moves;

  // Information of player turn.
  private bool is_white_turn;

  // Movment animation speed.
  private float camera_anim_speed;

  // Instance of 'BoardManager'.
  private static BoardManager _instance;
  public static BoardManager Instance
  {
    get
    {
      return BoardManager._instance;
    }
  }

  // Function that returns tile center.
  public Vector3 TileCenterGet(int x,int z)
  {
    // Return tile coordinates.
    return new Vector3((TILE_SIZE*x)+TILE_OFFSET,0,(TILE_SIZE*z)+TILE_OFFSET);
  } // End of TileCenterGet
  
  // Function that manage chess piece selection.
  public void PieceSelManage()
  {
    // If user didn't clicked on the left mouse button then exit from function.
    if(!Input.GetMouseButtonDown(0))
    {
      return;
    }
    // If piece is animating then exit from function.
    if(Pieces.Instance.is_piece_animating)
    {
      return;
    }
    // Ray to the mouse position.
    RaycastHit pos_info;
    // If there is a ray from main camera to mouse position ('board_layer' trigger a collider generate hit, when ray hits 
    // elemant called 'board_plane' -> because it have 'board_layer' attached).
    if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out pos_info,25.0f,LayerMask.GetMask("board_layer")))
    {      
      // Get tile coordinates.
      int x = (int)pos_info.point.x;
      int z = (int)pos_info.point.z;      
      // If there is no selected chess piece.
      if(Pieces.Instance.sel_piece==null)
      {
        // Get piece from position.
        Piece piece = Pieces.Instance[x,z];
        // If there is no chess piece on that position then exit from the function.
        if(piece==null)
        {
          return;
        }
        // If user try to select enemy chess piece then exit from the function.
        if(piece.Is_white!=this.is_white_turn)
        {
          return;
        }
        // If piece allowed moves was cached.
        if(this.moves[x,z]!=null)
        {
          // Select a chess piece.
          piece.Sel(this.moves[x,z]);
        }
        // If piece allowed moves was not cached.
        else
        {
          // Cache allowed moves.
          this.moves[x,z]=piece.MovesGet(Pieces.Instance.Get(),true);
          // Select a chess piece.
          piece.Sel(this.moves[x,z]);
        }
      }
      // If there is selected chess piece.
      else
      {
        // Info if chess piece should be promoted.
        bool promote = false;
        // Move chess piece.
        if(Pieces.Instance.sel_piece.Move(x,z,this.moves[Pieces.Instance.sel_piece.X,Pieces.Instance.sel_piece.Z],ref promote))
        {
          // If piece should be promoted (for 'Pawn' only).
          if(promote)
          {
            // Pawn promotion.
            Pieces.Instance.PawnPromote(this.is_white_turn,x,z);
          }
          // If game has ended.
          if(GameEndCheck())
          {
            // Return from function.
            return;
          }
          // Change turn.
          TurnChange();
        }
        // Unselect of chess piece.
        Pieces.Instance.sel_piece=null;
      }      
    }
    // If user clicked outside of chess board.
    else
    {
      // If there is selected chess piece.
      if(Pieces.Instance.sel_piece!=null)
      {
        // Unselect chess piece.
        Pieces.Instance.sel_piece.UnSel();
        // Unselect of chess piece.
        Pieces.Instance.sel_piece=null;
      }
    }
  } // End of PieceSelManage

  // Camera animation.
  public void CameraAnim(Quaternion end_rot,Vector3 end_pos)
  {
    StartCoroutine(this.CameraAnimStep(end_rot,end_pos));
  } // End of CameraAnim

  // Camera animation step.
  public IEnumerator CameraAnimStep(Quaternion end_rot,Vector3 end_pos)
  {
    // Start rotation.
    Quaternion start_rot = Camera.main.transform.rotation;
    // Start position.
    Vector3 start_pos = Camera.main.transform.position;
    // Non smoothed step.
    float step = 0.0f;
    // Smoothed step.
    float smooth_step = 0.0f;
    // While step is < 1.0 (animation is still processing).
    while(step<1.0)
    {
      // Actualize step.
      step+=Time.deltaTime*this.camera_anim_speed;
      // Actualize smoothed step.
      smooth_step=Mathf.SmoothStep(0.0f,1.0f,step);
      // Transform position.
      Camera.main.transform.position=Vector3.Slerp(start_pos,end_pos,smooth_step);
      // Transform rotation.
      Camera.main.transform.rotation=Quaternion.Slerp(start_rot,end_rot,smooth_step);
      // Return to coroutine.
      yield return null;
    }
    // Transform position.
    Camera.main.transform.position=end_pos;
    // Transform rotation.
    Camera.main.transform.rotation=end_rot;
  } // End of CameraAnimStep

  // Function that change turn.
  private void TurnChange()
  {
    // Clear cached allowed moves.
    Array.Clear(this.moves,0,this.moves.Length);
    // Change the turn.
    this.is_white_turn=!this.is_white_turn;
    // Change camera angle and position.
    if(this.is_white_turn)
    {
      CameraAnim(Quaternion.Euler(55f,0f,0f),new Vector3(4f,5.5f,-1f));
    }
    else
    {
      CameraAnim(Quaternion.Euler(55f,-180f,0f),new Vector3(4f,5.5f,9f));
    }
  } // End of TurnChange

  // Function that check if game has to end.
  private bool GameEndCheck()
  {
    // 0 - Game not ended.
    // 1 - Game ended with pat.
    // 2 - Game ended with draw (players don't have minimum pieces to make check mate).
    // 3 - Game ended with check mate.

    // Information about chess pieces.
    bool is_other_pieces = false;
    int white_knights = 0;
    int white_bishops = 0;
    int black_knights = 0;
    int black_bishops = 0;

    // Piece variable.
    Piece piece = null;

    // Loop of all chess pieces (get information about minimum pieces to win game).
    for(int i = 0; i<8; i++)
    {
      for(int j = 0; j<8; j++)
      {
        // Get piece from position.
        piece = Pieces.Instance[i,j];
        // If there is no piece on that position.
        if(piece==null)
        {
          continue;
        }
        // If piece is a knight.
        if(piece.GetType()==typeof(Knight))
        {
          // If white piece.
          if(piece.Is_white)
          {
            // Actualize information about knights.
            white_knights++;
          }
          // If black piece.
          else
          {
            // Actualize information about knights.
            black_knights++;
          }
        }
        // If not knight.
        else
        {
          // If piece is a bishop.
          if(piece.GetType()==typeof(Bishop))
          {
            // If white piece.
            if(piece.Is_white)
            {
              // Actualize information about bishops.
              white_bishops++;
            }
            // If black piece.
            else
            {
              // Actualize information about bishops.
              black_bishops++;
            }
          }
          // If not bishop.
          else
          {
            // If not a king.
            if(piece.GetType()!=typeof(King))
            {
              // Actualize information about other piece.
              is_other_pieces=true;
              // End loop.
              i=8;
              j=8;
            }
          }
        }
      }
    }
    // If there is no other pieces than king, knight and bishop.
    if(!is_other_pieces)
    {
      // If there are only kings.
      if((white_knights==0)&&(black_knights==0)&&(white_bishops==0)&&(black_bishops==0))
      {
        // End game with draw.
        GameEnd(2);
        // Return information that game has ended.
        return true;
      }
      // If there are kings and only one light piece.
      if(((white_knights+white_bishops==1)&&(black_knights+black_bishops==0))||
         ((white_knights+white_bishops==0)&&(black_knights+black_bishops==1)))
      {
        // End game with draw.
        GameEnd(2);
        // Return information that game has ended.
        return true;
      }
      // If there are kings and only two knights.
      if(((white_bishops==0)&&(black_bishops==0))&&(((white_knights==2)&&(black_knights==0))||((white_knights==0)&&(black_knights==2))))
      {
        // End game with draw.
        GameEnd(2);
        // Return information that game has ended.
        return true;
      }
    }

    // Loop of all enemy chess pieces (checks if enemy has at least one move).
    for(int i = 0; i<8; i++)
    {
      for(int j = 0; j<8; j++)
      {
        // Get piece from position.
        piece = Pieces.Instance[i,j];
        // If there is no piece on that position or if it is player piece then skip loop.
        if((piece==null)||(piece.Is_white==this.is_white_turn))
        {
          continue;
        }
        // Get enemy chess piece allowed moves.
        bool[,] moves_tmp = piece.MovesGet(Pieces.Instance.Get(),true);
        // Loop of enemy chess piece allowed moves.
        for(int k = 0; k<8; k++)
        {
          for(int l = 0; l<8; l++)
          {
            // If enemy chess piece can move.
            if(moves_tmp[k,l])
            {
              // Return information that game has not ended.
              return false;
            }
          }
        }
      }
    }

    // Get enemy king position.
    int king_x = (this.is_white_turn) ? Pieces.Instance.black_king_x : Pieces.Instance.white_king_x;
    int king_z = (this.is_white_turn) ? Pieces.Instance.black_king_z : Pieces.Instance.white_king_z;

    // Loop of all player chess pieces (checks if current player is checking enemy king).
    for(int i = 0; i<8; i++)
    {
      for(int j = 0; j<8; j++)
      {
        // Get piece from position.
        piece = Pieces.Instance[i,j];
        // If there is no piece on that position or if it is enemy piece then skip loop.
        if((piece==null)||(piece.Is_white!=this.is_white_turn))
        {
          continue;
        }
        // Get player chess piece allowed moves.
        bool[,] moves_tmp = piece.MovesGet(Pieces.Instance.Get(),false);
        // If player can move to enemy king position.
        if(moves_tmp[king_x,king_z])
        {
          // End game with check mate.
          GameEnd(3);
          // Return information that game has ended.
          return true;
        }
      }
    }

    // End game with pat.
    GameEnd(1);
    // Return information that game has ended.
    return true;
  } // Enf of GameEndCheck

  // Function that end the game.
  public void GameEnd(int state)
  {
    // Depending on game state.
    switch(state)
    {
      // Pat.
      case 1:
      {
        // Show game status.
        Debug.Log("Pat. No one wins!!!");
        // Break.
        break;
      }
      // Draw.
      case 2:
      {
        // Show game status.
        Debug.Log("Draw. No one wins!!!");
        // Break.
        break;
      }
      // Check mate.
      case 3:
      {
        // If white turn.
        if(this.is_white_turn)
        {
          // Show game status.
          Debug.Log("Check mate. White wins!!!");
        }
        // If black turn.
        else
        {
          // Show game status.
          Debug.Log("Check mate. Black wins!!!");
        }
        // Break.
        break;
      }
    }

    // Clear all game objects.
    Pieces.Instance.Clear();

    // Clear all highlights.
    BoardHighlights.Instance.HighlightsDeact();

    // Initialize of all pieces (new game).
    Pieces.Instance.Init(1);
  } // End of GameEnd

  // Function that initialize objects. Called exacty onece in lifetime of the script. 
  // Classes that inherit from 'MonoBehaviour' should use 'Start()' or 'Awake()' to initialise objects.
  // In other cases use standard constructor.
  private void Start()
  {
    // Initialize 'BoardManager' class.
    BoardManager._instance=this;
    // By default is white player turn.
    this.is_white_turn=true;
    // Set default camera speed.
    this.camera_anim_speed=0.5f;
    // Create array of cached allowed moves.
    this.moves=new bool[8,8][,];
    // Initialization of all pieces.
    Pieces.Instance.Init(1);
  } // End of Start
 
  // Function that update the scene (update is called once per frame).
  private void Update()
  {
    // Manage of chess piece selection.
    PieceSelManage();
  } // End of Update

} // End of BoardManager