public class LastStringReceivedListener : StringEventListener
{
    public string received;

    protected override void HandleEvent(string param)
    {
        base.HandleEvent(param);
        received = param;
    }
}