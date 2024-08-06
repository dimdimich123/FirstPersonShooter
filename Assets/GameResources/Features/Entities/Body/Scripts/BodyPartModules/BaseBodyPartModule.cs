namespace Features.Entities
{
    using UnityEngine;

    /// <summary>
    /// Базовая реализация модуля части тела.
    /// </summary>
    public class BaseBodyPartModule : MonoBehaviour
    {
        protected BaseBodyPart bodyPart = default;

        /// <summary>
        /// Инициализирует модуль.
        /// </summary>
        /// <param name="_bodyPart">Часть тела.</param>
        public virtual void Initialize(BaseBodyPart _bodyPart) => bodyPart = _bodyPart;
    }
}