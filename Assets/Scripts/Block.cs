using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Block", menuName = "New/Block")]
public class Block : ScriptableObject
{
    public string Color;

    public GameObject BlockGameObject;

    public Sprite DefaultIcon;
    public Sprite FirstIcon;
    public Sprite SecondIcon;
    public Sprite ThirdIcon;

}
