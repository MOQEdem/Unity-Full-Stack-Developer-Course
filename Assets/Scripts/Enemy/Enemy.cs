using Bullets;
using Components;
using UnityEngine;

namespace Enemy
{
    public sealed class Enemy : MonoBehaviour
    {
        public void Construct(GameObject player, BulletManager bulletSystem)
        {
            GetComponent<EnemyAttackAgent>().SetTarget(player);
            GetComponent<FireComponent>().Construct(bulletSystem);
        }
    }
}