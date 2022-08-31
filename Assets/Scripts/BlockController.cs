using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockController : MonoBehaviour
{
    private List<GameObject> output = new List<GameObject>();
    
    public bool[,] IsVisited;
    string color;


    private void Start()
    {
        
        Debug.Log(Grid.Instance.Board.GetLength(0));
        Debug.Log(Grid.Instance.Board.GetLength(1));

        IsVisited = new bool[Grid.Instance.Board.GetLength(0), Grid.Instance.Board.GetLength(1)];

        setAllVisited();

        setBlockIcon();
    }

    private void setBlockIcon()
    {
        for (int row = 0; row < Grid.Instance.Board.GetLength(0); row++)
        {
            for (int col = 0; col < Grid.Instance.Board.GetLength(1); col++)
            {
                if (IsVisited[row, col] == false)
                {
                    color = Grid.Instance.Board[row, col].Color;
                    DFS(row, col);
                    Debug.Log(output.Count);

                    for (int i = 0; i < output.Count; i++)
                    {
                        if (output.Count >= 5 && output.Count < 8)
                        {
                            output[i].GetComponent<SpriteRenderer>().sprite = Grid.Instance.Board[row, col].FirstIcon;
                        }
                        else if (output.Count >= 8 && output.Count < 10)
                        {
                            output[i].GetComponent<SpriteRenderer>().sprite = Grid.Instance.Board[row, col].SecondIcon;
                        }
                        else if (output.Count >= 10)
                        {
                            output[i].GetComponent<SpriteRenderer>().sprite = Grid.Instance.Board[row, col].ThirdIcon;
                        }
                    }

                    output.Clear();
                }
            }
        }
    }


    private void setAllVisited()
    {
        for (int row = 0; row < Grid.Instance.Board.GetLength(0); row++)
        {
            for (int col = 0; col < Grid.Instance.Board.GetLength(1); col++)
            {
                IsVisited[row, col] = false;
            }
        }
    }

    private void DFS(int row, int col)
    {
        if (
            row < 0 || row >= Grid.Instance.Board.GetLength(0)
            ||
            col < 0 || col >= Grid.Instance.Board.GetLength(1)
            ||
            IsVisited[row, col]
            ||
            Grid.Instance.Board[row, col].Color != color
            )
        {
            return;
        }
        else
        {
            IsVisited[row, col] = true;
            output.Add(Grid.Instance.BoardObject[row, col]);

            DFS(row-1, col);
            DFS(row+1, col);
            DFS(row, col-1);
            DFS(row, col+1);
        }

    }


}
