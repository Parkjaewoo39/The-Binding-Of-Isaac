using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Patterns { OnePress, TwoTurn, ThreeShotMove }

public class BabyPlum1 : MonoBehaviour
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
    

    // ��  boss test

    private Animator babyPlumAni = default;
    private Rigidbody2D babyPlumRigid = default;

    public Transform target;

    private float babyPlumHp = 35f;
    public static float babyPlumSpeed = 10f;

    private bool isTargetCheck = false;
    private bool isMoveCheck = false;
    private bool isPatternStart = false;
    private bool isPartternThreeCheck = false;

    Vector2 vec;
    Vector3 lastVelocity;
    Vector2 saveVector;

    private void Awake()
    {
        isMoveCheck = false;
        isUseCheck = false;
        isPartternThreeCheck = false;

        if (bossPattern == Pattern.OnePress)
        {
            //
            //StartCoroutine(PatternOne());
        }
        else if (bossPattern == Pattern.TwoTurn)
        {
            StartCoroutine(PatternTwo());
        }
        else if (bossPattern == Pattern.ThreeShotMove)
        {
            StartCoroutine(PatternThree());
        }
        else { /*Do nothing*/ }


        babyPlumAni = gameObject.GetComponentMust<Animator>();
        babyPlumRigid = gameObject.GetComponentMust<Rigidbody2D>();
        target = FindObjectOfType<PlayerController>().transform;
    }


    //Start()
    void Start()
    {
        gameObject.SetActive(true);
        saveVector = new Vector2(1f, 1f);

        StartCoroutine("mobMove");
    }   //Start()

    //FixedUpdate()
    void FixedUpdate()
    {
        if (!isMoveCheck)
        {
            babyPlumRigid.velocity = new Vector2(babyPlumSpeed, babyPlumSpeed);

            MoveCharacter(vec);
        }
        else { }
    }   //FixedUpdate()

    //
    

    void Update()
    {
        if (!isPatternStart)
        {
            isPatternStart = true;
            StartCoroutine(BabyPlumPattern());
        }
        saveVector = babyPlumRigid.velocity;

        lastVelocity = babyPlumRigid.velocity;
        // noAtFlyRigid.transform.LookAt(target);


        FollowTarget();


        Vector3 direction = target.position - transform.position;
        direction.Normalize();
        vec = direction;
    }

    void BabyPlumMove(float angle)
    {
        Vector2 direction = new Vector2(
            Mathf.Cos(iteration * Mathf.Deg2Rad), Mathf.Sin(iteration * Mathf.Deg2Rad));

        transform.Translate(direction * (circleSacle * Time.deltaTime));
        iteration++;
        if (iteration > 360)
            iteration -= 360; //������ 0~360�� �����ϱ� ����
    }   //BabyPlumMove()



    private IEnumerator BabyPlumPattern() 
    {
        int numberParttern;
        if (0 < babyPlumHp)
        {
            
            numberParttern = Random.Range(0, 2 + 1);
            Debug.Log(numberParttern);
            switch (numberParttern)
            {
                case 0:
                    StartCoroutine(PatternOne());
                    break;
                case 1:
                    StartCoroutine(PatternTwo());
                    break;
                case 2:
                    StartCoroutine(PatternThree());
                    break;
                default:
                    break;
            }
        }
        yield return new WaitForSeconds(5f);
        isPatternStart= false;
    }

    //!{PatternOne 
    // ���� ���� 360/8 ����
    //���� ���� 360/14 ����
    //���
    private IEnumerator PatternOne()
    {
        int babyPlumAngle = 0;
        //�ʱ� ���� 0��
        if (isUseCheck == false)
        {
            isMoveCheck = true;
            isUseCheck = true;

            yield return new WaitForSeconds(1f);
            babyPlumAni.SetBool("Press", true);
            yield return new WaitForSeconds(0.53f);
            babyPlumAni.SetBool("Press", false);

            // babyPlumRigid.velocity = Vector2.zero;

            //this.transform.position = transform.position;
            for (int aroundTear = startAngle; aroundTear < endAngle; aroundTear += angleInterval)
            {
                for (int aroundTearTwo = 0; aroundTearTwo < 360; aroundTearTwo += 30)
                {
                    GameObject bossTearObject = Instantiate(bossTearOne, bossTearContainer, true);
                    GameObject bossTearObjectTwo = Instantiate(bossTearTwo, bossTearContainerTwo, true);

                    Vector2 direction =
                        new Vector2(Mathf.Cos(aroundTear * Mathf.Deg2Rad), Mathf.Sin(aroundTear * Mathf.Deg2Rad));
                    Vector2 directionTwo =
                        new Vector2(Mathf.Cos(aroundTearTwo * Mathf.Deg2Rad), Mathf.Sin(aroundTearTwo * Mathf.Deg2Rad));

                    bossTearObject.transform.right = directionTwo;
                    bossTearObjectTwo.transform.right = direction;

                    bossTearObject.transform.position = transform.position;
                    bossTearObjectTwo.transform.position = transform.position;
                }

            }

        }
        yield return new WaitForSeconds(0.5f);
        isMoveCheck = false;
        isUseCheck = false;
        //isUseCheck = false;
    }       //IEnumerator PatternOne()


    private IEnumerator PatternTwo() 
    {
        yield return new WaitForSeconds(4f);
    }

    //!{PatternThree()
    //�Ի� �ݻ� ������
    private IEnumerator PatternThree()
    {

        isPartternThreeCheck = true;
        isMoveCheck = true;
        babyPlumAni.SetBool("ShootStart", true);
        yield return new WaitForSeconds(0.5f);
        babyPlumAni.SetBool("ShootUp", true);

        Vector2 babyPlumVector = new Vector2(10f, 10f);
        

        babyPlumRigid.AddForce(babyPlumVector, ForceMode2D.Impulse);
        yield return null;
    }
       

    IEnumerator mobMove()
    {
        // noAtFlySpeed = Random.Range(-1, 5);
        yield return new WaitForSeconds(0.3f);
        StartCoroutine("mobMove");
    }

    public void startMove()
    {
        StartCoroutine("mobMove");
    }

    private void MoveCharacter(Vector2 direction)
    {
        babyPlumRigid.MovePosition((Vector2)transform.position + (direction * babyPlumSpeed * Time.deltaTime));
    }

    public void DestroyMob()
    {
        gameObject.SetActive(false);
    }

    public void FollowTarget()
    {
        if (Vector2.Distance(transform.position, target.position) > 1 && isTargetCheck)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, babyPlumSpeed);
        }
    }


    //!{Hit
    public void Hit()
    {
        //Debug.Log(PlayerController.isaacDamage);
        babyPlumHp -= PlayerController.isaacDamage;
        // Debug.Log(babyPlumHp);

        if (0 < babyPlumHp)
        {

        }
        if (babyPlumHp <= 0)
        {
            Die();
        }
    }   //Hit()


    //!{Die()
    public void Die()
    {
        babyPlumAni.SetBool("Die", true);
        Invoke("DestroyMob", 0.3f);
    }   //Did()


    // ���� ������
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Tear")
        {
            Hit();
            //Debug.Log($"{noAtFlyHp}");
        }

    }
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isTargetCheck = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other_)
    {
        if (!isPartternThreeCheck)
        {
            var speed = lastVelocity.magnitude;
            var direc = Vector2.Reflect(lastVelocity.normalized, other_.contacts[0].normal);
            saveVector = direc * Mathf.Max(speed, 0f);
            
            babyPlumRigid.velocity = saveVector;
        }
        else {/*Do nothing*/ }
    }

    

    





}
