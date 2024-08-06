namespace Features.Entities
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    /// <summary>
    /// Модуль с параметрами защиты от урона.
    /// </summary>
    public class BodyPartProtectionAttributes : BaseBodyPartModule
    {
        [SerializeField]
        protected List<DamageProtection> damageProtections = new List<DamageProtection>();

        protected DamageProtection damageProtection = default;

        /// <summary>
        /// Возвращает количество защиты по типу урона.
        /// </summary>
        /// <param name="damageType">Тип урона.</param>
        /// <returns>Количество защиты.</returns>
        public virtual float GetProtectionValue(string damageType)
        {
            damageProtection = damageProtections.FirstOrDefault(protection => protection.ProtectionType.Id == damageType);
            return damageProtection == null ? 0f : damageProtection.ProtectionValue;
        }
    }
}