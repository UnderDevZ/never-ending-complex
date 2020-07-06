using Assets.Pixelation.Example.Scripts;
using Assets.Pixelation.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
public class PlayerScript : MonoBehaviour {


    //Sup laiser :)


	
    //Post processing initialization
    public PostProcessVolume volume;
    private ChromaticAberration _Chromatic;
    private Bloom _Bloom;
    private LensDistortion _Lens;
    private ColorGrading _Color;
    private ShaderEffect_CorruptedVram vRam;

	//General initialization
	public Rigidbody rb;

	public bool isPaused = false;
    private bool switchingToView = false;
	
	//Movement initialization
	public float speed;
	public float jumpHeight;
	private float gravity;

	private bool canMove = true;

	private Vector3 moveDirection = Vector3.zero;



    public bool isBot;


    //Camera movement
    private GameObject camera;
    float rotX;
    float rotY;
    public float sensitivity;



    //Sanity
    public float initialSanity;
    public float sanity {get; set;}
    public float sanityO;

    public float chunkyWait = 0;

    public float maxSanity = 100;
    public float minSanity = 0;

    public float time = 5f;
    public float StartTime;
    public float duration;


    







    // Use this for initialization
    void Start () {

        


        //Postprocessing intialization
        volume.profile.TryGetSettings(out _Chromatic);
        volume.profile.TryGetSettings(out _Bloom);
        volume.profile.TryGetSettings(out _Lens);
        volume.profile.TryGetSettings(out _Color);
        vRam = this.transform.GetChild(0).GetComponent<ShaderEffect_CorruptedVram>();
        vRam.shift = 0;

   
        //GO Initialization
		rb = this.transform.GetComponent<Rigidbody>();
        camera = this.transform.GetChild(0).gameObject;

        //Movement initialization
		speed = 5f;
		jumpHeight = 8f;
		gravity = 20f;

		canMove = true;


        //Camera initialization
        sensitivity = 2f;


        //Hiding and locking cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        

        //Sanity
        initialSanity = 100f;
        sanity = initialSanity;

        StartTime = Time.time;
      
       
        
	}

    void OnGUI ()
    {

    }

    public IEnumerator IntervalTime()
    
    {
        yield return new WaitForSeconds(0.5f);
        sanity = sanity - 10;
        yield return new WaitForSeconds(0.5f);
        sanity = sanity - 10;
        yield return new WaitForSeconds(0.5f);
        sanity = sanity - 10;
        yield return new WaitForSeconds(0.5f);
        sanity = sanity - 10;
        yield return new WaitForSeconds(0.5f);
        sanity = sanity - 10;
        yield return new WaitForSeconds(0.5f);
        sanity = sanity - 10;
        yield return new WaitForSeconds(0.5f);
        sanity = sanity - 10;
        yield return new WaitForSeconds(0.5f);
        sanity = sanity - 10;
        yield return new WaitForSeconds(0.5f);
        sanity = sanity - 10;
        yield return new WaitForSeconds(0.5f);
        sanity = sanity - 10;




    }




    // Update is called once per frame

