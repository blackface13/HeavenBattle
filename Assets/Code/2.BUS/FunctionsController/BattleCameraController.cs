using Lean.Touch;
using UnityEngine;

/// <summary>
/// Code điều khiển camera trong battle system
/// </summary>
namespace Assets.Code._2.BUS.FunctionsController
{
    public class BattleCameraController: LeanDragCamera
    {
        /// <summary>
        /// Giới hạn vùng camera
        /// </summary>
        private void RangeLimit()
        {
            if(transform.position.x < 0)
                transform.position = new Vector3(0, 0, transform.position.z);
            else
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }

        public override void LateUpdate()
        {
            base.LateUpdate();
            RangeLimit();
        }
    }
}
