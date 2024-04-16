using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RVTS.Behaviours;

namespace RVTS.Player
{
    public class PlayerCollision : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            PlayerBehaviour.OnCollision(this, new PlayerBehaviour.OnCollisionArgs
            {
                object_collided_ = collision.gameObject,
                object_type_ = collision.GetType()
            });
        }
    }
}