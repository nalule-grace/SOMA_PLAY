using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MultpleImages : MonoBehaviour
{
[SerializeField] private List<GameObject> prefabsToSpawn = new List<GameObject>();

    private ARTrackedImageManager _trackedImageManager;
    private Dictionary<string, GameObject> _spawnedPrefabs = new Dictionary<string, GameObject>();

    private void Awake()
    {
        _trackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    private void OnEnable()
    {
        if (_trackedImageManager != null)
            _trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;

        InitializePrefabs();
    }

    private void OnDisable()
    {
        if (_trackedImageManager != null)
            _trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void InitializePrefabs()
    {
        foreach (GameObject prefab in prefabsToSpawn)
        {
            GameObject spawned = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            spawned.name = prefab.name;
            spawned.SetActive(false);
            _spawnedPrefabs.Add(spawned.name, spawned);
        }
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
            UpdateImage(trackedImage);

        foreach (var trackedImage in eventArgs.updated)
            UpdateImage(trackedImage);

        foreach (var trackedImage in eventArgs.removed)
            DisableImage(trackedImage);
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        string imageName = trackedImage.referenceImage.name;

        if (!_spawnedPrefabs.ContainsKey(imageName))
        {
            Debug.LogWarning($"No prefab assigned for reference image: {imageName}");
            return;
        }

        GameObject prefab = _spawnedPrefabs[imageName];

        if (trackedImage.trackingState == TrackingState.Tracking)
        {
            prefab.SetActive(true);
            prefab.transform.position = trackedImage.transform.position;
            prefab.transform.rotation = trackedImage.transform.rotation;
        }
        else
        {
            prefab.SetActive(false);
        }
    }

    private void DisableImage(ARTrackedImage trackedImage)
    {
        string imageName = trackedImage.referenceImage.name;

        if (_spawnedPrefabs.ContainsKey(imageName))
        {
            _spawnedPrefabs[imageName].SetActive(false);
        }
    }

}
