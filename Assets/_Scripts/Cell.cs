using System;
using UnityEngine;

public class Cell : MonoBehaviour
{
   [SerializeField] private GameObject xObject;
    
    public bool isTaken = false;
    [HideInInspector] public int row;
    [HideInInspector] public int column;

    private void Awake()
    {
        xObject.SetActive(false);
    }

    public bool IsTaken()
    {
        return isTaken;
    }

    private void OnMouseDown()
    {
        if (isTaken) return;
        xObject.SetActive(true);
        isTaken = true;
        GridManager.i.CheckMatches();
    }

    public void ClearCell()
    {
        xObject.SetActive(false);
        isTaken = false;
    }
}
