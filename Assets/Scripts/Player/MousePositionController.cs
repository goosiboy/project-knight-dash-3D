using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     MousePosition - data handler/controller class. Use to store X and Y - values
/// </summary>
public class MousePositionController {

    private float x;
    private float y;
    private string context;
    private readonly string className;

    public MousePositionController(string context) {
        this.initMousePositionDown(context);
        this.className = this.GetType().Name;
    }

    public MousePositionController() {
        this.initMousePositionDown();
        this.className = this.GetType().Name;
    }

    // Initializes the class
    private void initMousePositionDown(string context) {
        this.setX(0.0f);
        this.setY(0.0f);
        this.setContext(context);
    }

    // Initializes the class
    private void initMousePositionDown() {
        this.setX(0.0f);
        this.setY(0.0f);
    }

    /// <summary>
    ///     Creates and returns a debug string. Debug string contains the currently stored X and Y - values
    /// </summary>
    /// <returns>String</returns>
    public string debug() {
        if (null != this.getContext()) {
            return "[" + this.getContext() + "; " + className + "] X: " + this.getX() + " | Y: " + this.getY();
        } else {
            return "[" + className + "] X: " + this.getX() + " | Y: " + this.getY();
        }
    }

    /// <summary>
    ///     Returns the X and Y values as Vector3
    /// </summary>
    /// <returns>Vector3</returns>
    public Vector3 getAsVector3() {
        return new Vector3(this.getX(), this.getY());
    }

    // GETTERS AND SETTERS
    public void setX(float x) {
        this.x = x;
    }

    public float getX() {
        return x;
    }

    public void setY(float y) {
        this.y = y;
    }

    public float getY() {
        return y;
    }

    public void setContext(string context) {
        this.context = context;
    }

    public string getContext() {
        return context;
    }

}
