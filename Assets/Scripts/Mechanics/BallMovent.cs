using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovent : MonoBehaviour
{
    Rigidbody2D rigidbody;
    PhotonView photonView;

    int playerOneScore, playerTwoScore;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        photonView = GetComponent<PhotonView>();
        rigidbody.velocity = new Vector2(5f, 5f);
    }


    void Update()
    {

    }

    public void WriteScore()
    {
        Debug.Log($"birinci oyuncu skor {playerOneScore}, ikinci oyuncu skor {playerTwoScore}");
    }

    [PunRPC]
    public void startMovent()
    {
        rigidbody.velocity = new Vector2(3f, 3f);
        WriteScore();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (photonView.IsMine)
        {
            if (collision.gameObject.tag == "RightStick")
            {
                photonView.RPC("goal", RpcTarget.All, 0, 1);
            }
            else if (collision.gameObject.tag == "LeftStick")
            {
                photonView.RPC("goal", RpcTarget.All, 1, 0);
            }
        }
    }

    [PunRPC]
    public void goal(int playerOne, int playerTwo)
    {
        playerOneScore += playerOne;
        playerTwoScore += playerTwo;

        WriteScore();
    }

    public void service()
    {
        rigidbody.velocity = Vector2.zero;
        transform.position = Vector2.zero;

        rigidbody.velocity = new Vector2(3, 3);
    }

}