    void OnTriggerEnter(Collider sanityTrigger)
    {
        if (sanityTrigger.tag == "SanityDepleter")

        {

            StartCoroutine(IntervalTime());           
        }

     }
    void Update () {

        //Assigning sanity left
        sanityO = 100 - sanity;


        float t = (Time.time - StartTime) / duration;
        
       






        //chromaticLayer.intensity = new FloatParameter { value = 100 };

        CharacterController player = GetComponent<CharacterController>();

		if(player.isGrounded && canMove == true && isBot == false)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(Vector3.ClampMagnitude(moveDirection, 1f));
            moveDirection *= speed;
        
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpHeight;
            }

            
                
        }

        
        // Apply gravity
        moveDirection.y -= gravity * Time.deltaTime;
        
        // Move the controller
        if(!isPaused) player.Move(moveDirection * Time.deltaTime);

        //Sprinting
        if(Input.GetKey(KeyCode.LeftShift) && isBot == false) speed = 10f;
        if(Input.GetKeyUp(KeyCode.LeftShift) && isBot == false) speed = 5f;


        //Camera
        rotX = Input.GetAxis("Mouse X") * sensitivity;
        rotY = Input.GetAxis("Mouse Y") * sensitivity;

        if(!isPaused)
        {
        camera.transform.Rotate(-rotY, 0, 0f);
        player.transform.Rotate(0, rotX, 0f);
        }





        

        //Debug menu
        if(Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(DebugMenu());
            
        }



        //View menu on
        if(Input.GetKeyDown(KeyCode.P) && isPaused == false && switchingToView == false) StartCoroutine(ViewMode(true)); 

        if(Input.GetKeyDown(KeyCode.P) && isPaused == true && switchingToView == false) StartCoroutine(ViewMode(false)); 

        
        










        

        //SANITY EFX
        _Chromatic.intensity.value = sanityO / 100f;
        _Bloom.intensity.value = sanityO / 5f;
        _Lens.intensity.value = sanityO / 1.5f;
        Camera.main.fieldOfView = 60 - sanityO / 4;

        //Pixellation being changed compared to sanity left
        if(sanity > 50) camera.transform.GetComponent<Pixelation>().enabled = false;
        else
        {
            camera.transform.GetComponent<Pixelation>().enabled = true;
            camera.transform.GetComponent<Pixelation>().BlockCount = 512 - sanityO * 4;
        }

        //Chunky being turned on randomly when sanity is low
        if(sanity < 20)
        {
            if(chunkyWait <= 0)
            {
                if(Random.Range(0f, 10f) > 9.95f)
                {
                camera.transform.GetComponent<Chunky>().enabled = true;
                chunkyWait = 1f;
                }
                else
                {
                    camera.transform.GetComponent<Chunky>().enabled = false;
                }
            }

            if(chunkyWait > 0)
            {
                camera.transform.GetComponent<Chunky>().enabled = true;
                chunkyWait -= 0.05f;
            }



               
                
            
        }
        else camera.transform.GetComponent<Chunky>().enabled = false;



        

        if(sanity < 30)
        {
            //camera.transform.GetComponent<Camera>().nearClipPlane = Random.Range(0.1f, 10f);
            camera.transform.GetComponent<Camera>().farClipPlane = Random.Range(20f, 300f);
        }
        else
        {
            camera.transform.GetComponent<Camera>().nearClipPlane = 0.01f;
            camera.transform.GetComponent<Camera>().farClipPlane = 1000f;
        }







	}



  
 
    //Wrapper for voyeurtext
    public void VoyeurTextWrapper ()
    {
        this.transform.GetChild(1).GetChild(2).transform.GetComponent<TypeScript>().vText = "MOVE.";
        StartCoroutine(this.transform.GetChild(1).GetChild(2).transform.GetComponent<TypeScript>().PlayText());
    }
    






    //Debug menu
    public IEnumerator DebugMenu ()
    {
        if(isPaused == false)
        {
        

            Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

            //Open debug menu
            this.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
            yield return null;
            isPaused = true;
            yield break;
        }

        if(isPaused == true)
        {
        isPaused = false;

            Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

            //Close debug menu
            this.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
            yield return null;
            isPaused = false;
            yield break;
        }
    }




    //View mode
    public IEnumerator ViewMode(bool on)
    {
        vRam.shift = 1;

        //Turning view mode on
        if(on == true)
        {

        switchingToView = true;
        isPaused = true;



        //Increase VRAM exponentially for less than a second
        for(int i = 0; i < 40; i++)
        {
            vRam.shift *= 1.5f;
            yield return null;
        }

        //Teleport player

        //Decrease VRAM exponentially for a second
        for(int i = 0; i < 60; i++)
        {
            vRam.shift /= 1.5f;
            yield return null;
        }



        switchingToView = false;



        }




        //Turning view mode off
        if(on == false)
        {


            //Stuff here

        switchingToView = false;
        isPaused = false;



        }




    }




}
