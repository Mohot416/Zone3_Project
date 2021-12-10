using UnityEngine;
using IE.RSB;

//玩家按滑鼠左鍵發射子彈
public class SpawningHitEffects : MonoBehaviour
{
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
        
    }
    // Update is called once per frame
    private void Update()
    {
        
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
