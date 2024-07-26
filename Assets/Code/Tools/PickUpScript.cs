using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    public GameObject player;
    public Transform holdPos;
    //if you copy from below this point, you are legally required to like the video
    public float throwForce = 500f; //force at which the object is thrown at
    public float pickUpRange = 5f; //how far the player can pickup the object from
    private float rotationSensitivity = 1f; //how fast/slow the object is rotated in relation to mouse movement
    [SerializeField]
    private GameObject heldObj; //object which we pick up
    private Rigidbody heldObjRb; //rigidbody of object we pick up
    private bool canDrop = true; //this is needed so we don't throw/drop object when rotating the object
    private int LayerNumber; //layer index

    private SetOutline currentOutline = null;

    //Reference to script which includes mouse movement of player (looking around)
    //we want to disable the player looking around when rotating the object
    //example below

    public FirstPersonController mouseLookScript;
    float originalSenseValue;

    float XaxisRotation;
    float YaxisRotation;

    Vector3 startTapeRotation = new Vector3(0, -130, -100);

    void Start()
    {
        LayerNumber = LayerMask.NameToLayer("HoldLayer"); //if your holdLayer is named differently make sure to change this ""

        originalSenseValue = mouseLookScript.mouseSensitivity;
        //mouseLookScript = player.GetComponent<MouseLookScript>();
    }

    private void FixedUpdate()
    {
        RaycastHit ray;
        Physics.Raycast(transform.position + transform.TransformDirection(Vector3.forward), transform.TransformDirection(Vector3.forward), out ray, pickUpRange);

        if (heldObj == null) //if currently not holding anything
        {

            if (ray.transform.gameObject.TryGetComponent(out TapeBox box))
            {
                if (currentOutline == box.GetComponent<SetOutline>()) return;

                if (currentOutline && currentOutline != box.GetComponent<SetOutline>())
                    currentOutline.Show(false);

                currentOutline = box.GetComponent<SetOutline>();
                currentOutline.Show(true);
            }
            else
            {
                if(currentOutline)
                    currentOutline.Show(false);

                currentOutline = null;
            }
        }
        else
        {
            if (ray.transform.gameObject.TryGetComponent(out VideoPlayer vhs))
            {
                currentOutline = vhs.GetComponent<SetOutline>();
                currentOutline.Show(true);
            }
            else
            {
                if (currentOutline)
                    currentOutline.Show(false);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) //change E to whichever key you want to press to pick up
        {
            //perform raycast to check if player is looking at object within pickuprange
            RaycastHit hit;
            if (heldObj == null) //if currently not holding anything
            {
                //perform raycast to check if player is looking at object within pickuprange
                //RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
                {
                    Debug.Log(hit.transform.name);
                    //make sure pickup tag is attached
                    if (hit.transform.gameObject.TryGetComponent(out TapeBox box))
                    {
                        PickUpObject(hit.transform.gameObject);
                    }
                }
            }
            else
            {
                if (Physics.Raycast(transform.position + transform.TransformDirection(Vector3.forward), transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
                {
                    Debug.Log(hit.transform.name);
                    //make sure pickup tag is attached
                    if (hit.transform.gameObject.TryGetComponent(out VideoPlayer vhs))
                    {
                        VideoTapeSO data = heldObj.GetComponent<TapeBox>().TapeData;
                        vhs.InsertTape(data);
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (heldObj != null && canDrop == true)
            {
                StopClipping(); //prevents object from clipping through walls
                //DropObject();
                ThrowObject();
            }
        }

        if (heldObj != null) //if player is holding object
        {
            MoveObject(); //keep object position at holdPos
            RotateObject();
            //if (Input.GetKeyDown(KeyCode.Mouse0) && canDrop == true) //Mous0 (leftclick) is used to throw, change this if you want another button to be used)
            //{
            //    StopClipping();
            //    ThrowObject();
            //}

        }
    }
    void PickUpObject(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>()) //make sure the object has a RigidBody
        {
            heldObj = pickUpObj; //assign heldObj to the object that was hit by the raycast (no longer == null)
            heldObjRb = pickUpObj.GetComponent<Rigidbody>(); //assign Rigidbody
            heldObjRb.isKinematic = true;
            heldObjRb.transform.parent = holdPos.transform; //parent object to holdposition
            heldObj.layer = LayerNumber; //change the object layer to the holdLayer


            heldObj.transform.Rotate(startTapeRotation);

            //make sure object doesnt collide with player, it can cause weird bugs
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        }
    }
    void DropObject()
    {
        //re-enable collision with player
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.layer = 0; //object assigned back to default layer
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null; //unparent object
        heldObj = null; //undefine game object
    }
    void MoveObject()
    {
        //keep object position the same as the holdPosition position
        heldObj.transform.position = holdPos.transform.position;
    }
    void RotateObject()
    {

        if (Input.GetKeyUp(KeyCode.R))
        {
            mouseLookScript.mouseSensitivity = originalSenseValue;
        }

        if (Input.GetKey(KeyCode.R))//hold R key to rotate, change this to whatever key you want
        {
            canDrop = false; //make sure throwing can't occur during rotating

            //disable player being able to look around
            mouseLookScript.mouseSensitivity = 0f;

            XaxisRotation = Input.GetAxis("Mouse X") * rotationSensitivity;
            YaxisRotation = Input.GetAxis("Mouse Y") * rotationSensitivity;
            //rotate the object depending on mouse X-Y Axis
            heldObj.transform.Rotate(Vector3.down, XaxisRotation);
            heldObj.transform.Rotate(Vector3.right, YaxisRotation);

            return;
        }
        else
        {
            //re-enable player being able to look around
            canDrop = true;
        }

        XaxisRotation = Input.GetAxisRaw("Horizontal") * 0.8f * rotationSensitivity;
        YaxisRotation = Input.GetAxisRaw("Vertical") * 0.8f * rotationSensitivity;

        heldObj.transform.Rotate(Vector3.right, XaxisRotation);
        heldObj.transform.Rotate(Vector3.back, YaxisRotation);

        if (Input.GetKey(KeyCode.Q))
            heldObj.transform.Rotate(Vector3.up, 0.8f * rotationSensitivity);

        if (Input.GetKey(KeyCode.E))
            heldObj.transform.Rotate(Vector3.down, 0.8f * rotationSensitivity);
    }
    void ThrowObject()
    {
        //same as drop function, but add force to object before undefining it
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.layer = 0;
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null;
        heldObjRb.AddForce(transform.forward * throwForce);
        heldObj = null;
    }
    void StopClipping() //function only called when dropping/throwing
    {
        var clipRange = Vector3.Distance(heldObj.transform.position, transform.position); //distance from holdPos to the camera
        //have to use RaycastAll as object blocks raycast in center screen
        //RaycastAll returns array of all colliders hit within the cliprange
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), clipRange);
        //if the array length is greater than 1, meaning it has hit more than just the object we are carrying
        if (hits.Length > 1)
        {
            //change object position to camera position 
            heldObj.transform.position = transform.position + new Vector3(0f, -0.5f, 0f); //offset slightly downward to stop object dropping above player 
            //if your player is small, change the -0.5f to a smaller number (in magnitude) ie: -0.1f
        }
    }
}
