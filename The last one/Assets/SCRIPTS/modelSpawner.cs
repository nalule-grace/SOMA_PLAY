using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class modelSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabsToSpawn;

    private ARTrackedImageManager imageManager;

    // Store instantiated models by image name
    private Dictionary<string, GameObject> spawnedPrefabs = new();
    private Dictionary<string, GameObject> originalPrefabs = new(); // Reference prefab to instantiate fresh

    void Awake()
    {
        imageManager = GetComponent<ARTrackedImageManager>();
        InitializeOriginalPrefabs();
    }

    void OnEnable()
    {
        imageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        imageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    // Store reference to original prefabs to instantiate fresh when resetting
    private void InitializeOriginalPrefabs()
    {
        foreach (var prefab in prefabsToSpawn)
        {
            originalPrefabs[prefab.name] = prefab;
        }
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var trackedImage in args.added)
            UpdateOrSpawn(trackedImage);

        foreach (var trackedImage in args.updated)
            UpdateOrSpawn(trackedImage);
    }

    private void UpdateOrSpawn(ARTrackedImage trackedImage)
    {
        string imageName = trackedImage.referenceImage.name;

        // If prefab already spawned, update its transform
        if (spawnedPrefabs.TryGetValue(imageName, out GameObject spawned))
        {
            // Always update position & rotation if tracking
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                spawned.transform.SetPositionAndRotation(trackedImage.transform.position, trackedImage.transform.rotation);
                spawned.SetActive(true);
            }
            // If not tracking, keep the model active and static (do nothing)
        }
        else
        {
            // Spawn prefab if not existing and tracking
            if (trackedImage.trackingState == TrackingState.Tracking && originalPrefabs.ContainsKey(imageName))
            {
                GameObject newInstance = Instantiate(originalPrefabs[imageName], trackedImage.transform.position, trackedImage.transform.rotation);
                spawnedPrefabs[imageName] = newInstance;
            }
        }
    }

    public void ResetModels()
    {
        // Destroy all spawned prefabs
        foreach (var pair in spawnedPrefabs)
        {
            Destroy(pair.Value);
        }
        spawnedPrefabs.Clear();
    }
}
