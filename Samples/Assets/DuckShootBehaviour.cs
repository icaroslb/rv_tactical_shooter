using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using RVTS.Behaviours;
using RVTS.Targets.NPC;

namespace DuckShootBehaviour
{
    public class DuckShootBehaviour : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Start()
        {
            is_running_ = false;


            WeaponBehaviour.OnInitialize += OnWeaponInitialize;
            WeaponBehaviour.OnShoot += OnShoot;
            PlayerBehaviour.OnCollision += OnPlayerCollision;

            start_button_.action.started += StartGame;
            // menu_button_.action.started += ReturnMenu;
        }

        private void OnDestroy()
        {
            WeaponBehaviour.OnInitialize -= OnWeaponInitialize;
            WeaponBehaviour.OnShoot -= OnShoot;
            PlayerBehaviour.OnCollision -= OnPlayerCollision;

            start_button_.action.started -= StartGame;
            // menu_button_.action.started -= ReturnMenu;
        }

        public void StartGame(InputAction.CallbackContext reference)
        {
            if (!is_running_)
            {
                is_running_ = true;
                AddLife(3);
                player_points_ = 0;

                EnviromentBehaviour.OnStartGame(this, null);
            }
        }

        public void EndGame()
        {
            if (is_running_)
            {
                is_running_ = false;
                EnviromentBehaviour.OnEndGame(this, null);
            }
        }

        private void AddPoints(int points)
        {
            player_points_ += points;
            points_text.text = $"Points: {player_points_}";
        }

        private void AddLife(int life)
        {
            player_life_ += life;
            life_text.text = $"Life: {player_life_}";
        }

        private void OnWeaponInitialize(object sender, WeaponBehaviour.WeaponInitializeArgs args)
        {
            (sender as Weapons).Reload(-1);
        }

        private void OnShoot(object sender, WeaponBehaviour.OnShootArgs args)
        {
            if (args.was_hit_)
            {
                switch (args.object_hitted_.layer)
                {
                    case 9:
                        Debug.Log("DuckTarget");
                        AddPoints(10);
                        args.object_hitted_.GetComponent<PolvoNPC>().Teleport(MathF.PI);
                        break;
                    case 6:
                        Debug.Log("Bullet");
                        Destroy(args.object_hitted_);
                        break;
                }
            }

            (sender as Weapons).Reload(-1);
        }

        private void OnPlayerCollision(object sender, PlayerBehaviour.OnCollisionArgs args)
        {
            AddLife(-1);
            life_text.text = $"Life: {player_life_}";

            if (player_life_ <= 0)
            {
                EndGame();
            }
        }

        protected void ReturnMenu(InputAction.CallbackContext reference)
        {
            SceneManager.LoadScene("Menu");
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1));
        }

        [SerializeField] private bool is_running_;
        [SerializeField] private int player_life_;
        [SerializeField] private int player_points_;

        [SerializeField] private Transform player_position_;

        [SerializeField] private TextMeshProUGUI points_text;
        [SerializeField] private TextMeshProUGUI life_text;

        [SerializeField] private InputActionReference start_button_;
        [SerializeField] private InputActionReference menu_button_;
    }
}