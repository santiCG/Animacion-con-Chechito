using System.Collections; // Asegúrate de tener este using
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image progressImage;
    [SerializeField] private float defaultSpeed = 1f;
    [SerializeField] private Gradient colorGradient;
    [SerializeField] private UnityEvent<float> onProgress;
    [SerializeField] private UnityEvent onComplete;

    private Coroutine animationCoroutine;
    private float currentTarget;

    private void Start()
    {
        if (progressImage.type != Image.Type.Filled)
        {
            Debug.LogError($"{name}: Progress Image type must be set to Filled", this);
            enabled = false;
        }
    }

    public void SetProgress(float progress)
    {
        SetProgress(progress, defaultSpeed);
    }

    public void SetProgress(float progress, float speed)
    {
        progress = Mathf.Clamp01(progress);
        currentTarget = progress;

        if (Mathf.Approximately(progressImage.fillAmount, progress))
        {
            UpdateVisuals(progress);
            return;
        }

        if (animationCoroutine != null)
        {
            /*if ((progress > progressImage.fillAmount && currentTarget > progressImage.fillAmount) ||
                (progress < progressImage.fillAmount && currentTarget < progressImage.fillAmount))
            {
                return;
            }*/
            StopCoroutine(animationCoroutine);
        }

        animationCoroutine = StartCoroutine(AnimateProgress(progress, speed));
    }

    // Corregido: IEnumerator sin parámetros de tipo
    private IEnumerator AnimateProgress(float progress, float speed)
    {
        float time = 0;
        float initialProgress = progressImage.fillAmount;
        float duration = Mathf.Abs(progress - initialProgress) / speed;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            float currentProgress = Mathf.Lerp(initialProgress, progress, t);

            if (!Mathf.Approximately(currentTarget, progress))
            {
                initialProgress = progressImage.fillAmount;
                progress = currentTarget;
                duration = Mathf.Abs(progress - initialProgress) / speed;
                time = 0;
                continue;
            }

            UpdateVisuals(currentProgress);
            yield return null;
        }

        UpdateVisuals(progress);
        onComplete?.Invoke();
    }

    private void UpdateVisuals(float progress)
    {
        progressImage.fillAmount = progress;
        progressImage.color = colorGradient.Evaluate(1 - progress);
        onProgress?.Invoke(progress);
    }
}