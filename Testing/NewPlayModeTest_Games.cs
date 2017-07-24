//Name: Weiyi Xia
//unit test for the project
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

public class NewPlayModeTest
{

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
    public void isrowfulltest()
    {
        var go = new GameObject();
        Game rowtest = go.AddComponent<Game>();
        rowtest.isrowfull(5);
        LogAssert.Expect(LogType.Log, "Log message");
        Debug.Log("Log message");
        Debug.LogError("Error message");
        LogAssert.Expect(LogType.Error, "Error message");
    }



    [Test]
    public void validpositiontest()
    {
        var go = new GameObject();
        Blocks positiontest = go.AddComponent<Blocks>();
        positiontest.validposition();
        LogAssert.Expect(LogType.Log, "Log message");
        Debug.Log("Log message");
        Debug.LogError("Error message");
        LogAssert.Expect(LogType.Error, "Error message");
    }



    [Test]
    public void isrowfull1test()
    {
        var go = new GameObject();
        Game1 rowtest = go.AddComponent<Game1>();
        rowtest.isrowfull1(3);
        LogAssert.Expect(LogType.Log, "Log message");
        Debug.Log("Log message");
        Debug.LogError("Error message");
        LogAssert.Expect(LogType.Error, "Error message");
    }

    [Test]
    public void isrowfull2test()
    {
        var go = new GameObject();
        Game2 rowtest = go.AddComponent<Game2>();
        rowtest.isrowfull2(7);
        LogAssert.Expect(LogType.Log, "Log message");
        Debug.Log("Log message");
        Debug.LogError("Error message");
        LogAssert.Expect(LogType.Error, "Error message");
    }





    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
}

