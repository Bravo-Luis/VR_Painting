public class DestroyGameObjectListener : VoidEventListener
{
    protected override void HandleEvent()
    {
        base.HandleEvent();
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        eventChannel.numListeners = 0;  
    }
}
