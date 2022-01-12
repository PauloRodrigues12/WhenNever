using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPiece : MonoBehaviour
{
    //Peça
    public bool hasPickedUp;
    public int pieceNr;

    private ChargeTP chargeTP;

    private void Start()
    {
        chargeTP = GameObject.FindGameObjectWithTag("TP").GetComponent<ChargeTP>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (hasPickedUp == false)
            {
                //Apanhar peça
                gameObject.SetActive(false);
                hasPickedUp = true;

                if (pieceNr == 1)
                    chargeTP.hasPiece1 = true;
                if (pieceNr == 2)
                    chargeTP.hasPiece2 = true;
                if (pieceNr == 3)
                    chargeTP.hasPiece3 = true;
            }
        }
    }
}
