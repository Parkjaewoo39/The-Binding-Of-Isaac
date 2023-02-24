using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//enum 패턴변수에 원하는 이름타입으로 저장
public enum Pattern { OnePress, TwoTurn, ThreeShotMove }

public class BabyPlumPattern : MonoBehaviour
{
    private int iteration = 0;  //각도의 Default
    private bool isUseCheck = false;
    //눈물을 오브젝트나 프리팹 형
    //public PrefabType bossTearPrefab = default;
    public List<GameObject> bossTearObj = default;

    public GameObject bossTearOne = default;
    public GameObject bossTearTwo = default;

    public Transform bossTearContainer = default;
    public Transform bossTearContainerTwo = default;

    public Pattern bossPattern = default;

    public float turnSpeed = 2f;    //돌아가는 속도
    public float circleSacle = 5f;  //360도의 기준이 되는 원크기

    public int angleInterval = 15;  //눈물 간격
    public int startAngle = 30;   //발사하는 첫 각
    public int endAngle = 360;    //마지막 각도

    // Start is called before the first frame update
    void Start()
    {
        isUseCheck = false;

        if (bossPattern == Pattern.OnePress)
        {
            //
            StartCoroutine(PatternOne());
        }
        else if (bossPattern == Pattern.TwoTurn)
        {
            //StartCoroutine(PatternTwo());
        }
        else if (bossPattern == Pattern.ThreeShotMove)
        {
            //StartCoroutine(PatternThree());
        }
        else { /*Do nothing*/ }

    }

    // Update is called once per frame
    void Update()
    {

    }

    //!{BabyPlumMove
    void BabyPlumMove(float angle)
    {
        Vector2 direction = new Vector2(
            Mathf.Cos(iteration * Mathf.Deg2Rad), Mathf.Sin(iteration * Mathf.Deg2Rad));

        transform.Translate(direction * (circleSacle * Time.deltaTime));
        iteration++;
        if (iteration > 360)
            iteration -= 360; //각도를 0~360로 설정하기 위해
    }   //BabyPlumMove()

    //!{PatternOne 
    // 빠른 눈물 360/8 방향
    //보통 눈물 360/14 방향
    //찍기
    private IEnumerator PatternOne()
    {
        int babyPlumAngle = 0;  //초기 각도 0도
        if (!isUseCheck)
        {
            isUseCheck = true;
            for (int aroundTear = startAngle; aroundTear < endAngle; aroundTear += angleInterval)
            {
                GameObject bossTearObject = Instantiate(bossTearOne, bossTearContainer, true);
                GameObject bossTearObjectTwo = Instantiate(bossTearTwo, bossTearContainerTwo, true);

                Vector2 direction = 
                    new Vector2(Mathf.Cos(aroundTear * Mathf.Deg2Rad), Mathf.Sin(aroundTear * Mathf.Deg2Rad));
                Vector2 directionTwo =
                    new Vector2(Mathf.Cos(aroundTear * 15),Mathf.Sin(aroundTear * 15));

                bossTearObject.transform.right = directionTwo;
                bossTearObjectTwo.transform.right = direction;

                bossTearObject.transform.position = transform.position;
                bossTearObjectTwo.transform.position = transform.position;
            }

            yield return new WaitForSeconds(4f);

        }

    }

}
