using System;
using NUnit.Framework;

internal class AreaTests
{
    [Test]
    public void Instantiate()
    {
        //Arrange
        Area area1 = new(10, 1);
        Area area2 = new(10);

        //Assert

        Assert.IsNotNull(area1);
        Assert.IsNotNull(area2);
    }

    [TestCase(-1, -1)]
    [TestCase(0, 0)]
    [TestCase(999, -1)]
    public void WhenInstantiateWithInvalidArgumentsThenException(int count, int maxCount)
    {
        Assert.Catch<ArgumentException>(() => new Area(maxCount, count));
        Assert.Catch<ArgumentException>(() => new Area(maxCount));
    }

    [Test]
    public void AddResourceSuccessful()
    {
        //Arrange
        Area area = new(10);

        //Act

        area.AddResource(1, out int change);

        //Assert

        Assert.AreEqual(1, area.Count);
    }

    [Test]
    public void WhenAddResourcesOverLimitThenResourcesOverLimitBurn()
    {
        //Arrange
        Area area = new(10);

        //Act

        area.AddResource(12, out int change);

        //Assert

        Assert.AreEqual(area.Count, area.MaxCount);
    }

    [TestCase(3, 0)]
    [TestCase(3, 3)]
    [TestCase(3, 4)]
    [TestCase(3, 10)]
    public void CanAddResources(int maxCount, int count)
    {
        //Arrange
        Area area = new(maxCount);

        //Act

        bool canAdd = area.CanAddResources(count);

        //Assert

        Assert.AreEqual(maxCount >= count, canAdd);
    }

    [TestCase(3, 0)]
    [TestCase(3, 3)]
    public void CanRemoveResources(int maxCount, int count)
    {
        //Arrange
        Area area = new(maxCount);

        //Act
        area.AddResource(count, out int change);

        bool canAdd = area.CanRemoveResources(count);

        //Assert

        Assert.AreEqual(count >= 0, canAdd);
    }

    [TestCase(10, 5, 3)]
    [TestCase(10, 3, 5)]
    public void RemoveResourcessSuccessful(int maxCount, int count, int demandedResources)
    {
        //Arrange
        Area area = new(maxCount);

        //Act

        area.AddResource(count, out int change);
        int takedResources = area.RemoveResources(demandedResources);

        //Assert

        Assert.AreEqual(Math.Min(demandedResources, count), takedResources);
    }

    [TestCase(-2)]
    [TestCase(0)]
    public void WhenRemoveResourcesWithInvalidArgumentsThenException(int count)
    {
        var area = new Area(10);

        Assert.Catch<ArgumentException>(() =>
        {
            area.RemoveResources(count);
        });
    }
}