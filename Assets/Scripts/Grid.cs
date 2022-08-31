using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] [Range(1,10)] public int rows = 9;
    [SerializeField] [Range(1, 10)] public int cols = 9;
    [SerializeField] private float tileSize = 0.5f;

    [Header("Color")]
    [SerializeField][Range(1, 6)] private int colorCount = 4;
    [SerializeField] private List<Block> Blocks;


    public Block[,] Board;
    public GameObject[,] BoardObject;

    private static Grid instance;
    public static Grid Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        Board = new Block[rows, cols];
        BoardObject = new GameObject[rows, cols];

        generate_grid();

    }

    private void generate_grid()
    {
        for (int row = 0; row < Board.GetLength(0); row++)
        {
            for (int col = 0; col < Board.GetLength(1); col++)
            {
                Board[row, col] = pickRandomBlock();
                BoardObject[row, col] = Instantiate(Board[row, col].BlockGameObject, transform);
                

                float x = col * tileSize;
                float y = row * -tileSize;

                BoardObject[row, col].transform.position = new Vector2(x, y);
            }
        }


        float w = cols * tileSize;
        float h = rows * tileSize;

        transform.position = new Vector2(-w / 2 + tileSize / 2, h / 2 - tileSize / 2);
    }

    private Block pickRandomBlock()
    {
        return Blocks[Random.Range(0, colorCount)];
    }


}
