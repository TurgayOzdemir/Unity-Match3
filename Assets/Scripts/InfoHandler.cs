using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoHandler : MonoBehaviour
{

    int row;
    int col;
    string color;

    public int GetRow() { return row; }
    public int GetCol() { return col; }

    public void SetRow(int val) { row = val; }
    public void SetCol(int val) { col = val; }

    public string GetColor() { return color; }
    public void SetColor(string val) { color = val; }
}
