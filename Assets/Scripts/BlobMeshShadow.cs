using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobMeshShadow : MonoBehaviour
{
    public Material material;
    public Transform target;

    void Start()
    {
        var obj = GameObject.CreatePrimitive(PrimitiveType.Quad);
        obj.transform.parent = transform;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.Euler(90, 0, 0);

        // set material
        var renderer = obj.GetComponent<MeshRenderer>();
        renderer.material = material;
    }

    void LateUpdate()
    {
        if (target == null) return;

        transform.position = target.position;
    }
}