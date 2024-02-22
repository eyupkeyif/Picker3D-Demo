using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Misc;
using UnityEditor.PackageManager.Requests;
public class CheckPointTile : TileBase
{
    public override TileType TileType => TileType.CheckPointTile;

    private int reqNumber;
    [SerializeField] private CheckBox checkBox;
    [SerializeField] private Transform leftGate,rightGate;
    [SerializeField] private Transform successTile;
    Vector3 firstPosition;

    private void Start() {
        Initialize();

        EventManager.GameEvents.SuccessEvent+=Reset;
        EventManager.GameEvents.FailEvent+=Reset;
    }
    public void Setup(int checkPointAmount)
    {
        reqNumber = checkPointAmount;
        checkBox.Setup(reqNumber);
    }

    public override void Initialize()
    {
        base.Initialize();
        firstPosition = successTile.position;
    }
    private void OnTriggerEnter(Collider other) {

        Picker picker = other.gameObject.GetComponent<Picker>();
        if (picker)
        {
            Debug.Log("hit");
            EventManager.LevelEvents.CheckPointEvent?.Invoke();
            Timer.Instance.TimerWait(2f,()=>CheckTotal());
        }
    }

    private void CheckTotal()
    {
        var counter = checkBox.GetCounter();

        if (counter>=reqNumber)
        {
            StartCoroutine(AscendTile(1f));
        }

        else
        {
            EventManager.GameEvents.FailEvent?.Invoke();
        }
    }

    private IEnumerator AscendTile(float time)
    {
        float timer = 0;
        Vector3 ascendedPos = new Vector3(successTile.transform.position.x,0,successTile.transform.position.z);

        while (timer<=time)
        {

            successTile.transform.position = Vector3.MoveTowards(successTile.position,ascendedPos,timer/time);         
            timer+=Time.fixedDeltaTime;

            yield return new WaitForFixedUpdate();
        }

        EventManager.GameEvents.PassedEvent?.Invoke();
        StartCoroutine(OpenGate(1f));


    }

    public void Reset()
    {
        checkBox.Reset();
        successTile.position = firstPosition;
        leftGate.eulerAngles = new Vector3(90,90,90);
        rightGate.eulerAngles = new Vector3(90,90,90);
    }

    private IEnumerator OpenGate(float time)
    {

        float timer = 0;
        while (timer<=time)
        {
            float rotateX = timer/time;
            rightGate.rotation = Quaternion.Euler(90 + 90*rotateX,90,90);
            leftGate.rotation = Quaternion.Euler(90-90*rotateX,90,90);
            timer+=Time.fixedDeltaTime;

            yield return new WaitForFixedUpdate();
        }


    }

    
}
