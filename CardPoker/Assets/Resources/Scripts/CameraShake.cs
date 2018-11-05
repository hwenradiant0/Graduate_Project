using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    private void Start()
    {
    }

    public IEnumerator Shake(float duration, float magniruede)
    {
        Vector3 originalPos = transform.localPosition;

        FindObjectOfType<SoundManager>().Play("WrongSound");

        float elapsed = 0.0f;

        while (elapsed < duration)
        {

            float x = Random.Range(-1f, 1f) * magniruede;
            float y = Random.Range(-1f, 1f) * 0;

            transform.localPosition = new Vector3(x, originalPos.y, originalPos.z);

            elapsed = elapsed + Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
