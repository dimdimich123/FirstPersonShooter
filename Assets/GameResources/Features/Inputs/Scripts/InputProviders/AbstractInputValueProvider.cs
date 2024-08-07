namespace Features.Inputs
{
    using System;
    using UnityEngine.InputSystem;

    /// <summary>
    /// Провайдер событий ввода с данными.
    /// </summary>
    /// <typeparam name="T">Тип значения передоваемый вводом.</typeparam>
    public class AbstractInputValueProvider<T> : AbstractInputProvider where T : struct
    {
        /// <summary>
        /// Было выполнено начало нажатия кнопки.
        /// </summary>
        public event Action<T> onInputStarted = delegate { };

        /// <summary>
        /// Нажатие кнопки было выполнено.
        /// </summary>
        public event Action<T> onInputPerformed = delegate { };

        /// <summary>
        /// Нажатие кнопки было завершено.
        /// </summary>
        public event Action<T> onInputCanceled = delegate { };

        /// <summary>
        /// Оповещает о старте нажатия на кнопку.
        /// </summary>
        /// <param name="data">Передаваемые данные при оповещении.</param>
        protected virtual void NotifyOnInputStarted(T data) => onInputStarted(data);

        /// <summary>
        /// Оповещает о выполнении нажатия на кнопку.
        /// </summary>
        /// <param name="data">Передаваемые данные при оповещении.</param>
        protected virtual void NotifyOnInputPerformed(T data) => onInputPerformed(data);

        /// <summary>
        /// Оповещает об окончании нажатия на кнопку.
        /// </summary>
        /// <param name="data">Передаваемые данные при оповещении.</param>
        protected virtual void NotifyOnInputCanceled(T data) => onInputCanceled(data);

        /// <summary>
        /// Обработчик начала нажатия на клавишу.
        /// </summary>
        /// <param name="context">Контекст.</param>
        protected override void OnInputStarted(InputAction.CallbackContext context) => NotifyOnInputStarted(context.ReadValue<T>());

        /// <summary>
        /// Обработчик выполнения нажатия на клавишу.
        /// </summary>
        /// <param name="context">Контекст.</param>
        protected override void OnInputPerformed(InputAction.CallbackContext context) => NotifyOnInputPerformed(context.ReadValue<T>());

        /// <summary>
        /// Обработчик завершения нажатия на клавишу.
        /// </summary>
        /// <param name="context">Контекст.</param>
        protected override void OnInputCanceled(InputAction.CallbackContext context) => NotifyOnInputCanceled(context.ReadValue<T>());
    }
}