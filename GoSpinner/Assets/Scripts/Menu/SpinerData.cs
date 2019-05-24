using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spiner Data", menuName = "New Spiner Data")]
public class SpinerData : ScriptableObject
{
    public string ID;
    public string Name;
    public Mesh MeshSpiner;
    public bool IsBought = false;

    public int NowRang;
    public int NowCountRang;
}
