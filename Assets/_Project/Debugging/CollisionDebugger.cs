using UnityEngine;

namespace FlappyBird.Debugging
{
    /// <summary>
    /// ============================================
    /// COLLISION DEBUGGER
    /// ============================================
    /// Ayuda a diagnosticar problemas de colisión
    /// Muestra en consola TODAS las colisiones/triggers
    /// 
    /// USO: Adjuntar al Bird GameObject temporalmente
    /// ============================================
    /// </summary>
    public class CollisionDebugger : MonoBehaviour
    {
        [Header("Debug Settings")]
        [SerializeField] private bool logTriggers = true;
        [SerializeField] private bool logCollisions = true;
        [SerializeField] private bool showGizmos = true;

        private void OnTriggerEnter2D(Collider2D collision)
        {
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
        }

        private void OnDrawGizmos()
        {
            if (!showGizmos) return;

            // Draw all colliders
            var colliders = GetComponents<Collider2D>();
            foreach (var col in colliders)
            {
                Gizmos.color = col.isTrigger ? new Color(0, 1, 0, 0.3f) : new Color(1, 0, 0, 0.3f);
                
                if (col is BoxCollider2D box)
                {
                    Gizmos.DrawWireCube(transform.position + (Vector3)box.offset, box.size);
                }
                else if (col is CircleCollider2D circle)
                {
                    Gizmos.DrawWireSphere(transform.position + (Vector3)circle.offset, circle.radius);
                }
            }
        }
    }
}
