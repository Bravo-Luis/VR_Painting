namespace UnityEngine.XR.Interaction.Toolkit
{
    public class CustomTwoHandedGrabMoveProvider : TwoHandedGrabMoveProvider
    {
        protected override Vector3 ComputeDesiredMove(out bool attemptingMove)
        {
            Vector3 move = base.ComputeDesiredMove(out attemptingMove);

            // freeze movement and scaling while rotating
            if (leftGrabMoveProvider.IsGrabbing() && rightGrabMoveProvider.IsGrabbing())
            {
                move = Vector3.zero;
                enableFreeYMovement = false;
            } else enableFreeYMovement = true;
            return move;
        }
    }
}