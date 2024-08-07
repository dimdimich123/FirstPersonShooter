namespace Features.Inputs
{
    using System;
    using UnityEngine;
    using UnityEngine.InputSystem;

    /// <summary>
    /// Абстракция провайдера ввода.
    /// </summary>
    public abstract class AbstractInputProvider : ScriptableObject, IDisposable
    {
        /// <summary>
        /// Тип кнопки ввода.
        /// </summary>
        public string InputEventName => inputEventName;

        [SerializeField]
        protected string inputEventName = default;

        protected InputAction inputAction = default;

        /// <summary>
        /// Инициализирует провайдер.
        /// </summary>
        /// <param name="_inputAction">Событие ввода.</param>
        public virtual void Initialize(InputAction _inputAction)
        {
            inputAction = _inputAction;
            SubscribeOnEvents();
        }

        /// <summary>
        /// Подписывается на события.
        /// </summary>
        protected virtual void SubscribeOnEvents()
        {
            if (inputAction != null)
            {
                inputAction.started += OnInputStarted;
                inputAction.performed += OnInputPerformed;
                inputAction.canceled += OnInputCanceled;
            }
        }

        /// <summary>
        /// Отписывается от событий.
        /// </summary>
        protected virtual void UnsubscribeOnEvents()
        {
            if (inputAction != null)
            {
                inputAction.started -= OnInputStarted;
                inputAction.performed -= OnInputPerformed;
                inputAction.canceled -= OnInputCanceled;
            }
        }

        /// <summary>
        /// Обработчик начала нажатия на клавишу.
        /// </summary>
        /// <param name="context">Контекст.</param>
        protected abstract void OnInputStarted(InputAction.CallbackContext context);

        /// <summary>
        /// Обработчик выполнения нажатия на клавишу.
        /// </summary>
        /// <param name="context">Контекст.</param>
        protected abstract void OnInputPerformed(InputAction.CallbackContext context);

        /// <summary>
        /// Обработчик завершения нажатия на клавишу.
        /// </summary>
        /// <param name="context">Контекст.</param>
        protected abstract void OnInputCanceled(InputAction.CallbackContext context);

        void IDisposable.Dispose() => UnsubscribeOnEvents();
    }
}