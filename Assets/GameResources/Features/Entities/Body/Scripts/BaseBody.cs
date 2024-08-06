namespace Features.Entities
{
    using System.Linq;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Базовая реализация части тела.
    /// </summary>
    public class BaseBody : MonoBehaviour
    {
        /// <summary>
        /// Части тела.
        /// </summary>
        public virtual IReadOnlyList<BaseBodyPart> BodyParts => bodyParts;

        /// <summary>
        /// Контроллеры модулей частей тела.
        /// </summary>
        public virtual IReadOnlyList<BaseBodyPartModulesController> BodyPartsModuleControllers => bodyPartsModuleControllers;

        [SerializeField]
        protected List<BaseBodyPart> bodyParts = new List<BaseBodyPart>();

        [SerializeField]
        protected List<BaseBodyPartModulesController> bodyPartsModuleControllers = new List<BaseBodyPartModulesController>();

        protected virtual void Awake()
        {
            foreach(BaseBodyPartModulesController controller in bodyPartsModuleControllers)
            {
                controller.Initialize(this);
            }
        }

        /// <summary>
        /// Возвращает часть тела по типу.
        /// </summary>
        /// <param name="bodyType">Тип части тела.</param>
        /// <param name="bodyPart">Экземпляр части тела.</param>
        /// <returns>Получилось найти часть тела.</returns>
        public virtual bool GetBodyPartByType(string bodyType, ref BaseBodyPart bodyPart)
        {
            bodyPart = bodyParts.FirstOrDefault(_bodyPart => _bodyPart.Type == bodyType);
            return bodyPart != null;
        }

        /// <summary>
        /// Возвращает контроллер модулей частей тела по типу.
        /// </summary>
        /// <param name="bodyPartModuleController">Контроллер модулей частей тела.</param>
        /// <returns>Получилось найти контроллер модулей частей тела.</returns>
        public virtual bool GetBodyPartModulesControllerByType<T>(ref T bodyPartModuleController) where T : BaseBodyPartModulesController
        {
            IEnumerable<T> modules = bodyPartsModuleControllers.OfType<T>();

            if(modules != null)
            {
                bodyPartModuleController = modules.ToList()[0];
            }

            return bodyPartModuleController != null;
        }
    }
}