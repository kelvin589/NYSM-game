using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


public class PrefebLoading
{
    // A Test behaves as an ordinary method
    [Test]
    public void SpawnedPlayer()
    {
        var PlayerPrefab = Resources.Load<GameObject>("PhotonPrefabs/Player 1 Controller");
        Assert.AreNotEqual(PlayerPrefab, null); //Verifies that the var has found the player Prefab by verifying that it is not Null

    }

    [Test]
    public void SpawnedDeadBody()
    {
        var DeadObject = GameObject.Instantiate(Resources.Load<GameObject>("DeadPhotonPrefabs/Dead Player 1 Controller"), new Vector3(0, 0, 10), new Quaternion(0, 180, 0, 0));
        Assert.AreNotEqual(DeadObject, null); //Verifies that the var has found the player Prefab by verifying that it is not Null
    }

    [Test]
    public void SpawnedCamera()
    {
        var DeadObject = GameObject.Instantiate(Resources.Load<GameObject>("Main Camera"), new Vector3(0, 0, 10), new Quaternion(0, 180, 0, 0));
        Assert.AreNotEqual(DeadObject, null); //Verifies that the var has found the player Prefab by verifying that it is not Null
    }

    [Test]
    public void SpawnedTasks()
    {
        var TaskObject = GameObject.Instantiate(Resources.Load<GameObject>("TaskPrefabs/0Task"), new Vector3(0, 0, 10), new Quaternion(0, 180, 0, 0));
        Assert.AreNotEqual(TaskObject, null); //Verifies that the var has found the player Prefab by verifying that it is not Null
    }
}
