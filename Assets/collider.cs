using UnityEngine;

public class collider : MonoBehaviour
{
    void Start()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        for (int i = 0; i < meshFilters.Length; i++)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false); // Disable child mesh renderers
        }

        MeshFilter parentMeshFilter = gameObject.AddComponent<MeshFilter>();
        parentMeshFilter.mesh = new Mesh();
        parentMeshFilter.mesh.CombineMeshes(combine);

        MeshRenderer parentRenderer = gameObject.AddComponent<MeshRenderer>(); // Add renderer if needed

        gameObject.SetActive(true); // Reactivate parent object
    }
}