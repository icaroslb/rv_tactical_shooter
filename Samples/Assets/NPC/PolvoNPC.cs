using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

using RVTS.Behaviours;
using RVTS.Targets.Rules;

namespace RVTS.Targets.NPC
{
    public class PolvoNPC : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            speed_ = 0.1f;
            time_to_shoot_ = 2.5f;
            circle_position_ = 0.0f;
            distance_ = 10.0f;
            is_running_ = false;

            target_ = GameObject.Find("Main Camera").transform;

            Invoke("Shoot", time_to_shoot_);

            EnviromentBehaviour.OnStartGame += OnStart;
            EnviromentBehaviour.OnEndGame += OnStop;
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void OnStart(object sender, EventArgs e)
        {
            is_running_ = true;

            walk_audio_.Play();
        }

        public void OnStop(object sender, EventArgs e)
        {
            is_running_ = false;
            walk_audio_.Stop();
        }



        private void LateUpdate()
        {
            if (is_running_)
            {
                circle_position_ += speed_ * Time.fixedDeltaTime;

                transform.position = new Vector3(MathF.Sin(circle_position_) * distance_, 0.2f, MathF.Cos(circle_position_) * distance_);

                Vector3 look_at = (target_.position - transform.position).normalized;
                Quaternion rotation = Quaternion.LookRotation(look_at, Vector3.up);

                transform.rotation = rotation;
            }
        }

        public void Teleport(float teleport_const)
        {
            circle_position_ += teleport_const;
            speed_ = MathF.Min(speed_ + 0.02f, 1.0f);
            distance_ = Mathf.Min(20.0f, distance_ + 0.5f);
            time_to_shoot_ = Mathf.Max(1.5f, time_to_shoot_ - 0.05f);
        }

        private void Shoot()
        {
            if (is_running_)
            {
                Transform target = target_;
                target.position = new Vector3(target.position.x, target.position.y / 2.0f, target.position.z);


                GameObject bullet = BulletRules.CreateObject(bullet_creation_, target, bullet_);
                shoot_audio_.Play();
            }

            Invoke("Shoot", time_to_shoot_);
        }

        private void OnDestroy()
        {
            EnviromentBehaviour.OnStartGame -= OnStart;
            EnviromentBehaviour.OnEndGame -= OnStop;
        }

        [SerializeField] private float speed_;
        [SerializeField] private float circle_position_;
        [SerializeField] private float distance_;
        [SerializeField] private bool is_running_;

        [SerializeField] private float time_to_shoot_;
        [SerializeField] private Transform bullet_creation_;
        [SerializeField] private Transform target_;
        [SerializeField] private GameObject bullet_;

        [SerializeField] private AudioSource walk_audio_;
        [SerializeField] private AudioSource shoot_audio_;
    }
}