using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxesManager : MonoBehaviour
{
    public System.Action OnBoxCreated;
    [SerializeField] int boxCuantity;
    [SerializeField] GameObject box;
    [SerializeField] GameObject spawner;
    [SerializeField] GameObject conteinerBoxes;
    [SerializeField] LayerMask TerrainLayer;
    [SerializeField] public List <Box> boxes;

    public void dependentStart()
    {
        for (int i = 0; i < boxCuantity; i++)
        {
            GameObject go= Instantiate(box, Vector3.zero, Quaternion.identity, conteinerBoxes.transform);
            RandvecInTerrain(go);
            boxes.Add(go.GetComponent<Box>());
        }
        OnBoxCreated?.Invoke();
        Destroy(spawner);
    }
    void RandvecInTerrain(GameObject go)
    {
        RaycastHit hit;
        float randomPositionX = spawner.transform.position.z + UnityEngine.Random.Range(-spawner.transform.localScale.x / 2, spawner.transform.localScale.x / 2);
        float randomPositionZ = spawner.transform.position.z + UnityEngine.Random.Range(-spawner.transform.localScale.z / 2, spawner.transform.localScale.z / 2);
        float randomPositionY = 0f;

        if (Physics.Raycast(new Vector3(randomPositionX, 9999f, randomPositionZ), Vector3.down, out hit, Mathf.Infinity, TerrainLayer))
        {
            randomPositionY = hit.point.y + go.transform.localScale.y;
            go.transform.position = new Vector3(randomPositionX, randomPositionY, randomPositionZ);
            go.transform.rotation = Quaternion.LookRotation(hit.normal);
            return;
        }
    }
}
