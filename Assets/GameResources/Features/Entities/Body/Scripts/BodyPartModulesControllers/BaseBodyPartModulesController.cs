namespace Features.Entities
{
    using UnityEngine;

    /// <summary>
    /// Базовая реализация контроллера 
    /// </summary>
    public class BaseBodyPartModulesController : MonoBehaviour
    {
        protected BaseBody body = default; 

        /// <summary>
        /// Инициализирует контроллер.
        /// </summary>
        /// <param name="_body">Тело.</param>
        public virtual void Initialize(BaseBody _body) => body = _body;
    }
}