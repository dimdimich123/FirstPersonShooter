namespace Features.Entities
{
    using System;
    using UnityEngine;

    /// <summary>
    /// Компонент здоровья сущности.
    /// </summary>
    public class EntityHealth : MonoBehaviour
    {
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
        /// Здоровье.
        /// </summary>
        public virtual float Health
        {
            get => health; 
            
            set
            {
                if(health != value)
                {
                    if (health > value)
                    {
                        health = value;

                        if (health <= 0)
                        {
                            health = 0;
                        }

                        onHealthDecreased();

                        if(health == 0)
                        {
                            onHealthOver();
                        }
                    }
                    else
                    {
                        health = value;

                        if(health > maxHealth)
                        {
                            health = maxHealth;
                        }

                        onHealthIncreased();
                    }
                }
            }
        }

        [SerializeField, Min(0.01f)]
        protected float defaultHealth = default;

        [SerializeField, Min(0.01f)]
        protected float maxHealth = default;

        protected float health = default;

        protected virtual void Awake() => ResetHealth();

        /// <summary>
        /// Востанавливет здоровье.
        /// </summary>
        public virtual void ResetHealth() => health = defaultHealth;

        /// <summary>
        /// Инициализирует здоровье.
        /// </summary>
        /// <param name="_defaultHealth">Дефолтное здоровье.</param>
        /// <param name="_maxHealth">Максимальное здоровье.</param>
        public virtual void Initialize(float _defaultHealth, float _maxHealth)
        {
            defaultHealth = _defaultHealth;
            maxHealth = _maxHealth;
            ResetHealth();
        }
    }
}