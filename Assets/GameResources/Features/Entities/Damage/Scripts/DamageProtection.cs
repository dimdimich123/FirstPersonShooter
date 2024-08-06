namespace Features.Entities
{
    /// <summary>
    /// Защита от урона.
    /// </summary>
    [System.Serializable]
    public class DamageProtection
    {
        /// <summary>
        /// Тип блокироуемого урона.
        /// </summary>
        public DamageType ProtectionType = default;

        /// <summary>
        /// Значение блокируемого урона.
        /// </summary>
        public float ProtectionValue = default;
    }
}