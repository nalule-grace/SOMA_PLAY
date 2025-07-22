using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GlossaryManager : MonoBehaviour
{
    [System.Serializable]
    public class GlossaryEntry
    {
        public string term;
        [TextArea] public string description;
        public Sprite image;
    }

    [Header("Glossary Data")]
    public List<GlossaryEntry> glossaryList = new List<GlossaryEntry>();
    private Dictionary<string, GlossaryEntry> glossaryDict;

    [Header("UI References")]
    public TMP_InputField searchInput;
    public Image detailImage;
    public TMP_Text detailTitle;
    public TMP_Text detailDescription;
    public GameObject detailPanel;

    void Awake()
    {
        glossaryDict = new Dictionary<string, GlossaryEntry>();
        foreach (var entry in glossaryList)
        {
            if (!string.IsNullOrWhiteSpace(entry.term))
                glossaryDict[entry.term.ToLower()] = entry;
        }
    }

    void Start()
    {
        searchInput.onEndEdit.AddListener(OnSearchSubmitted);
        detailPanel.SetActive(false);
    }

    void OnSearchSubmitted(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            detailPanel.SetActive(false);
            return;
        }

        if (glossaryDict.TryGetValue(input.ToLower(), out var entry))
        {
            detailTitle.text = entry.term;
            detailDescription.text = entry.description;
            detailImage.sprite = entry.image;
            detailPanel.SetActive(true);
        }
        else
        {
            detailTitle.text = "Not Found";
            detailDescription.text = $"No entry for \"{input}\".";
            detailImage.sprite = null;
            detailPanel.SetActive(true);
        }
    }
}

