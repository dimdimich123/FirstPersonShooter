namespace Features.Inputs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.InputSystem;

    /// <summary>
    /// Контроллер ввода.
    /// </summary>
    public class InputController : MonoBehaviour
    {
        /// <summary>
        /// События ввода.
        /// </summary>
        public BaseInputActions InputActions => inputActions;

        protected BaseInputActions inputActions = default;

        [SerializeField]
        protected List<AbstractInputProvider> inputProviders = new List<AbstractInputProvider>();

        protected AbstractInputProvider inputProvider = default;

        protected virtual void Awake()
        {
            inputActions = new BaseInputActions();
            inputActions.Enable();

            foreach (InputActionMap actionMap in inputActions.asset.actionMaps)
            {
                foreach (InputAction inputAction in actionMap.actions)
                {
                    inputProvider = inputProviders.FirstOrDefault(provider => provider.InputEventName == inputAction.name);
                    inputProvider?.Initialize(inputAction);
                }
            }
        }

        protected virtual void OnDestroy()
        {
            foreach(IDisposable inputProvider in inputProviders)
            {
                inputProvider.Dispose();
            }

            inputActions?.Disable();
            inputActions?.Dispose();
        }
    }
}