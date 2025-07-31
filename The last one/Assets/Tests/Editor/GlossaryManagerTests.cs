using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Reflection;

[TestFixture]
public class GlossaryManagerTests
{
    private GameObject testObject;
    private object managerInstance;
    private System.Type managerType;

    [SetUp]
    public void Setup()
    {
        testObject = new GameObject();
        testObject.AddComponent<Text>(); // Just in case needed
        var glossaryManager = testObject.AddComponent<GlossaryManager>();

        // Store reflection context
        managerInstance = glossaryManager;
        managerType = glossaryManager.GetType();

        // Inject fake glossaryDict via reflection
        var glossaryDictField = managerType.GetField("glossaryDict", BindingFlags.NonPublic | BindingFlags.Instance);
        glossaryDictField.SetValue(managerInstance, new Dictionary<string, GlossaryEntry>
        {
            { "heart", new GlossaryEntry { term = "Heart", description = "Pumps blood", image = null } },
            { "earth", new GlossaryEntry { term = "Earth", description = "Third planet", image = null } }
        });

        // Optionally inject UI references too if needed for rendering
    }

    [Test]
    public void OnSearchSubmitted_ValidTerm_DisplaysCorrectDescription()
    {
        // Access private method via reflection
        var method = managerType.GetMethod("OnSearchSubmitted", BindingFlags.NonPublic | BindingFlags.Instance);
        method.Invoke(managerInstance, new object[] { "heart" });

        // Use reflection to access private UI field for validation (optional)
        var detailTitleField = managerType.GetField("detailTitle", BindingFlags.NonPublic | BindingFlags.Instance);
        var detailTitle = (Text)detailTitleField.GetValue(managerInstance);

        Assert.AreEqual("Heart", detailTitle.text); // Adjust depending on what you're validating
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(testObject);
    }
}
