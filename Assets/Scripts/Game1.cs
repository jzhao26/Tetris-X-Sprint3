using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game1 : MonoBehaviour {

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

    public static int boundaryheight = 20;
    public static int boundarywidth = 10;
    public static Transform[,] boundaryaxis = new Transform[boundarywidth, boundaryheight];

    private int number_of_canceled_lines1 = 0;
    public int score_cancel_oneline1 = 30;
    public int score_cancel_twoline1 = 70;
    public int score_cancel_threeline1 = 200;
    public int score_cancel_fourline1 = 700;//score for Game1 is higher than Game
    private int normalmode_current_score = 0;
    public Text normalmode_score;
	public int normalScore;


    void Start() {
        generatenextblock1();
    }


    void Update() {
        gamescore_function1();
        gamescore_helper_function1();
    }

    public void gamescore_helper_function1()
    {
        normalmode_score.text = normalmode_current_score.ToString();
    }


	public int returnScore1(){
		normalScore = normalmode_current_score;
		return normalScore;
	}

    public void gamescore_function1()
    {
        if (number_of_canceled_lines1 > 0)
        {
            if (number_of_canceled_lines1 == 4)
            {
                normalmode_current_score = normalmode_current_score + score_cancel_fourline1;
            }
            else if (number_of_canceled_lines1 == 3)
            {
                normalmode_current_score = normalmode_current_score + score_cancel_threeline1;
            }
            else if (number_of_canceled_lines1 == 2)
            {
                normalmode_current_score = normalmode_current_score + score_cancel_twoline1;
            }
            else if (number_of_canceled_lines1 == 1)
            {
                normalmode_current_score = normalmode_current_score + score_cancel_oneline1;
            }

            number_of_canceled_lines1 = 0;
        }
    }

    public bool checkwithinboundary1(Vector2 pos) {

        return ((int)pos.x >= 1 && (int)pos.x <= 9 && (int)pos.y >= 1);

    }

    public Vector2 Round1(Vector2 pos) {
        return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
    }

    public void generatenextblock1() {
        GameObject next_block1 = (GameObject)Instantiate(Resources.Load(get_block_names1(), typeof(GameObject)), new Vector2(5.0f, 22.0f), Quaternion.identity);
    }

    string get_block_names1() {
        int random_num = Random.Range(1, 8);
        string random_block_name = "Prefabs 1/theBLOCK_J";//gameobjects is called from folder Prefabs 1
        switch (random_num) {

            case 1:
                random_block_name = "Prefabs 1/theBLOCK_J";
                break;
            case 2:
                random_block_name = "Prefabs 1/theBLOCK_L";
                break;
            case 3:
                random_block_name = "Prefabs 1/theBLOCK_long";
                break;
            case 4:
                random_block_name = "Prefabs 1/theBLOCK_s";
                break;
            case 5:
                random_block_name = "Prefabs 1/theBLOCK_square";
                break;
            case 6:
                random_block_name = "Prefabs 1/theBLOCK_T";
                break;
            case 7:
                random_block_name = "Prefabs 1/theBLOCK_z";
                break;
        }
        return random_block_name;
    }

    public void update_boundary1(Blocks1 block) {

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
            Vector2 pos = Round1(mino.position);
            if (pos.y < boundaryheight) {
                boundaryaxis[(int)pos.x, (int)pos.y] = mino;
            }
        }

    }

    public Transform TransBlockHeplerFunc1(Vector2 pos) {
        if (pos.y >= boundaryheight - 1) {
            return null;
        } else {
            return boundaryaxis[(int)pos.x, (int)pos.y];
        }
    }

    public bool isrowfull1(int y) {

        for (int x = 1; x < boundarywidth; ++x) {
            if (boundaryaxis[x, y] == null) {
                return false;
            }
        }
        number_of_canceled_lines1++;
        return true;
    }

    public void deletetherow1(int y) {

        for (int x = 1; x < boundarywidth; ++x) {
            Destroy(boundaryaxis[x, y].gameObject);
            boundaryaxis[x, y] = null;
        }

    }

    public void movetherowdown1(int y) {

        for (int x = 1; x < boundarywidth; ++x) {

            if (boundaryaxis[x, y] != null) {

                boundaryaxis[x, y - 1] = boundaryaxis[x, y];
                boundaryaxis[x, y] = null;
                boundaryaxis[x, y - 1].position += new Vector3(0, -1, 0);

            }
        }

    }

    public void moveentirerowdown1(int y) {

        for (int j = y; j < boundaryheight; ++j) {
            movetherowdown1(j);
        }

    }

    public void removerow1() {

        for (int y = 1; y < boundaryheight; ++y) {

            if (isrowfull1(y)) {
                deletetherow1(y);
                moveentirerowdown1(y + 1);
                --y;
            }

        }

    }

    public bool isoverlimit1(Blocks1 theblock){

        for (int x = 0; x < boundarywidth; ++x) {

            foreach (Transform mino in theblock.transform) {
                Vector2 theposition = Round1(mino.position);
                if (theposition.y >= 19 ) {
                    return true;
                }
            }

        }
        return false;
    }

    public void Gameend() {
        Application.LoadLevel("GameOver");
    }
}
