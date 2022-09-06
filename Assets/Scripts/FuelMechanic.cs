using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FuelMechanic : MonoBehaviour
{
    [SerializeField] float fuelAmount = 100f;
    [SerializeField] float fuelGain = 20f;
    [SerializeField] float fuelReductionSpeed = 1f;
    [SerializeField] AudioClip ranOutOfFuel;

    short i = 0;
    public TMP_Text fuelAmountText;
    Movement movement;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        movement = GetComponent<Movement>();
    }

    void Update()
    {
        if (fuelAmount <= Mathf.Epsilon && i != 1)
        {
            RanOutOfFuelSequense();
        }
        else if (fuelAmount > Mathf.Epsilon)
        {
            ContinuePlayingSequense();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fuel")
        {
            AddFuel();
        }
    }

    private void ContinuePlayingSequense()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            FuelReductionByTrusting();
        }

        if (Input.GetKey(KeyCode.A))
        {
            FuelReductionByRotation();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            FuelReductionByRotation();
        }

        ShowFuelAmount();
    }

    private void ShowFuelAmount()
    {
        fuelAmountText.text = "Fuel: " + string.Format("{0:0}", fuelAmount);
        fuelAmountText.color = new Color(1f + fuelAmount / 100 * -1, 0f + fuelAmount / 100f, 0f);
    }

    private void FuelReductionByTrusting()
    {
        fuelAmount -= fuelReductionSpeed * Time.deltaTime;
    }

    private void FuelReductionByRotation()
    {
        fuelAmount -= (fuelReductionSpeed / 2) * Time.deltaTime;
    }

    private void RanOutOfFuelSequense()
    {
        movement.StopRotation();
        movement.StopTrusting();

        audioSource.PlayOneShot(ranOutOfFuel);

        GetComponent<Movement>().enabled = false;

        ++i;
    }

    void AddFuel()
    {
        fuelAmount += fuelGain;
    }
}
