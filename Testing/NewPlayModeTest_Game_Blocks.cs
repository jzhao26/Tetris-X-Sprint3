using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

public class NewPlayModeTest {

    //variables for the range of game area
    public static int boundaryheight = 20;
    public static int boundarywidth = 10;

    //axis used determine the block position, used in all necessary functional methods
    public static Transform[,] boundaryaxis = new Transform[boundarywidth, boundaryheight];

    //variables for generating score
    private int number_of_canceled_lines = 0;
    public int score_cancel_oneline = 10;
    public int score_cancel_twoline = 30;
    public int score_cancel_threeline = 70;
    public int score_cancel_fourline = 250;
    private int easymode_current_score = 0;//score
    public int easyScore;

    [Test]
    public void NewPlayModeTestSimplePasses()
    {
        // Use the Assert class to test conditions.
    }

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode

   
    [Test]
    public void movetoright() {
        var go = new GameObject();
        var origionalpositon = go.transform.position;
        go.transform.position += new Vector3(1, 0, 0);
        var position2 = go.transform.position;

        if (origionalpositon.Equals(position2))
        {
            Assert.Fail();
        }
        else {
            Assert.Pass();
        }
    }

    [Test]
    public void movetoleft()
    {
        var go = new GameObject();
        var origionalpositon = go.transform.position;
        go.transform.position += new Vector3(-1, 0, 0);
        var position2 = go.transform.position;

        if (origionalpositon.Equals(position2))
        {
            Assert.Fail();
        }
        else
        {
            Assert.Pass();
        }
    }

    [Test]
    public void movedown()
    {
        var go = new GameObject();
        var origionalpositon = go.transform.position;
        go.transform.position += new Vector3(0, -1, 0);
        var position2 = go.transform.position;

        if (origionalpositon.Equals(position2))
        {
            Assert.Fail();
        }
        else
        {
            Assert.Pass();
        }
    }


    [Test]
    public void generateblocktest()
    {
        var go = new GameObject();
        Game blocktest = go.AddComponent<Game>();
        blocktest.generatenextblock();
        LogAssert.Expect(LogType.Log, "Log message");
        Debug.Log("Log message");
        Debug.LogError("Error message");
        LogAssert.Expect(LogType.Error, "Error message");
    }

    [Test]
    public void gamescoretest() {
        var go = new GameObject();
        Game blocktest = go.AddComponent<Game>();
        blocktest.gamescore_function();
        LogAssert.Expect(LogType.Log, "Log message");
        Debug.Log("Log message");
        Debug.LogError("Error message");
        LogAssert.Expect(LogType.Error, "Error message");
    }

    [Test]
    public void checkwithinboundarytest() {
        var go = new GameObject();
        Game blocktest = go.AddComponent<Game>();
        go.transform.position = new Vector2(1,2);
        if (blocktest.checkwithinboundary(go.transform.position).Equals(true))
        {
            Assert.Pass();
        }
        else {
            Assert.Fail();
        }

    }

}
