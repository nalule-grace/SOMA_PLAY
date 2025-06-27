using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class spawnModels : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabsToSpawn;

    private ARTrackedImageManager imageManager;
    private Dictionary <string, GameObject> spawnedPrefabs = new();

    void Awake()
    {
        imageManager = GetComponent<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        imageManager.trackedImagesChanged += OnTrackedImagesChanged;
        InitializePrefabs();
    }

    void OnDisable()
    {
        imageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void InitializePrefabs()
    {
        foreach (var prefab in prefabsToSpawn)
        {
            var instance = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            instance.name = prefab.name;
            instance.SetActive(false);
            spawnedPrefabs[prefab.name] = instance;
        }
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var img in args.added)
            UpdateImage(img);

        foreach (var img in args.updated)
            UpdateImage(img);

        foreach (var img in args.removed)
            DisableImage(img);
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        if (spawnedPrefabs.TryGetValue(trackedImage.referenceImage.name, out var prefab))
        {
            bool isTracking = trackedImage.trackingState == TrackingState.Tracking;
            prefab.SetActive(isTracking);
            if (isTracking)
            {
                prefab.transform.SetPositionAndRotation(trackedImage.transform.position, trackedImage.transform.rotation);
            }
        }
    }

    private void DisableImage(ARTrackedImage trackedImage)
    {
        if (spawnedPrefabs.TryGetValue(trackedImage.referenceImage.name, out var prefab))
        {
            prefab.SetActive(false);
        }
    }
}

