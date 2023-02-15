using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public enum Binding
    {
        C4,
        D4,
        E4,
        F4,
        G4,
        A4,
        B4,
        C5,
        C4Sharp,
        D4Sharp,
        F4Sharp,
        G4Sharp,
        A4Sharp,
        C5Sharp
    }
    public static GameInput Instance { get; private set; }

    public PlayerInputActions playerInputActions { get; private set; }

    private void Awake()
    {
        Instance = this;

        playerInputActions = new PlayerInputActions();
    }

    private void OnApplicationQuit()
    {
        StopAllCoroutines();
    }

    public string GetBindingText(int num)
    {
        Binding binding = (Binding)num;
        switch (binding)
        {
            default:
            case Binding.C4:
                return playerInputActions.Instrument.C4.bindings[0].ToDisplayString();
            case Binding.D4:
                return playerInputActions.Instrument.D4.bindings[0].ToDisplayString();
            case Binding.E4:
                return playerInputActions.Instrument.E4.bindings[0].ToDisplayString();
            case Binding.F4:
                return playerInputActions.Instrument.F4.bindings[0].ToDisplayString();
            case Binding.G4:
                return playerInputActions.Instrument.G4.bindings[0].ToDisplayString();
            case Binding.A4:
                return playerInputActions.Instrument.A4.bindings[0].ToDisplayString();
            case Binding.B4:
                return playerInputActions.Instrument.B4.bindings[0].ToDisplayString();
            case Binding.C5:
                return playerInputActions.Instrument.C5.bindings[0].ToDisplayString();
            case Binding.C4Sharp:
                return playerInputActions.Instrument.C4Sharp.bindings[0].ToDisplayString();
            case Binding.D4Sharp:
                return playerInputActions.Instrument.D4Sharp.bindings[0].ToDisplayString();
            case Binding.F4Sharp:
                return playerInputActions.Instrument.F4Sharp.bindings[0].ToDisplayString();
            case Binding.G4Sharp:
                return playerInputActions.Instrument.G4Sharp.bindings[0].ToDisplayString();
            case Binding.A4Sharp:
                return playerInputActions.Instrument.A4Sharp.bindings[0].ToDisplayString();
            case Binding.C5Sharp:
                return playerInputActions.Instrument.C5Sharp.bindings[0].ToDisplayString();
        }
    }

    void RebindingCompress(InputActionRebindingExtensions.RebindingOperation callback, 
        System.Action OnActionRebound)
    {
        callback.Dispose();
        OnActionRebound();
    }

    public void RebindBinding(int num, System.Action OnActionRebound)
    {
        Binding binding = (Binding)num;
        playerInputActions.Disable();

         switch (binding)
        {
            default:
            case Binding.C4:
                playerInputActions.Instrument.C4.PerformInteractiveRebinding(0).
                    OnComplete(callback => RebindingCompress(callback, OnActionRebound)).Start();
                break;
            case Binding.D4:
                 playerInputActions.Instrument.D4.PerformInteractiveRebinding(0).
                    OnComplete(callback => RebindingCompress(callback, OnActionRebound)).Start();
                break;
            case Binding.E4:
                 playerInputActions.Instrument.E4.PerformInteractiveRebinding(0).
                    OnComplete(callback => RebindingCompress(callback, OnActionRebound)).Start();
                break;
            case Binding.F4:
                 playerInputActions.Instrument.F4.PerformInteractiveRebinding(0).
                    OnComplete(callback => RebindingCompress(callback, OnActionRebound)).Start();
                break;
            case Binding.G4:
                 playerInputActions.Instrument.G4.PerformInteractiveRebinding(0).
                    OnComplete(callback => RebindingCompress(callback, OnActionRebound)).Start();
                break;
            case Binding.A4:
                 playerInputActions.Instrument.A4.PerformInteractiveRebinding(0).
                    OnComplete(callback => RebindingCompress(callback, OnActionRebound)).Start();
                break;
            case Binding.B4:
                 playerInputActions.Instrument.B4.PerformInteractiveRebinding(0).
                    OnComplete(callback => RebindingCompress(callback, OnActionRebound)).Start();
                break;
            case Binding.C5:
                 playerInputActions.Instrument.C5.PerformInteractiveRebinding(0).
                    OnComplete(callback => RebindingCompress(callback, OnActionRebound)).Start();
                break;
            case Binding.C4Sharp:
                 playerInputActions.Instrument.C4Sharp.PerformInteractiveRebinding(0).
                    OnComplete(callback => RebindingCompress(callback, OnActionRebound)).Start();
                break;
            case Binding.D4Sharp:
                 playerInputActions.Instrument.D4Sharp.PerformInteractiveRebinding(0).
                    OnComplete(callback => RebindingCompress(callback, OnActionRebound)).Start();
                break;
            case Binding.F4Sharp:
                 playerInputActions.Instrument.F4Sharp.PerformInteractiveRebinding(0).
                    OnComplete(callback => RebindingCompress(callback, OnActionRebound)).Start();
                break;
            case Binding.G4Sharp:
                 playerInputActions.Instrument.G4Sharp.PerformInteractiveRebinding(0).
                    OnComplete(callback => RebindingCompress(callback, OnActionRebound)).Start();
                break;
            case Binding.A4Sharp:
                 playerInputActions.Instrument.A4Sharp.PerformInteractiveRebinding(0).
                    OnComplete(callback => RebindingCompress(callback, OnActionRebound)).Start();
                break;
            case Binding.C5Sharp:
                 playerInputActions.Instrument.C5Sharp.PerformInteractiveRebinding(0).
                    OnComplete(callback => RebindingCompress(callback, OnActionRebound)).Start();
                break;
        }
    }
}
