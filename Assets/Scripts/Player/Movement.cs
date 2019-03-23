using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Handles player's movement logic
/// </summary>
public class Movement : MonoBehaviour {

    public float force = 50000.0f;

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
        this.eventController();
    }

    void eventController() {
        if (Input.GetButtonDown("Fire1")) {
            this.mouseDownConfig();
        }
        if (Input.GetButtonUp("Fire1")) {
            this.mouseUpConfig();
            this.playerAttackController();
        }

        if (Player.getPlayerState().getIsJumping().Equals(PLAYER_NOT_JUMPING)) {
            if (Input.GetKey("right")) {
                this.moveToRight();
            } else if (Input.GetKey("left")) {
                this.moveToLeft();
            }
        }

    }

    public void mouseDownConfig() {
        Player.getMouseDown().setX(Camera.main.ScreenToWorldPoint((Vector3)Input.mousePosition).x);
        Player.getMouseDown().setY(Camera.main.ScreenToWorldPoint((Vector3)Input.mousePosition).y);
    }

    public void mouseUpConfig() {
        Player.getMouseUp().setX(Camera.main.ScreenToWorldPoint((Vector3)Input.mousePosition).x);
        Player.getMouseUp().setY(Camera.main.ScreenToWorldPoint((Vector3)Input.mousePosition).y);
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

    /*private float handleDistance() {
        dist = (float)System.Math.Sqrt(
            System.Math.Pow((this.mouseDown.getX() - this.mouseUp.getX()), 2) + 
            System.Math.Pow((this.mouseDown.getY() - this.mouseUp.getY()), 2));
        thrust = this.force * dist;
        return this.force;
    }*/

    void playerAttackController() {
        this.executeJump(this.handleAngle(), this.getForce());
    }

    private void executeJump(float angle, float force) {
        if (force > 0) {
            this.gravityController(ENABLE_GRAVITY);
            this.playerStateController(true, true);
            Vector3 dir = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
            rb.AddForce(dir * force);
            Debug.Log("Player is jumping");
        }      
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name == "Floor") {
            this.gravityController(ENABLE_GRAVITY);
            this.playerStateController(false, false);
            Debug.Log("Player has landed");
        }
    }

    private void gravityController(bool useGravity) {
        rb.useGravity = useGravity;
    }

    private void killVeloctiy() {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
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
        return this.force;
    }

    public void setForce(float force) {
        this.force = force;
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
