using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Scatter : MonoBehaviour
{
    public GameObject prefab;
    public float radius = 10;
    public int count = 10;
    public int seed = 0;

    [Header( "Randomize Transform" )]
    public Vector2 scaleRange = new Vector2( 1, 1 );
    public Vector3 rotationRange = new Vector3( 0, 360, 0 );
    public bool parent = true;

    void Start()
    {
        Random.InitState( seed );
        ScatterPrefab();
    }

    public void ScatterPrefab()
    {
        for(int i = 0; i < count; i++) {
            var obj = Instantiate(prefab, transform);
            RandomizeTransform(obj);
            if(!parent)obj.transform.parent = null;
        }
    }

    void RandomizeTransform(GameObject obj)
    {
        var randomPos = Random.insideUnitSphere * radius;
        var randomRot = Vector3.zero;

        // optional
        if(Math.Abs(scaleRange.x - scaleRange.y) > 0.01f)obj.transform.localScale *= Random.Range( scaleRange.x, scaleRange.y);
        if(rotationRange != Vector3.zero ) {
            randomRot = new Vector3(
                Random.Range( -rotationRange.x, rotationRange.x ),
                Random.Range( -rotationRange.y, rotationRange.y ),
                Random.Range( -rotationRange.z, rotationRange.z ));
        }
        obj.transform.SetLocalPositionAndRotation( randomPos, Quaternion.Euler( randomRot ) );
    }

    
    #if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    #endif
}