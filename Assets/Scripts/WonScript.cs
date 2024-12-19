using UnityEngine;

public class WinUI : MonoBehaviour
{
    // Drag and drop the GameObjects in the Inspector
    [SerializeField] private GameObject stars; // The stars image
    [SerializeField] private GameObject Win;   // The "You Won" image

    [SerializeField] private GameObject characterImage;

    [SerializeField] private ParticleSystem myParticalSystem;

    void Start()
    {
        // Check if the objects are assigned
        if (stars == null || Win == null)
        {
            Debug.LogError("Assign both Stars and Win GameObjects in the Inspector!");
            return;
        }

        // Ensure the "You Won" image is hidden at the start
        Win.SetActive(false);
        characterImage.SetActive(false);

        // Animate the stars with scale-up, move-up, and scale-down sequence
        LeanTween.scale(stars, new Vector3(11.5f, 11.5f, 11.5f), 2f)
            .setDelay(0.5f)
            .setEase(LeanTweenType.easeOutElastic)
            .setOnComplete(() =>
            {
                // Move stars up
                LeanTween.moveLocalY(stars, stars.transform.localPosition.y + 50f, 0.5f)
                    .setEase(LeanTweenType.easeInOutSine)
                    .setOnComplete(() =>
                    {
                        // Scale stars down
                        LeanTween.scale(stars, new Vector3(9f, 9f, 9f), 0.7f)
                            .setEase(LeanTweenType.easeInOutQuad);
                    });
            });

        // After stars animation, handle the "You Won" image
        LeanTween.delayedCall(3f, ShowWinPanel); // Delay showing "You Won" until stars animation finishes
    }

    void ShowWinPanel()
    {
        // Activate the "You Won" image
        Win.SetActive(true);

        // Scale animation for the "You Won" image
        LeanTween.scale(Win, new Vector3(11.5f, 11.5f, 11.5f), 1.51f)
            .setEase(LeanTweenType.easeOutBack);

        // Move animation for the "You Won" image
        LeanTween.moveLocal(Win, new Vector3(0f, -10f, 0f), 0.7f)
            .setDelay(0.5f)
            .setEase(LeanTweenType.easeInOutCubic);

        characterImage.SetActive(true);
        LeanTween.moveLocal(characterImage,new Vector3(-739f,-93f,0f),0.7f).setDelay(0.5f).setEase(LeanTweenType.easeInOutBounce);
        myParticalSystem.Play();
    }


}
