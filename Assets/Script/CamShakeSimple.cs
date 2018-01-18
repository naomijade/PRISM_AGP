using UnityEngine;
using System.Collections;

public class CamShakeSimple : MonoBehaviour
{

    Vector3 originalCameraPosition;

    float shakeAmt = 0;

    public Camera mainCamera;

    public GameObject Bullet;


    void OnCollisionEnter2D(Collision2D coll)
    {
        originalCameraPosition = mainCamera.transform.position;
        shakeAmt = coll.relativeVelocity.magnitude * .0008f;
        InvokeRepeating("CameraShake", 0, .01f);
        Invoke("StopShaking", 0.3f);

    }

    void CameraShake()
    {
        if (shakeAmt > 0)
        {
            float quakeAmt = Random.value * shakeAmt * 2 - shakeAmt;
            Vector3 pp = mainCamera.transform.position;
            pp.y += quakeAmt; // can also add to x and/or z
            pp.z += quakeAmt;
            pp.x += quakeAmt;
            mainCamera.transform.position = pp;
        }
    }

    void OnDestroy()
    {

        InvokeRepeating("CameraShake", 0, .01f);
    }

    void StopShaking()
    {
        CancelInvoke("CameraShake");
        mainCamera.transform.position = originalCameraPosition;
    }

}
