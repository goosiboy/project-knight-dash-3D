using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Handles player's movement logic
/// </summary>
public class Movement : MonoBehaviour {

    public float jumpForce = 450.0f;
    public float fallMultiplier = 2.0f;
    public float maxThrust = 18000.0f;

    private float thrust;
    private float radians;
    private float angle;
    private float speed = 150.0f;
    private float dist;
    private Vector3 currentLocation;
    private Vector3 destination;

    private bool? direction;
    private GameTimer gameTimer;
    private Rigidbody rb;
    private MathUtils mathUtils;
    private Vector3 lastPosition;

    private readonly bool ENABLE_GRAVITY = true;
    private readonly bool DISABLE_GRAVITY = false;
    public static readonly bool PLAYER_ACTIVE = true;
    public static readonly bool PLAYER_NOT_ACTIVE = false;
    public static readonly bool PLAYER_JUMPING = true;
    public static readonly bool PLAYER_NOT_JUMPING = false;

    public static readonly string RIGHT = "right";
    public static readonly string LEFT = "left";

    /// <summary>
    ///     Class name
    /// </summary>
    private readonly string className;

    public Movement() {
        this.className = this.GetType().Name;
        this.mathUtils = new MathUtils();
    }

    private void Awake() {
        gameTimer = GameObject.Find("Timer").GetComponent<GameTimer>();
    }

    // Start is called before the first frame update
    void Start() {      
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (Input.GetButtonDown("Fire1")) {
            Player.getMouseDown().setX(Camera.main.ScreenToWorldPoint((Vector3)Input.mousePosition).x);
            Player.getMouseDown().setY(Camera.main.ScreenToWorldPoint((Vector3)Input.mousePosition).y);
        }
        if (Input.GetButtonUp("Fire1")) {
            Player.getMouseUp().setX(Camera.main.ScreenToWorldPoint((Vector3)Input.mousePosition).x);
            Player.getMouseUp().setY(Camera.main.ScreenToWorldPoint((Vector3)Input.mousePosition).y);
            this.playerJumpController();
        }

        // Makes the player fall faster after reaching the maximum altitude
        if (true == this.getPlayerJumpState()) {
            if (rb.velocity.y < 0.0f) {
                rb.velocity += Vector3.up * Physics.gravity.y * (this.fallMultiplier - 1) * Time.deltaTime;
            } 
        }
    }

    private float handleAngle() {
        radians = (float)System.Math.Atan2(
            (Player.getMouseDown().getY() -
             Player.getMouseUp().getY()), 
            (Player.getMouseDown().getX() -
             Player.getMouseUp().getX()));
        angle = (float)(radians * (180 / System.Math.PI));
        return angle;
    }

    private float handleDistance() {
        dist = (float)System.Math.Sqrt(
            System.Math.Pow((Player.getMouseDown().getX() - Player.getMouseUp().getX()), 2) + 
            System.Math.Pow((Player.getMouseDown().getY() - Player.getMouseUp().getY()), 2));
        thrust = this.jumpForce * dist;

        if(this.thrust > this.maxThrust) {
            this.thrust = this.maxThrust;
        }

        Debug.Log("JUMP FORCE: " + this.thrust);

        return this.thrust;
    }

    void playerJumpController() {
        this.executeJump(this.handleAngle(), this.handleDistance());
    }

    private void executeJump(float angle, float thrust) {
        if (this.jumpForce > 0) {          
            this.playerStateController(true, true);
            Vector3 dir = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
            rb.AddForce(dir * thrust);
            Debug.Log("Player is jumping");
        }      
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name == "Floor") {
            this.playerStateController(false, false);
            Debug.Log("Player has landed");
        }
    }

    void moveToRight() {
        this.playerStateController(true, false);
        rb.velocity = transform.right * this.getSpeed() * Time.fixedDeltaTime;
        Debug.Log("Player is moving to the right");
    }

    void moveToLeft() {
        this.playerStateController(true, false);
        rb.velocity = -transform.right * this.getSpeed() * Time.fixedDeltaTime;
        Debug.Log("Player is moving to the left");
    }

    void playerStateController(bool isActive, bool isJumping) {
        if(isActive && !isJumping) {
            Player.getPlayerState().stateControl(PLAYER_ACTIVE, PLAYER_NOT_JUMPING);
        }
        if (isActive && isJumping) {
            Player.getPlayerState().stateControl(PLAYER_ACTIVE, PLAYER_JUMPING);
        }
        if(!isActive && !isJumping) {
            Player.getPlayerState().stateControl(PLAYER_NOT_ACTIVE, PLAYER_NOT_JUMPING);
        }
        if (!isActive && isJumping) {
            Player.getPlayerState().stateControl(PLAYER_NOT_ACTIVE, PLAYER_JUMPING);
        }
        Player.getPlayerState().getDebugString();
    }

    bool? getPlayerJumpState() {
        return Player.getPlayerState().getIsJumping();
    }

    bool? getPlayerIsActiveState() {
        return Player.getPlayerState().getIsActive();
    }

    //GETTERS AND SETTERS
    public float getSpeed() {
        return this.speed;
    }

    public void setSpeed(float speed) {
        this.speed = speed;
    }

    public void setThrust(float thrust) {
        this.thrust = thrust;
    }

    public float getThrust() {
        return this.thrust;
    }

    public float getDist() {
        return this.dist;
    }

    public float getForce() {
        return this.jumpForce;
    }

    public void setForce(float force) {
        this.jumpForce = force;
    }

    public void setDist(float dist) {
        this.dist = dist;
    }

    public float getAngle() {
        return this.angle;
    }

    public void setAngle(float angle) {
        this.angle = angle;
    }

    public void setRadians(float radians) {
        this.radians = radians;
    }

    public float getRadians() {
        return this.radians;
    }

    public Vector3 getLastPosition() {
        return this.lastPosition;
    }

    public void setLastPosition(Vector3 lastPosition) {
        this.lastPosition = lastPosition;
    }

}
