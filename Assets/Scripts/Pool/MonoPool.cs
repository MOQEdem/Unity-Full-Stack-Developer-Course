using System.Collections.Generic;
using UnityEngine;

namespace Pool
{
    public abstract class MonoPool<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] protected T Prefab;
        [SerializeField] protected int Size;
        [SerializeField] protected Transform Container;
        [SerializeField] protected Transform WorldTransform;

        private readonly Queue<T> Pool = new();
        private readonly HashSet<T> ActiveObject = new();

        public IEnumerable<T> ActiveObjects => ActiveObject;

        public void Awake()
        {
            for (var i = 0; i < Size; i++)
            {
                Pool.Enqueue(CreateObject());
            }
        }

        public virtual T Rent()
        {
            if (Pool.TryDequeue(out T gameObject))
            {
                gameObject.transform.SetParent(WorldTransform);
            }
            else
            {
                gameObject = CreateObject();
                gameObject.transform.SetParent(WorldTransform);
            }


            ActiveObject.Add(gameObject);
            return gameObject;
        }

        public virtual void Return(T gameObject)
        {
            Pool.Enqueue(gameObject);
            ActiveObject.Remove(gameObject);
            gameObject.transform.SetParent(Container);
        }

        protected virtual T CreateObject()
        {
            return Instantiate(Prefab, Container);
        }
    }
}
