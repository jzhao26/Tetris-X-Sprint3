using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {
    //Game script stores all functional methods for the tetris game
    //Game script is attached to the boundary (wall) object by the unity enginee
    //in order to achieve a functional game process

    //Mentions:
    //the functions for Game, Game1, Game2 are same
    //I used three versions of code due to the restrictions of Unity enginee
    //they have few differences refers to features of different difficulty levels
    //variables are named differently according to the naming convensions
    //see the comments in Game script for Game, Game1, Game2 scripts
    //Game1, Game2 only contains comment for the different features and Reference information

    //Reference:
    //The framework of how to construct an Android tetris game is inspried by
    //a YouTube PC Tetris game tutorial series uploaded by "The Weekly Coder"
    //We used his method to achieve the basic functions of the Tetris game
    //tutorials can be found by the following link
    //https://www.youtube.com/watch?v=aurEgWxDfQQ
    //the website contains links for the tutorials of the rest of series

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
    public Text easymode_score;


    // Use this for initialization
    void Start() {
        generatenextblock();
    }

    // Update is called once per frame
    void Update() {
        gamescore_function();
        gamescore_helper_function();
        //update the score during the game
    }

	public int returnScore(){//return score
		easyScore = easymode_current_score;
		return easyScore;
	}

    public void gamescore_helper_function() {
        easymode_score.text = easymode_current_score.ToString();
        //convert the current score to text format
    }

    //function for adding scores
    public void gamescore_function()
    {   
        if (number_of_canceled_lines > 0)//if line cancels, add score, else not
        {
            if (number_of_canceled_lines == 4) {
                easymode_current_score = easymode_current_score + score_cancel_fourline;
            } else if (number_of_canceled_lines == 3) {
                easymode_current_score = easymode_current_score + score_cancel_threeline;
            } else if (number_of_canceled_lines == 2) {
                easymode_current_score = easymode_current_score + score_cancel_twoline;
            } else if (number_of_canceled_lines == 1) {
                easymode_current_score = easymode_current_score + score_cancel_oneline;
            } // add different scores according to number of lines canceled
              // max possible lines to be cancelled is 4

            number_of_canceled_lines = 0;
            // initialize to zero after adding scores
        }
    }

    //function to check whether the block is in game area or not
    public bool checkwithinboundary(Vector2 pos) {
        return ((int)pos.x >= 1 && (int)pos.x <= 9 && (int)pos.y >= 1);
    }

    //function to call Mathf.Round function 
    public Vector2 Round(Vector2 pos) {
        return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
    }

    //function to generate the game block during the game process
    //get_block_names is called to get a random generated block
    //Vector2(5.0f, 22.0f) decides the generate position which is a fixed position in easy mode
    public void generatenextblock() {
        GameObject next_block = (GameObject)Instantiate(Resources.Load(get_block_names(), typeof(GameObject)), new Vector2(5.0f, 22.0f), Quaternion.identity);
    }

    //get_block_names is used to select a random block
    //an random interger is used by a switch function to achieve this goal
    string get_block_names() {
        int random_num = Random.Range(1, 8);//random number
        string random_block_name = "Prefabs/theBLOCK_J";//initialize variable
        switch (random_num) {

            case 1:
                random_block_name = "Prefabs/theBLOCK_J";
                break;
            case 2:
                random_block_name = "Prefabs/theBLOCK_L";
                break;
            case 3:
                random_block_name = "Prefabs/theBLOCK_long";
                break;
            case 4:
                random_block_name = "Prefabs/theBLOCK_s";
                break;
            case 5:
                random_block_name = "Prefabs/theBLOCK_square";
                break;
            case 6:
                random_block_name = "Prefabs/theBLOCK_T";
                break;
            case 7:
                random_block_name = "Prefabs/theBLOCK_z";
                break;
        }
        return random_block_name;
    }

    //update_boundary function is used to update the block position when a row is cancelled
    //it oscillates between all game coordinates to check the required case
    public void update_boundary(Blocks block) {

        for (int y = 1; y < 20; y++) {
            for (int x = 1; x < 10; x++) {

                if (boundaryaxis[x, y] != null) {
                    if (boundaryaxis[x, y].parent == block.transform) {
                        boundaryaxis[x, y] = null;
                    }
                }

            }
        }

        foreach (Transform mino in block.transform) {
            Vector2 pos = Round(mino.position);
            if (pos.y < boundaryheight) {
                boundaryaxis[(int)pos.x, (int)pos.y] = mino;
            }
        }

    }

    //a helper function used in block script to restrict block transform
    //ensure all transforms are valid
    public Transform TransBlockHeplerFunc(Vector2 pos) {
        if (pos.y >= boundaryheight - 1) {
            return null;
        } else {
            return boundaryaxis[(int)pos.x, (int)pos.y];
        }
    }

    //osciallate all x coordinates to check if row is full at row y
    public bool isrowfull(int y) {

        for (int x = 1; x < boundarywidth; ++x) {
            if (boundaryaxis[x, y] == null) {
                return false;
            }
        }
        number_of_canceled_lines++;
        return true;
    }

    //method to delete a specific row when a row is full
    public void deletetherow(int y) {

        for (int x = 1; x < boundarywidth; ++x) {
            Destroy(boundaryaxis[x, y].gameObject);
            boundaryaxis[x, y] = null;
        }

    }

    //method to move the row down after a row is cancelled
    public void movetherowdown(int y) {

        for (int x = 1; x < boundarywidth; ++x) {

            if (boundaryaxis[x, y] != null) {

                boundaryaxis[x, y - 1] = boundaryaxis[x, y];
                boundaryaxis[x, y] = null;
                boundaryaxis[x, y - 1].position += new Vector3(0, -1, 0);

            }
        }

    }

    //method calls movetherowdown to move all rows down by 1 unit
    public void moveentirerowdown(int y) {

        for (int j = y; j < boundaryheight; ++j) {
            movetherowdown(j);
        }

    }

    //main method for row cancel and coordinates update by calling specific functions
    //it checks if a row is full
    //if full, delete row and move all rows down by 1 unit
    public void removerow() {

        for (int y = 1; y < boundaryheight; ++y) {

            if (isrowfull(y)) {
                deletetherow(y);
                moveentirerowdown(y + 1);
                --y;
            }

        }

    }

    //function used to check if the highest block has been over the top boundary
    //it is used to end the game, game over situation
    public bool isoverlimit(Blocks theblock){

        for (int x = 0; x < boundarywidth; ++x) {

            foreach (Transform mino in theblock.transform) {
                Vector2 theposition = Round(mino.position);
                if (theposition.y >= 19 ) {
                    return true;
                }
            }

        }
        return false;
    }

    //Gameend function is a method to jump to the gameover scene
    public void Gameend() {
        Application.LoadLevel("GameOver");
    }

    
}
