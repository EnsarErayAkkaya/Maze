using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakpoint
{
    Maze maze;
    [SerializeField]float x;
    [SerializeField]float y;  
    [SerializeField]Texture2D texture;
    Pipe[] pipes = {null, null, null, null}; // RTLB
    SpriteRenderer renderer;

    public Vector2 Point{
        get{
            return new Vector2(x,y);
        }
    }
    public int PipeCount{
        get{
            int c = 0;
            for (int i = 0; i < 4; i++)
            {
                if(pipes[i] != null)
                    c++;
            }
            return c;
        }
    }
    public List<int> AvailablePipes{
        get{
            List<int> list = new List<int>();
            if(pipes[0] == null){
                list.Add(0);
            }
            if(pipes[1] == null){
                list.Add(1);
            }
            if(pipes[2] == null){
                list.Add(2);
            }
            if(pipes[3] == null){
                list.Add(3);
            }
            return list;
        }
    }
    public Maze Maze{
        get => maze;
    }
    public Breakpoint(float x, float y, Maze maze)
    {
        this.maze = maze;
        this.x = x;
        this.y = y;

        GameObject g = new GameObject("BP_"+this.x+"_"+this.y);

        g.transform.position = new Vector2(x,y);

        renderer = g.AddComponent<SpriteRenderer>();  
        renderer.sortingOrder = 1;
        draw(); 
    }

    void draw(){
        texture = new Texture2D((int)Constants.cellWidth,(int)Constants.cellWidth,TextureFormat.ARGB32,false);

        //float border = (Constants.cellSize - (Constants.cellSize * 0.875f)) / 2; 

        for (int i = 0; i < Constants.cellWidth; i++)
        {
            for (int j = 0; j < Constants.cellWidth; j++)
            {
                texture.SetPixel(i,j, Color.red);
                
            }
        }

        texture.filterMode = FilterMode.Point;

        texture.Apply();

        renderer.material.mainTexture = texture;

        renderer.sprite = Sprite.Create(texture, 
            new Rect(0.0f, 0.0f, Constants.cellWidth, Constants.cellWidth), 
            new Vector2(0.5f, 0.5f), 100.0f);
    }
    public void setPipes(Pipe[] pipes){
        this.pipes = pipes;
    }
    public void setPipes(Pipe pipe, int index){
        this.pipes[index] = pipe;
    }
    public Pipe setPipes(int index){
        Pipe p = new Pipe(this, index);
        this.pipes[index] = p;
        return p;
    }


}   
