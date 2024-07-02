using System.Collections;
using System.Collections.Generic;
<<<<<<< HEAD
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class BabyPlum : MonoBehaviour
{
    private Animator babyPlumAni = default;
    private Rigidbody2D babyPlumRigid = default;

    public Transform target;

    private float babyPlumHp = 35f;
    public static float babyPlumSpeed = 10f;

    private bool isTargetCheck = false;
    Vector2 vec;

    private void Awake()
    {
        babyPlumAni = gameObject.GetComponentMust<Animator>();
        babyPlumRigid = gameObject.GetComponentMust<Rigidbody2D>();
        target = FindObjectOfType<PlayerController>().transform;
    }

    void Start()
    {
        gameObject.SetActive(true);
        
        
        StartCoroutine("mobMove");
    }
=======

using UnityEngine;

public enum Patterns { OnePress, TwoTurn, ThreeShotMove }

public class BabyPlum : MonoBehaviour, IEnemy
{
    public Patterns patterns;
    Room room;
    RoomManager roomManager;
    public BabyPlumHP babyPlumHpUi;

    private int iteration = 0;
    public List<GameObject> bossTearObj = default;

    public GameObject bossTearOne = default;
    public GameObject bossTearTwo = default;

    public Transform bossTearContainer = default;
    public Transform bossTearContainerTwo = default;

    public Pattern bossPattern = default;

    public float turnSpeed = 2f;
    public float circleSacle = 5f;

    public float coolDown = default;

    public int angleInterval = 15;
    public int startAngle = 30;
    public int endAngle = 360;

    private Animator babyPlumAni = default;
    private Rigidbody2D babyPlumRigid = default;
    private SpriteRenderer rend;
    private BoxCollider2D boxCollider2D;

    public Transform target;
    public Transform boss;



    public static float babyPlumHp = 200f;
    public static float babyPlumSpeed = 3f;

    private bool isTargetCheck = false;
    private bool isMoveCheck = false;
    private bool isPatternStart = false;
    private bool isPartternThreeCheck = false;
    private bool coolDownAttack = false;
    private bool isUseCheck = false;

    Vector2 vec;
    Vector3 lastVelocity;
    Vector2 saveVector;

    private void Awake()
    {
        babyPlumAni = gameObject.GetComponent<Animator>();
        babyPlumRigid = gameObject.GetComponent<Rigidbody2D>();
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
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
        room = GetComponentInParent<Room>();
        roomManager = FindObjectOfType<RoomManager>();

        StartCoroutine(StartSpawn());
        babyPlumHpUi = FindObjectOfType<BabyPlumHP>();
        babyPlumHpUi.BossHpUiActiveTrue();
        gameObject.SetActive(true);
        saveVector = new Vector2(1f, 1f);


        StartCoroutine("mobMove");
    }   //Start()
>>>>>>> Develop

    //FixedUpdate()
    void FixedUpdate()
    {
<<<<<<< HEAD
        babyPlumRigid.velocity = new Vector2(babyPlumSpeed, babyPlumSpeed);

        //
        MoveCharacter(vec);
    }

    //
    private void MoveCharacter(Vector2 direction)
    {
        babyPlumRigid.MovePosition((Vector2)transform.position + (direction * babyPlumSpeed * Time.deltaTime));
    }

    void Update()
    {
         // noAtFlyRigid.transform.LookAt(target);
        FollowTarget();

        Vector3 direction = target.position - transform.position;        
        direction.Normalize();
        vec = direction;
    }
        
    IEnumerator mobMove()
    {
       // noAtFlySpeed = Random.Range(-1, 5);
=======
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
            iteration -= 360; //ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ 0~360ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½Ï±ï¿½ ï¿½ï¿½ï¿½ï¿½
    }   //BabyPlumMove()



    private IEnumerator BabyPlumPattern()
    {
        int numberParttern;
        if (0 < babyPlumHp)
        {
            yield return new WaitForSeconds(3f);
            numberParttern = Random.Range(0, 2);
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



    private IEnumerator PatternOne()
    {
        //ï¿½Ê±ï¿½ ï¿½ï¿½ï¿½ï¿½ 0ï¿½ï¿½
        if (!isUseCheck)
        {
            isMoveCheck = true;
            isUseCheck = true;
            babyPlumRigid.velocity = Vector3.zero;

            yield return new WaitForSeconds(0.3f);
            babyPlumAni.SetBool("Press", true);
            yield return new WaitForSeconds(0.53f);
            babyPlumAni.SetBool("Press", false);

            for (int aroundTear = startAngle; aroundTear < endAngle; aroundTear += angleInterval)
            {
                MobTear mobTearObject = MonsterObjectPool.GetObject();
                MobSlowTear mobSlowTearObject = MonsterObjectPoolSlow.GetObject();

                Vector2 direction =
                    new Vector2(Mathf.Cos(aroundTear * Mathf.Deg2Rad), Mathf.Sin(aroundTear * Mathf.Deg2Rad));
                Vector2 directionTwo =
                    new Vector2(Mathf.Cos(aroundTear * Mathf.Deg2Rad), Mathf.Sin(aroundTear * Mathf.Deg2Rad)); // ê°ë„ê°€ ì•„ë‹ˆë¼ aroundTearTwoë¥¼ ì‚¬ìš©í•´ì•¼ í•¨

                mobTearObject.transform.position = transform.position;
                mobTearObject.transform.right = direction;

                mobSlowTearObject.transform.position = transform.position;
                mobSlowTearObject.transform.right = directionTwo;

                mobTearObject.gameObject.SetActive(true);
                mobSlowTearObject.gameObject.SetActive(true);

                yield return null;
            }

        }

        isMoveCheck = false;
        isUseCheck = false;

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
            babyPlumRigid.AddForce(new Vector2(3f, 0f), ForceMode2D.Impulse);
        }
        else
        {
            rend.flipX = true;
            babyPlumAni.SetBool("ShootTurn", true);
            babyPlumRigid.AddForce(new Vector2(-3f, 0f), ForceMode2D.Impulse);


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
    //ï¿½Ô»ï¿½ ï¿½Ý»ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½
    private IEnumerator PatternThree()
    {

        isPartternThreeCheck = false;
        isMoveCheck = true;
        babyPlumAni.SetBool("ShootMove", true);
        yield return new WaitForSeconds(0.2f);
        babyPlumAni.SetBool("Shootmove", false);
        babyPlumAni.SetBool("ShootUp", true);

    }


    IEnumerator mobMove()
    {
>>>>>>> Develop
        yield return new WaitForSeconds(0.3f);
        StartCoroutine("mobMove");
    }

    public void startMove()
    {
        StartCoroutine("mobMove");
    }

<<<<<<< HEAD
    // ´«¹° ¸ÂÀ»¶§
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Tear")
        {
            Hit();
            //Debug.Log($"{noAtFlyHp}");
        }
=======
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

    void Attack()
    {
        if (!coolDownAttack)
        {

            StartCoroutine(CoolDown());
        }
    }

    private IEnumerator CoolDown()
    {
        coolDownAttack = true;
        yield return new WaitForSeconds(coolDown);
        coolDownAttack = false;
    }

    public void HitExp(int a)
    {
        babyPlumHp -= a;
        if (babyPlumHp <= 0)
        {
            Die();
        }
    }

    //!{Hit
    public void Hit(float _float)
    {
        babyPlumHp -= _float;

        if (0 < babyPlumHp)
        {

        }
        if (babyPlumHp <= 0)
        {
            babyPlumHp = 0;
            Die();
        }
    }   //Hit()

    public void BombHit()
    {
        babyPlumHp -= GameManager.bombDamage;
        if (0 < babyPlumHp)
        {

        }
        if (babyPlumHp <= 0)
        {
            babyPlumHp = 0;
            Die();
        }
    }


    //!{Die()
    public void Die()
    {
        babyPlumAni.SetBool("Die", true);
        Invoke("DestroyMob", 0.5f);

        if (room != null)
        {
            room.MobDie();
            
            room.ClearBossRoom(!roomManager.isBossRoomClearCheck);

            babyPlumHpUi.BossHpUiAcitveFalse();


        }
        else { Debug.Log(room); }

    }   //Did()

    public void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Tear")
        {
            Hit(GameManager.Instance.IsaacDamage);

        }
        if (other.tag == "Player")
        {
            Attack();

        }
        if (other.tag == "UseBomb")
        {
            BombHit();
        }

>>>>>>> Develop
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isTargetCheck = false;
        }
    }

<<<<<<< HEAD
    //!{Hit()
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

    //private void Distroy()
    //{
    //    Object.Destroy(this);

    //}   //Distroy()

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

    



=======
    private void OnCollisionEnter2D(Collision2D other_)
    {

        if (!isPartternThreeCheck)
        {
            var speed = lastVelocity.magnitude;

            Vector2 direc = Vector2.Reflect(lastVelocity.normalized, other_.contacts[0].normal);

            babyPlumRigid.AddForce(direc * 40f, ForceMode2D.Impulse);

        }
        else {/*Do nothing*/ }
    }
>>>>>>> Develop
}
