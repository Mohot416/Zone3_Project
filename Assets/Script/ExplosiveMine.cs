using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveMine : MonoBehaviour
{
    [Header("Particle")]
    [SerializeField] private GameObject explosiveParticle = null;
    [SerializeField] private Vector3 explosionOffset = new Vector3(0, 1, 0);

    [Header("Audio Effect")]
    [SerializeField] private AudioSource bombAudioSource = null;

    //execute when the object is enabled
    private void Start()
    {
        bombAudioSource = GameObject.Find("Mine Explosion Sound").GetComponent<AudioSource>();
    }


    private void OnTriggerEnter(Collider other)
    {
        //Update Enemy Health
        GameObject particle = Instantiate(explosiveParticle, transform.position + explosionOffset, Quaternion.identity);    //生成粒子特效
        bombAudioSource.Play();
        Destroy(particle, 2);       //消除粒子特效
        gameObject.SetActive(false);    //清除物件
        Destroy(gameObject, 3);
    }
}
