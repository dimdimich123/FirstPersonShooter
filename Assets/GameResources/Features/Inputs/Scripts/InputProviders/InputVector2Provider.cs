namespace Features.Inputs
{
    using System.Threading;
    using System.Threading.Tasks;
    using UnityEngine;
    using UnityEngine.InputSystem;

    /// <summary>
    /// Провайдер событий ввода с данными типа <see cref="Vector2"/>.
    /// </summary>
    [CreateAssetMenu(fileName = nameof(InputVector2Provider), menuName = "Features/InputEvents/" + nameof(InputVector2Provider))]
    public class InputVector2Provider : AbstractInputValueProvider<Vector2>
    {
        protected CancellationTokenSource cancellationTokenSource = default;

        protected override void OnInputStarted(InputAction.CallbackContext context)
        {
            cancellationTokenSource?.Cancel();
            cancellationTokenSource = new CancellationTokenSource();
            CheckInput();
        }

        protected override void OnInputCanceled(InputAction.CallbackContext context) => cancellationTokenSource?.Cancel();

        /// <summary>
        /// Проверяет ввод клавиш передвижения.
        /// </summary>
        protected virtual async void CheckInput()
        {
            while(inputAction.IsPressed())
            {
                NotifyOnInputPerformed(inputAction.ReadValue<Vector2>());
                await Task.Yield();
            }
        }

        protected virtual void OnDestroy() => cancellationTokenSource?.Dispose();
    }
}