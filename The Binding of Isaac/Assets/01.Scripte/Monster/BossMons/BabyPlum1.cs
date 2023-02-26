using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Patterns { OnePress, TwoTurn, ThreeShotMove }

public class BabyPlum1 : MonoBehaviour
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


    // ↑  boss test

    private Animator babyPlumAni = default;
    private Rigidbody2D babyPlumRigid = default;
    private SpriteRenderer rend;

    public Transform target;
    public Transform boss;



    public static float babyPlumHp = 35f;
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



        babyPlumAni = gameObject.GetComponentMust<Animator>();
        babyPlumRigid = gameObject.GetComponentMust<Rigidbody2D>();
        rend = gameObject.GetComponent<SpriteRenderer>();
        target = FindObjectOfType<PlayerController>().transform;
    }
    private IEnumerator StartSpawn()
    {
        isMoveCheck = true;
        babyPlumRigid.velocity = Vector2.zero;
        babyPlumAni.SetBool("Start", true);
        yield return new WaitForSeconds(0.8f);
        babyPlumAni.SetBool("Start", false);
        isMoveCheck = false;
        yield return new WaitForSeconds(2f);

    }

    //Start()
    void Start()
    {
        StartCoroutine(StartSpawn());


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

        if (!isMoveCheck)
        {
            Vector3 direction = target.position - transform.position;
            direction.Normalize();
            vec = direction;
        }

    }

    void BabyPlumMove(float angle)
    {
        Vector2 direction = new Vector2(
            Mathf.Cos(iteration * Mathf.Deg2Rad), Mathf.Sin(iteration * Mathf.Deg2Rad));

        transform.Translate(direction * (circleSacle * Time.deltaTime));
        iteration++;
        if (iteration > 360)
            iteration -= 360; //각도를 0~360로 설정하기 위해
    }   //BabyPlumMove()



    private IEnumerator BabyPlumPattern()
    {
        int numberParttern;
        if (0 < babyPlumHp)
        {
            yield return new WaitForSeconds(3f);


            //numberParttern = Random.Range(0, 2 + 1);
            numberParttern = 2;
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
        isPatternStart = false;
    }

    //!{PatternOne 
    // 빠른 눈물 360/8 방향
    //보통 눈물 360/14 방향
    //찍기
    private IEnumerator PatternOne()
    {
        //초기 각도 0도
        if (!isUseCheck)
        {
            isMoveCheck = true;
            isUseCheck = true;
            babyPlumRigid.velocity = Vector3.zero;

            yield return new WaitForSeconds(0.3f);
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
        isMoveCheck = true;
        int shootAngle = 0;
        bool isSpinShoot = false;
        babyPlumRigid.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.2f);
        if (0 < vec.x)
        {
            babyPlumAni.SetBool("ShootTurn", true);
            babyPlumRigid.AddForce(new Vector2(20f, 0f), ForceMode2D.Impulse);
        }
        else
        {
            rend.flipX = true;
            babyPlumAni.SetBool("ShootTurn", true);
            babyPlumRigid.AddForce(new Vector2(-20f, 0f), ForceMode2D.Impulse);


        }

        while (!isSpinShoot)
        {

            GameObject bossTearObject = Instantiate(bossTearOne, bossTearContainer, true);
            Vector2 direction = new Vector2(Mathf.Cos(shootAngle * Mathf.Deg2Rad), Mathf.Sin(shootAngle * Mathf.Deg2Rad));
            bossTearObject.transform.right = direction;
            bossTearObject.transform.position = transform.position;
            yield return new WaitForSeconds(0.05f);
            shootAngle += angleInterval;

            if (shootAngle > 360)
            {
                isSpinShoot = true;
            }
        }
        babyPlumAni.SetBool("ShootTurn", false);
        isMoveCheck = false;
    }

    //!{PatternThree()
    //입사 반사 어케해
    private IEnumerator PatternThree()
    {

        isPartternThreeCheck = false;
        isMoveCheck = true;
        babyPlumAni.SetBool("ShootMove", true);
        yield return new WaitForSeconds(0.2f);
        babyPlumAni.SetBool("Shootmove", false);
        babyPlumAni.SetBool("ShootUp", true);
        

        //Vector2 babyPlumVector = new Vector2(100f, 100f);   
        //babyPlumRigid.AddForce(babyPlumVector, ForceMode2D.Impulse);
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
            babyPlumRigid.velocity = Vector2.MoveTowards(transform.position, target.position, babyPlumSpeed);
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


    // 눈물 맞을때
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

            Vector2 direc = Vector2.Reflect(lastVelocity.normalized, other_.contacts[0].normal);
            //abyPlumRigid.velocity = direc * Mathf.Max(speed, 30f);
            babyPlumRigid.AddForce(direc * 40f, ForceMode2D.Impulse);

            // babyPlumRigid.velocity = saveVector;
        }
        else {/*Do nothing*/ }
    }
}
