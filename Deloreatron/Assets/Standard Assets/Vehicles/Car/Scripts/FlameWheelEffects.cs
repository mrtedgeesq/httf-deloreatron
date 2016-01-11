using System.Collections;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (AudioSource))]
    public class FlameWheelEffects : MonoBehaviour
    {
        public Transform SkidTrailPrefab;
        public static Transform flameTrailsDetachedParent;
        public ParticleSystem skidParticles;
        public bool flaming{ get; private set; }
        public bool PlayingAudio { get; private set; }

        private AudioSource m_AudioSource;
        private Transform m_SkidTrail;
        private WheelCollider m_WheelCollider;


        private void Start()
        {

            skidParticles = transform.root.GetComponentInChildren<ParticleSystem>();

            if (skidParticles == null)
            {
                Debug.LogWarning(" no particle system found on car to generate smoke particles");
            }
            else
            {
                skidParticles.Stop();
            }

            m_WheelCollider = GetComponent<WheelCollider>();
            m_AudioSource = GetComponent<AudioSource>();
            PlayingAudio = false;

            if (flameTrailsDetachedParent == null)
            {
                flameTrailsDetachedParent = new GameObject("Flame Trails - Detached").transform;
            }
        }


        public void EmitFlames()
        {
            skidParticles.transform.position = transform.position - transform.up*m_WheelCollider.radius;
            skidParticles.Emit(1);
            if (!flaming)
            {
                StartCoroutine(StartFlameTrail());
            }
        }

        public void PlayAudio()
        {
            m_AudioSource.Play();
            PlayingAudio = true;
        }


        public void StopAudio()
        {
            m_AudioSource.Stop();
            PlayingAudio = false;
        }

        Vector3 lastDotPosition;
        private bool lastPointExists;

        public IEnumerator StartFlameTrail()
        {
            flaming = true;
            m_SkidTrail = Instantiate(SkidTrailPrefab);
            while (m_SkidTrail == null)
            {
                yield return null;
            }
            
            m_SkidTrail.parent = transform;
            m_SkidTrail.localPosition = -Vector3.up * m_WheelCollider.radius;
        }

        public void EndFlameTrail()
        {
            if (!flaming)
            {
                return;
            }
            flaming = false;
            m_SkidTrail.parent = flameTrailsDetachedParent;
            //Destroy(m_SkidTrail.gameObject, 10);
        }
    }
}
