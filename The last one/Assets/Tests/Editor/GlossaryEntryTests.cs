using NUnit.Framework;
using UnityEngine;

public class GlossaryEntryTests
{
    [Test]
    public void GlossaryEntry_CanBeCreatedAndAssigned()
    {
        // Arrange
        var entry = new GlossaryEntry();
        string testTerm = "Photosynthesis";
        string testDescription = "The process by which plants make food using sunlight.";
        Sprite testSprite = Sprite.Create(Texture2D.blackTexture, new Rect(0, 0, 10, 10), Vector2.zero);

        // Act
        entry.term = testTerm;
        entry.description = testDescription;
        entry.image = testSprite;

        // Assert
        Assert.AreEqual(testTerm, entry.term);
        Assert.AreEqual(testDescription, entry.description);
        Assert.AreEqual(testSprite, entry.image);
    }

    [Test]
    public void GlossaryEntry_DefaultValues_AreNullOrEmpty()
    {
        var entry = new GlossaryEntry();

        Assert.IsTrue(string.IsNullOrEmpty(entry.term));
        Assert.IsTrue(string.IsNullOrEmpty(entry.description));
        Assert.IsNull(entry.image);
    }
}
