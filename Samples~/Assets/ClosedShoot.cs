using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

using RVTS.Behaviours;
using RVTS.Targets.Objects;

public class ClosedShoot : MonoBehaviour
{
    void Start()
    {
        is_running_ = false;
        current_phase_ = 0;

        start_button_.action.started += StartGame;
        menu_button_.action.started += ReturnMenu;

        WeaponBehaviour.OnShoot += OnShoot;
        WeaponBehaviour.OnInitialize += OnWeaponInitialize;

        TargetBehaviour.OnStartEvent += OnStartTargetMovement;
        TargetBehaviour.OnEndEvent += OnEndTargetMovement;
    }

    private void OnDestroy()
    {
        start_button_.action.started -= StartGame;
        menu_button_.action.started -= ReturnMenu;

        WeaponBehaviour.OnShoot -= OnShoot;
        WeaponBehaviour.OnInitialize -= OnWeaponInitialize;

        TargetBehaviour.OnStartEvent -= OnStartTargetMovement;
        TargetBehaviour.OnEndEvent -= OnEndTargetMovement;
    }

    public void StartGame(InputAction.CallbackContext reference)
    {
        if (!is_running_ && current_phase_ == 0)
        {
            is_running_ = true;
            current_phase_ = 0;
            score_ = 0.0f;

            score_text_.gameObject.SetActive(false);

            GameObject[] collision_pointers = GameObject.FindGameObjectsWithTag("Collision Pointer");

            foreach (var collision_point in collision_pointers)
            {
                Destroy(collision_point);
            }

            EnviromentBehaviour.OnStartGame?.Invoke(this, null);
        }
    }

    public void ChangePhase()
    {
        current_phase_ = (current_phase_ + 1) % 3;

        if (current_phase_ == 0)
        {
            EndGame();
        }
        else
        {
            EnviromentBehaviour.OnPhaseChange?.Invoke(this, current_phase_);
        }
    }

    public void EndGame()
    {
        is_running_ = false;
        current_phase_++;

        score_text_.gameObject.SetActive(true);

        score_text_.text = $"Score: {score_.ToString("#.00")}";

        EnviromentBehaviour.OnEndGame?.Invoke(this, null);
    }

    private void OnShoot(object sender, WeaponBehaviour.OnShootArgs args)
    {
        if (args.was_hit_ && args.object_hitted_.layer == 9)
        {
            GameObject new_collision_pointer = Instantiate(collision_ponter_prefab_);
            new_collision_pointer.transform.position = args.hit_position_;
            new_collision_pointer.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            new_collision_pointer.transform.parent = args.object_hitted_.transform;

            score_ += args.object_hitted_.GetComponent<PermanentTarget>().HitScore(args.hit_position_);
        }

        if (args.munition_ == 0)
        {
            ChangePhase();
            (sender as Weapons).Reload(10);
        }
    }

    private void OnWeaponInitialize(object sender, WeaponBehaviour.WeaponInitializeArgs args)
    {
        if (args.munition_ != 10)
        {
            (sender as Weapons).Reload(10);
        }
    }

    private void OnStartTargetMovement(object sender, TargetBehaviour.TargetEventArgs args)
    {
        EnviromentBehaviour.OnPauseGame?.Invoke(this, null);
    }

    private void OnEndTargetMovement(object sender, TargetBehaviour.TargetEventArgs args)
    {
        if (is_running_)
        {
            EnviromentBehaviour.OnReloadGame?.Invoke(this, null);
        } else if (current_phase_ != 0)
        {
            current_phase_ = 0;
        }
    }

    protected void ReturnMenu(InputAction.CallbackContext reference)
    {
        SceneManager.LoadScene("Menu");
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1));
    }

    [SerializeField] bool is_running_;
    [SerializeField] int current_phase_;

    [SerializeField] float score_;

    [SerializeField] GameObject collision_ponter_prefab_;

    [SerializeField] private InputActionReference start_button_;
    [SerializeField] private InputActionReference menu_button_;

    [SerializeField] private TextMeshProUGUI score_text_;

}
