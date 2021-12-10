using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IE.RSB;

//玩家按滑鼠左鍵發射子彈
public class Fire : MonoBehaviour
{
    public BulletProperties myBulletProperties;
    //導入彈道參數、彈道模擬、子彈
    public Transform myBulletTimeObject;    //子彈時間
    public Transform mainCamera;    //Frosy
    public ObjectPooler defaultHitPooler;
    //導入彈著顯示

    private void OnEnable()
    {
        SniperAndBallisticsSystem.EAnyHit += OnAnyHit;
    }
    private void OnDisable()
    {
        SniperAndBallisticsSystem.EAnyHit -= OnAnyHit;
    }

    // Start is called before the first frame update
    private void Start()
    {
        SniperAndBallisticsSystem.instance.ActivateBullet(myBulletProperties);
        //藉由參數預先實作出子彈
    }
    // Update is called once per frame
    private void Update()
    {
        
        //按下右鍵射出實作之子彈
        if(Input.GetMouseButtonDown(0))
        {
            SniperAndBallisticsSystem.instance.FireBallisticsBullet(myBulletProperties, mainCamera, myBulletTimeObject);
        }
        // Enable scope
        if (Input.GetMouseButtonDown(1))
        {
            DynamicScopeSystem.instance.ScopeActivation(true, myBulletProperties);
            //有了BulletProperties reference狙擊鏡就可以獲取子彈的下墜距離等各項參數
        }

        // Disable scope
        if (Input.GetMouseButtonUp(1))
        {
            DynamicScopeSystem.instance.ScopeActivation(false, myBulletProperties);
        }

        // Cycle zero up
        if (Input.GetKeyDown(KeyCode.C))
        {
            SniperAndBallisticsSystem.instance.CycleZeroDistanceUp();
        }

        // Cycle zero down
        if (Input.GetKeyDown(KeyCode.V))
        {
            SniperAndBallisticsSystem.instance.CycleZeroDistanceDown();
        }
        
    }
    
    private void OnAnyHit(BulletPoint point)
    {
        //當射中物體即印出偵錯資訊
        Debug.Log("Hit Position: " + point.m_endPoint + " Hit Type: " + point.m_hitType);

        GameObject hitParticle = defaultHitPooler.GetPooledObject();    //獲取特效
        hitParticle.transform.position = point.m_endPoint;  //獲取彈著位置
        hitParticle.transform.rotation = Quaternion.LookRotation(point.m_hitNormal);    //調整轉動值
        hitParticle.SetActive(true);        //生成粒子特效
    }
    
}
