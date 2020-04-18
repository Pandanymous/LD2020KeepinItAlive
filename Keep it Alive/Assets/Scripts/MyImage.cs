using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MyImage 
{
    [SerializeField] string name;
    [SerializeField] Sprite image;

    #region GETTER && SETTER
    public Color Color { get => color; set => color = value; }
    public string Name { get => name; set => name = value; }
    #endregion
}
