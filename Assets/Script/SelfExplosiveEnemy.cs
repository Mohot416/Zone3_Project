using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IE.RSB;
//怪物狀態、碰撞、動畫
//自爆敵人

[System.Serializable]
public class Dmg_sector
{
    public float multiplier;       //區間傷害倍率
    private bool flag;      //判斷是否進入傷害區間
    private bool hurt;      //判斷是否正在受到傷害
    public Dmg_sector(float a, bool b, bool c)
    {
        this.multiplier = a;
        this.flag = b;
        this.hurt = c;
    }
    public bool Flag
    {
        get { return flag; }
        set { flag = value; }
    }
    public bool Hurt
    {
        get { return hurt; }
        set { hurt = value; }
    }
}

public class SelfExplosiveEnemy : MonoBehaviour
{
    //Animation members
    public const string IDLE	= "Anim_Idle";
	public const string RUN		= "Anim_Run";
	public const string ATTACK	= "Anim_Attack";
	public const string DAMAGE	= "Anim_Damage";
	public const string DEATH	= "Anim_Death";
    Animation anim;

    //public class members
    public float starDamage = 150;
    public float teamDps = 20;
    public float bulletSniperDmg = 60;
    public float mineDamage = 120;
    public float dmgDelay = 3.0f;  //傷害函式延遲時間
    public float damage = 10;


    //private class members
    private const float maxHealth = 100;
    private const float initialSpeed = 4;
    private Rigidbody enemyRigBody;
    private BulletTimeTarget enemyBTT;
    private bool enemyDead;
    private float currentHealth = maxHealth;
    private float enemySpeed = initialSpeed;

    [Header("Particle")]
    [SerializeField] private GameObject explosiveParticle = null;

    [Header("Audio Effect")]
    [SerializeField] private AudioSource bombAudioSource = null;

    /*傷害倍率
    Haruel:90%/110%/140% 5.56
    Huster:100%/75%/50%   9mm
    Lity:150%/100%/60%    .45ACP
    */
    //創建並設定參數
    public Dmg_sector close_556 = new Dmg_sector(0.9f, false, false);
    public Dmg_sector mid_556 = new Dmg_sector(1.1f, false, false);
    public Dmg_sector far_556 = new Dmg_sector(1.4f, false, false);
    public Dmg_sector close_9mm = new Dmg_sector(1.0f, false, false);
    public Dmg_sector mid_9mm = new Dmg_sector(0.75f, false, false);
    public Dmg_sector far_9mm = new Dmg_sector(0.5f, false, false);
    public Dmg_sector close_45acp = new Dmg_sector(1.5f, false, false);
    public Dmg_sector mid_45acp = new Dmg_sector(1.0f, false, false);
    public Dmg_sector far_45acp = new Dmg_sector(0.6f, false, false);

    private void Awake()
    {
        enemyRigBody = GetComponent<Rigidbody>();
        enemyBTT = GetComponent<BulletTimeTarget>();
        bombAudioSource = GameObject.Find("Enemy Explosion Sound").GetComponent<AudioSource>();
    }


    private void OnEnable()
    {
        SniperAndBallisticsSystem.EAnyHit += OnAnyHit;  //detect hit from RSB
    }

    private void OnDisable()
    {
        SniperAndBallisticsSystem.EAnyHit -= OnAnyHit;  
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
    }

