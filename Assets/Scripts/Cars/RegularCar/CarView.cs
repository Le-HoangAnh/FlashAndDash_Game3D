using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarView : MonoBehaviour
{
    [SerializeField] MeshRenderer carBodyMesh;
    [SerializeField] List<GameObject> wheels;
    [SerializeField] List<MeshRenderer> rimMeshes;

    //public void SetCarRims()
    //{
    //    Material[] carBodyMaterials = GetCarBodyMaterials();
    //    if (GameManager.GetRimMaterial() != null)
    //    {
    //        SetRimMaterial(GameManager.GetRimMaterial());
    //    }
    //}

    public void SetRimMaterial(Material material)
    {
        foreach (MeshRenderer meshRenderer in rimMeshes)
        {
            Material[] materials = meshRenderer.materials;
            materials[0] = material;
            meshRenderer.materials = materials;
        }
    }

    public Material GetRimMaterial()
    {
        Material[] materials = rimMeshes[0].materials;
        return materials[0];
    }

    public Material[] GetCarBodyMaterials()
    {
        MeshRenderer meshRenderer = carBodyMesh.GetComponent<MeshRenderer>();
        return meshRenderer.materials;
    }
}
