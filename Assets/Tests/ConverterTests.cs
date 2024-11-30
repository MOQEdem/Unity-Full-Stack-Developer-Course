using System;
using System.Runtime.CompilerServices;
using NUnit.Framework;

public class ConverterTests
{
    [Test]
    public void Instantiate()
    {
        //Arrange
        Converter converter = new(3, 3, 3, 5, 3);

        //Assert
        Assert.IsNotNull(converter);
    }

    [Test]
    public void CanPutResources()
    {
        // Arrange
        Converter converter = new(10, 10,3,5,3);

        //Act
        converter.PutConversionResources(9, out int overflow);
        bool hasFreeSpace = converter.HasFreeSpace();

        //Assert
        Assert.IsTrue(hasFreeSpace);
    }

    [Test]
    public void CanTakeResources()
    {
        // Arrange
        Converter converter = new(10, 10,3,5,3);

        //Act
        converter.PutConversionResources(10, out int overflow);
        bool canTake = converter.CanTakeResources();

        //Assert
        Assert.IsTrue(canTake);
    }

    [Test]
    public void PutResourcesSuccessful()
    {
        // Arrange
        Converter converter = new(10, 10,3,5,3);

        //Act
        converter.PutConversionResources(5, out int overflow);

        //Assert
        Assert.AreEqual(converter.ConversionResourcesCount, 5);
    }

    [Test]
    public void TakeResourcesSuccessful()
    {
        // Arrange
        Converter converter = new(10, 10,3,5,3);

        //Act
        converter.PutConversionResources(10, out int overflow);
        converter.TakeResourcesToConvert();

        //Assert
        Assert.AreEqual(converter.ConversionResourcesCount, 5);
    }

    [Test]
    public void TurnOffSuccessful()
    {
        // Arrange
        Converter converter = new(10, 10,3,5,3);

        //Act
        converter.PutConversionResources(10, out int overflow);
        converter.SetActive(true);

        for (int i = 0; i < 2; i++)
        {
            converter.Update(1f);
        }

        converter.SetActive(false);

        for (int i = 0; i < 10; i++)
        {
            converter.Update(1f);
        }

        //Assert
        Assert.AreEqual(converter.ConvertedResourcesCount, 0);
        Assert.AreEqual(converter.ConversionResourcesCount, 10);
        Assert.IsFalse(converter.IsActive);
    }

    [Test]
    public void ConvertResourcesSuccessful()
    {
        // Arrange
        Converter converter = new(10, 10,3,5,3);

        //Act
        converter.PutConversionResources(10, out int overflow);
        converter.SetActive(true);

        for (int i = 0; i < 12; i++)
        {
            converter.Update(1f);
        }

        //Assert
        Assert.AreEqual(converter.ConvertedResourcesCount, 6);
        Assert.AreEqual(converter.ConversionResourcesCount, 0);
        Assert.IsFalse(converter.IsActive);
    }

    [Test]
    public void WhenUpdateWithInvaliDeltaTimeThenException()
    {
        //Assert

        Assert.Catch<ArgumentException>(() =>
        {
          Converter converter = new(10, 10, 3, 5, 3);  
          converter.Update(-1f);
        });
    }

    [TestCase(0)]
    [TestCase(-2)]
    public void WhenPutConversationResourcesWithInvalidArgumentsThenException(int count)
    {
        //Assert

        Assert.Catch<ArgumentException>(() =>
        {
            Converter converter = new(10, 10, 3, 5, 3);  
            converter.PutConversionResources(count, out int overflow);
        });
    }
}