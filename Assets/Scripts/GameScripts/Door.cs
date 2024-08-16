using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum DoorType
{ 
    ENTRANCE,
    EXIT,
}

public class Door : MonoBehaviour
{
    [SerializeField] DoorType door;
    float OpenTime = 0.6f;
    float OpenAngle = 20f;
    float CloseAngle = 90f;
    Vector3 OpenVector;
    Vector3 CloseVector;
    bool isIdle;

    public bool idle { get { return isIdle; } }

    void Start()
    {
        switch (door)
        { 
            case DoorType.ENTRANCE:
                OpenAngle *= -1f;
                break;
            case DoorType.EXIT:
                CloseAngle *= -1f;
                break;
        }

        isIdle = false;
        OpenVector = new Vector3(0f, 0f, OpenAngle);
        CloseVector = new Vector3(0f, 0f, CloseAngle);
        transform.rotation = Quaternion.Euler(CloseVector);
    }

    public void OpenAnim()
    {
        StartCoroutine(OpenDoor());
    }

    public void CloseAnim()
    {
        StartCoroutine(CloseDoor());
    }

    public IEnumerator OpenDoor()
    {
        isIdle = false;

        float timer = 0f;

        while (timer < OpenTime)
        {
            timer += Time.deltaTime;

            Vector3 EulerRot = new Vector3(0f, 0f, Mathf.Lerp(CloseAngle, OpenAngle, timer / OpenTime));
            transform.rotation = Quaternion.Euler(EulerRot);
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        CloseAnim();
    }

    public IEnumerator CloseDoor()
    {

        float timer = 0f;

        while (timer < OpenTime)
        {
            timer += Time.deltaTime;
           
            Vector3 EulerRot = new Vector3(0f, 0f, Mathf.Lerp(OpenAngle, CloseAngle, timer / OpenTime));
            transform.rotation = Quaternion.Euler(EulerRot);
            yield return null;
        }

        isIdle = true;
    }
}
