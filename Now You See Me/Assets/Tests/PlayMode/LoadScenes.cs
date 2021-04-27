using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
namespace Tests
{
    public class LoadScenes
    {
        [Test]
        public void LoadStartMenuScene()
        {
            Application.LoadLevel(0);
            int ID = SceneManager.GetActiveScene().buildIndex;
            Assert.AreNotEqual(0, ID);
        }

        [Test]
        public void LoadLabScreen()
        {
            Application.LoadLevel(1);
            int ID = SceneManager.GetActiveScene().buildIndex;
            Assert.AreNotEqual(1, ID);
        }

        [Test]
        public void LoadStudentWinScreen()
        {
            Application.LoadLevel(2);
            int ID = SceneManager.GetActiveScene().buildIndex;
            Assert.AreNotEqual(2, ID);
        }

        [Test]
        public void LoadVirusWinScreen()
        {
            Application.LoadLevel(3);
            int ID = SceneManager.GetActiveScene().buildIndex;
            Assert.AreNotEqual(3, ID);
        }

    }
}
