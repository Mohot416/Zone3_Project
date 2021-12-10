using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IE.RSB;

//玩家按滑鼠左艦發射子彈
public class Tutorial : MonoBehaviour
{
    public BulletProperties myBulletProperties;

    // Start is called before the first frame update
    private void Start()
    {
        SniperAndBallisticsSystem.instance.ActivateBullet(myBulletProperties);
    }
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SniperAndBallisticsSystem.instance.FireBallisticsBullet(myBulletProperties);
        }
    }
}
