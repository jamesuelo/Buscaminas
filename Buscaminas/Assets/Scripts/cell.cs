using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class cell : MonoBehaviour
{
    public Sprite[] emptyTextures;
    public Sprite mineTexture;
    public bool hasMine;
    // Start is called before the first frame update
    void Start()
    {
        GridHelper helper = GameObject.Find("Panel").GetComponent<GridHelper>();
        hasMine = (Random.value < helper.mineWeight);
        
        int x  = (int)this.transform.position.x;
        int y = (int)this.transform.parent.transform.position.y;
        GridHelper.cells[x,y] = this;
    }

    public void loadTexture(int adjacent)
    {
        if(hasMine)
            GetComponent<SpriteRenderer>().sprite = mineTexture;
        else
        {
            GetComponent<SpriteRenderer>().sprite = emptyTextures[adjacent];
        }
    }


public bool isCovered(){
    return GetComponent<SpriteRenderer>().sprite.texture.name == "Casilla";

}

private void OnMouseUpAsButton(){

    if(hasMine){

        GridHelper.UncoverAllMines();
        Invoke("ReturnTo", 3.0f);
    }
    else
    {
        int x = (int) this.transform.position.x;
        int y = (int) this.transform.parent.transform.position.y;
        loadTexture(GridHelper.countAdjMines(x,y));
        GridHelper.FloodFill(x,y,new bool[GridHelper.w,GridHelper.h]);

        if(GridHelper.GameEnd()){
            Debug.Log("Fin de la Partida, Ganaste pendejo");
            Invoke("ReturnTo", 3.0f);
        }

    }
}

    private void ReturnTo(){
        SceneManager.LoadScene("Buscaminas");
    }

}
