// GENERATED AUTOMATICALLY FROM 'Assets/Controls/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    private InputActionAsset asset;
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""PlayerControls"",
            ""id"": ""118ef9be-ced7-489f-b211-fd043470bcf9"",
            ""actions"": [
                {
                    ""name"": ""MoveDir"",
                    ""type"": ""Value"",
                    ""id"": ""bc8b4986-194b-4508-a698-b839ad9fbb59"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveButton"",
                    ""type"": ""Button"",
                    ""id"": ""b0695776-e0b3-42d7-9c73-bd7806412579"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AttackButton"",
                    ""type"": ""Button"",
                    ""id"": ""3de931e3-4932-4423-aa9e-2fd14a26e687"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""608ff79b-6961-414f-bd06-5be76795a4b0"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveDir"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""063683ca-1d2b-4eb2-826d-6d1551ed41b3"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveDir"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""cea9a8a1-896d-4e1d-8c21-8b94d7cb9b20"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveDir"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""f8e49b34-8073-4ee2-bcaa-b41a335bdd9f"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveDir"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""3d74a271-dce9-444c-bb3e-0aab80b39e95"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveDir"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""a46deaa0-6e76-4060-93cd-8ddce78af3d3"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveDir"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""faf6e617-dd59-4a79-93cf-9148027471c6"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e6a45d7a-74f6-4f3a-a615-ba1e4cddfd3c"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d2804db9-d240-4b0d-931a-ac115c91c2d9"",
                    ""path"": ""<Keyboard>/period"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AttackButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b0e5997f-e7c8-48b6-9c16-90bcd37f91ba"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AttackButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerControls
        m_PlayerControls = asset.FindActionMap("PlayerControls", throwIfNotFound: true);
        m_PlayerControls_MoveDir = m_PlayerControls.FindAction("MoveDir", throwIfNotFound: true);
        m_PlayerControls_MoveButton = m_PlayerControls.FindAction("MoveButton", throwIfNotFound: true);
        m_PlayerControls_AttackButton = m_PlayerControls.FindAction("AttackButton", throwIfNotFound: true);
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

    // PlayerControls
    private readonly InputActionMap m_PlayerControls;
    private IPlayerControlsActions m_PlayerControlsActionsCallbackInterface;
    private readonly InputAction m_PlayerControls_MoveDir;
    private readonly InputAction m_PlayerControls_MoveButton;
    private readonly InputAction m_PlayerControls_AttackButton;
    public struct PlayerControlsActions
    {
        private @Controls m_Wrapper;
        public PlayerControlsActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveDir => m_Wrapper.m_PlayerControls_MoveDir;
        public InputAction @MoveButton => m_Wrapper.m_PlayerControls_MoveButton;
        public InputAction @AttackButton => m_Wrapper.m_PlayerControls_AttackButton;
        public InputActionMap Get() { return m_Wrapper.m_PlayerControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerControlsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerControlsActions instance)
        {
            if (m_Wrapper.m_PlayerControlsActionsCallbackInterface != null)
            {
                @MoveDir.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMoveDir;
                @MoveDir.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMoveDir;
                @MoveDir.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMoveDir;
                @MoveButton.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMoveButton;
                @MoveButton.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMoveButton;
                @MoveButton.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMoveButton;
                @AttackButton.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnAttackButton;
                @AttackButton.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnAttackButton;
                @AttackButton.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnAttackButton;
            }
            m_Wrapper.m_PlayerControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveDir.started += instance.OnMoveDir;
                @MoveDir.performed += instance.OnMoveDir;
                @MoveDir.canceled += instance.OnMoveDir;
                @MoveButton.started += instance.OnMoveButton;
                @MoveButton.performed += instance.OnMoveButton;
                @MoveButton.canceled += instance.OnMoveButton;
                @AttackButton.started += instance.OnAttackButton;
                @AttackButton.performed += instance.OnAttackButton;
                @AttackButton.canceled += instance.OnAttackButton;
            }
        }
    }
    public PlayerControlsActions @PlayerControls => new PlayerControlsActions(this);
    public interface IPlayerControlsActions
    {
        void OnMoveDir(InputAction.CallbackContext context);
        void OnMoveButton(InputAction.CallbackContext context);
        void OnAttackButton(InputAction.CallbackContext context);
    }
}
