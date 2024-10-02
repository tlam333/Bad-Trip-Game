using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bong : ItemTest {

    [SerializeField] float jumpBoostDuration = 5f;
    // [SerializeField] PlayerMovementAdvanced playerMovementAdvanced;

    private bool isUsingBong = false;


    public override void Use(IntoxicationManager intoxicationManagerRef)
    {
        if (!isUsingBong) {
            isUsingBong = true;

            intoxicationManagerRef.AddIntoxication();

            // Set Jump Height
            // playerMovementAdvanced.SetJumpHeight(playerMovementAdvanced.GetJumpHeight() * 3);

            // Set Gravity
            // playerMovementAdvanced.SetGravity(playerMovementAdvanced.GetGravity() / 8);

            // Initialize and start progress bar
            itemDurationBar.gameObject.SetActive(true); // Show progress bar
            itemDurationBar.maxValue = jumpBoostDuration; // Set max value
            itemDurationBar.value = jumpBoostDuration; // Initialize value

            StartCoroutine(ResetJumpHeightAfterDuration(jumpBoostDuration));
        }
    }

    private IEnumerator ResetJumpHeightAfterDuration(float duration) {
        float elapsedTime = 0f;

        while (elapsedTime < duration) {
            elapsedTime += Time.deltaTime;
            itemDurationBar.value = duration - elapsedTime; // Update progress bar value
            yield return null;
        }

        // Reset Jump Height
        //playerMovementAdvanced.SetJumpHeight(playerMovementAdvanced.GetJumpHeight() / 3);

        // Reset Gravity
        // playerMovementAdvanced.SetGravity(playerMovement.GetGravity() * 8);

        isUsingBong = false;

        // Hide progress bar
        itemDurationBar.gameObject.SetActive(false);
    }
}