using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random=UnityEngine.Random;

[Flags]
public enum WallState
{
    // 0000 -> NO WALLS
    // 1111 -> LEFT, RIGHT, UP, DOWN
    left = 1, // 0001
    right = 2, //0010
    up = 4, //0100
    down = 8, //1000

    visited = 128, //1000 000
}

public struct Position
{
    public int X;
    public int Y;
}

public struct Neighbor
{
    public Position Position;
    public WallState SharedWall;
}

public class MazeGenerator : MonoBehaviour
{
    private static WallState GetOppositeWall(WallState wall)
    {
        switch(wall)
        {
            case WallState.right: return WallState.left;
            case WallState.left: return WallState.right;
            case WallState.up: return WallState.down;
            case WallState.down: return WallState.up;
            default: return WallState.left;
        }
    }

    private static WallState[,] ApplyRecursiveBacktracker(WallState[,] maze, int width, int height)
    {
        //here we make changes
        var rng = new System.Random(/*seed*/);
        var positionStack = new Stack<Position>();
        var position = new Position { X = rng.Next(0,width), Y = rng.Next(0,height)};

        maze[position.X, position.Y] |= WallState.visited; //1000 1111
        positionStack.Push(position);

        while (positionStack.Count > 0)
        {
            var current = positionStack.Pop();
            var neighbors = GetUnvisitedNeighbors(current, maze, width, height);

            if(neighbors.Count > 0)
            {
                positionStack.Push(current);

                var randIndex = rng.Next(0, neighbors.Count);
                var randomNeighbor = neighbors[randIndex];

                var nPosition = randomNeighbor.Position;
                maze[current.X, current.Y] &= ~randomNeighbor.SharedWall;
                maze[nPosition.X, nPosition.Y] &= ~GetOppositeWall(randomNeighbor.SharedWall);

                maze[nPosition.X, nPosition.Y] |= WallState.visited;

                positionStack.Push(nPosition);
            }
        }

        return maze;
    }

    private static List<Neighbor> GetUnvisitedNeighbors(Position p, WallState[,] maze, int width, int height)
    {
        var list = new List<Neighbor>();

        if(p.X > 0) //left
        {
            if(!maze[p.X -1, p.Y].HasFlag(WallState.visited))
            {
                list.Add(new Neighbor
                        {
                            Position = new Position
                            {
                                X = p.X - 1,
                                Y = p.Y
                            },
                            SharedWall = WallState.left
                        });
            }
        }

        if(p.Y > 0) //down
        {
            if(!maze[p.X, p.Y - 1].HasFlag(WallState.visited))
            {
                list.Add(new Neighbor
                        {
                            Position = new Position
                            {
                                X = p.X,
                                Y = p.Y - 1
                            },
                            SharedWall = WallState.down
                        });
            }
        }

        if(p.Y < height - 1) //up
        {
            if(!maze[p.X, p.Y + 1].HasFlag(WallState.visited))
            {
                list.Add(new Neighbor
                        {
                            Position = new Position
                            {
                                X = p.X,
                                Y = p.Y + 1
                            },
                            SharedWall = WallState.up
                        });
            }
        }

        if(p.X < width - 1) //right
        {
            if(!maze[p.X + 1, p.Y].HasFlag(WallState.visited))
            {
                list.Add(new Neighbor
                        {
                            Position = new Position
                            {
                                X = p.X + 1,
                                Y = p.Y
                            },
                            SharedWall = WallState.right
                        });
            }
        }

        return list;
    }

    public static WallState[,] Generate(int width, int height)
    {
        WallState[,] maze = new WallState[width, height];
        WallState initial = WallState.right | WallState.left | WallState.up | WallState.down;
        for (int i=0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                maze[i,j] = initial; // 1111
            }
        }

        maze[0, Random.Range(0, height)] &= ~WallState.left; // Remove left wall of leftmost cell
        maze[width-1, Random.Range(0, height)] &= ~WallState.right; // Remove right wall of rightmost cell
        
        return ApplyRecursiveBacktracker(maze, width, height);
    }
}
