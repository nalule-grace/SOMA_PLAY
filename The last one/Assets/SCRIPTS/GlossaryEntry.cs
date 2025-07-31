using UnityEngine;

[System.Serializable]
public class GlossaryEntry 
{
    public string term;
    [TextArea] public string description;
    public Sprite image;
}