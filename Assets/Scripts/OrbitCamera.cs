using UnityEngine;

[ExecuteAlways]
public class OrbitCamera : MonoBehaviour
{
    [Header("Following")]
    public Transform target;
    public Vector3 offset;
    public bool centerTargetBounds = true;

    [Header("Rotation")]
    [Min(0)]public float rotateSpeed = 30;
    public Vector2 verticalLimits = new Vector2(0, 80);
    [Range(0f,1f)]public float rotationDampening = 0.9f;

    [Header("Zooming")]
    [Min(1)]public float distance = 5.0f;
    public Vector2 distanceLimits = new Vector2(1, 10);
    [Min(0)]public float zoomSpeed = 10;
    [Range(0f,1f)]public float zoomDampening = 0.9f;


    Vector3 angle;
    Transform camTransform;


    void Start()
    {
        camTransform = transform.GetChild( 0 );
        if(centerTargetBounds) {
            // get center of all renderers
            var renderers = target.GetComponentsInChildren<Renderer>();
            var center = Vector3.zero;
            foreach(var r in renderers) {
                center += r.bounds.center;
            }
            center /= renderers.Length;
            offset = center;
        }
    }

    void LateUpdate()
    {
        // following
        if(target == null) return;
        transform.position = target.position + offset;

        // zooming
        distance += Input.GetAxisRaw("Mouse ScrollWheel") * zoomSpeed;
        distance = Mathf.Clamp(distance, distanceLimits.x, distanceLimits.y);
        camTransform.localPosition = Vector3.Lerp(camTransform.localPosition, new Vector3(0, 0, -distance),1 - zoomDampening);

        // rotation
        if (!Application.isPlaying) return;
        angle.x += Input.GetAxisRaw("Vertical") * rotateSpeed * Time.deltaTime;
        angle.y += -Input.GetAxisRaw("Horizontal") * rotateSpeed * Time.deltaTime;
        angle.x = Mathf.Clamp(angle.x, verticalLimits.x, verticalLimits.y);
        transform.rotation = Quaternion.Lerp( transform.rotation, Quaternion.Euler(angle),1 - rotationDampening);
    }
}