namespace Features.Entities
{
    /// <summary>
    /// Контроллер получаемого частью тела урона.
    /// </summary>
    public class BodyPartHealthController : BaseBodyPartModulesController
    {
        protected BodyPartDamageCalculatorController bodyPartDamageCalculator = default;

        protected BodyPartHealth bodyPartHealth = default;

        protected BaseBodyPart bodyPart = default;

        public override void Initialize(BaseBody _body)
        {
            base.Initialize(_body);
            body.GetBodyPartModulesControllerByType(ref bodyPartDamageCalculator);
            Subscribe();
        }

        /// <summary>
        /// Высчитывает получаемый частью тела урон.
        /// </summary>
        /// <param name="bodyType">Часть тела получающая урон.</param>
        /// <param name="damageType">Тип получаемого урона.</param>
        /// <param name="damage">Количество получаемого урона.</param>
        public virtual void TakeDamage(string bodyType, float damage)
        {
            if (body.GetBodyPartByType(bodyType, ref bodyPart))
            {
                if (bodyPart.GetModuleByType(ref bodyPartHealth))
                {
                    bodyPartHealth.DecreaseHealth(damage);
                }
            }
        }

        /// <summary>
        /// Подписывается на события.
        /// </summary>
        protected virtual void Subscribe() => bodyPartDamageCalculator.onDamageCalculated += TakeDamage;

        /// <summary>
        /// Отписывается от событий.
        /// </summary>
        protected virtual void Unsubscribe() => bodyPartDamageCalculator.onDamageCalculated -= TakeDamage;

        protected virtual void OnDestroy() => Unsubscribe();
    }
}