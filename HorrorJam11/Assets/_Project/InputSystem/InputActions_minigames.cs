//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/_Project/InputSystem/InputActions_minigames.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @InputActions_minigames : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputActions_minigames()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActions_minigames"",
    ""maps"": [
        {
            ""name"": ""Minigame_circle"",
            ""id"": ""f733c464-71b4-4ce9-a76e-980f55acc9bd"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""f35633c2-2341-49ae-91c8-35aa2ba84ff4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""774968fe-b26e-4491-bfa8-501128ecdc46"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""7d0eb691-3939-42f2-96f9-2310cc8718f5"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""7211b08b-0c52-4899-8c92-a8d9a2eef7bd"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Minigame_circle
        m_Minigame_circle = asset.FindActionMap("Minigame_circle", throwIfNotFound: true);
        m_Minigame_circle_Move = m_Minigame_circle.FindAction("Move", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Minigame_circle
    private readonly InputActionMap m_Minigame_circle;
    private IMinigame_circleActions m_Minigame_circleActionsCallbackInterface;
    private readonly InputAction m_Minigame_circle_Move;
    public struct Minigame_circleActions
    {
        private @InputActions_minigames m_Wrapper;
        public Minigame_circleActions(@InputActions_minigames wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Minigame_circle_Move;
        public InputActionMap Get() { return m_Wrapper.m_Minigame_circle; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Minigame_circleActions set) { return set.Get(); }
        public void SetCallbacks(IMinigame_circleActions instance)
        {
            if (m_Wrapper.m_Minigame_circleActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_Minigame_circleActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_Minigame_circleActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_Minigame_circleActionsCallbackInterface.OnMove;
            }
            m_Wrapper.m_Minigame_circleActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
            }
        }
    }
    public Minigame_circleActions @Minigame_circle => new Minigame_circleActions(this);
    public interface IMinigame_circleActions
    {
        void OnMove(InputAction.CallbackContext context);
    }
}