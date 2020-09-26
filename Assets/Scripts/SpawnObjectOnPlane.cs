using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
[RequireComponent(typeof(ARPlaneManager))]
public class SpawnObjectOnPlane : MonoBehaviour
{
    // Start is called before the first frame update
    /*void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
    private ARRaycastManager raycastManager;
    private GameObject spawnedObject;
    private GameObject spawnedObject_rescuer;
    //public Animation anim;

    [SerializeField]
    private GameObject PlaceablePrefab;

    [SerializeField]
    private GameObject PlaceablePrefab_rescuer;

    //[SerializeField]
    //private Camera arCamera;

    //[SerializeField]
    //private ARPlaneManager arPlaneManager;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    float direction;
    public float zoomSpeed;

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        //arPlaneManager = GetComponent<ARPlaneManager>();
        //arPlaneManager.planesChanged += PlaneChanged;
    }

    //private void PlaneChanged(ARPlanesChangedEventArgs args)
    //{
    //    if (args.added != null && PlaceablePrefab==null)
    //    {
    //        ARPlane arPlane = args.added[0];
    //        spawnedObject = Instantiate(PlaceablePrefab, arPlane.transform.position, Quaternion.identity);
    //    }
    //}

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        touchPosition = default;
        return false;
    }

    private void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))
        {
            return;
        }
        if (raycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = s_Hits[0].pose;
            if (spawnedObject == null)
            {
                var rotation_rescued = Quaternion.LookRotation(hitPose.position);
                rotation_rescued *= Quaternion.Euler(0, 180, 0);
                spawnedObject = Instantiate(PlaceablePrefab, hitPose.position, rotation_rescued);
                //PlaceablePrefab_rescuer.transform.localScale(0.5, 0.5, 0.5);
                var rotation_rescuer = Quaternion.LookRotation(hitPose.position);
                rotation_rescuer *= Quaternion.Euler(0, 90, -90);
                spawnedObject_rescuer = Instantiate(PlaceablePrefab_rescuer, new Vector3((hitPose.position.x) - 0.5F, hitPose.position.y, (hitPose.position.z) + 0.3F), rotation_rescuer);

            }
            else
            {
                //spawnedObject.transform.position = hitPose.position;
                //spawnedObject.transform.rotation = hitPose.rotation;
                //spawnedObject_rescuer.transform.position = hitPose.position;
                //spawnedObject_rescuer.transform.rotation = hitPose.rotation;
            }
            //anim.Play("New Animation");
        }

        /////////////////////////////////////////////////////////////////////////////////
        //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        //{

        //    // Get movement of the finger since last frame
        //    Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

        //    //actual touch
        //    Touch touchZero = Input.GetTouch(0);
        //    //current touch x position
        //    float touchZeroCurrPos = touchZero.position.x;
        //    //previous touch x position
        //    float touchZeroPrevPos = touchZero.position.x - touchDeltaPosition.x;
        //    //difference between previous and current finger position
        //    float Diff = touchZeroPrevPos - touchZeroCurrPos;


        //    if (touchZeroCurrPos < touchZeroPrevPos)
        //    {
        //        Debug.Log("right");
        //        direction = -1;
        //        spawnedObject.transform.Rotate(Vector3.up, -Diff * Time.deltaTime * zoomSpeed);
        //    }
        //    if (touchZeroCurrPos > touchZeroPrevPos)
        //    {
        //        Debug.Log("left");
        //        direction = 1;
        //        spawnedObject.transform.Rotate(Vector3.down, Diff * Time.deltaTime * zoomSpeed);
        //    }

        //}


        ////////////////////////////////////////////////////////////////////////////////
    }
    public void SetPrefabType(GameObject prefabType)
    {
        PlaceablePrefab = prefabType;
    }
}
