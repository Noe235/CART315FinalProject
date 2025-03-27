using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Rendering.UI;

public class Flamethrower : MonoBehaviour {
  [SerializeField] private ParticleSystem ShootingSystem;
  [SerializeField] private ParticleSystem OnFireSystemPrefab;
  [SerializeField] private FlamethrowerAttackRadius AttackRadius;

  [Space] [SerializeField] private int BurningDPS = 5;
  [SerializeField] private float BurnDuration = 3f;

  private ObjectPool<ParticleSystem> OnFirePool;

  private Dictionary<Enemy, ParticleSystem> EnemyParticleSystems = new();

  private void Awake() {
    OnFirePool = new ObjectPool<ParticleSystem>(CreateOnFireSystem);
    AttackRadius.OnEnemyEnter += StartDamagingEnemy;
    AttackRadius.OnEnemyExit += StopDamagingEnemy;
   StopShooting();
  }

 
  private ParticleSystem CreateOnFireSystem() {
    return Instantiate(OnFireSystemPrefab);
  }

  private void StartDamagingEnemy(Enemy Enemy) {
    if (Enemy.TryGetComponent<IBurnable>(out IBurnable burnable) ) {
     burnable.StartBurning(BurningDPS);
     //Enemy.HeathBar.OnDeath += HandeEnmyDeath;
     ParticleSystem OnFireSystem = OnFirePool.Get();
     OnFireSystem.transform.SetParent(Enemy.transform, false);
     OnFireSystem.transform.localPosition = Vector3.zero;
     ParticleSystem.MainModule main = OnFireSystem.main;
     main.loop = true;
     EnemyParticleSystems.Add(Enemy, OnFireSystem);
     
    }
  }

  private void HandeEnmyDeath(Enemy Enemy) {
  //  Enemy.Health.OnDeath -= HandleEnmyDeath;
  if (EnemyParticleSystems.ContainsKey(Enemy)) {
    EnemyParticleSystems.Remove(Enemy);
  }
  }
  public void Shoot() {
    ShootingSystem.gameObject.SetActive(true);
    AttackRadius.gameObject.SetActive(true);
  }

 public void StopShooting() {
    AttackRadius.gameObject.SetActive(false);
    ShootingSystem.gameObject.SetActive(false);
  }
  private void StopDamagingEnemy(Enemy Enemy) {
    //  Enemy.Health.OnDeath -= HandleEnmyDeath;
    if (EnemyParticleSystems.ContainsKey(Enemy)) {
      EnemyParticleSystems.Remove(Enemy);
    }
  }

}
