using UnityEngine;

namespace Ru1t3rl.Tools
{
    [ExecuteAlways]
    public class SineTool : MonoBehaviour
    {
        [SerializeField] bool pause = false;

        [Header("Wave Settings")]
        [SerializeField] bool useRandomStartOffset = true;
        [SerializeField] float freqeunce = 1;
        [SerializeField] float amplitude = 1;
        [SerializeField] float centre = 0;

        [Header("Transformation Settings")]

        [SerializeField] SineTransform sineTransformAction = SineTransform.Scale;

        [SerializeField] bool fixedX = false, fixedY = false, fixedZ = false;

        Vector3 startPos, localStartPos, startScale;
        Vector3 scale, pos;

        Vector3 localStartRot, rot;

        float angle;
        float angleIncrement = 0.01f;

        void Awake()
        {
            startPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            localStartPos = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
            startScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
            localStartRot = transform.localRotation.eulerAngles;

            if (useRandomStartOffset)
            {
                angle = Random.Range(0f, 1f);
            }
        }

        void FixedUpdate()
        {
            if (pause)
                return;

            angle += angleIncrement;

            if (sineTransformAction == SineTransform.Scale)
            {
                scale.x = startScale.x + ((!fixedX) ? (amplitude * Mathf.Sin((2 * Mathf.PI / freqeunce) * angle) + centre) : centre);
                scale.y = startScale.y + ((!fixedY) ? (amplitude * Mathf.Cos((2 * Mathf.PI / freqeunce) * angle) + centre) : centre);
                scale.z = startScale.z + ((!fixedZ) ? (amplitude * Mathf.Cos((2 * Mathf.PI / freqeunce) * angle) + centre) : centre);

                transform.localScale = scale;
            }

            if (sineTransformAction == SineTransform.Position)
            {
                pos.x = localStartPos.x + ((!fixedX) ? (amplitude * Mathf.Sin((2 * Mathf.PI / freqeunce) * angle) + centre) : centre);
                pos.y = localStartPos.y + ((!fixedY) ? (amplitude * Mathf.Cos((2 * Mathf.PI / freqeunce) * angle) + centre) : centre);
                pos.z = localStartPos.z + ((!fixedZ) ? (amplitude * Mathf.Cos((2 * Mathf.PI / freqeunce) * angle) + centre) : centre);

                transform.localPosition = pos;
            }
        }

        enum SineTransform
        {
            Scale,
            Position
        }
    }
}