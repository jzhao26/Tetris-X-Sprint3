using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blocks : MonoBehaviour {
    //Blocks script contains all the code attached to all blocks during the game process
    //it contains functions used by blocks to achieve Tetris game feature
    //Functions are attached to blocks by the Unity enginee
    //it also contains codes for game control on both android platform and test equipments

    //Mentions:
    //the functions for Blocks, Blocks1, Blocks2 are same
    //I used three versions of code due to the restrictions of Unity enginee
    //they have few differences refers to features of different difficulty levels
    //variables are named differently according to the naming convensions
    //see the comments in Blocks script for Blocks, Blocks1, Blocks2 scripts
    //Blocks1, Blocks2 only contains comment for the different features and Reference information

    //Reference:
    //The framework of how to construct an Android tetris game is inspried by
    //a YouTube PC Tetris game tutorial series uploaded by "The Weekly Coder"
    //We used his method to achieve the basic functions of the Tetris game
    //tutorials can be found by the following link
    //https://www.youtube.com/watch?v=aurEgWxDfQQ
    //the website contains links for the tutorials of the rest of series

    //declare variables 
    float fall = 0;
	public float falling_speed = 1;//variables used to make block fall by 1 unit per frame
	public bool allowedrotation = true;
	public bool limitedrotation = false;//variables used to restrict block rotations
    public Button rightbutton;
    public Button leftbutton;
    public Button rotatebutton;
    public Button downbutton;
	public Button easy_mode_button;
	public Button normal_mode_button;
	public Button hard_mode_button;//variables for all buttons
	int helpernum;

    // Use this for initialization
    void Start() {

        rightbutton = GameObject.FindGameObjectWithTag("Rightbutton").GetComponent<Button>(); 
        rightbutton.onClick.AddListener(() => movetoright());

        leftbutton = GameObject.FindGameObjectWithTag("Leftbutton").GetComponent<Button>();
        leftbutton.onClick.AddListener(() => movetoleft());

        rotatebutton = GameObject.FindGameObjectWithTag("Rotatebutton").GetComponent<Button>();
        rotatebutton.onClick.AddListener(() => buttonrotatefunc());

        downbutton = GameObject.FindGameObjectWithTag("Downbutton").GetComponent<Button>();
        downbutton.onClick.AddListener(() => movedownbutton());
        //add clicklistener method in order to check the button click 
        //if the button is clicked, run the function specified
    }
	
	// Update is called once per frame
	void Update () {
		func_move_block ();
        //move block is called to make block fall once per frame
    }
		
	//code to move the blocks and make block falling
	void func_move_block(){

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            transform.position += new Vector3 (1,0,0);
			if(validposition()){
				FindObjectOfType<Game> ().update_boundary (this);
			}else{
				transform.position += new Vector3 (-1,0,0);
			}
			
		}else if(Input.GetKeyDown(KeyCode.LeftArrow)){

			transform.position += new Vector3 (-1,0,0);
			if(validposition()){
				FindObjectOfType<Game> ().update_boundary (this);
			}else{
				transform.position += new Vector3 (1,0,0);
			}
			
		}else if(Input.GetKeyDown(KeyCode.UpArrow)){
			
			if(allowedrotation){
				if (limitedrotation) {
					if (transform.rotation.eulerAngles.z >= 90) {
						transform.Rotate (0, 0, -90);
					} else {
						transform.Rotate (0, 0, 90);
					}
				} else {
					transform.Rotate (0, 0, 90);
				}	
			}

			if(validposition()){
				FindObjectOfType<Game> ().update_boundary (this);
			}else{
				if(transform.rotation.eulerAngles.z >= 90){
					transform.Rotate (0, 0, -90);
				}else{
					transform.Rotate (0, 0, 90);
				}
			}
		//code above are used for test purposes during the early phase of software development
        //make blocks able to be controlled by PC before buttons are added

		}else if(Input.GetKeyDown(KeyCode.DownArrow) || (Time.time - fall) >= falling_speed){
            //this part of functions makes block fall down at a specific speed
			transform.position += new Vector3 (0,-1,0);
			if(validposition()){//if valid position, process movement, update axis information
				FindObjectOfType<Game> ().update_boundary (this);
			}else{
				//if not valid position
				transform.position += new Vector3 (0,1,0);//cancel movement by move in opposite direction
				FindObjectOfType<Game> ().removerow ();//check if need to remove row

                if (FindObjectOfType<Game>().isoverlimit(this)) {//check if game is over
					FindObjectOfType<login> ().addScoreToDB (FindObjectOfType<Game> ().returnScore());//call function in login to upload score to DB
                    FindObjectOfType<Game>().Gameend();//if game is over, call Gameend to end the game

                }

				enabled = false;//if block reaches ground, disable its movement
				FindObjectOfType<Game> ().generatenextblock ();//then generate the next random block
				
			}
			fall = Time.time;

		}
	}

    
    //functions to check if the block is within the game range and with no violation on movement
    //used to restrict the block movement and rotations
	public bool validposition(){

		foreach (Transform mino in transform){
			Vector2 pos = FindObjectOfType<Game> ().Round (mino.position);//get position
			if(FindObjectOfType<Game>().checkwithinboundary(pos) == false){
				return false;//check if not in game area
			}
			if(FindObjectOfType<Game>().TransBlockHeplerFunc(pos) != null && FindObjectOfType<Game>().TransBlockHeplerFunc(pos).parent != transform){
				return false;//check if valid movement
			}
		}
		return true;//if both case false, allow the movement
	}

    //function called by the right button listener
    //move the block to right when the movement is allowed by validposition method
    public void movetoright() {
		if(enabled == true)	//if not disabled
        {
            transform.position += new Vector3(1, 0, 0);//move to right
            if (validposition())
            {//if valid, update coordiante information
                FindObjectOfType<Game>().update_boundary(this);
            }
            else
            {//if not valid, cancel movement
                transform.position += new Vector3(-1, 0, 0);
            }
        }
        else {
            //enabled = false;
        }
        
    }

    //function called by the left button listener
    //move the block to left when the movement is allowed by validposition method
    public void movetoleft() {
		if (enabled == true)//if not disabled
        {
            transform.position += new Vector3(-1, 0, 0);//move to left
            if (validposition())
            {//if valid, update coordiante information
                FindObjectOfType<Game>().update_boundary(this);
            }
            else
            {//if not valid, cancel movement
                transform.position += new Vector3(1, 0, 0);
            }
        }
        else {
            //enabled = false;
        }
    }

    //function called by the rotate button listener
    //rotate the block when the movement is allowed by validposition method
    public void buttonrotatefunc() {
		if (enabled == true) {//if not disabled
            if (allowedrotation)
            {
                if (limitedrotation)
                {//check rotate condidtions and rotate according to different conditions
                    if (transform.rotation.eulerAngles.z >= 90)
                    {
                        transform.Rotate(0, 0, -90);
                    }
                    else
                    {
                        transform.Rotate(0, 0, 90);
                    }
                }
                else
                {
                    transform.Rotate(0, 0, 90);
                }
            }

            if (validposition())//if valid movement
            {//update coordinate information
                FindObjectOfType<Game>().update_boundary(this);
            }
            else
            {//if not valid, cancel movement
                if (transform.rotation.eulerAngles.z >= 90)
                {
                    transform.Rotate(0, 0, -90);
                }
                else
                {
                    transform.Rotate(0, 0, 90);
                }
            }
        }
    }


    //function called by the down button listener
    //move the block down when the movement is allowed by validposition method
    public void movedownbutton() {
        transform.position += new Vector3(0, -1, 0);
        if (validposition())//if valid movement
        {//update coordinate information
            FindObjectOfType<Game>().update_boundary(this);
        }
        else
        {
            //if not valid, cancel movement
            transform.position += new Vector3(0, 1, 0);
            
            //if game over, call gameend function to end the game
            if (FindObjectOfType<Game>().isoverlimit(this))
            {
                FindObjectOfType<Game>().Gameend();
            }

            
            

        }
        
    }
   
}
