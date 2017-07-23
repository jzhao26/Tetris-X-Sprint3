using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game2 : MonoBehaviour {

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

    private int number_of_canceled_lines2 = 0;
    public int score_cancel_oneline2 = 60;
    public int score_cancel_twoline2 = 180;
    public int score_cancel_threeline2 = 800;
    public int score_cancel_fourline2 = 2200;//Score is higher than Game and Game1
    private int hardmode_current_score = 0;
    public Text hardmode_score;
	public int hardScore;

    void Start() {
        generatenextblock2();
    }

    void Update() {
        gamescore_function2();
        gamescore_helper_function2();
    }

    public void gamescore_helper_function2()
    {

        hardmode_score.text = hardmode_current_score.ToString();
    }


	public int returnScore2(){
		hardScore = hardmode_current_score;
		return hardScore;
	}

    public void gamescore_function2()
    {
        if (number_of_canceled_lines2 > 0)
        {
            if (number_of_canceled_lines2 == 4)
            {
                hardmode_current_score = hardmode_current_score + score_cancel_fourline2;
            }
            else if (number_of_canceled_lines2 == 3)
            {
                hardmode_current_score = hardmode_current_score + score_cancel_threeline2;
            }
            else if (number_of_canceled_lines2 == 2)
            {
                hardmode_current_score = hardmode_current_score + score_cancel_twoline2;
            }
            else if (number_of_canceled_lines2 == 1)
            {
                hardmode_current_score = hardmode_current_score + score_cancel_oneline2;
            }

            number_of_canceled_lines2 = 0;
        }
    }

    public bool checkwithinboundary2(Vector2 pos) {

        return ((int)pos.x >= 1 && (int)pos.x <= 9 && (int)pos.y >= 1);

    }

    public Vector2 Round2(Vector2 pos) {
        return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
    }

    public void generatenextblock2() {
		int random_x = Random.Range (3,8);
		float x_value = 1.0f * random_x;//blocks generate from random locations within the game area
		GameObject next_block1 = (GameObject)Instantiate(Resources.Load(get_block_names2(), typeof(GameObject)), new Vector2(x_value, 22.0f), Quaternion.identity);
    }


    string get_block_names2() {
        int random_num = Random.Range(1, 8);
        string random_block_name = "Prefabs 2/theBLOCK_J";//gameobjects is called from folder Prefabs 2
        switch (random_num) {

            case 1:
                random_block_name = "Prefabs 2/theBLOCK_J";
                break;
            case 2:
                random_block_name = "Prefabs 2/theBLOCK_L";
                break;
            case 3:
                random_block_name = "Prefabs 2/theBLOCK_long";
                break;
            case 4:
                random_block_name = "Prefabs 2/theBLOCK_s";
                break;
            case 5:
                random_block_name = "Prefabs 2/theBLOCK_square";
                break;
            case 6:
                random_block_name = "Prefabs 2/theBLOCK_T";
                break;
            case 7:
                random_block_name = "Prefabs 2/theBLOCK_z";
                break;
        }
        return random_block_name;
    }



    public void update_boundary2(Blocks2 block) {

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
            Vector2 pos = Round2(mino.position);
            if (pos.y < boundaryheight) {
                boundaryaxis[(int)pos.x, (int)pos.y] = mino;
            }
        }

    }

    public Transform TransBlockHeplerFunc2(Vector2 pos) {
        if (pos.y >= boundaryheight - 1) {
            return null;
        } else {
            return boundaryaxis[(int)pos.x, (int)pos.y];
        }
    }

    public bool isrowfull2(int y) {

        for (int x = 1; x < boundarywidth; ++x) {
            if (boundaryaxis[x, y] == null) {
                return false;
            }
        }
        number_of_canceled_lines2++;
        return true;
    }

    public void deletetherow2(int y) {

        for (int x = 1; x < boundarywidth; ++x) {
            Destroy(boundaryaxis[x, y].gameObject);
            boundaryaxis[x, y] = null;
        }

    }

    public void movetherowdown2(int y) {

        for (int x = 1; x < boundarywidth; ++x) {

            if (boundaryaxis[x, y] != null) {

                boundaryaxis[x, y - 1] = boundaryaxis[x, y];
                boundaryaxis[x, y] = null;
                boundaryaxis[x, y - 1].position += new Vector3(0, -1, 0);

            }
        }

    }

    public void moveentirerowdown2(int y) {

        for (int j = y; j < boundaryheight; ++j) {
            movetherowdown2(j);
        }

    }

    public void removerow2() {

        for (int y = 1; y < boundaryheight; ++y) {

            if (isrowfull2(y)) {
                deletetherow2(y);
                moveentirerowdown2(y + 1);
                --y;
            }

        }

    }

    public bool isoverlimit2(Blocks2 theblock){

        for (int x = 0; x < boundarywidth; ++x) {

            foreach (Transform mino in theblock.transform) {
                Vector2 theposition = Round2(mino.position);
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
