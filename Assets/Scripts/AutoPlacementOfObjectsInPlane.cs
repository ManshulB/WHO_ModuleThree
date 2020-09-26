using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARPlaneManager))]
public class AutoPlacementOfObjectsInPlane : MonoBehaviour
{
    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
    //[SerializeField]
    //private GameObject welcomePanel;

    [SerializeField]
    private GameObject placedPrefab_rescued;

    private GameObject placedObject_rescued;

    [SerializeField]
    private GameObject placedPrefab_rescuer;

    private GameObject placedObject_rescuer;

    //[SerializeField]
    //private Button dismissButton;

    [SerializeField]
    private ARPlaneManager arPlaneManager;

    void Awake()
    {
        //dismissButton.onClick.AddListener(Dismiss);
        arPlaneManager = GetComponent<ARPlaneManager>();
        arPlaneManager.planesChanged += PlaneChanged;
    }

    private void PlaneChanged(ARPlanesChangedEventArgs args)
    {
        if (args.added != null && placedObject_rescued == null)
        {
            ARPlane arPlane = args.added[0];
            var rotation_rescued = Quaternion.LookRotation(arPlane.transform.position);
            rotation_rescued *= Quaternion.Euler(0, 180, 0);
            placedObject_rescued = Instantiate(placedPrefab_rescued, arPlane.transform.position, rotation_rescued);
            var rotation_rescuer = Quaternion.LookRotation(arPlane.transform.position);
            rotation_rescuer *= Quaternion.Euler(0, 90, -90);
            placedObject_rescuer = Instantiate(placedPrefab_rescuer, new Vector3((arPlane.transform.position.x)-0.3F,arPlane.transform.position.y, (arPlane.transform.position.z) + 0.5F), rotation_rescuer);
        }
    }

    //private void Dismiss() => welcomePanel.SetActive(false);
}
