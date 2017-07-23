using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;

//author: yilin xu
//tests the login, registar function and database upload and retrive data function;

public class NewPlayModeTest {

	[Test]
	public void logInTest() {//test login
		// Use the Assert class to test conditions.
		var go = new GameObject();
		login logTest = go.AddComponent<login>();


		//login logTest = new login();
		logTest.logname = go.AddComponent<InputField> ();
		logTest.logpass = go.GetComponent<InputField> ();
		logTest.auth = Firebase.Auth.FirebaseAuth.DefaultInstance;



		logTest.logname.text = "a"+"@"+"a.cc";
		logTest.logpass.text = "aaaaaa";
		logTest.signIn ();
		if (logTest.auth.CurrentUser.UserId.Equals (logTest.logname.text)) {
			Assert.Pass ();
		} else {
			Assert.Fail ();
		}
	}

	[Test]
	public void regTest(){//test register

		var go = new GameObject();
		login logTest = go.AddComponent<login>();


		//login logTest = new login();
		logTest.regnam = go.AddComponent<InputField> ();
		logTest.regpass = go.GetComponent<InputField> ();
		logTest.logname = go.GetComponent<InputField> ();
		logTest.logpass = go.GetComponent<InputField> ();
		logTest.auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

		string[] randomStr = new string[12]{ "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l" };

		logTest.regnam.text =  randomStr[Random.Range(0,11)]+randomStr[Random.Range(0,11)]
			+randomStr[Random.Range(0,11)]+randomStr[Random.Range(0,11)]+"@a.cc";
		logTest.regpass.text = "aaaaaa";

		logTest.logname.text = logTest.regnam.text;
		logTest.logpass.text = logTest.regpass.text;

		logTest.register ();
		if (logTest.auth.CurrentUser.UserId.Equals (logTest.logname.text)) {
			Assert.Pass ();
		} else {
			Assert.Fail ();
		}

	}


	[Test]
	public void databaseTese(){
		var go = new GameObject();
		login logTest = go.AddComponent<login>();

		//login logTest = new login();
		go.AddComponent<Text>();
		logTest.logname = go.AddComponent<InputField> ();
		logTest.logpass = go.GetComponent<InputField> ();//gComponent<InputField> ();
		logTest.auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
		logTest.rank_board_text = go.GetComponent<Text>();


		logTest.logname.text = "a"+"@"+"a.cc";
		logTest.logpass.text = "aaaaaa";
		logTest.signIn ();
		//login logTest = new login();
		//logTest.logname.text = "a@a.cc";

		logTest.ranking ();
		if (logTest.rank_board_text.text != null) {
			Assert.Pass ();
		} else {
			Assert.Fail ();

		}

	}

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator NewPlayModeTestWithEnumeratorPasses() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		yield return null;
	}
}
