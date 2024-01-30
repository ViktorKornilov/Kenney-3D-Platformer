using UnityEngine;

public class Hover : MonoBehaviour
{
    [Range(0f,5)]public float height = 1;
    [Range(0.1f,10f)]public float duration = 1;
    public bool randomize = true;
    public bool globalSpace;
    float phase;
    Vector3 startPos;

    void Start()
    {
        if (globalSpace)
        {
            startPos = transform.position;
        }
        else
        {
            startPos = transform.localPosition;
        }


        if(randomize) {
            phase = Random.Range(0f, Mathf.PI * 2);
        }
    }

    void Update()
    {
        Vector3 pos = new();
        pos.y = height + Mathf.Sin(phase + Time.time / duration * Mathf.PI * 2) * height;

        if (globalSpace)
        {
            transform.position = startPos + pos;
        }
        else
        {
            transform.localPosition = startPos + pos;
        }
    }
}