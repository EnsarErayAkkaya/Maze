using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe
{
    private Vector2 startPoint;
    private Vector2 endPoint;
    List<Breakpoint> breakpoints;
    SpriteRenderer renderer;
    Texture2D texture;
    float lengthAsPixel;
    
    int rtlb;
    

    public Breakpoint LastBreakpoint{
        get => breakpoints[breakpoints.Count - 1];
    }
    public Breakpoint FirstBreakpoint{
        get => breakpoints[0];
    }

    public Pipe(Breakpoint bp, int rtlb)
    {
        this.rtlb = rtlb;
        breakpoints.Add(bp);
        int power =  Random.Range((int)Constants.minPipeMultiplier, (int)Constants.maxPipeMultiplier+1);
        lengthAsPixel = Mathf.Pow(2, power);
    

        startPoint = bp.Point + getDistBetweenBreakpoint();
        setEndPoint();

        GameObject g = new GameObject("Pipe");

        g.transform.position = (startPoint + endPoint) / 2;

        renderer = g.AddComponent<SpriteRenderer>();
        draw();
        Vector2 lastBp = endPoint + getDistBetweenBreakpoint();
        breakpoints.Add(new Breakpoint( lastBp.x, lastBp.y, bp.Maze ));
    }

    void draw(){
        
        // left, right
        if(rtlb == 0 || rtlb == 2){
            texture = new Texture2D((int)lengthAsPixel, (int)Constants.cellWidth, TextureFormat.ARGB32,false);

            for (int i = 0; i < (int)lengthAsPixel; i++)
            {
                for (int j = 0; j < (int)Constants.cellWidth; j++)
                {
                    texture.SetPixel(i,j, Color.black);
                }
            }
            texture.filterMode = FilterMode.Point;

            texture.Apply();

            renderer.material.mainTexture = texture;

            renderer.sprite = Sprite.Create(texture, 
                new Rect(0.0f, 0.0f, lengthAsPixel, Constants.cellWidth), 
                new Vector2(0.5f, 0.5f), 100.0f);
        }
        //top, bottom
        else if(rtlb == 1 || rtlb == 3){
            texture = new Texture2D((int)Constants.cellWidth, (int)lengthAsPixel, TextureFormat.ARGB32,false);

            for (int i = 0; i < (int)Constants.cellWidth; i++)
            {
                for (int j = 0; j < (int)lengthAsPixel; j++)
                {
                    texture.SetPixel(i,j, Color.black);
                }
            }
            texture.filterMode = FilterMode.Point;

            texture.Apply();

            renderer.material.mainTexture = texture;

            renderer.sprite = Sprite.Create(texture, 
                new Rect(0.0f, 0.0f, Constants.cellWidth, lengthAsPixel), 
                new Vector2(0.5f, 0.5f), 100.0f);
        }
    }

    void setEndPoint(){
        // right
        if(rtlb == 0){
            endPoint = startPoint + new Vector2(lengthAsPixel / 100, 0);
        }
        //top
        else if(rtlb == 1){
            endPoint = startPoint + new Vector2(0, lengthAsPixel / 100);
        }
        // left
        else if(rtlb == 2){
            endPoint = startPoint + new Vector2(-lengthAsPixel / 100, 0);
        }
        //bottom
        else {
            endPoint = startPoint + new Vector2(0, -lengthAsPixel / 100);
        }
    }

    Vector2 getDistBetweenBreakpoint(){
        if(rtlb == 0){
            return new Vector2(Constants.cellWidth /2 / 100, 0);
        }
        else if(rtlb == 1){
            return new Vector2(0, Constants.cellWidth /2 / 100);
        }
        else if(rtlb == 2){
            return new Vector2(-Constants.cellWidth /2 / 100, 0);
        }
        else {
            return new Vector2(0, -Constants.cellWidth /2 / 100);
        }
    }
}
