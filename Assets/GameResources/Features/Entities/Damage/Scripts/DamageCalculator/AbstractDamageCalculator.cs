namespace Features.Entities
{
    /// <summary>
    /// Абстракция калькулятора урона.
    /// </summary>
    public abstract class AbstractDamageCalculator
    {
        /// <summary>
        /// Просчитывает урон по количеству урона и количеству защиты.
        /// </summary>
        /// <param name="damage">Количество урона.</param>
        /// <param name="protection">Количество защиты.</param>
        /// <returns>Получаемое количество урона.</returns>
        public abstract float Calculate(float damage, float protection);
    }
}