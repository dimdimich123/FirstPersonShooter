namespace Features.Extensions
{
    /// <summary>
    /// Интерфейс получения урона.
    /// </summary>
    public interface IDamageable
    {
        /// <summary>
        /// Получает урон.
        /// </summary>
        /// <param name="damageType">Тип урона.</param>
        /// <param name="damage">Количество урона.</param>
        void TakeDamage(string damageType, float damage);
    }
}