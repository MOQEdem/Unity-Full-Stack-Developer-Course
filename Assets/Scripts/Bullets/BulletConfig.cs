using Common;
using UnityEngine;

namespace Bullets
{
    [CreateAssetMenu(
        fileName = "BulletConfig",
        menuName = "Bullets/New BulletConfig"
    )]
    public sealed class BulletConfig : ScriptableObject
    {
        [field: SerializeField] 
        public PhysicsLayer PhysicsLayer { get; private set; }

        [field: SerializeField] 
        public Color Color { get; private set; }

        [field: SerializeField]
        public int Damage { get; private set; }

        [field: SerializeField]
        public float Speed { get; private set; }
    }
}