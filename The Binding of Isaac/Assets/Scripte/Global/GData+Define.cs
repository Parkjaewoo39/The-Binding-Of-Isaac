using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class GData
{    
    public const string SCENE_NAME_TITLE = "00.TitleScene";

    public const string SCENE_NAME_LOAD_STAGE_ONE = "11.StageScene1";

    public const string SCENE_NAME_LOAD_STAGE_Two = "12.StageScene2";


    public const string SCENE_NAME_STAGE_ONE = "02.PlayStage1";

    public const string SCENE_NAME_STAGE_TWO = "03.PlayStage2";

    public const string OBJ_NAME_CURRENT_LEVEL = "Level_1";

    public static int gameScore = 0;

    public static int life = 3;

    public static int bonusTime = 5000;
}

public enum PuzzleType 
{
    //enum타입으로 지정하면
    //영문타입을 지정하면 숫자랑 1:1 맵핑해줌
    //-1을 해주면 그 타입은 0이 됨.
    //-1로 시작하면 정산적이지 않음
    //위에서 아래로 숫자 타입이 늘어남.
    //none으로 0부터 시작할지 1부터 시작할지 초기화 시작점을 만들수 있다?
    NONE = -1,
    PUZZLE_BIG_TRIANGLE,
    PUZZLE_SQUARE,
    PUZZLE_MIDDLE_TRIANGLE,
    //Puzzle_Parallelogram
    PUZZLE_PARALLELOGRAM,
    PUZZLE_SMALL_TRIANGLE,
}   //PuzzleType()
