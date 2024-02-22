using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Camera mainCam;
    [SerializeField] float modifiedOffsetCoeff, modifierOffsetLerpCoeff;
    [SerializeField] float minSize, maxSize;
// [SerializeField] CinemachineVirtualCamera podiumCamera,closeCam;
    //Follow
    bool isFollowing;
    GameObject targetObj;
    Vector3 offset;
    Vector3 modifiedOffsetDirection, modifiedOffset;
    private Coroutine shakeCoroutine;

    void Awake()
    {
        isFollowing = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isFollowing)
        {
            Following();
        }
    }


    public Camera GetMainCam()
    {
        return mainCam;
    }

    #region Camera Follow

    private void Following()
    {
        modifiedOffset = Vector3.Lerp(modifiedOffset, modifiedOffsetDirection * modifiedOffsetCoeff, modifierOffsetLerpCoeff * Time.fixedDeltaTime);

        Vector3 desiredPosition = targetObj.transform.position + offset + modifiedOffset;

        // Vector3 lerpedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, lerpTime * Time.fixedDeltaTime);// for smooth lerp
        // Vector3 lerpedPosition = Vector3.Slerp(transform.position, desiredPosition, lerpTime * Time.fixedDeltaTime);// for smooth lerp

        // transform.position = lerpedPosition;
        transform.position = new Vector3(transform.position.x,desiredPosition.y,desiredPosition.z);

        // transform.LookAt(lerpedPosition);
    }

    public void SetModifiedOffsetDirection(Vector3 direction)
    {
        modifiedOffsetDirection = direction.normalized;
    }

    public void StartFollow(GameObject obj)
    {
        isFollowing = true;
        targetObj = obj;
        offset = mainCam.transform.position - obj.transform.position;

        
    }

    public void StartFollow(GameObject obj, Vector3 offset)
    {
        isFollowing = true;
        targetObj = obj;
        this.offset = offset;
    }

    public void StopFollow()
    {
        isFollowing = false;
    }

    public void CloseCam(float maxSpeed, float speed, float minSpeed,float distance)
    {

        float targetSize = Mathf.Lerp(minSize, maxSize, speed / maxSpeed);

        mainCam.orthographicSize = Mathf.Lerp(mainCam.orthographicSize, targetSize, 5 * Time.fixedDeltaTime);

    }

    #endregion

    #region Camera Shake

    public void ShakeCamera(float duration = 1, float magnitude = 1, bool decreasingMagnitude = false)
    {
        shakeCoroutine = StartCoroutine(CameraShakeCoroutine(duration, magnitude, decreasingMagnitude));
    }


    private IEnumerator CameraShakeCoroutine(float duration = 1, float magnitude = 1, bool decreasingMagnitude = false)
    {
        float timer = 0;

        while (timer < duration)
        {
            Vector2 randomShake = Random.insideUnitCircle * magnitude;

            if (decreasingMagnitude)
            {
                randomShake *= (1 - (timer / duration));
            }
            transform.Translate((Vector3)randomShake, Space.Self);
            timer += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        yield return null;
    }

    #endregion
}
