using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickerController : MonoBehaviour 
{
    // Start is called before the first frame update
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private float speed,swipeSpeed;
    [SerializeField] private Rigidbody rb;
    private bool isMoving=false;

    Vector3 startPos,draggingPos;
    Vector3 mousePos;
    Vector3 lastMousePos;
    [SerializeField] private Vector2 clampValues;
    

    private void Start() 
    {    
        cameraManager.StartFollow(gameObject);        
    }
    void FixedUpdate()
    {
        if (isMoving)
        {
            Move();
        }
        
        
    }
    private void Update() {

        Swipe();

    }

    void Move(){

        Vector3 movePos = Vector3.forward*speed*Time.fixedDeltaTime;
        Vector3 newPos = transform.position + movePos + draggingPos; 

        var offsetX = Mathf.Clamp(newPos.x,clampValues.x,clampValues.y);

        newPos = new Vector3(offsetX,newPos.y,newPos.z);
        rb.MovePosition(newPos);
       
        // transform.position += movePos + draggingPos;
        // float clampX = Mathf.Clamp(transform.position.x,clampValues.x,clampValues.y);
        // transform.position = new Vector3 (clampX,transform.position.y,transform.position.z);
        

    }

    
    void Swipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,10));
            lastMousePos = startPos;
        }
        else if (Input.GetMouseButton(0))
        {   
            
            mousePos= Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,10));

           if (startPos!=mousePos)
           {
                float xDiff = mousePos.x-lastMousePos.x;
                // Debug.Log(xDiff);
                draggingPos.x = xDiff*Time.fixedDeltaTime*swipeSpeed;
                draggingPos.y = 0;
                draggingPos.z = 0;

                lastMousePos = mousePos;

           }
                
            

        }
        else if (Input.GetMouseButtonUp(0))
        {
            draggingPos=Vector3.zero;
        }            
                
                
    }

    public void IsMoving(bool isMoving){
        this.isMoving=isMoving;
    }
}
