using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using RVTS.Behaviours;

public class Weapons : MonoBehaviour
{
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject muzzle;
    [SerializeField] private LayerMask mask;

    [SerializeField] private bool is_running;

    private AudioSource shootSound_;
    private int munition_;

    private void Awake()
    {
        shootSound_ = muzzle.GetComponent<AudioSource>();
        munition_ = 0;
        is_running = false;

        muzzle.transform.SetParent(gun.transform, true);

        EnviromentBehaviour.OnStartGame += StartWeapon;
        EnviromentBehaviour.OnPauseGame += OnPauseGame;
        EnviromentBehaviour.OnReloadGame += OnReloadGame;
        EnviromentBehaviour.OnEndGame += StopWeapon;
    }

    private void OnDestroy()
    {
        EnviromentBehaviour.OnStartGame -= StartWeapon;
        EnviromentBehaviour.OnPauseGame -= OnPauseGame;
        EnviromentBehaviour.OnReloadGame -= OnReloadGame;
        EnviromentBehaviour.OnEndGame -= StopWeapon;
    }

    public void Shoot(bool register = false)
    {
        if (is_running && munition_ != 0)
        {
            RaycastHit hit;
            Ray ray = new(muzzle.transform.position, muzzle.transform.forward);
            bool hit_something = false;

            GameObject hitted_target_gameobject = null;
            Vector3 hit_position = Vector3.zero;

            shootSound_.Play();

            if (Physics.Raycast(ray, out hit, mask))
            {
                hit_something = true;
                hitted_target_gameobject = hit.collider.gameObject;
                hit_position = hit.point;
            }

            munition_--;

            WeaponBehaviour.OnShoot(this, new WeaponBehaviour.OnShootArgs
            {
                was_hit_ = hit_something,
                object_hitted_ = hitted_target_gameobject,
                layer_mask_ = mask,
                hit_position_ = hit_position,
                munition_ = munition_
            });
        }
    }

    public void Reload(int qtd)
    {
        munition_ = qtd;

        WeaponBehaviour.OnReloadMunition?.Invoke(this, munition_);
    }

    private void StartWeapon(object sender, System.EventArgs e)
    {
        is_running = true;

        WeaponBehaviour.OnInitialize?.Invoke(this, new WeaponBehaviour.WeaponInitializeArgs { munition_ = munition_ });
    }

    private void OnPauseGame(object sender, System.EventArgs e)
    {
        is_running = false;
    }

    private void OnReloadGame(object sender, System.EventArgs e)
    {
        is_running = true;
    }

    private void StopWeapon(object sender, System.EventArgs e)
    {
        is_running = false;
    }
}