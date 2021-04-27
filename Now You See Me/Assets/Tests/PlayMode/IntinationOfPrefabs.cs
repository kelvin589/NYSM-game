using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
namespace Tests
{
    public class IntiationOfPrefabs
    {
        [Test]
        public void IntiateTask()
        {
            var TaskPrefab = Resources.Load("TaskPrefabs/0Task");
            Assert.AreEqual(TaskPrefab.name, "0Task");
        }

        [Test]
        public void IntiatePlayer()
        {
            var PlayerPrefab = Resources.Load("PhotonPrefabs/Player 1 Controller");
            Assert.AreEqual(PlayerPrefab.name, "Player 1 Controller");
        }

        [Test]
        public void IntiateDeadBody()
        {
            var PlayerPrefab = Resources.Load("DeadPhotonPrefabs/Dead Player 1 Controller");
            Assert.AreEqual(PlayerPrefab.name, "Dead Player 1 Controller");
        }

    }
}
