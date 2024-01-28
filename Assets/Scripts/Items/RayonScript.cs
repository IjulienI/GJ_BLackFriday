using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayonScript : MonoBehaviour
{
    [SerializeField] private GameObject itemShelf;
    [SerializeField] private GameObject[] itemHolder;

    private void Start()
    {
        for(int i = 0; i < itemHolder.Length; i++)
        {
            itemHolder[i].GetComponent<MeshFilter>().mesh = itemShelf.transform.GetChild(0).GetComponent<MeshFilter>().sharedMesh;
            itemHolder[i].GetComponent<MeshRenderer>().material = itemShelf.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial;
            itemHolder[i].GetComponent<MeshFilter>().transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);
        }
    }
    public GameObject GetItem()
    {
        return itemShelf;
    }

    public GameObject GetItemShelf()
    {
        return itemShelf;
    }
}
