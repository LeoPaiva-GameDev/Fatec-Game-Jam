using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    [SerializeField] private Sprite vida3, vida2, vida1,vida0;
   
    private GameObject myHealthbar;
    private Vector3 direction; // (1,0,0) for right, (-1,0,-0) left, (0,0,1) up, (0,0,-1) down


    private float rotation = 0;

    //For instancing
    public int playerNumber;
    public string horizontal;
    public string vertical;
    public string Rhorizontal;
    public string Rvertical;
    public string bombInput;
    public string BlueInput;
    public string GreenInput;
    public string PinkInput;


    [SerializeField] private AudioClip changeDimensionSound;
    private AudioSource source;
    public int playerLives = 3;

    [SerializeField] GameObject[] bodyPrefabs;
    [SerializeField] private float playerMoveX;
    [SerializeField] private float playerMoveZ;

    [SerializeField] private float playerSpeed;
    private Vector3 moveVelocity;
    private Vector3 moveInput;
    private Rigidbody rb;

    private float changeDimensionCD;
    private bool canChangeDimension = true;

    [SerializeField] public Color blue, orange, green;

    private GameObject body;
    private GameObject bodyMesh;
    private SkinnedMeshRenderer bodyMesh_skinned;

    public bool bombThrew = false;

    //Bomb stuff
    [SerializeField] private GameObject bombprefab;
    private GameObject bomb;
    private GameObject bombMesh;
    private Bomb bombScript;

    [SerializeField] private float bombThrowForce;

    private bool isBombInstantiated = false;
    private bool bombCanChangeColors = false;

    //Raycasting on pc
    private Camera mainCamera;
    
    #region start and setting stuff
    // Use this for initialization
    void Start () {

        mainCamera = FindObjectOfType<Camera>();
        source = gameObject.GetComponent<AudioSource>();

        body = Instantiate(bodyPrefabs[1], transform.position, Quaternion.identity);
        body.transform.SetParent(gameObject.transform);
        //Pegando valor dentro do body com o mesh
        bodyMesh = body.transform.GetChild(3).gameObject;
        bodyMesh_skinned = bodyMesh.GetComponent<SkinnedMeshRenderer>();

        TrocaDimensao(body, bodyMesh, blue, "Azul");
        rb = body.GetComponent<Rigidbody>();
        changeDimensionCD = 2;
    }
    #endregion

    private void Update()
    {
        /*//Death
        if(playerLives == 3)
        {
            myHealthbar.GetComponent<Image>().sprite = vida3;
        } else if(playerLives == 2)
        {
            myHealthbar.GetComponent<Image>().sprite = vida2;
        } else if (playerLives == 1)
        {
            myHealthbar.GetComponent<Image>().sprite = vida1;
        } else
        {
            myHealthbar.GetComponent<Image>().sprite = vida0;
            Destroy(gameObject);
        }
        */
        if (!isBombInstantiated)
        {
            isBombInstantiated = true;
            InstantiateBomb();
        } else if (bombCanChangeColors)
        {
            bomb.transform.rotation = body.transform.rotation;
        }

        if (bombThrew)
        {
            bombCanChangeColors = false;
            StartCoroutine(bomb_countdown());
            bombThrew = false;
        }

        //Dimension cooldown Code
        changeDimensionCD -= Time.deltaTime;

        if(changeDimensionCD <= 0)
        {
            canChangeDimension = true;
        } else
        {
            canChangeDimension= false;
        }

        

    }

    private void updateRotation()
    {/*
        if(moveInput.z < 0 && moveInput.x < 0)
        {
            rotation = 45;
        } else if (moveInput.z > 0 && moveInput.x > 0)
        {
            rotation = 225;
        } else if (moveInput.x > 0 && moveInput.z < 0)
        {
            rotation = 315;
        } else if (moveInput.x < 0 && moveInput.z > 0)
        {
            rotation = 135;
        } else if (moveInput.z < 0){
            rotation = 0;
        } else if (moveInput.x < 0)
        {
            rotation = 90;
        } else if (moveInput.z > 0)
        {
            rotation = 180;
        } else if (moveInput.x > 0)
        {
            rotation = 270;
        }

        Quaternion newRotation = Quaternion.Euler(0, rotation, 0);
        body.transform.localRotation = Quaternion.Lerp(body.transform.localRotation, newRotation, 5 * Time.deltaTime);
        //body.transform.eulerAngles = new Vector3(body.transform.eulerAngles.x, rotation, body.transform.eulerAngles.z);
        */
    }

    // Update is called once per frame
    void FixedUpdate() {

            playerMoveX = Input.GetAxis(horizontal);
            playerMoveZ = Input.GetAxis(vertical);

            moveInput = new Vector3(Input.GetAxisRaw(horizontal), 0f, -Input.GetAxisRaw(vertical));
            moveVelocity = moveInput * playerSpeed;
            body.GetComponent<Rigidbody>().velocity = moveVelocity;
           

            //BOMBA
            /*if (Input.GetAxis(bombInput) > 0 && isBombInstantiated && !bombThrew && bombCanChangeColors)
            {
                source.clip = changeDimensionSound;
                source.Play();
                bombScript.ThrowBomb();
                bombThrew = true;
                bombScript.bombThrew = true;
            }

            //Dimencao azul - 1
            if (Input.GetAxis(BlueInput) > 0 && canChangeDimension)
            {

                TrocaDimensaoBomba(bomb, bombMesh, blue, "BombaAzul");
                TrocaDimensao(body, bodyMesh, blue, "Azul");
                changeDimensionCD = 2;
            }

            //dimencao verde - 2
            if (Input.GetAxis(GreenInput) > 0 && canChangeDimension)
            {
                TrocaDimensaoBomba(bomb, bombMesh, green, "BombaVerde");
                TrocaDimensao(body, bodyMesh, green, "Verde");
                changeDimensionCD = 2;
            }

            //dimencao rosa - 3
            if (Input.GetAxis(PinkInput) > 0 && canChangeDimension)
            {
                TrocaDimensaoBomba(bomb, bombMesh, orange, "BombaLaranja");
                TrocaDimensao(body, bodyMesh, orange, "Laranja");
                changeDimensionCD = 2;
            }
            */
        #region determining if the controller is a keyboard & mouse, to add the raycaster for looking //// or if the controller is a joystick
        if (horizontal.Substring(horizontal.Length - 1) == "5") //finding out if the keyboard index is for mouse and keyboard
        {
            Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition); //Creating the ray from the input mouse position
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero); //Creating a mathematical plane to serve as the raycast target
            float rayLength; //Giving this a value will show "how far it is from the camera to the ground"

            if(groundPlane.Raycast(cameraRay,out rayLength))
            {
                //Create a point for the character to look at
                Vector3 pointToLook = cameraRay.GetPoint(rayLength);
                Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
                //body.transform.LookAt(new Vector3(pointToLook.x,transform.position.y,pointToLook.z));
                body.transform.LookAt(body.transform.position * 2 - pointToLook);
            }
        } else
        {
            //Joystick
            //Create a point to look at that is the transform of the object + the axis
            Vector3 playerDirection = Vector3.right * -Input.GetAxisRaw(Rhorizontal) + Vector3.forward * Input.GetAxisRaw(Rvertical);
            if (playerDirection.sqrMagnitude > 0.0f) //detecting if it is not zero
            {
                body.transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
            }
            
            
        }
        
        #endregion

    }
    

    private void TrocaDimensao(GameObject player, GameObject playerMesh, Color cor, string tag)
    {
        SkinnedMeshRenderer mesh = playerMesh.GetComponent<SkinnedMeshRenderer>();
        mesh.materials[0].color = cor;
        player.tag = tag;
    }

    public void TrocaDimensaoBomba(GameObject bomba, GameObject bombaMesh, Color cor, string tag)
    {
        if (bombCanChangeColors)
        {
            MeshRenderer mesh = bombaMesh.GetComponent<MeshRenderer>();
            mesh.material.color = cor;
            bomba.tag = tag;
        }
    }

    private void InstantiateBomb()
    {
        bomb = null;
        bomb = Instantiate(bombprefab, transform.position, Quaternion.identity);
        bomb.transform.SetParent(body.transform);
        bomb.transform.localScale = new Vector3(2, 2, 2);
        bomb.transform.localPosition = new Vector3(0.4f, 1.3f, 0);
        bomb.GetComponent<Rigidbody>().useGravity = false;
        bomb.GetComponent<SphereCollider>().isTrigger = true;
        bombScript = bomb.GetComponent<Bomb>();
        bombMesh = bomb.transform.GetChild(4).gameObject;
        bombCanChangeColors = true;

        get_player_color();
    }

   IEnumerator bomb_countdown()
    {
        yield return new WaitForSeconds(3);
        isBombInstantiated = false;
    }

    private void get_player_color()
    {
        if (body.tag == "Azul")
        {
            TrocaDimensaoBomba(bomb, bombMesh, blue, "BombaAzul");
        }

        if (body.tag == "Verde")
        {
            TrocaDimensaoBomba(bomb, bombMesh, green, "BombaVerde");
        }

        if (body.tag == "Laranja")
        {
            TrocaDimensaoBomba(bomb, bombMesh, orange, "BombaLaranja");
        }
    }
}
