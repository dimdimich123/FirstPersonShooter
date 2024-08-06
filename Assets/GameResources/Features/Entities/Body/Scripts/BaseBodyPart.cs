namespace Features.Entities
{
    using System.Linq;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Базовая реализация части тела.
    /// </summary>
    public class BaseBodyPart : MonoBehaviour
    {
        /// <summary>
        /// Тип части тела.
        /// </summary>
        public virtual string Type => type.Id;

        /// <summary>
        /// Модули части тела.
        /// </summary>
        public virtual IReadOnlyList<BaseBodyPartModule> Modules => modules;

        [SerializeField]
        protected BodyPartType type = default;

        [SerializeField]
        protected List<BaseBodyPartModule> modules = new List<BaseBodyPartModule>();

        protected virtual void Awake()
        {
            foreach(BaseBodyPartModule module in modules) 
            {
                module.Initialize(this);
            }
        }

        /// <summary>
        /// Находит модуль по типу.
        /// </summary>
        /// <typeparam name="T">Тип модулей.</typeparam>
        /// <param name="module"></param>
        /// <returns>Получилось найти модуль части тела.</returns>
        public virtual bool GetModuleByType<T>(ref T module) where T : BaseBodyPartModule
        {
            BaseBodyPartModule baseModule = modules.FirstOrDefault(module => module is T);
            module = baseModule != null ? baseModule as T : null;
            return module != null;
        }
    }
}