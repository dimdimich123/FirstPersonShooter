namespace Features.Inputs
{
    using System;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.InputSystem;

    /// <summary>
    /// Провайдер нажатия клавиш перемещения.
    /// </summary>
    public class BaseMovementInputProvider : MonoBehaviour
    {
        protected const float DEFAULT_AXIS_VALUE = 0f;

        /// <summary>
        /// Горизонтальная ось перемещения изменилась.
        /// </summary>
        public event Action<float> onHorizontalAxisChanged = delegate { };

        /// <summary>
        /// Вертикальная ось перемещения изменилась.
        /// </summary>
        public event Action<float> onVerticalAxisChanged = delegate { };

        /// <summary>
        /// Выполнен прыжок.
        /// </summary>
        public event Action onJump = delegate { };

        protected BaseInputActions inputActions = default;

        protected Vector2 inputDirection = Vector2.zero;

        protected Coroutine coroutine = null;

        protected virtual void Awake()
        {
            inputActions = new BaseInputActions();
            inputActions.Enable();
        }

        protected virtual void OnEnable() => Subscribe();

        protected virtual void OnDisable() => Unsubscribe();

        protected virtual void OnDestroy()
        {
            inputActions?.Disable();
            inputActions?.Dispose();
        }

        /// <summary>
        /// Подписывается на события нажатия клавиш.
        /// </summary>
        protected virtual void Subscribe()
        {
            inputActions.GamePlay.Jump.started += OnJumpStarted;
            inputActions.GamePlay.Movement.started += OnMovementStarted;
            inputActions.GamePlay.Movement.canceled += OnMovementCanceled;
        }

        /// <summary>
        /// Отписывается от событий нажатия клавиш.
        /// </summary>
        protected virtual void Unsubscribe()
        {
            inputActions.GamePlay.Jump.started -= OnJumpStarted;
            inputActions.GamePlay.Movement.started -= OnMovementStarted;
            inputActions.GamePlay.Movement.canceled -= OnMovementCanceled;
        }

        /// <summary>
        /// Оповещает об изменении горизонтальном оси перемещения.
        /// </summary>
        protected virtual void NotifyOnHorizontalAxisChanged(float value) => onHorizontalAxisChanged(value);

        /// <summary>
        /// Оповещает об изменении вертикальной оси перемещения.
        /// </summary>
        protected virtual void NotifyOnVerticalAxisChanged(float value) => onVerticalAxisChanged(value);

        /// <summary>
        /// Оповещает о прыжке.
        /// </summary>
        protected virtual void NotifyOnJump() => onJump();

        /// <summary>
        /// Оповещает о нажатии клавиши прыжка.
        /// </summary>
        protected virtual void OnJumpStarted(InputAction.CallbackContext context) => NotifyOnJump();

        /// <summary>
        /// Запускает проверку ввода клавиш передвижения.
        /// </summary>
        protected virtual void OnMovementStarted(InputAction.CallbackContext context)
        {
            StopCheckMovement();
            coroutine = StartCoroutine(CheckMovement());
        }

        /// <summary>
        /// Останавливает проверку ввода клавиш передвижения по остановке нажатия клавиш.
        /// </summary>
        protected virtual void OnMovementCanceled(InputAction.CallbackContext context) => StopCheckMovement();

        /// <summary>
        /// Останавливает проверку ввода клавиш передвижения.
        /// </summary>
        protected virtual void StopCheckMovement()
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }
        }

        /// <summary>
        /// Проверяет ввод клавиш передвижения.
        /// </summary>
        protected virtual IEnumerator CheckMovement()
        {
            while(isActiveAndEnabled && inputActions.GamePlay.Movement.IsInProgress())
            {
                inputDirection = inputActions.GamePlay.Movement.ReadValue<Vector2>();

                if (!Mathf.Approximately(inputDirection.x, DEFAULT_AXIS_VALUE))
                {
                    NotifyOnHorizontalAxisChanged(inputDirection.x);
                }

                if (!Mathf.Approximately(inputDirection.y, DEFAULT_AXIS_VALUE))
                {
                    NotifyOnVerticalAxisChanged(inputDirection.y);
                }

                yield return null;
            }
        }
    }
}