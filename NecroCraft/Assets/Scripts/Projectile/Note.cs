using UnityEngine;
using PlayerScripts;


namespace Projectile
{
    public class Note : MonoBehaviour
    {
        [SerializeField] public float speed = 2f;
        [SerializeField] public float lifetime = 5f;
        [SerializeField] public float cooldown = 10f;
        [SerializeField] public short amount = 10;
        
        private ParticleSystem _particleSystem;
        
        void Start()
        {
            _particleSystem = GetComponent<ParticleSystem>();
            
            var particleSystemMain = _particleSystem.main;
            particleSystemMain.startLifetime = lifetime;
            particleSystemMain.startSpeed = speed;
            particleSystemMain.duration = cooldown;
            
            var particleSystemEmission = _particleSystem.emission;
            particleSystemEmission.SetBurst(0, new ParticleSystem.Burst(0f, amount));
            
            _particleSystem.Play();
        }

        void Update()
        {
            transform.position = PlayerController.Instance.Position;
        }
    }
}
