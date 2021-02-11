using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;


public class ChangeTrackingMode : MonoBehaviour
{
    [SerializeField]
    private ARSessionOrigin _sessionOrigin;
    [SerializeField]
    private Text Mode;

    private ARPlaneManager _planeManager = null;
    private ARTrackedImageManager _trackedImageManager = null;

    private bool _planeManagerCached = false;
    private bool _trackedImageManagerCached = false;

    public ARPlaneManager cachedPlaneManager
    {
        get
        {
            if (!_planeManagerCached)
            {
                _planeManagerCached = true;
                _planeManager = _sessionOrigin.GetComponent<ARPlaneManager>();
            }
            return _planeManager;
        }
    }

    public ARTrackedImageManager cachedTrackedImageManager
    {
        get
        {
            if (!_trackedImageManagerCached)
            {
                _trackedImageManagerCached = true;
                _trackedImageManager = _sessionOrigin.GetComponent<ARTrackedImageManager>();
            }
            return _trackedImageManager;
        }
    }

    private void Start()
    {
        cachedPlaneManager.enabled = true;
        cachedTrackedImageManager.enabled = false;
    }

    public void ChangeMode()
    {
        if (cachedPlaneManager.enabled)
        {
            cachedPlaneManager.enabled = false;
            cachedTrackedImageManager.enabled = true;
            Mode.GetComponent<Text>().text = "Tracked image mod";
        }
        else
        if (cachedTrackedImageManager.enabled)
        {
            cachedPlaneManager.enabled = true;
            cachedTrackedImageManager.enabled = false;
            Mode.GetComponent<Text>().text = "Plane mod";
        }
    }
}
