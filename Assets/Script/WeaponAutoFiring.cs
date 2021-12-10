using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IE.RSB
{
    public class WeaponAutoFiring : MonoBehaviour
    {
        public Transform muzzlePosition = null;
        public float fireRate = 1;

        [SerializeField, Tooltip("Audio source that'll be used to play weapon sfx.")]
        private AudioSource m_weaponSource = null;

        [SerializeField, Tooltip("Pooler reference that will be used to request a new muzzle flash particle.")]
        private ObjectPooler m_muzzleFlashPooler = null;
        private float m_lastFired = 0;

        [SerializeField, Tooltip("Fire clip.")]
        private AudioClip m_fireSFX = null;


        // Update is called once per frame
        void Update()
        {
            if (Time.time > fireRate + m_lastFired)
            {
                m_lastFired = Time.time;
                Fire();
            }
        }

        private void Fire()
        {
            // Cast audio
            m_weaponSource.PlayOneShot(m_fireSFX);

            GameObject flash = m_muzzleFlashPooler.GetPooledObject();
            flash.transform.position = muzzlePosition.position;
            flash.transform.rotation = muzzlePosition.rotation;
            flash.SetActive(true);
        }
    }
}
