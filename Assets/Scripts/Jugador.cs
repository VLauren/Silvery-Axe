using UnityEngine;
using System.Collections;

public class Jugador : MonoBehaviour
{
    private const string NOMBRE_MODELO = "Modelo";
    private const string NOMBRE_ANIM_VELOCIDAD = "Velocidad";

    public static Jugador instancia { get; private set; }

    public Transform modelo { get; private set; }
    public CharacterController cc { get; private set; }
    private Animator anim;
    private Arma arma;

    private bool bloqueoAnimacion = false;

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

        // esconder el cursor
        // TODO que esto esté en el gestor y se haga de forma contínua
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
    private Vector3 direccionAnterior = Vector3.zero;
    private Vector3 movAnterior = Vector3.zero;
    
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

            // TODO repasar esto
            float rCam = Camera.main.transform.eulerAngles.y;
            //if (direccion != direccionAnterior)
            //{
                direccionAnterior = direccion;
                direccion = Quaternion.Euler(0, rCam, 0) * direccion;
                movAnterior = direccion;
            //}
            //else
            //    direccion = movAnterior;
        }
        else
            direccionAnterior = direccion;

        // variable del Animator
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
        modeloEspadaB.SetActive(true);
        modeloEspada.SetActive(false);
        instancia.anim.SetTrigger("Espada");
        bloqueoAnimacion = true;

        yield return new WaitForSeconds(tAnimEspada);

        modeloEspadaB.SetActive(false);
        modeloEspada.SetActive(true);
        instancia.anim.SetTrigger("FinEspada");
        bloqueoAnimacion = false;
    }

    //==============================

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
