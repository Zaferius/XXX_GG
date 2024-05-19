using UnityEngine;

public class Cell : MonoBehaviour
{
    public GameObject xObject;
    
    public bool isTaken = false; 

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (isTaken) return;
        xObject.SetActive(true);
        isTaken = true;
    }
}