    private void OnAnyHit(BulletPoint point)
    {
        if (enemyDead)
        {
            Dead();
        }
        else
        {
            if (point.m_hitTransform == enemyRigBody.transform)
            {
                currentHealth -= bulletSniperDmg;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Defense")
            Dead(); //self-explosive

        if (other.gameObject.tag == "Star")
        {
            currentHealth = currentHealth - starDamage;
        }

        if (other.gameObject.tag == "Mine")
        {
            currentHealth = currentHealth - mineDamage;
        }

        /*判斷進入哪個傷害區間*/
        //Haruel
        if (other.gameObject.CompareTag("Close_556"))
            close_556.Flag = true;
        if (other.gameObject.CompareTag("Middle_556"))
            mid_556.Flag = true;
        if (other.gameObject.CompareTag("Far_556"))
            far_556.Flag = true;

        //Lity
        if (other.gameObject.CompareTag("Close_45ACP"))
            close_45acp.Flag = true;
        if (other.gameObject.CompareTag("Middle_45ACP"))
            mid_45acp.Flag = true;
        if (other.gameObject.CompareTag("Far_45ACP"))
            far_45acp.Flag = true;

        //Huster
        if (other.gameObject.CompareTag("Close_9mm"))
            close_9mm.Flag = true;
        if (other.gameObject.CompareTag("Middle_9mm"))
            mid_9mm.Flag = true;
        if (other.gameObject.CompareTag("Far_9mm"))
            far_9mm.Flag = true;

        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Defense")  //避免怪物跟怪物、防線重合.
            enemySpeed = 0;
        
    }

    void OnTriggerExit(Collider other)
    {
        /*離開傷害區間就取消傷害*/
        //Haruel
        if (other.gameObject.CompareTag("Close_556"))
            close_556.Flag = false;
        if (other.gameObject.CompareTag("Middle_556"))
            mid_556.Flag = false;
        if (other.gameObject.CompareTag("Far_556"))
            far_556.Flag = false;

        //Lity
        if (other.gameObject.CompareTag("Close_45ACP"))
            close_45acp.Flag = false;
        if (other.gameObject.CompareTag("Middle_45ACP"))
            mid_45acp.Flag = false;
        if (other.gameObject.CompareTag("Far_45ACP"))
            far_45acp.Flag = false;

        //Huster
        if (other.gameObject.CompareTag("Close_9mm"))
            close_9mm.Flag = false;
        if (other.gameObject.CompareTag("Middle_9mm"))
            mid_9mm.Flag = false;
        if (other.gameObject.CompareTag("Far_9mm"))
            far_9mm.Flag = false;
        //回復到無阻礙的狀況就繼續前進
        if (enemySpeed != initialSpeed)
        {
            enemySpeed = initialSpeed;
            anim.CrossFade (RUN);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if(!GameControl.isPaused)
        {
            DeathCheck();
            //dead
            if (enemyDead)
            {
                enemySpeed = 0;
                Dead();
            }
            else
            {

                //Haruel
                if (close_556.Flag == true && close_556.Hurt == false)  //進入該傷害區間且未受傷害
                {
                    StartCoroutine(enemy_hurt(close_556));
                }
                if (mid_556.Flag == true && mid_556.Hurt == false)
                {
                    StartCoroutine(enemy_hurt(mid_556));
                }
                if (far_556.Flag == true && far_556.Hurt == false)
                {
                    StartCoroutine(enemy_hurt(far_556));
                }

                //Lity
                if (close_45acp.Flag == true && close_45acp.Hurt == false)
                {
                    StartCoroutine(enemy_hurt(close_45acp));
                }
                if (mid_45acp.Flag == true && mid_45acp.Hurt == false)
                {
                    StartCoroutine(enemy_hurt(mid_45acp));
                }
                if (far_45acp.Flag == true && far_45acp.Hurt == false)
                {
                    StartCoroutine(enemy_hurt(far_45acp));
                }

                //Huster
                if (close_9mm.Flag == true && close_9mm.Hurt == false)
                {
                    StartCoroutine(enemy_hurt(close_9mm));
                }
                if (mid_9mm.Flag == true && mid_9mm.Hurt == false)
                {
                    StartCoroutine(enemy_hurt(mid_9mm));
                }
                if (far_9mm.Flag == true && far_9mm.Hurt == false)
                {
                    StartCoroutine(enemy_hurt(far_9mm));
                }
            }

            //movement
            transform.position += transform.forward * enemySpeed * Time.deltaTime;
            if (enemySpeed > 0)
                anim.CrossFade(RUN);  //切換成跑步動畫
        }
    }

    private bool DeathCheck()
    {
        if (currentHealth <= 0)
        {
            enemyDead = true;
        }
        return enemyDead;
        
    }

    private void Dead()
    {
        GameObject particle = Instantiate(explosiveParticle, transform.position, Quaternion.identity);    //生成粒子特效
        bombAudioSource.Play();
        Destroy(particle, 2);       //消除粒子特效
        gameObject.SetActive(false);    //清除物件
        Destroy(gameObject, 3);
    }


    IEnumerator enemy_hurt(Dmg_sector _Sector)
    {
        _Sector.Hurt = true;
        currentHealth = currentHealth - (teamDps * _Sector.multiplier);
        yield return new WaitForSeconds(dmgDelay);  //防止敵人快速失血死亡
        _Sector.Hurt = false;
    }
}
