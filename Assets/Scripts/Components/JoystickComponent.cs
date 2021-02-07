using UnityEngine;
using UnityEngine.EventSystems;

namespace Snake3D.Components
{
    public class JoystickComponent : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        #region --------------------------------- Private Fields ---------------------------------------

        private Transform steeringWheelTransform;
        private bool isSteering = false;
        private float steeringResetSpeed = 10;
        private float steeringAngle;

        #endregion -------------------------------------------------------------------------------------

        #region --------------------------------- Private Fields ---------------------------------------

        public float SteeringAngle
        {
            get { return steeringAngle; }
        }

        #endregion -------------------------------------------------------------------------------------

        #region --------------------------------- Private Methods ---------------------------------------

        private void Start()
        {
            steeringWheelTransform = transform.parent;
        }

        private void Update()
        {
            if (!isSteering)
            {
                steeringWheelTransform.rotation = Quaternion.Slerp(steeringWheelTransform.transform.rotation,
                                                                    Quaternion.identity,
                                                                    steeringResetSpeed * Time.deltaTime);
            }
        }

        #endregion -------------------------------------------------------------------------------------


        #region --------------------------------- Interface Methods ---------------------------------------

        public void OnDrag(PointerEventData eventData)
        {
            Vector3 touchPos = eventData.position;
            Vector3 direction = touchPos - steeringWheelTransform.position;
            direction.Normalize();
            steeringAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            steeringWheelTransform.rotation = Quaternion.Euler(0, 0, -steeringAngle);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            isSteering = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isSteering = false;
        }

        #endregion -------------------------------------------------------------------------------------
    }
}
