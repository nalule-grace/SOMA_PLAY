using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class modelSpawner : MonoBehaviour
{
     [SerializeField] private List<GameObject> prefabsToSpawn;

    private ARTrackedImageManager imageManager;
    private Dictionary <string, GameObject> spawnedPrefabs = new();
    private HashSet<string> lockedPrefbas = new();
    private Dictionary<string, (Vector3 position, Quaternion rotation)> defaultTransforms = new();

    void Awake()
    {
        imageManager = GetComponent<ARTrackedImageManager>();
        InitializePrefabs();
    }

    void OnEnable()
    {
        imageManager.trackedImagesChanged += OnTrackedImagesChanged;
       // InitializePrefabs();
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


            defaultTransforms[prefab.name] =(instance.transform.position, instance.transform.rotation);
        }
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var img in args.added)
            UpdateImage(img);

        foreach (var img in args.updated)
            UpdateImage(img);

        //foreach (var img in args.removed)
            //DisableImage(img);
    }

    /*private void UpdateImage(ARTrackedImage trackedImage)
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
    }*/
    private void UpdateImage(ARTrackedImage trackedImage)
    {
        string imageName = trackedImage.referenceImage.name;

        if (spawnedPrefabs.TryGetValue(imageName, out var prefab))
        {
            if(!lockedPrefbas.Contains(imageName))
            {
                if(trackedImage.trackingState == TrackingState.Tracking)
                {
                    prefab.transform.SetPositionAndRotation(trackedImage.transform.position, trackedImage.transform.rotation);
                    prefab.SetActive(true);
                    lockedPrefbas.Add(trackedImage.referenceImage.name);
                }
            }
        }
    }

    /*private void DisableImage(ARTrackedImage trackedImage)
    {
        if (spawnedPrefabs.TryGetValue(trackedImage.referenceImage.name, out var prefab))
        {
            prefab.SetActive(false);
        }
    }*/
    public void ResetModels()
    {
        foreach (var pair in spawnedPrefabs)
        {
            pair.Value.SetActive(false);
            if(defaultTransforms.TryGetValue(pair.Key, out var defaultTransform))
            {
            pair.Value.transform.position = Vector3.zero;
            pair.Value.transform.rotation = Quaternion.identity;
            }
            
        }
        lockedPrefbas.Clear();
    }
}
