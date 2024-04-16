using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RVTS.Targets.Objects
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField]
        private float speed_;

        private void Start()
        {
            // EnviromentBehaviour.OnEndGame += OnEndGame;
            Destroy(gameObject, 5.0f);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            // EnviromentBehaviour.OnEndGame -= OnEndGame;
        }

        private void OnEndGame(object sender, EventArgs e)
        {
            Destroy(gameObject);
        }

        public float speed { get { return speed_; } }
    }
}
