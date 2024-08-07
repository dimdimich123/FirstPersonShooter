namespace Features.Inputs
{
    using System;
    using UnityEngine;
    using UnityEngine.InputSystem;

    /// <summary>
    /// Провайдер событий ввода.
    /// </summary>
    [CreateAssetMenu(fileName = nameof(InputButtonProvider), menuName = "Features/InputEvents/" + nameof(InputButtonProvider))]
    public class InputButtonProvider : AbstractInputProvider
    {
        /// <summary>
        /// Было выполнено начало нажатия кнопки.
        /// </summary>
        public event Action onInputStarted = delegate { };

        /// <summary>
        /// Нажатие кнопки было выполнено.
        /// </summary>
        public event Action onInputPerformed = delegate { };

        /// <summary>
        /// Нажатие кнопки было завершено.
        /// </summary>
        public event Action onInputCanceled = delegate { };

        /// <summary>
        /// Оповещает о старте нажатия на кнопку.
        /// </summary>
        protected virtual void NotifyOnInputStarted() => onInputStarted();

        /// <summary>
        /// Оповещает о выполнении нажатия на кнопку.
        /// </summary>
        protected virtual void NotifyOnInputPerformed() => onInputPerformed();

        /// <summary>
        /// Оповещает об окончании нажатия на кнопку.
        /// </summary>
        protected virtual void NotifyOnInputCanceled() => onInputCanceled();

        protected override void OnInputStarted(InputAction.CallbackContext context) => NotifyOnInputStarted();

        protected override void OnInputPerformed(InputAction.CallbackContext context) => NotifyOnInputPerformed();

        protected override void OnInputCanceled(InputAction.CallbackContext context) => NotifyOnInputCanceled();
    }
}