using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickController : MonoBehaviour
{

    private List<GameObject> list = new List<GameObject> ();

    private void Update()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hitData = Physics2D.Raycast(new Vector2(worldPosition.x, worldPosition.y), Vector2.zero, 0);
        if (hitData && Input.GetMouseButtonDown(0))
        {
            gameObject.GetComponent<BlockController>().setAllVisited();
            gameObject.GetComponent<BlockController>().DFS(hitData.transform.gameObject.GetComponent<InfoHandler>().GetRow(), hitData.transform.gameObject.GetComponent<InfoHandler>().GetCol(), hitData.transform.gameObject.GetComponent<InfoHandler>().GetColor());

            if (gameObject.GetComponent<BlockController>().GetOutput().Count >= 2)
            {
                foreach (var item in gameObject.GetComponent<BlockController>().GetOutput())
                {
                    Debug.Log(item.name);
                    item.gameObject.SetActive(false);

                }
                shiftBlocks();
            }
            gameObject.GetComponent<BlockController>().setAllVisited();
            gameObject.GetComponent<BlockController>().GetOutput().Clear();
            gameObject.GetComponent<BlockController>().setBlockIcon();

        }
    }

    private void shiftBlocks()
    {
        int outputCount = gameObject.GetComponent<BlockController>().GetOutput().Count-1;

        for (int i = 0; i < Grid.Instance.cols; i++)
        {
            int shiftCounter = 0;

            for (int j = Grid.Instance.rows - 1; j >= 0; j--)
            {
                if (!Grid.Instance.BoardObject[j, i].activeSelf)
                {
                    shiftCounter++;
                }
                else
                {
                    if (shiftCounter > 0)
                    {
                        Grid.Instance.Board[j + shiftCounter, i] = Grid.Instance.Board[j, i];
                        Grid.Instance.BoardObject[j, i].transform.localPosition = new Vector2(Grid.Instance.BoardObject[j, i].transform.localPosition.x, Grid.Instance.BoardObject[j, i].transform.localPosition.y - shiftCounter * 0.5f);
                        Grid.Instance.BoardObject[j + shiftCounter, i] = Grid.Instance.BoardObject[j, i];
                        Grid.Instance.BoardObject[j + shiftCounter, i].GetComponent<InfoHandler>().SetRow(j + shiftCounter);
                        Grid.Instance.BoardObject[j + shiftCounter, i].GetComponent<InfoHandler>().SetCol(i);


                    }
                }
            }
            colList(i);
            for (int k = 0; k < shiftCounter; k++)
            {
                list[k].SetActive(true);
                list[k].transform.localPosition = new Vector2(i * 0.5f, k * -0.5f);

                Grid.Instance.Board[k, i] = Grid.Instance.pickRandomBlock();
                Grid.Instance.BoardObject[k, i] = list[k];
                Grid.Instance.BoardObject[k, i].GetComponent<InfoHandler>().SetRow(k);
                Grid.Instance.BoardObject[k, i].GetComponent<InfoHandler>().SetCol(i);
                Grid.Instance.BoardObject[k, i].GetComponent<InfoHandler>().SetColor(Grid.Instance.Board[k, i].Color);
                Grid.Instance.BoardObject[k, i].GetComponent<SpriteRenderer>().sprite = Grid.Instance.Board[k, i].DefaultIcon;

                outputCount--;
            }
            list.Clear();
        }
    }

    private void colList(int col)
    {
        for (int i = 0; i < gameObject.GetComponent<BlockController>().GetOutput().Count; i++)
        {
            if (gameObject.GetComponent<BlockController>().GetOutput()[i].GetComponent<InfoHandler>().GetCol() == col)
            {
                list.Add(gameObject.GetComponent<BlockController>().GetOutput()[i]);
            }
        }
    }

}