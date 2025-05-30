using UnityEngine;

namespace Controller
{
    public class ThirdPersonCamera : PlayerCamera
    {
        [SerializeField, Range(0f, 2f)]
        private float m_Offset = 1.5f;
        [SerializeField, Range(0f, 360f)]
        private float m_CameraSpeed = 90f;

        private Vector3 m_LookPoint;
        private Vector3 m_TargetPos;

        private bool m_Initialized = false; // щоб стартове положення застосовувалось лише один раз

        private void LateUpdate()
        {
            // Якщо не було введення — зберігаємо стартову позицію з редактора
            if (!m_Initialized && m_Player != null)
            {
                Vector3 dir = m_Transform.position - m_Player.position;
                m_Distance = dir.magnitude;

                // Кут огляду розраховується з позиції камери на гравця
                Vector3 lookDir = (m_Player.position + m_Offset * Vector3.up) - m_Transform.position;
                Quaternion rot = Quaternion.LookRotation(lookDir);
                m_Angles = new Vector2(rot.eulerAngles.x, rot.eulerAngles.y);

                m_Initialized = true;
            }

            Move(Time.deltaTime);
        }

        public override void SetInput(in Vector2 delta, float scroll)
        {
            // Тільки якщо камера вже ініціалізована — дозволяємо введення
            if (!m_Initialized) return;
            
            base.SetInput(delta, scroll);

            var dir = new Vector3(0, 0, -m_Distance);
            var rot = Quaternion.Euler(m_Angles.x, m_Angles.y, 0f);

            var playerPos = (m_Player == null) ? Vector3.zero : m_Player.position;
            m_LookPoint = playerPos + m_Offset * Vector3.up;
            m_TargetPos = m_LookPoint + rot * dir;
        }

        private void Move(float deltaTime)
        {
            camera();
            target();

            void camera()
            {
                var direction = m_TargetPos - m_Transform.position;
                var delta = m_CameraSpeed * deltaTime;

                if(delta * delta > direction.sqrMagnitude)
                {
                    m_Transform.position = m_TargetPos;
                }
                else
                {
                    m_Transform.position += delta * direction.normalized;
                }

                m_Transform.LookAt(m_LookPoint);
            }

            void target()
            {
                if(m_Target == null)
                {
                    return;
                }

                m_Target.position = m_Transform.position + m_Transform.forward * TargetDistance;
            }
        }
    }
}