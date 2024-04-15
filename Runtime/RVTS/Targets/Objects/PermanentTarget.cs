using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace RVTS.Targets.Objects
{

    public class PermanentTarget : Target
    {
        [SerializeField] private Transform collisionSet;
        [SerializeField] private GameObject hitPrefab;

        private Vector3 meshSize;

        // public EventHandler<HittedScoreArgs> hittedScore;

        private void Start()
        {
            meshSize = GetComponent<MeshRenderer>().bounds.size / 2.0f;
            // db = DBConnection.GetInstance();
        }

        public override void Hitted(Vector3 hitPosition)
        {
            GameObject point = Instantiate(hitPrefab, hitPosition, Quaternion.identity);
            point.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            point.transform.SetParent(collisionSet.transform, true);

            PlayHitsound();
            HitScore(hitPosition);
        }

        public float HitScore(Vector3 hitPosition)
        {
            Vector3 hitVector = hitPosition - collisionSet.position;

            float axisI = Vector3.Dot(transform.right, hitVector) / meshSize.x;
            float axisJ = Vector3.Dot(transform.up, hitVector) / meshSize.y;
            float d = Mathf.Sqrt((axisI * axisI) + (axisJ * axisJ));

            float score = 5.0f * (1.0f - Mathf.Min(d, 1.0f));

            // NetworkData network_data = new NetworkData
            // {
            //     x = axisI,
            //     y = axisJ,
            //     time = DateTime.Now.ToString()
            // };

            // DataShot network_data = new DataShot
            // {
            //     id = 1,
            //     x = axisI,
            //     y = axisJ,
            //     time = DateTime.Now.ToString()
            // };

            //db.AddShot(1, axisI, axisJ);
            // OnHittedScore(new HittedScoreArgs(score));
            // string teste = JsonConvert.SerializeObject(network_data);
            // Debug.Log(teste);
            // Client.Send(teste);

            return score;
        }

        private void OnHittedScore(HittedScoreArgs e)
        {
            // EventHandler<HittedScoreArgs> handler = hittedScore;
            // if (handler != null)
            // {
            //     handler(this, e);
            // }
        }
    }

    public class HittedScoreArgs : EventArgs
    {
        public float score { get; private set; }

        public HittedScoreArgs(float newScore)
        {
            score = newScore;
        }
    }
}