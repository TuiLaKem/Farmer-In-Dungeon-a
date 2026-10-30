using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Scriptable Objects/Inventory")]
public class Inventory : ScriptableObject
{
    public List<GameObject> inventory = new List<GameObject>(new GameObject[6]);

    private void OnEnable()
    {
        while (inventory.Count <6) { inventory.Add(null); }
    }
}
