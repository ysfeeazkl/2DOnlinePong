using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Photon.Pun;

namespace Assets.Scripts.Mechanics
{
    public class PlayerMovent:MonoBehaviour
    {
        PhotonView photonView;

        private void Start()
        {
            photonView = GetComponent<PhotonView>();

            if (photonView.IsMine)
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    transform.position = new Vector2(7, 0f);
                    gameObject.name = "1. Player";
                    InvokeRepeating("playerControl", 0, 0.5f);
                }
                else if (!PhotonNetwork.IsMasterClient)
                {
                    gameObject.name = "2. Player";
                    transform.position = new Vector2(-7, 0f);
                }
            }
        }

        private void Update()
        {
            if (photonView.IsMine)
            {
                movent();
            }
        }

        void playerControl()
        {
            if (PhotonNetwork.PlayerList.Length==2)
            {
                GameObject.Find("Ball").GetComponent<PhotonView>().RPC("startMovent" , RpcTarget.All, null);
                CancelInvoke("playerControl");
            }
        }

        void movent()
        {
            float vertical = Input.GetAxisRaw("Vertical") * Time.deltaTime * 20;
            transform.Translate(0, vertical,0);
        }

    }
}
