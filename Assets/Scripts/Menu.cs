using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

    //Menu script stores all methods for page jump
    //Menu script is attched to a game object in all scenes which uses the jump fucntion
    //An onclick function is added and call the jump function by the Unity enginee


    //playagain method jump to GameContent scene (Easy mode)
    public void PlayAgainmethod() {
        Application.LoadLevel("GameContent");
    }

    //playagain1 method jump to GameContent1 scene (Normal mode)
    public void PlayAgainmethod1() {
		Application.LoadLevel("GameContent 1");
	}

    //playagain2 method jump to GameContent scene (Hard mode)
    public void PlayAgainmethod2() {
		Application.LoadLevel("GameContent 2");
	}

    //GotoStartMenumethod jump to the GameStart scene (difficulty selection)
    public void GotoStartMenumethod(){
		Application.LoadLevel ("GameStart");
	}

    //GotoLoginMenumethod jump to the LogIn scene (login and register page)
    public void GotoLoginMenumethod()
    {
        Application.LoadLevel("LogIn");
    }

}
