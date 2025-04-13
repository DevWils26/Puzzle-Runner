using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MazeCellObject : MonoBehaviour
{
    
    [SerializeField] private GameObject finishMarkerPrefab;
    [SerializeField] private Renderer cellRenderer;
    [SerializeField] private GameObject leftWall;
    [SerializeField] private GameObject rightWall;
    [SerializeField] private GameObject frontWall;
    [SerializeField] private GameObject backWall;

    [SerializeField] private GameObject unvisitedBlock;

    public bool IsVisited { get; private set;}

    public void Visit(){

        IsVisited = true;
        unvisitedBlock.SetActive(false);

    }

    public void ClearLeftWall(){

        leftWall.SetActive(false);

    }

    public void ClearRightWall(){

        rightWall.SetActive(false);
        
    }

    public void ClearFrontWall(){

        frontWall.SetActive(false);
        
    }

    public void ClearBackWall(){

        backWall.SetActive(false);
        
    }

    public void SetAsStart()
    {
        if (cellRenderer != null)
            cellRenderer.material.color = Color.green;
    }

    public void SetAsFinish()
    {
        if (cellRenderer != null)
            cellRenderer.material.color = Color.red;

        gameObject.tag = "Finish";

        Collider col = GetComponent<Collider>();
        if (col != null)
            col.isTrigger = true;

        if (finishMarkerPrefab != null)
        {
            Vector3 markerPos = transform.position + new Vector3(0, 1, 0);
            Instantiate(finishMarkerPrefab, markerPos, Quaternion.identity);
        }
    }
    
}
