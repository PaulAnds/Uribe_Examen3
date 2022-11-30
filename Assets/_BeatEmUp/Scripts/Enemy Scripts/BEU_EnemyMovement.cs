using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BEU_EnemyMovement : MonoBehaviour
{
    // Variables publicas
    public float speed = 5f; // Velocidad del enemigo
    public float attackDistance = 1f; // Distancia de ataque del enemigo

    // Variables privadas
    private BEU_CharacterAnimation enemyAnim; // Referencia del script de animaciones

    private Rigidbody RB; // Referencia del componente RB del enemigo

    private Transform playerTarget; // Objetivo del enemigo

    private float chasePlayerAfterAttack = 1f; // Distancia en la que el enemigo persigue al jugador despues de atacar
    private float currentAttackTime; // Tiempo actual de ataques del enemigo
    private float defaultAttackTime = 2f; // Tiempo establecido de ataques del enemigo

    private bool followPlayer; // El enemigo sigue al jugador
    private bool attackPlayer; // El enemigo ataca al jugador

    private void Awake()
    {
        // Inicializar referencias
        enemyAnim = GetComponentInChildren<BEU_CharacterAnimation>();
        RB = GetComponent<Rigidbody>();

        // Busqueda en escena para detectar el objetivo del enemigo
        playerTarget = GameObject.FindWithTag(BEU_Tags.PLAYER_TAG).transform;
    }

    private void Start()
    {
        // Declarar que el enemigo puede seguir al jugador
        followPlayer = true;

        // Inicializacion de tiempo de ataque del enemigo
        currentAttackTime = defaultAttackTime;
    }

    private void Update()
    {
        // Llamada a la funcion de ataque del enemigo
        Attack();
    }

    private void FixedUpdate()
    {
        // Llamada a la funcion en la que el enemigo sigue a su objetivo
        FollowTarget();
    }

    // Metodo para seguir al objetivo
    void FollowTarget()
    {
        // Vamos a checar si el enemigo no sigue al jugador
        if (!followPlayer)
        {
            // Si es el caso se saldra de la funcion
            return;
        }

        // Todo esto se hara si el enemigo puede seguir al jugador
        // Evaluar si la distancia entre el enemigo y el jugador es mayor a la distancia de ataque
        if (Vector3.Distance(transform.position, playerTarget.position) > attackDistance)
        {
            // Si es el caso, el enemigo no puede atacar
            // El enemigo seguir� al jugador 
            // Rotar al enemigo hacia el jugador
            transform.LookAt(playerTarget);

            // Modificar la velocidad del rigidbody del enemigo para ir hacia adelante
            RB.velocity = transform.forward * speed;

            // Animar el caminado del enemigo
            // Checar que el enemigo se este moviendo
            // Usar la magnitud de velocidad del RB para ver si el enemigo se mueve
            if(RB.velocity.sqrMagnitude != 0)
            {
                // Si existe movimiento, animaremos caminado del enemigo
                // Llamar al metodo de caminado del CharacterAnimation
                enemyAnim.Walk(true);
            }
        }
        // Evaluar si la distancia entre el enemigo y el jugador es igual o menor a la distancia de ataque
        else if (Vector3.Distance(transform.position, playerTarget.position) <= attackDistance)
        {
            // El enemigo esta en rango para atacar al jugador

            // Detener el movimiento del enemigo
            RB.velocity = Vector3.zero;

            // Detener animacion de caminado del enemigo
            // Se llama a la funcion de caminado del CharacterAnimations
            enemyAnim.Walk(false);

            // Enemigo deja de seguir al jugador. Saldremos de la funcion
            followPlayer = false;

            // El enemigo puede atacar al jugador 
            attackPlayer = true;
        }
    }

    // Funcion de ataque del enemigo
    void Attack()
    {
        // Checar si el enemigo no puede atacar al jugador
        if (!attackPlayer)
        {
            // Si esto se cumple saldremos de la funcion
            return;
        }

        // Todo esto ocurre si el enemigo puede atacar al jugador

        // Aumentar el tiempo de ataque actual usando el tiempo
        currentAttackTime += Time.deltaTime;

        // Evaluar si el tiempo de ataque actual es mayor al tiempo 
        if (currentAttackTime > defaultAttackTime)
        {
            // Si es el caso, el enemigo atacara de forma aleatoria
            // Esto usa las animaciones de ataque del enemigo y el indicee que determina el ataque
            enemyAnim.EnemyAttack(Random.Range(0, 2));

            // Reseteo del tiempo actual de ataque
            currentAttackTime = 0f;
        }

        // Evaluar si la distancia entre el enemigo y el jugador es mayor a la distancia de ataque
        // Aqui vamos a a�adir la distancia de persecucion para tener mas espacio para huir
        if(Vector3.Distance(transform.position, playerTarget.position) > attackDistance + chasePlayerAfterAttack)
        {
            // Dar un espacio al jugador antes de ser perseguido
            attackPlayer = false;

            // El enemigo sigue al jugador
            followPlayer = true;
        }
    }
}
