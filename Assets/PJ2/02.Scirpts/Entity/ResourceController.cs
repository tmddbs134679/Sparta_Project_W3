using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = 0.5f;

    private BaseController basecontroller;
    private StatHandler stathandler;
    private AnimationHandler animationHandler;

    private float timeSinceLastChange = float.MaxValue;

    public float CurrentHealth { get; private set; }
    public float MaxHealth => stathandler.Health;

    public AudioClip damageClip;

    public Action<float, float> OnChangeHealth;
    private void Awake()
    {
        basecontroller = GetComponent<BaseController>();
        stathandler = GetComponent<StatHandler>();
        animationHandler = GetComponent<AnimationHandler>();
    }

    private void Start()
    {
        {
            CurrentHealth = stathandler.Health;
        }
    }

    private void Update()
    {
        if(timeSinceLastChange < healthChangeDelay)
        {
            timeSinceLastChange += Time.deltaTime;

            if(timeSinceLastChange >= healthChangeDelay)
            {
                animationHandler.InvincibilityEnd();
            }
        }
    }

    public bool ChangeHealth(float change)
    {
        if( change == 0 || timeSinceLastChange < healthChangeDelay )
        {
            return false;
        }

        timeSinceLastChange = 0;
        CurrentHealth += change;
        CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
        CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;

        OnChangeHealth?.Invoke(CurrentHealth, MaxHealth);

        if (change < 0)
        {
            animationHandler.Damage();

            if(damageClip != null)
                SoundManager.PlayClip(damageClip);
        }

        if(CurrentHealth <= 0f)
        {
            Death();
        }

        return true;
    }

    private void Death()
    {
        basecontroller.Death();
    }

    public void AddHealthChangeEvent(Action<float, float> action)
    {
        OnChangeHealth += action;
    }

    public void RemoveHealthChangeEvent(Action<float, float> action)
    {
        OnChangeHealth -= action;
    }
}
