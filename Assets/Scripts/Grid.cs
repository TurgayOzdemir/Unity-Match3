using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] [Range(1,10)] public int rows = 9;
    [SerializeField] [Range(1, 10)] public int cols = 9;
    [SerializeField] private float blockSize = 0.5f;

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
                BoardObject[row, col].GetComponent<InfoHandler>().SetRow(row);
                BoardObject[row, col].GetComponent<InfoHandler>().SetCol(col);
                BoardObject[row, col].GetComponent<InfoHandler>().SetColor(Board[row, col].Color);

                float x = col * blockSize;
                float y = row * -blockSize;

                BoardObject[row, col].transform.position = new Vector2(x, y);
            }
        }


        float w = cols * blockSize;
        float h = rows * blockSize;

        transform.position = new Vector2(-w / 2 + blockSize / 2, h / 2 - blockSize / 2);
    }

    public Block pickRandomBlock()
    {
        return Blocks[Random.Range(0, colorCount)];
    }


}
