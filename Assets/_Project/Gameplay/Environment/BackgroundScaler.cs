using UnityEngine;

namespace FlappyBird.Gameplay.Environment
{
    /// <summary>
    /// Escala el background para que cubra toda la cámara
    /// </summary>
    public class BackgroundScaler : MonoBehaviour
    {
        private void Start()
        {
            ScaleToFitCamera();
        }

        private void ScaleToFitCamera()
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (sr == null) return;

            // Obtener tamaño de la cámara
            float cameraHeight = Camera.main.orthographicSize * 2f;
            float cameraWidth = cameraHeight * Camera.main.aspect;

            // Obtener tamaño del sprite
            float spriteWidth = sr.sprite.bounds.size.x;
            float spriteHeight = sr.sprite.bounds.size.y;

            // Calcular escala necesaria
            float scaleX = cameraWidth / spriteWidth;
            float scaleY = cameraHeight / spriteHeight;

            // Usar la escala mayor para cubrir toda la pantalla
            float scale = Mathf.Max(scaleX, scaleY);

            transform.localScale = new Vector3(scale, scale, 1f);
        }
    }
}
