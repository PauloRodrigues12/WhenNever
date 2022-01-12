using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementJoystick : MonoBehaviour
{
    #region Variaveis

    //Imagem
    public Image InnerStick;

    //Vetores
    public Vector2 initialDisplacement;
    public Vector2 displacement;
    public Vector2 touchPos;

    //Joystick
    public FireJoystick fireJoystick;
    public float maximumDisplacement;

    private int tapCount;

    public bool isMoving = false;

    #endregion

    #region Start e Update

    private void Start()
    {
        initialDisplacement = InnerStick.rectTransform.position;
    }

    private void Update()
    {
        //Numero de toques
        tapCount = Input.touchCount;

        //Se nao haver toques
        if (tapCount <= 0)
            isMoving = false;

        //Se houver apenas 1 toque
        if (tapCount == 1)
        {
            touchPos = new Vector2(Input.GetTouch(0).position.x,
                           Input.GetTouch(0).position.y);

            if (touchPos.x < Screen.width / 2)
            {
                displacement = new Vector2(Input.GetTouch(0).position.x - initialDisplacement.x,
                    Input.GetTouch(0).position.y - initialDisplacement.y);

                isMoving = true;
                fireJoystick.isShooting = false;
            }
        }

        //Se houver 2 ou mais toques
        if (tapCount >= 2)
        {
            isMoving = true;
            fireJoystick.isShooting = true;

            //Se o toque for do lado esquerdo do ecra
            if (touchPos.x < Screen.width / 2)
            {
                displacement = new Vector2(Input.GetTouch(0).position.x - initialDisplacement.x,
                    Input.GetTouch(0).position.y - initialDisplacement.y);

                touchPos = new Vector2(Input.GetTouch(0).position.x,
                           Input.GetTouch(0).position.y);
                fireJoystick.touchPos = new Vector2(Input.GetTouch(1).position.x,
                           Input.GetTouch(1).position.y);
            }

            //Se o toque for do lado direito do ecra
            if (touchPos.x > Screen.width / 2)
            {
                displacement = new Vector2(Input.GetTouch(1).position.x - initialDisplacement.x,
                    Input.GetTouch(1).position.y - initialDisplacement.y);

                touchPos = new Vector2(Input.GetTouch(1).position.x,
                           Input.GetTouch(1).position.y);
                fireJoystick.touchPos = new Vector2(Input.GetTouch(0).position.x,
                           Input.GetTouch(0).position.y);
            }
        }

        //Movimento da imagem do joystick
        if (isMoving == false)
        {
            InnerStick.rectTransform.position = initialDisplacement;
            displacement = Vector2.zero;
        }

        float distance = displacement.magnitude;

        if (distance > maximumDisplacement)
            displacement = displacement.normalized * maximumDisplacement;

        InnerStick.rectTransform.position = initialDisplacement + displacement;
    }

    #endregion

    #region Funções

    public float GetAxis(string axis)
    {
        if (axis == "Horizontal")
            return displacement.x / maximumDisplacement;
        else
            if (axis == "Vertical")
            return displacement.y / maximumDisplacement;
        else
            return 0;
    }

    #endregion
}
