using Sirenix.OdinInspector;
using UnityEngine;

public class cameraMove : MonoBehaviour
{
    //public float trasitionSpeed;
    //public Transform currentview;



    //public void LateUpdate()
    //{
    //    //currentview = GameManager.Instance.CamPosition[GameManager.Instance.CamIndex].transform;
    //    transform.position = Vector3.Lerp(transform.position, currentview.position, Time.deltaTime * trasitionSpeed);
    //    Vector3 currentAngle = new Vector3(Mathf.LerpAngle(transform.rotation.eulerAngles.x, currentview.transform.rotation.eulerAngles.x, Time.deltaTime * trasitionSpeed),
    //        Mathf.LerpAngle(transform.rotation.eulerAngles.y, currentview.transform.eulerAngles.y, Time.deltaTime * trasitionSpeed),
    //        Mathf.LerpAngle(transform.rotation.eulerAngles.z, currentview.transform.eulerAngles.z, Time.deltaTime * trasitionSpeed));
    //    transform.eulerAngles = currentAngle;

    //}



    [ShowInInspector] public static Transform target;

    public float smoothSpeed = 0.125f;

    void FixedUpdate()
    {
        if (target == null) return;
        Vector3 desiredPosition = target.position;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        //transform.LookAt(target);
    }

}
