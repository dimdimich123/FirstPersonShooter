namespace Features.Entities
{
    using System;
    using UnityEngine;

    /// <summary>
    /// Модуль здоровья части тела.
    /// </summary>
    public class BodyPartHealth : BaseBodyPartModule
    {
        //TODO: интерфейс увеличения здоровья. 
        //Логгер - тип сообщения.

        /// <summary>
        /// Здоровье увеличилось.
        /// </summary>
        public event Action onHealthIncreased = delegate { };

        /// <summary>
        /// Здоровье уменьшилось.
        /// </summary>
        public event Action onHealthDecreased = delegate { };

        /// <summary>
        /// Здоровье закончилось.
        /// </summary>
        public event Action onHealthOver = delegate { };

        /// <summary>
        /// Количество здоровья.
        /// </summary>
        public virtual float Health => health.Health;

        [SerializeField]
        protected EntityHealth health = default;

        protected virtual void OnEnable()
        {
            health.onHealthDecreased += NotifyOnHealthDecreased;
            health.onHealthIncreased += NotifyOnHealthIncreased;
            health.onHealthOver += NotifyOnHealthhOver;
        }

        /// <summary>
        /// Оповещает об уменьшении здоровья.
        /// </summary>
        protected virtual void NotifyOnHealthDecreased() => onHealthDecreased();

        /// <summary>
        /// Оповещает об увеличении здоровья.
        /// </summary>
        protected virtual void NotifyOnHealthIncreased() => onHealthIncreased();

        /// <summary>
        /// Оповещает об окончании здоровья.
        /// </summary>
        protected virtual void NotifyOnHealthhOver() => onHealthOver();

        /// <summary>
        /// Уменьшает здоровье.
        /// </summary>
        /// <param name="healthDecreased">Количество отнимаемого здоровья.</param>
        public void DecreaseHealth(float healthDecreased) => health.Health -= healthDecreased;

        protected virtual void OnDisable()
        {
            health.onHealthDecreased -= NotifyOnHealthDecreased;
            health.onHealthIncreased -= NotifyOnHealthIncreased;
            health.onHealthOver -= NotifyOnHealthhOver;
        }
    }
}