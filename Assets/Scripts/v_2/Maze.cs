using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    private void Start() {
        CreateMaze();
    }

    void CreateMaze(){
        Stack<Breakpoint> breakpointStack = new Stack<Breakpoint>();

        Breakpoint first = new Breakpoint(0,0, this);
        breakpointStack.Push(first);

        int dir = Random.Range(0,4);
        int lastDir = 0;

        while (breakpointStack.Count < Constants.breakpointCount)
        {
            lastDir = dir;

            Pipe pipe = breakpointStack.Peek().setPipes(dir);

            breakpointStack.Push(pipe.LastBreakpoint);

            do
            {
                dir = Random.Range(0,4);
            } while ((dir - lastDir) % 2 == 0 || (lastDir - dir) % 2 == 0);
        }

        while(breakpointStack.Count > 0 ){
            Breakpoint bp = breakpointStack.Pop();
            //while()
        }
    }
}
