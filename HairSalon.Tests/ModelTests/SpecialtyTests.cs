using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class SpecialtyTest : IDisposable
  {
    //
    public void Dispose()
    {
      Specialty.ClearAll();
    }

    [TestMethod]
    public void SpecialtyConstructor_CreatesInstanceOfSpecialty_Specialty()
    {
      Specialty newSpecialty = new Specialty("test stylist");
      Assert.AreEqual(typeof(Specialty), newSpecialty.GetType());
    }

    [TestMethod]
    public void GetName_ReturnsName_String()
    {
      //Arrange
      string name = "Test Specialty";
      Specialty newSpecialty = new Specialty(name);

      //Act
      string result = newSpecialty.GetName();

      //Assert
      Assert.AreEqual(name, result);
    }

    [TestMethod]
    public void GetStylists_ReturnsEmptyStylistList_StylistList()
    {
      //Arrange
      string name = "Ann";
      Specialty newSpecialty = new Specialty(name);
      List<Stylist> newList = new List<Stylist> { };

      //Act
      List<Stylist> result = newSpecialty.GetStylists();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }
  }
}
