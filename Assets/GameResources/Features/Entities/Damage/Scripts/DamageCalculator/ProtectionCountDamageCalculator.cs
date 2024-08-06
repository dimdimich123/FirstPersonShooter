namespace Features.Entities
{
    using UnityEngine;

    /// <summary>
    /// Калькулятор урона по количеству брони (без использования формул).
    /// </summary>
    public class ProtectionCountDamageCalculator : AbstractDamageCalculator
    {
        protected const float MIN_DAMAGE = 0f;

        public override float Calculate(float damage, float protection)
            => Mathf.Clamp(damage - protection, MIN_DAMAGE, float.MaxValue);
    }
}