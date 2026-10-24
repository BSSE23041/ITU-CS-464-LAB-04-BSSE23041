using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset = new Vector3(0, 4, -7);
    [SerializeField] float positionSmooth = 10f;
    [SerializeField] float rotationSmooth = 10f;
    [SerializeField] float lookHeight = 1f;

    void Start()
    {
        if (target == null)
        {
            var player = FindFirstObjectByType<PlayerController>();
            if (player != null) target = player.transform;
        }

        if (target == null)
            Debug.LogError("CameraFollow: no target assigned and no PlayerController found in the scene.", this);
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 wantedPos = target.position + target.rotation * offset;

        float posT = 1f - Mathf.Exp(-positionSmooth * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, wantedPos, posT);

        Vector3 lookPoint = target.position + Vector3.up * lookHeight;
        Quaternion wantedRot = Quaternion.LookRotation(lookPoint - transform.position);
        float rotT = 1f - Mathf.Exp(-rotationSmooth * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, wantedRot, rotT);
    }
}