using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseLife : MonoBehaviour
{
    public const float maxHealth = 100;
    private float currentHealth = maxHealth;
    
    public float CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }

    public GameObject explosiveEnemy;      //導入普通敵人物件
    public GameObject lity;
    public RectTransform HealthBar,Hurt;
    public GameObject obj;

    private float selfExplosiveEnemyDmg;     //存取普通敵人傷害變數
    private float lityHealing;
    private float litySkillCD;
    private float litySkillCDTimer;

    // Start is called before the first frame update
    void Start()
    {
        selfExplosiveEnemyDmg = explosiveEnemy.GetComponent<SelfExplosiveEnemy>().damage;
        lityHealing = lity.GetComponent<Lity>().healing;
        litySkillCD = lity.GetComponent<Lity>().skillCD;
        litySkillCDTimer = -litySkillCD;
    }
    void OnTriggerEnter( Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            healthDecrease(selfExplosiveEnemyDmg);
        }
    }


    private void healthDecrease(float damage)
    {
        currentHealth = currentHealth - damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            obj.SetActive(false);
        }
    }

    private void healthIncrease()
    {
        if(Time.time >= litySkillCD + litySkillCDTimer)
        {
            Debug.Log(lityHealing);
            currentHealth = currentHealth + lityHealing;
            if (currentHealth > 100)
                currentHealth = 100;
            litySkillCDTimer = Time.time;
            Debug.Log("Repair successfully.");
        }
        else
        {
            Debug.Log("Repairing Skill is cooling down.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameControl.isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (currentHealth < 100)
                    healthIncrease();
            }
            if (Input.GetKeyDown(KeyCode.Backspace ))
            {
                healthDecrease(20.0f);
            }

            HealthBar.sizeDelta = new Vector2(currentHealth, HealthBar.sizeDelta.y);
            //呈現傷害量
            if (Hurt.sizeDelta.x > HealthBar.sizeDelta.x)
            {
                //讓傷害量(紅色血條)逐漸追上當前血量
                Hurt.sizeDelta += new Vector2(-1, 0) * Time.deltaTime * 10;
            }
            else if (Hurt.sizeDelta.x < HealthBar.sizeDelta.x)
            {
                //讓傷害量(紅色血條)回復
                Hurt.sizeDelta += new Vector2(1, 0) * Time.deltaTime * 10;
            }
        }
        
    }
}
