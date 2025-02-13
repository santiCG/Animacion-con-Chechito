using System;
using System.Data;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Sesiones.Santiago.Animation
{
    public class CharacterLook : MonoBehaviour
    {
        [SerializeField] private Transform target;

        [SerializeField] private FloatDampener horizontalDampener;
        [SerializeField] private FloatDampener verticalDampener;

        [SerializeField] private float horzRotSpeed;
        [SerializeField] private float vertRotSpeed;
        public void OnLook(InputAction.CallbackContext context)
        {
            Vector2 inputValue = context.ReadValue<Vector2>();

            inputValue = inputValue / new Vector2(Screen.width, Screen.height); // normalizar el vector dependiendo del ancho de la pantalla

            horizontalDampener.TargetValue = inputValue.x;
            verticalDampener.TargetValue = inputValue.y;
        }

        private void ApplyLookRotation()
        {
            if (target == null)
            {
                throw new NullReferenceException("Tager is null");
            }

            Quaternion horizontalRotation = Quaternion.AngleAxis(horizontalDampener.CurrentValue * horzRotSpeed, transform.up);
            //Quaternion verticalRotation = Quaternion.AngleAxis(verticalDampener.CurrentValue * vertRotSpeed, transform.right);

            transform.rotation *= horizontalRotation;
        }

        private void Update()
        {
            horizontalDampener.Update();
            verticalDampener.Update();

            ApplyLookRotation();
        }
    }

}

