using System.Collections.Generic;
using UnityEngine;

// Class that 
public class BoardHighlights:MonoBehaviour
{
  // Static field 'Instance' that is accesiable from all instances of class 'BoardHighlights'.
  public static BoardHighlights Instance { private set; get; }

  // Highlight object prefab.
  public GameObject highlight_prefabs;

  // List of highlight objects.
  private List<GameObject> highlights;

  // Initlization function.
  private void Start()
  {
    // Create instance of highlight class.
    Instance=this;
    // Initialization of highlights list.
    this.highlights=new List<GameObject>();
  } // End of Start

  // Function that return highlights.
  private GameObject HighlightObjGet()
  {
    // Find actvie highlight.
    GameObject highlight=this.highlights.Find(g=>!g.activeSelf);
    // If there is no highlight.
    if(highlight==null)
    {
      // Create copy of current highlight.
      highlight=Instantiate(this.highlight_prefabs);
      // Add new highlight to highlights list.
      this.highlights.Add(highlight);
    }
    // Return highlight.
    return highlight;
  } // End of HighlightObjGet

  // Function that highlights allowed moves on chess board.
  public void MovesHighlight(bool[,] moves)
  {
    // Lopp after all moves.
    for(int i=0; i<8; i++)
    {
      for(int j=0; j<8; j++)
      {
        // If move is not allowed then skip loop step.
        if(!moves[i,j])
        {
          continue;
        }
        // Get highliht.
        GameObject highlight=HighlightObjGet();
        // Activate highlight.
        highlight.SetActive(true);
        // Set position of highlight.
        highlight.transform.position=BoardManager.Instance.TileCenterGet(i,j);
      }
    }
  } // End of MovesHighlight

  // Function that deactivate all highlights.
  public void HighlightsDeact()
  {
    foreach(GameObject highlight in this.highlights)
    {
      highlight.SetActive(false);
    }
  } // End of HighlightsDeact

} // End of BoardHighlights