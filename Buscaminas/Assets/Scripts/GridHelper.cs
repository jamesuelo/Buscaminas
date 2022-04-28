using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridHelper : MonoBehaviour
{
    public float mineWeight = 0.1f;
    public static int w = 23;
    public static int h = 19;

    public static cell[,] cells = new cell[w, h];
    // Start is called before the first frame update
    public static void UncoverAllMines(){
        foreach (cell c in cells)
        {
            if(c.hasMine){
                c.loadTexture(0);
            }
        }
    }
    public static bool HasMineAt(int x, int y){
        if(x>=0 && y >=0 && x<w && y<h){
            cell c = cells[x,y];
            return c.hasMine;
        }
        else{
            return false;
        }
    }
    public static int countAdjMines(int x, int y){
        int count = 0;
        if(HasMineAt(x-1,y-1)) count++;
        if(HasMineAt(x-1,y)) count++;
        if(HasMineAt(x,y-1)) count++;
        if(HasMineAt(x+1,y-1)) count++;
        if(HasMineAt(x-1,y+1)) count++;
        if(HasMineAt(x+1,y)) count++;
        if(HasMineAt(x,y+1)) count++;
        if(HasMineAt(x+1,y+1)) count++;
        return count;
    }


    public static void FloodFill(int x, int y, bool [,] visited){
        if(x>=0 && y >=0 && x<w && y<h){
        
        if(visited[x,y])
        {
            return;
        }

        int count = countAdjMines(x,y);

        cells[x,y].loadTexture(count);
        if(count > 0) return;

        visited[x,y] = true;
        FloodFill(x,y+1, visited);
        FloodFill(x+1,y, visited);
        FloodFill(x,y-1, visited);
        FloodFill(x-1,y, visited);
    }
    }

        public static bool GameEnd(){
        foreach (cell c in cells)
        {
            if(c.isCovered() && !c.hasMine){
                return false;
            }
        }
        return true;
    }
}
