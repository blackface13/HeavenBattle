using Lean.Touch;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

/// <summary>
/// Code điều khiển camera trong battle system
/// </summary>
namespace Assets.Code._2.BUS.FunctionsController
{
    public class BattleCameraController : LeanDragCamera
    {
        [TabGroup("Cài đặt thông số")]
        public float PosXMinMap, PosXMaxMap, ZMax, ZMin;

        //Các thông số dưới đây fix cứng, nếu thay đổi độ dài, hoặc góc camera thì cần phải tính toán lại
        private float LimitY, LimitX, MaxY = 6, MaxXPlux = 12;

        private void Start()
        {
        }

        /// <summary>
        /// Tính giới hạn X, Y
        /// </summary>
        private void CalculatorLimitXY()
        {
            LimitY = (Math.Abs(ZMin) - Math.Abs(transform.position.z)) * 10 * MaxY / 100;
            LimitX = (Math.Abs(ZMin) - Math.Abs(transform.position.z)) * 10 * MaxXPlux / 100;
        }

        /// <summary>
        /// Giới hạn vùng camera
        /// </summary>
        private void RangeLimit()
        {
            CalculatorLimitXY();
            //Giới hạn X
            if (transform.position.x  < PosXMinMap - LimitX)
                transform.position = new Vector3(PosXMinMap - LimitX, transform.position.y, transform.position.z);

            if (transform.position.x  > PosXMaxMap + LimitX)
                transform.position = new Vector3(PosXMaxMap + LimitX, transform.position.y, transform.position.z);

            //Giới hạn Y
            if(transform.position.y > LimitY)
                transform.position = new Vector3(transform.position.x, LimitY, transform.position.z);
            if (transform.position.y < 0-LimitY)
                transform.position = new Vector3(transform.position.x, 0-LimitY, transform.position.z);

            //Giới hạn Z
            if (transform.position.z > ZMax)
                transform.position = new Vector3(transform.position.x, transform.position.y, ZMax);
            if (transform.position.z < ZMin)
                transform.position = new Vector3(transform.position.x, transform.position.y, ZMin);

            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }

        public override void LateUpdate()
        {
            base.LateUpdate();
            RangeLimit();
        }
    }
}
