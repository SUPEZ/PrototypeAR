using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class SpawnDummyOnPlane : MonoBehaviour
{

    private ARRaycastManager _raycastManager;
    private GameObject _spawnedObject;
    private List<GameObject> _placedPrefabList = new List<GameObject>();

    [SerializeField]
    private int _maxPrefabSpawnCount = 0;
    private int _placementPrefabCount;

    [SerializeField]
    private GameObject _placeablePrefab;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        _raycastManager = GetComponent<ARRaycastManager>();
    }

    private bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                touchPosition = Input.GetTouch(0).position;
                return true;
            }
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

        if (_raycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;
            if (_placementPrefabCount < _maxPrefabSpawnCount)
            {
                SpawnPrefab(hitPose);
            }
        }
    }

    public void SetPrefabType(GameObject prefabType)
    {
        _placeablePrefab = prefabType;
    }

    private void SpawnPrefab(Pose hitPose)
    {
        _spawnedObject = Instantiate(_placeablePrefab, hitPose.position,hitPose.rotation);
        _placedPrefabList.Add(_spawnedObject);
        _placementPrefabCount++;
    }

}
