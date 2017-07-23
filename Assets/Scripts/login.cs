using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
using System;
using System.Text;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;

public class login : MonoBehaviour {
    //login script contains the code for registration and login functions
    //login script contains the code for upload game score function
    //login script contains the code for rank and download game score function

    //Mentions:
    //we complete the most part of this function: the game will create a json file that 
    //stores user email and score and upload it to firebase database after gameover. 
    //It can retrieve the data of top 5 users from database. However we cannot parse
    //the DataSnapshot object retrieved from database or convert it to string.
    //Therefore, we do not have a string to pass to the game UI which would should the ranking information
    //Thus the game cannot show the ranking.

    //Summary:
    //Able to upload score and get ranked score back in DataSnapshot form
    //Scores can be viewed by developer in the Firebase database website
    //Unable to show the ranked score due to format convert issues

    protected Firebase.Auth.FirebaseAuth auth;//authantication 
	private DatabaseReference rankdata;//database

	public InputField logname;
	public InputField logpass;
	public InputField regnam;
	public InputField regpass;
	public InputField rankboard;
    public Text rank_board_text;

    private int userScore;
	private static string uEmail;//user email
	private string uPassword;//user password

	// Use this for initialization
	void Start () {


		auth = Firebase.Auth.FirebaseAuth.DefaultInstance;//auth instance

		//FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tetristest-d27a7.firebaseio.com/");//set database url
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		

	public void signIn(){//log in function
		
		uEmail = logname.text; 
		uPassword = logpass.text;
		auth.SignInWithEmailAndPasswordAsync (uEmail, uPassword).ContinueWith((authTask) => {
			if (authTask.IsCompleted){//if login success
				jumptogame();//goto game
			}
			else{//do not go to game if not success
				return;
			}
		});
			
	}


	public void register(){
		uEmail = regnam.text;
		uPassword = regpass.text;
		auth.CreateUserWithEmailAndPasswordAsync(uEmail, uPassword).ContinueWith((authTask) => {
			if (authTask.IsCompleted){///if registration success, go to game
				
				logname.text = uEmail;//update registrated email and password,
				logpass.text = uPassword;//sign in after registration
				signIn();
				//addUserToDB();//add new user to database
				jumptogame();
			}
			else{//do not go to game if not success
				return;
			}
		});
	}



	public class user//user object, add user data to firebaseDB
	{
		public string email;
		public string score;
		public user(){}
		public user( string email, string score){
			this.email=email;
			this.score=score;
		}

	}



	public void addUserToDB(){//unable to push with costum key, unable to search child's key by child's value,firebase documentation is obsecure
		auth.SignOut ();//signout to prevent invaild auth credential error
		//public access,can read,write w/out login
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tetristest-d27a7.firebaseio.com/");//initialize database,set url
		rankdata = FirebaseDatabase.DefaultInstance.RootReference;//database reference
		user newUser = new user (uEmail, "0");//create new user
		string json = JsonUtility.ToJson (newUser);//convert user data to json
		//rankdata.Child ("users").Child (auth.CurrentUser.UserId).SetRawJsonValueAsync (json);
		string nodeID = rankdata.Push().Key;
		rankdata.Child (nodeID).SetRawJsonValueAsync (json);//upload email and score to database
		//rankdata.Child(nodeID).UpdateChildrenAsync(uEmail,json);//replace key w/ user email
	}




	public void addScoreToDB(int userScore ){// upload user email and score to DB
		string score = userScore.ToString();
		if (auth.CurrentUser != null){
			uEmail = auth.CurrentUser.Email;
			auth.SignOut ();//signout to prevent invaild auth credential error
		}
		//public access,can read,write w/out login
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tetristest-d27a7.firebaseio.com/");//initialize database,set url
		rankdata = FirebaseDatabase.DefaultInstance.RootReference;//database reference
		user newUser = new user (uEmail, score);//create new user
		string json = JsonUtility.ToJson (newUser);//convert user data to json
		string nodeID = rankdata.Push().Key;
		rankdata.Child (nodeID).SetRawJsonValueAsync (json);//upload email and score to database

		//ranking ();//getranking

	}


	public void ranking(){//get ranking
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tetristest-d27a7.firebaseio.com/");//initialize database,set url
        //rankdata = FirebaseDatabase.DefaultInstance.RootReference.OrderByChild("score").LimitToFirst(20);//database reference
        FirebaseDatabase.DefaultInstance.RootReference.OrderByChild("score").LimitToFirst(5).GetValueAsync().ContinueWith(task =>
        {if(task.IsFaulted){
            return;//if error,return
            }
            else if (task.IsCompleted){//display top 5 palyer's email and score
            DataSnapshot rank = task.Result;
            Debug.Log(task.Result.Value);
            rank_board_text.text = rank.Value.ToString();
            }

        }

        );
    }

   

	public void jumptogame(){
		Application.LoadLevel("GameStart");
	}










}
