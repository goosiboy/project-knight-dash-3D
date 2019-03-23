using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Class for the player state control
/// </summary>
public class PlayerStateController : MonoBehaviour {
    private bool? isActive;
    private bool? isJumping;

    // Start is called before the first frame update
    void Awake() {
        this.initPlayerState();
    }

    private void initPlayerState() {
        this.setIsActive(false);
        this.setIsJumping(false);
    }

    public void resetPlayerState() {
        this.setIsActive(null);
        this.setIsJumping(null);
    }

    /// <summary>
    ///     Controller for the player state
    /// </summary>
    /// <param name="isActive"></param>
    /// <param name="isJumping"></param>
    public void stateControl(bool? isActive, bool? isJumping) {
        if (null != isActive) {
            this.setIsActive(isActive);
        }
        if (null != isJumping) {
            this.setIsJumping(isJumping);
        }
    }

    public void getDebugString() {
        Debug.Log("[PlayerStateController] isJumping: " + this.getIsJumping() + " | isActive: " + this.getIsActive());
    }

    public bool? getIsActive() {
        return this.isActive;
    }

    public void setIsActive(bool? isActive) {
        this.isActive = isActive;
    }

    public bool? getIsJumping() {
        return this.isJumping;
    }

    public void setIsJumping(bool? isJumping) {
        this.isJumping = isJumping;
    }

}
