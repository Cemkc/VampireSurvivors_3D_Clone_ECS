using UnityEngine.InputSystem;

public static class InputActionExtensions
{
    public static void SubscribeAll(this InputAction action, 
        System.Action<InputAction.CallbackContext> callback)
    {
        action.started   += callback;
        action.performed += callback;
        action.canceled  += callback;
    }

    public static void UnsubscribeAll(this InputAction action, 
        System.Action<InputAction.CallbackContext> callback)
    {
        action.started   -= callback;
        action.performed -= callback;
        action.canceled  -= callback;
    }
}
