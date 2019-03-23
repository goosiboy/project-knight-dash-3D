using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Player {
    private static string playerDirection;
    private static string playerSpeed;

    private static MousePositionController mouseDown;
    private static MousePositionController mouseUp;
    private static PlayerStateController playerState;
    private static readonly string className;

    // Start is called before the first frame update
    static Player() {
        InitPlayerValues();
    }

    private static void InitPlayerValues() {
        playerDirection = null;
        playerSpeed = null;
        mouseDown = new MousePositionController();
        mouseUp = new MousePositionController();
        playerState = new PlayerStateController();
    }

    public static bool ResetPlayerValues(){
        playerDirection = null;
        playerSpeed = null;
        return true;
    }

    public static MousePositionController getMouseDown() {
        return mouseDown;
    }

    public static MousePositionController getMouseUp() {
        return mouseUp;
    }

    public static PlayerStateController getPlayerState() {
        return playerState;
    }

}
