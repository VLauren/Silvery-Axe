using UnityEngine;
using System.Collections;

public class Jugador : MonoBehaviour
{
    private const string NOMBRE_MODELO = "Modelo";
    private const string NOMBRE_ANIM_VELOCIDAD = "Velocidad";

    public static Jugador instancia { get; private set; }

    // referencias
    public Transform modelo { get; private set; }
    public CharacterController cc { get; private set; }
    private Animator anim;
    private Arma arma;

    private bool bloqueoAnimacion = false; // indica si se está en una animacion que bloquee el movimiento

    public float velocidad;

    void Start()
    {
        // referencias
        modelo = transform.Find(NOMBRE_MODELO);
        instancia = this;
        anim = GetComponentInChildren<Animator>();
        cc = GetComponent<CharacterController>();
        IniciarArmas();

        // suscripciones
        NotificationCenter.DefaultCenter().AddObserver(this, "LanzaRecogida");
        NotificationCenter.DefaultCenter().AddObserver(this, "EspadaRecogida");
    }

    public void Update()
    {
        // input de armas
        if(arma != null)
        {
            if (Input.GetButtonDown("Fire1"))
                arma.Usar();
            if (Input.GetButtonDown("Fire2"))
                arma.Usar2();
            if (Input.GetButtonUp("Fire1"))
                arma.Soltar();
            if (Input.GetButtonDown("Jump"))
                LanzaArma.Teleportar();
        }

        // rotacion del modelo mientras se carga el lanzamiento de la lanza
        if(cargando)
        {
            Vector3 alante = Camera.main.transform.forward;
            alante.y = 0;
            rotObjetivo = Quaternion.LookRotation(alante, Vector3.up);
            if (modelo)
                modelo.rotation = Quaternion.RotateTowards(modelo.rotation, rotObjetivo, Time.deltaTime * 360);
        }
    }

    private Vector3 movimiento;
    private Vector3 gravedad;
    private float accelGravedad = 0.7f;

    void FixedUpdate()
    {
        // gravedad
        if (cc.isGrounded)
            gravedad = Vector3.zero;
        gravedad += new Vector3(0, -accelGravedad * Time.fixedDeltaTime, 0);

        // cc.Move
        if (cc && !cargando && !bloqueoAnimacion)
            cc.Move(movimiento + gravedad);

        anim.SetBool("Suelo", cc.isGrounded);
    }

    private Quaternion rotObjetivo;

    // convierte vector de dirección en movimiento del jugador
    public void Mover(Vector3 direccion)
    {
        if (direccion != Vector3.zero)
        {
            // rotacion del modelo
            if(movimiento!=Vector3.zero && !bloqueoAnimacion)
            {
                rotObjetivo = Quaternion.LookRotation(movimiento, Vector3.up);
                if (modelo)
                    modelo.rotation = Quaternion.RotateTowards(modelo.rotation, rotObjetivo, Time.deltaTime * 360);
            }
            
            // tomo la rotacion en el eje y de la cámara
            float rCam = Camera.main.transform.eulerAngles.y;

            // el movimiento es relativo a la direccion de la cámara
            direccion = Quaternion.Euler(0, rCam, 0) * direccion;
        }

        if (anim)
            anim.SetFloat(NOMBRE_ANIM_VELOCIDAD, direccion.magnitude);

        // actualiza el vector de movimiento
        movimiento = direccion * Time.fixedDeltaTime * velocidad;
    }

    void Reset()
    {
        velocidad = 4;
    }

    //==============================
    // Animaciones

    private bool cargando = false;

    public static void AnimCargando()
    {
        instancia.cargando = true;
        if (instancia.anim)
        {
            instancia.anim.SetBool("Cargando", true);
        }
    }

    public static void AnimIdle()
    {
        instancia.cargando = false;
        if (instancia.anim)
        {
            instancia.anim.SetBool("Cargando", false);
        }
    }

    private const float tAnimEspada = 2;

    public static void AnimEspada()
    {
        instancia.StartCoroutine(instancia.AnimacionEspada());
    }

    private IEnumerator AnimacionEspada()
    {
        // muestro la espada que apunta hacia abajo
        modeloEspadaB.SetActive(true);
        modeloEspada.SetActive(false);
        
        instancia.anim.SetTrigger("Espada");
        bloqueoAnimacion = true;

        // espero el tiempo de animacion
        yield return new WaitForSeconds(tAnimEspada);

        // vuelvo a mostrar la espada normal
        modeloEspadaB.SetActive(false);
        modeloEspada.SetActive(true);

        instancia.anim.SetTrigger("FinEspada");
        bloqueoAnimacion = false;
    }

    //==============================
    // Armas

    private GameObject modeloEspada;
    private GameObject modeloEspadaB;
    private GameObject modeloLanza;

    void IniciarArmas()
    {
        modeloEspada = GameObject.Find("ModeloEspada");
        modeloEspadaB = GameObject.Find("ModeloEspadaB");
        modeloLanza = GameObject.Find("ModeloLanza");

        modeloEspada.SetActive(false);
        modeloEspadaB.SetActive(false);
        modeloLanza.SetActive(false);
    }
    
    // métodos para mostrar los modelos correctos al recoger un arma
    void LanzaRecogida()
    {
        arma = new LanzaArma();

        modeloEspada.SetActive(false);
        modeloLanza.SetActive(true);
    }

    void EspadaRecogida()
    {
        arma = new EspadaArma();
        Destroy(GameObject.Find("HieloCubo"));

        modeloEspada.SetActive(true);
        modeloLanza.SetActive(false);
    }


}
