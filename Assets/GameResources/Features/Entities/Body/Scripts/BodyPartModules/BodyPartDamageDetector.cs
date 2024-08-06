namespace Features.Entities
{
    using System;
    using Extensions;

    /// <summary>
    /// Детектор получения урона частью тела.
    /// </summary>
    public class BodyPartDamageDetector : BaseBodyPartModule, IDamageable
    {
        /// <summary>
        /// Зафиксировано получение урона.
        /// </summary>
        public event Action<string, string, float> onTakeDamage = delegate { };

        void IDamageable.TakeDamage(string damageType, float damage) => onTakeDamage(bodyPart.Type, damageType, damage);
    }
}