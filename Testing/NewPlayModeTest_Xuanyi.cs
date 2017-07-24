// Xuanyi Zhang
// Team Name: Spirit
// CMPS 115

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

	//axis used determine the block position, with all necessary functional methods
	public static Transform[,] boundaryaxis = new Transform[boundarywidth, boundaryheight];

	//variables to produce score
	private int number_of_canceled_lines = 0;
	public int score_cancel_oneline = 10;
	public int score_cancel_twoline = 30;
	public int score_cancel_threeline = 70;
	public int score_cancel_fourline = 250;
	private int easymode_current_score = 0;//score
	public int easyScore;

	[Test]
	public void NewPlayModeTestSimplePasses() {
		// Use the Assert class to test conditions.
	}

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode

	[Test]
	public void removerowtest() {
		var go = new GameObject();
		Game blocktest = go.AddComponent<Game>();
		blocktest.removerow();
		LogAssert.Expect(LogType.Log, "Log message");
		Debug.Log("Log message");
		Debug.LogError("Error message");
		LogAssert.Expect(LogType.Error, "Error message");
	}

	[Test]
	public void removerow2test() {
		var go = new GameObject();
		Game2 blocktest = go.AddComponent<Game2>();
		blocktest.removerow2();
		LogAssert.Expect(LogType.Log, "Log message");
		Debug.Log("Log message");
		Debug.LogError("Error message");
		LogAssert.Expect(LogType.Error, "Error message");
	}

	[Test]
	public void removerow1test(){
		var go = new GameObject();
		Game1 blocktest = go.AddComponent<Game1>();
		blocktest.removerow1();
		LogAssert.Expect(LogType.Log, "Log message");
		Debug.Log("Log message");
		Debug.LogError("Error message");
		LogAssert.Expect(LogType.Error, "Error message");
	}

	[Test]
	public void gamescore2test(){
		var go = new GameObject();
		Game2 blocktest = go.AddComponent<Game2>();
		blocktest.gamescore_function2();
		LogAssert.Expect(LogType.Log, "Log message");
		Debug.Log("Log message");
		Debug.LogError("Error message");
		LogAssert.Expect(LogType.Error, "Error message");
	}

	[Test]
	public void generateblock2test(){
		var go = new GameObject();
		Game2 blocktest = go.AddComponent<Game2>();
		blocktest.generatenextblock2();
		LogAssert.Expect(LogType.Log, "Log message");
		Debug.Log("Log message");
		Debug.LogError("Error message");
		LogAssert.Expect(LogType.Error, "Error message");
	}


}