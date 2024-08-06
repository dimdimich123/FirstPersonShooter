namespace Features.Entities
{
    using System;

    /// <summary>
    /// Контроллер просчёта получаемого частью тела урона.
    /// </summary>
    public class BodyPartDamageCalculatorController : BaseBodyPartModulesController
    {
        protected const float DEFAULT_PROTECTION_VALUE = 0f;

        /// <summary>
        /// Урон просчитан.
        /// </summary>
        public event Action<string, float> onDamageCalculated = delegate { };

        // Модули проверки попадания и уменьшения урона.
        // Возможно нужно где-то хранить все бафы и проходить по ним.
        protected BodyPartDamageDetector bodyPartDamageDetector = default;

        protected BodyPartProtectionAttributes bodyPartProtectionAttributes = default;

        protected BaseBodyPart bodyPart = default;

        //TODO: определение калькулятора урона через DI.
        protected AbstractDamageCalculator damageCalculator = new ProtectionCountDamageCalculator();

        protected float protectionValue = default;
        protected float decreaseHealthValue = default;

        public override void Initialize(BaseBody _body)
        {
            base.Initialize(_body);
            Subscribe();
        }

        /// <summary>
        /// Высчитывает получаемый частью тела урон.
        /// </summary>
        /// <param name="bodyType">Часть тела получающая урон.</param>
        /// <param name="damageType">Тип получаемого урона.</param>
        /// <param name="damage">Количество получаемого урона.</param>
        public virtual void CalculateDamage(string bodyType, string damageType, float damage)
        {
            decreaseHealthValue = 0f;

            if (body.GetBodyPartByType(bodyType, ref bodyPart))
            {
                bodyPart.GetModuleByType(ref bodyPartProtectionAttributes);
                protectionValue = bodyPartProtectionAttributes != null ? bodyPartProtectionAttributes.GetProtectionValue(damageType) : DEFAULT_PROTECTION_VALUE;
                decreaseHealthValue = damageCalculator.Calculate(damage, protectionValue);
                onDamageCalculated(bodyType, decreaseHealthValue);
            }
        }

        /// <summary>
        /// Подписывается на события.
        /// </summary>
        protected virtual void Subscribe()
        {
            foreach (BaseBodyPart bodyPart in body.BodyParts)
            {
                if (bodyPart.GetModuleByType(ref bodyPartDamageDetector))
                {
                    bodyPartDamageDetector.onTakeDamage += CalculateDamage;
                }
            }
        }

        /// <summary>
        /// Отписывается от событий.
        /// </summary>
        protected virtual void Unsubscribe()
        {
            foreach (BaseBodyPart bodyPart in body.BodyParts)
            {
                if (bodyPart.GetModuleByType(ref bodyPartDamageDetector))
                {
                    bodyPartDamageDetector.onTakeDamage -= CalculateDamage;
                }
            }
        }

        protected virtual void OnDestroy() => Unsubscribe();
    }
}