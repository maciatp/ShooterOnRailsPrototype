// GENERATED AUTOMATICALLY FROM 'Assets/Input/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""812776cb-2c6d-4b3f-bb5f-8045e725be82"",
            ""actions"": [
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""31cd1ccd-b466-4b6d-ba99-fc402fd022de"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""FireBomb"",
                    ""type"": ""Button"",
                    ""id"": ""8ef4ef2e-db0a-4099-a0b6-466bd960ccac"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""4e19b352-878d-42d2-9b4c-f75b50642d2a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TiltLeft"",
                    ""type"": ""Button"",
                    ""id"": ""587d8c48-b113-48a0-86f2-7e178b836780"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TiltRight"",
                    ""type"": ""Button"",
                    ""id"": ""e6914587-b77d-49ee-9221-a92856e68d44"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Boost"",
                    ""type"": ""Button"",
                    ""id"": ""86ed636b-b38d-45c3-a069-2bfc3e6bb65a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Brake"",
                    ""type"": ""Button"",
                    ""id"": ""d2be67c2-8682-4e23-84e2-3e36a242adcb"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TimeControl"",
                    ""type"": ""Button"",
                    ""id"": ""d1f0bf45-a525-4fb2-96fd-22b65c2b7347"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""adf72d92-e1c3-4e41-ada8-b47eda89b161"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TankMode"",
                    ""type"": ""Button"",
                    ""id"": ""d77e7939-0ea2-417c-8ec0-b3e18f32fb43"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a12e26f2-4a2f-4c1e-bdf1-2add10b78dff"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""24750087-c458-4165-8b2b-024b11e723c9"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""674dd956-d6b2-4f11-96a0-7993ac10b434"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2b5af621-928a-41bf-a597-29e66eb415d7"",
                    ""path"": ""<Joystick>/trigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""JoyStick"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""242aa65b-0d09-4cd8-a815-a9c47546aefd"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""FireBomb"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fddd3d08-68ad-47fd-8dda-bacba81452fb"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""FireBomb"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c50bbb03-b106-42f7-9da8-2b788390ec4d"",
                    ""path"": ""<Keyboard>/leftAlt"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""FireBomb"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""edde880d-b569-4207-844c-fc98e7bd7daf"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""67848e33-a7cb-4175-9434-8cc958820636"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""15baba5f-84bd-4fe4-9202-7ad5e2de70e3"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""9450476a-0c60-4a2b-af53-6dc7baa45a55"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""5ca5e728-2595-4025-9133-c003d7be5991"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""3dffb8b5-a4c5-4507-8a2d-e60304a8195a"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""ARROWS"",
                    ""id"": ""76d81f60-3eb0-40d6-9c6d-79ec646c40b8"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""5aff0a6d-1e8e-47f5-9d73-a954e5f73d4b"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""92faf405-f93c-490e-a2e0-7de5e58caf92"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""200ae712-6b9e-494e-9abd-3838963aa42f"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ad61742b-295c-448a-a588-568b04e48696"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""a0b843e1-12a0-4336-b709-929247a552e0"",
                    ""path"": ""<Joystick>/stick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""JoyStick"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""80057e60-a182-4224-905e-faebd301104f"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""TiltLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""35a34f60-b500-45ac-972f-21db02f2ff78"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""TiltLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5839a990-35f6-4baf-96cd-6b73444643d4"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""TiltRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5428ee24-4e13-427a-8814-6018f76348b6"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""TiltRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""29dc4fc2-1b10-47ef-818c-343984c475d3"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Boost"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""356b0bed-75d1-40cc-9467-e89ddea6d654"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Boost"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""04668448-853f-4dde-859d-cf22288929df"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Brake"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""97552174-e781-4dcf-852f-b15b091478a5"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Brake"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9c0d92cf-4cef-4706-87c4-43b5d7258e07"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""TimeControl"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d43d8827-085f-4943-8e78-64a43d6cebfd"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""TimeControl"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bbe9460a-489d-4c76-80e1-6a6af287f9c6"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fb693a48-dc2b-4042-875c-8d8e06d72058"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b86168eb-882b-4ee7-849e-ff32ca53acc6"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""TankMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Button With One Modifier"",
                    ""id"": ""11798052-1b31-414a-ad27-5c10768291c7"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TankMode"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""a45291d6-db79-4525-8d56-a8ceb1042b17"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""TankMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""c865a0dd-9227-4269-a410-53362b980cf4"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""TankMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""KeyboardMouse"",
            ""bindingGroup"": ""KeyboardMouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""JoyStick"",
            ""bindingGroup"": ""JoyStick"",
            ""devices"": [
                {
                    ""devicePath"": ""<Joystick>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Fire = m_Gameplay.FindAction("Fire", throwIfNotFound: true);
        m_Gameplay_FireBomb = m_Gameplay.FindAction("FireBomb", throwIfNotFound: true);
        m_Gameplay_Move = m_Gameplay.FindAction("Move", throwIfNotFound: true);
        m_Gameplay_TiltLeft = m_Gameplay.FindAction("TiltLeft", throwIfNotFound: true);
        m_Gameplay_TiltRight = m_Gameplay.FindAction("TiltRight", throwIfNotFound: true);
        m_Gameplay_Boost = m_Gameplay.FindAction("Boost", throwIfNotFound: true);
        m_Gameplay_Brake = m_Gameplay.FindAction("Brake", throwIfNotFound: true);
        m_Gameplay_TimeControl = m_Gameplay.FindAction("TimeControl", throwIfNotFound: true);
        m_Gameplay_Pause = m_Gameplay.FindAction("Pause", throwIfNotFound: true);
        m_Gameplay_TankMode = m_Gameplay.FindAction("TankMode", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_Fire;
    private readonly InputAction m_Gameplay_FireBomb;
    private readonly InputAction m_Gameplay_Move;
    private readonly InputAction m_Gameplay_TiltLeft;
    private readonly InputAction m_Gameplay_TiltRight;
    private readonly InputAction m_Gameplay_Boost;
    private readonly InputAction m_Gameplay_Brake;
    private readonly InputAction m_Gameplay_TimeControl;
    private readonly InputAction m_Gameplay_Pause;
    private readonly InputAction m_Gameplay_TankMode;
    public struct GameplayActions
    {
        private @PlayerControls m_Wrapper;
        public GameplayActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Fire => m_Wrapper.m_Gameplay_Fire;
        public InputAction @FireBomb => m_Wrapper.m_Gameplay_FireBomb;
        public InputAction @Move => m_Wrapper.m_Gameplay_Move;
        public InputAction @TiltLeft => m_Wrapper.m_Gameplay_TiltLeft;
        public InputAction @TiltRight => m_Wrapper.m_Gameplay_TiltRight;
        public InputAction @Boost => m_Wrapper.m_Gameplay_Boost;
        public InputAction @Brake => m_Wrapper.m_Gameplay_Brake;
        public InputAction @TimeControl => m_Wrapper.m_Gameplay_TimeControl;
        public InputAction @Pause => m_Wrapper.m_Gameplay_Pause;
        public InputAction @TankMode => m_Wrapper.m_Gameplay_TankMode;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Fire.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnFire;
                @FireBomb.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnFireBomb;
                @FireBomb.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnFireBomb;
                @FireBomb.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnFireBomb;
                @Move.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @TiltLeft.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTiltLeft;
                @TiltLeft.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTiltLeft;
                @TiltLeft.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTiltLeft;
                @TiltRight.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTiltRight;
                @TiltRight.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTiltRight;
                @TiltRight.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTiltRight;
                @Boost.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBoost;
                @Boost.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBoost;
                @Boost.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBoost;
                @Brake.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBrake;
                @Brake.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBrake;
                @Brake.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBrake;
                @TimeControl.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTimeControl;
                @TimeControl.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTimeControl;
                @TimeControl.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTimeControl;
                @Pause.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPause;
                @TankMode.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTankMode;
                @TankMode.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTankMode;
                @TankMode.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTankMode;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
                @FireBomb.started += instance.OnFireBomb;
                @FireBomb.performed += instance.OnFireBomb;
                @FireBomb.canceled += instance.OnFireBomb;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @TiltLeft.started += instance.OnTiltLeft;
                @TiltLeft.performed += instance.OnTiltLeft;
                @TiltLeft.canceled += instance.OnTiltLeft;
                @TiltRight.started += instance.OnTiltRight;
                @TiltRight.performed += instance.OnTiltRight;
                @TiltRight.canceled += instance.OnTiltRight;
                @Boost.started += instance.OnBoost;
                @Boost.performed += instance.OnBoost;
                @Boost.canceled += instance.OnBoost;
                @Brake.started += instance.OnBrake;
                @Brake.performed += instance.OnBrake;
                @Brake.canceled += instance.OnBrake;
                @TimeControl.started += instance.OnTimeControl;
                @TimeControl.performed += instance.OnTimeControl;
                @TimeControl.canceled += instance.OnTimeControl;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @TankMode.started += instance.OnTankMode;
                @TankMode.performed += instance.OnTankMode;
                @TankMode.canceled += instance.OnTankMode;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("KeyboardMouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    private int m_JoyStickSchemeIndex = -1;
    public InputControlScheme JoyStickScheme
    {
        get
        {
            if (m_JoyStickSchemeIndex == -1) m_JoyStickSchemeIndex = asset.FindControlSchemeIndex("JoyStick");
            return asset.controlSchemes[m_JoyStickSchemeIndex];
        }
    }
    public interface IGameplayActions
    {
        void OnFire(InputAction.CallbackContext context);
        void OnFireBomb(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnTiltLeft(InputAction.CallbackContext context);
        void OnTiltRight(InputAction.CallbackContext context);
        void OnBoost(InputAction.CallbackContext context);
        void OnBrake(InputAction.CallbackContext context);
        void OnTimeControl(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnTankMode(InputAction.CallbackContext context);
    }
}
