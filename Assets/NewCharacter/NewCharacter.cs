///Crédit
///Alexandre MORIZE -> Epileptic Range

using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class NewCharacter : MonoBehaviour
{
    #region Public property
    [Range(0, 20)]
    public float WalkSpeed = 4, RunSpeed = 9, CrouchSpeed = 2, ClimbSpeed = 2;
    [Range(0, 90)]
    public float CameraLimitAngle = 75;
    public float Gravity = 14, CamSensibilty = 3;
    public float JumpHeight = 1.5f;
    public float CharacterHeight = 2, CharacterCrouchHeight = 1;
    public float GoToCrouchSpeed = 5;
    public float CamHeightFromGround = 1.7f;
    public float SlideTime = 1;
    public float MaxClimbHeight = 5, MaxDistanceWallRun = 10;
    [Range(0, 10)]
    public float CoefClimbDuringWallRun = 3;
    #endregion

    #region Private Property

    private CharacterController controller;
    private Camera cam;
    private Vector3 move;
    private float verticalSpeed;
    private float currentSpeed;
    private float camAngleX = 0;
    private float camAngleY = 0;
    private float desiredAngleCamY = 0;
    private float jumpSpeed;
    private float currentHeight;
    private float diffHeight;
    private Vector3 slideDirection;
    private float slideTimer = 0;
    private bool previouslyGrounded;
    private float countClimbHeight = 0, countDistanceWallRun = 0;
    private float camHeightFromCenter;
    private float coefStand = 1;
    private bool standUpSucess;


    //For setters

    /// <summary>
    /// Ne pas modifier, utiliser IsRunning
    /// </summary>
    bool _isRunning = false;


    /// <summary>
    /// Ne pas modifier, utiliser IsCrouch
    /// </summary>
    bool _isCrouch = false;
    #endregion

    #region Property setter

    public bool IsRunning
    {
        get => _isRunning;
        private set
        {
            if (value == _isRunning) return;
            _isRunning = value;
            if (_isRunning)
            {
                currentSpeed = RunSpeed;
            }
            else
            {
                currentSpeed = WalkSpeed;
            }
        }
    }

    public bool isSliding
    {
        get
        {
            return slideTimer > 0;
        }
    }

    public bool isCrouch
    {
        get
        {
            return _isCrouch;

        }
        private set
        {
            
            if (value)
            {
                CrouchDown();
                if (!isSliding) currentSpeed = CrouchSpeed;
                _isCrouch = true;
            }
            else
            {
                if (!standUpSucess)
                    StandUp();
                else
                    _isCrouch = false;
            }
        }
    }

    #endregion

    #region Invoke Method

    void CrouchDown()
    {
        standUpSucess = false;
        CancelInvoke("StandUp");
        coefStand -= GoToCrouchSpeed * Time.deltaTime;

        if (coefStand > 0)
        {
            Invoke("CrouchDown", Time.deltaTime);
        }
        else
        {
            coefStand = 0;
        }

        controller.height = (1 - coefStand) * CharacterCrouchHeight + coefStand * CharacterHeight;
        cam.transform.localPosition = Vector3.up * camHeightFromCenter * coefStand;
    }

    void StandUp()
    {
        CancelInvoke("CrouchDown");

        int layerMask = ~LayerMask.GetMask("Player");
        //Verifie qu'il n'y a rien au dessus du perso
        if (Physics.Raycast(transform.position - Vector3.up * controller.height / 2, Vector3.up, CharacterHeight, layerMask))
        {
            Invoke("StandUp", Time.deltaTime);
            return;
        }

        coefStand += GoToCrouchSpeed * Time.deltaTime;

        if (coefStand < 1)
        {
            Invoke("StandUp", Time.deltaTime);
        }
        else
        {
            coefStand = 1;
            if (!IsRunning) currentSpeed = WalkSpeed;
            standUpSucess = true;
            isCrouch = false;
            if (Input.GetButton("Run")) IsRunning = true;
        }
        controller.height = (1 - coefStand) * CharacterCrouchHeight + coefStand * CharacterHeight;
        cam.transform.localPosition = Vector3.up * camHeightFromCenter * coefStand;
    }



    #endregion

    void Slide()
    {
        slideDirection = transform.forward;
        slideTimer = SlideTime;
        isCrouch = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>();
        if (cam == null) Debug.LogError("Il n'y a pas de caméra attaché attaché au script NewCharacter en tant qu'enfant");
        else if (cam.transform == transform) Debug.Log("La caméra doit etre un enfant un enfant de l'objet joueur");
        currentSpeed = WalkSpeed;
        jumpSpeed = Mathf.Sqrt(2 * Gravity * JumpHeight);
        diffHeight = CharacterHeight - CharacterCrouchHeight;
        currentHeight = CharacterHeight;
        camHeightFromCenter = CamHeightFromGround - CharacterHeight / 2;
        cam.transform.localPosition = Vector3.up * camHeightFromCenter;
    }

    // Update is called once per frame
    void Update()
    {
        //Get Move
            move = (transform.forward * Input.GetAxisRaw("Vertical") +
              transform.right * Input.GetAxisRaw("Horizontal")).normalized * currentSpeed * Time.deltaTime;

        //Courir
        if (Input.GetButtonDown("Run") && !isCrouch) IsRunning = true;
        if (Input.GetButtonUp("Run")) IsRunning = false;


        //Crouch
        if (Input.GetButtonDown("Crouch") && !IsRunning) isCrouch = true;
        if (Input.GetButtonUp("Crouch") && !isSliding) isCrouch = false;

        //Get on ground
        if (controller.isGrounded)
        {
            desiredAngleCamY = 0;
            previouslyGrounded = true;
            countDistanceWallRun = countClimbHeight = 0;
            verticalSpeed = 0;
            if (Input.GetButton("Jump")) verticalSpeed = jumpSpeed;
        }
        else
            verticalSpeed -= Gravity * Time.deltaTime;

        if (IsRunning && !isSliding && Input.GetButtonDown("Crouch") && Input.GetAxisRaw("Vertical")>0) Slide();

        //Get CameraOrientation
        if (Time.timeScale > 0)//Si le jeu n'est pas sur pause
        {
            // Left and right
            if (!isSliding)
            {
                transform.eulerAngles += Vector3.up * CamSensibilty * Input.GetAxis("Mouse X");

                // Up and down
                camAngleX -= CamSensibilty * Input.GetAxis("Mouse Y");
                camAngleX = Mathf.Clamp(camAngleX, -CameraLimitAngle, CameraLimitAngle);
            }


        }

        // Climb and WallRun
        if (Input.GetButton("Run") && previouslyGrounded)
        {
            Vector3 PositionPied = transform.position - Vector3.up * (controller.height / 2 - .1f);
            //climb
            int layer_mask = LayerMask.GetMask("Wall");
            if (Physics.Raycast(PositionPied, transform.forward, controller.radius + .1f, layer_mask))
            {
                verticalSpeed = ClimbSpeed;
                countClimbHeight += verticalSpeed * Time.deltaTime;
                if (countClimbHeight > MaxClimbHeight) previouslyGrounded = false;
            }

            RaycastHit hit;
            //Wall Run Right
            if (Physics.Raycast(PositionPied, transform.right, out hit, controller.radius + .1f, layer_mask))
            {
                Vector3 hitPosition = hit.point;
                if (Physics.Raycast(PositionPied + transform.forward * controller.radius, transform.right, out hit, controller.radius + .1f, layer_mask))
                {
                    desiredAngleCamY = 10;
                    move = (hit.point - hitPosition).normalized * RunSpeed * Time.deltaTime;
                    verticalSpeed = (MaxDistanceWallRun / 2 - countDistanceWallRun) / (MaxDistanceWallRun / 2) * CoefClimbDuringWallRun;
                    countDistanceWallRun += RunSpeed * Time.deltaTime;
                    if (countDistanceWallRun > MaxDistanceWallRun)
                    {
                        desiredAngleCamY = 0;
                        previouslyGrounded = false;
                    }
                }
            }
            else//Wall Run left
            if (Physics.Raycast(PositionPied, -transform.right, out hit, controller.radius + .1f, layer_mask))
            {
                Vector3 hitPosition = hit.point;
                if (Physics.Raycast(PositionPied + transform.forward * controller.radius, -transform.right, out hit, controller.radius + .1f, layer_mask))
                {
                    desiredAngleCamY = -10;
                    move = (hit.point - hitPosition).normalized * RunSpeed * Time.deltaTime;
                    verticalSpeed = (MaxDistanceWallRun / 2 - countDistanceWallRun) / (MaxDistanceWallRun / 2) * CoefClimbDuringWallRun;
                    countDistanceWallRun += RunSpeed * Time.deltaTime;
                    if (countDistanceWallRun > MaxDistanceWallRun)
                    {
                        desiredAngleCamY = 0;
                        previouslyGrounded = false;
                    }
                }
            }
            else desiredAngleCamY = 0;
        }

        camAngleY += (desiredAngleCamY - camAngleY) * Time.deltaTime * 5;

        //Set angle camera
        cam.transform.localEulerAngles = Vector3.right * camAngleX + Vector3.forward * camAngleY;

        if (isSliding)
        {
            slideTimer -= Time.deltaTime;
            if (!isSliding) isCrouch = false;

            camAngleX -= camAngleX * Time.deltaTime * 5;

            controller.Move((slideDirection * RunSpeed + Vector3.up * verticalSpeed) * Time.deltaTime);
        }
        else
            controller.Move(move + Vector3.up * verticalSpeed * Time.deltaTime);
    }
}
