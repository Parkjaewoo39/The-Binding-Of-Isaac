using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//enum ���Ϻ����� ���ϴ� �̸�Ÿ������ ����
public enum Pattern { OnePress, TwoTurn, ThreeShotMove }

public class BabyPlumPattern : MonoBehaviour
{
    private int iteration = 0;  //������ Default
    private bool isUseCheck = false;
    //������ ������Ʈ�� ������ ��
    //public PrefabType bossTearPrefab = default;
    public List<GameObject> bossTearObj = default;

    public GameObject bossTearOne = default;
    public GameObject bossTearTwo = default;

    public Transform bossTearContainer = default;
    public Transform bossTearContainerTwo = default;

    public Pattern bossPattern = default;

    public float turnSpeed = 2f;    //���ư��� �ӵ�
    public float circleSacle = 5f;  //360���� ������ �Ǵ� ��ũ��

    public int angleInterval = 15;  //���� ����
    public int startAngle = 30;   //�߻��ϴ� ù ��
    public int endAngle = 360;    //������ ����

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
            iteration -= 360; //������ 0~360�� �����ϱ� ����
    }   //BabyPlumMove()

    //!{PatternOne 
    // ���� ���� 360/8 ����
    //���� ���� 360/14 ����
    //���
    private IEnumerator PatternOne()
    {
        //int babyPlumAngle = 0;  //�ʱ� ���� 0��
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
