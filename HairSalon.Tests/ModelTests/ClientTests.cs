using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class ClientTest : IDisposable
  {

    public void Dispose()
    {
      Client.ClearAll();
    }

    public ClientTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=mathew_akre_test;";
    }

    [TestMethod]
    public void ClientConstructor_CreatesInstanceOfClient_Client()
    {
      Client newClient = new Client("test");
      Assert.AreEqual(typeof(Client), newClient.GetType());
    }

    [TestMethod]
    public void GetName_ReturnsName_String()
    {
      //Arrange
      string name = "Susan";
      Client newClient = new Client(name);

      //Act
      string result = newClient.GetName();

      //Assert
      Assert.AreEqual(name, result);
    }

    [TestMethod]
    public void SetName_SetName_String()
    {
      //Arrange
      string name = "Susan";
      Client newClient = new Client(name);

      //Act
      string updatedName = "Jesse";
      newClient.SetName(updatedName);
      string result = newClient.GetName();

      //Assert
      Assert.AreEqual(updatedName, result);
    }

    [TestMethod]
    public void GetAll_ReturnsEmptyListFromDatabase_ClientList()
    {
      //Arrange
      List<Client> newList = new List<Client> { };

      //Act
      List<Client> result = Client.GetAll();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfNameAreTheSame_Client()
    {
      Client firstClient = new Client("Lisa");
      Client secondClient = new Client("Lisa");
      Assert.AreEqual(firstClient, secondClient);
    }
    [TestMethod]
    public void Save_SavesToDatabase_ClientList()
    {
      Client testClient = new Client("Lisa");
      testClient.Save();
      List<Client> result = Client.GetAll();
      List<Client> testList = new List<Client>{testClient};
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void GetAll_ReturnsClients_ClientList()
    {
      //Arrange
      string name01 = "Susan";
      string name02 = "Sophie";
      Client newClient1 = new Client(name01);
      newClient1.Save();
      Client newClient2 = new Client(name02);
      newClient2.Save();
      List<Client> newList = new List<Client> { newClient1, newClient2 };

      //Act
      List<Client> result = Client.GetAll();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Save_AssignsIdObject_Id()
    {
      Client testClient = new Client("Susan");
      testClient.Save();
      Client savedClient = Client.GetAll()[0];

      int result = savedClient.GetId();
      int testId = testClient.GetId();

      Assert.AreEqual(testId, result);

    }
    // [TestMethod]
    // public void GetId_ClientsInstantiateWithAnIdAndGetterReturns_Int()
    // {
    //   //Arrange
    //   string name = "Walk the dog.";
    //   Item newItem = new Item(name);
    //
    //   //Act
    //   int result = newItem.GetId();
    //
    //   //Assert
    //   Assert.AreEqual(1, result);
    // }
    //
    [TestMethod]
    public void Find_ReturnsCorrectItemFromDatabase_Item()
    {
      //Arrange
      Item testItem = new Item("Mow the lawn");
      testItem.Save();

      //Act
      Item foundItem = Item.Find(testItem.GetId());

      //Assert
      Assert.AreEqual(testItem, foundItem);
    }


  }
}
