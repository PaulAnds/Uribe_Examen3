using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BEU_CharacterAnimationDelegate : MonoBehaviour
{
    public string nextLevel;
    //Variables Publicas
    public GameObject left_Arm_AttackPoint, right_Arm_AttackPoint, left_Leg_AttackPoint, right_Leg_AttackPoint; //Puntos de ataque
    
    public float standUpTimer = 2f; //Temporizador para que el personaje se pueda poner de pie

    //Variables Privadas
    private BEU_CharacterAnimation animationScript;

    private AudioSource audioSource;

    [SerializeField] private AudioClip wooshSound, fallSound, groundHitSound, deadSound;

    public BEU_EnemyMovement enemyMovement;
    public CapsuleCollider colisionador;
    public Rigidbody rb;
    public BEU_PlayerAttack jugador;
    private void Awake()
    {
        //Iniccializaci�n de Referencia
        animationScript = GetComponent<BEU_CharacterAnimation>();

        audioSource = GetComponent<AudioSource>();

        if (gameObject.CompareTag(BEU_Tags.ENEMY_TAG))
        {
            enemyMovement = GetComponentInParent<BEU_EnemyMovement>();
        }
    }

    //Funci�n para activar punto de ataque de mano izquierda
    void LeftArmAttack_ON()
    {
        //Activar punto de ataque de mano izquierda
        left_Arm_AttackPoint.SetActive(true);
    }

    //Funci�n para desactivar punto de ataque de mano izquierda
    void LeftArmAttack_Off()
    {
        //Checar si el punto de ataque se encuentra activo en jerarquia
        if(left_Arm_AttackPoint.activeInHierarchy)
        {
            //Si es el caso, desactiva punto de ataque de la mano izquirda
            left_Arm_AttackPoint.SetActive(false);
        }
    }

    //Funci�n para activar punto de ataque de mano derecha
    void RightArmAttack_ON()
    {
        //Activar punto de ataque de mano rigth
        right_Arm_AttackPoint.SetActive(true);
    }

    //Funci�n para desactivar punto de ataque de mano dercha
    void RightArmAttack_Off()
    {
        //Checar si el punto de ataque se encuentra activo en jerarquia
        if (right_Arm_AttackPoint.activeInHierarchy)
        {
            //Si es el caso, desactiva punto de ataque de la mano derecha
            right_Arm_AttackPoint.SetActive(false);
        }
    }

    //Funci�n para activar punto de ataque de pierna izquierda
    void LeftLegAttack_ON()
    {
        //Activar punto de ataque de pierna izquierda
        left_Leg_AttackPoint.SetActive(true);
    }

    //Funci�n para desactivar punto de ataque de pierna izquierda
    void LeftLegAttack_Off()
    {
        //Checar si el punto de ataque se encuentra activo en jerarquia
        if (left_Leg_AttackPoint.activeInHierarchy)
        {
            //Si es el caso, desactiva punto de ataque de la pierna izquirda
            left_Leg_AttackPoint.SetActive(false);
        }
    }

    //Funci�n para activar punto de ataque de pierna dercha
    void RightLegAttack_ON()
    {
        //Activar punto de ataque de pierna derecha
        right_Leg_AttackPoint.SetActive(true);
    }

    //Funci�n para desactivar punto de ataque de pierna dercha
    void RightLegAttack_Off()
    {
        //Checar si el punto de ataque se encuentra activo en jerarquia
        if (right_Leg_AttackPoint.activeInHierarchy)
        {
            //Si es el caso, desactiva punto de ataque de la pierna dercha
            right_Leg_AttackPoint.SetActive(false);
        }
    }

    //Funci�n para asignar el tag de brazo izquierdo
    void Tag_LeftArmm()
    {
        //Asignar el tag de brazo izquierdo al punto su punto de ataque
        left_Arm_AttackPoint.tag = BEU_Tags.LEFT_ARM_TAG;
    }

    //Funci�n para quitar el tag de brazo izquierdo
    void Untag_Left_Arm()
    {
        //Quitar asignaci�n de tag al brazo izquierdo
        left_Arm_AttackPoint.tag = BEU_Tags.UNTAGGED_TAG;
    }

    //Funci�n para signar el tag de pierna izquierda
    void Tag_Left_Leg()
    {
        //asignar el tag de pierna izquierda a su punto de ataque
        left_Leg_AttackPoint.tag = BEU_Tags.LEFT_LEG_TAG;
    }

    //Funci�n para quitar el tag de pierna izquierda
    void Untag_Left_Leg()
    {
        //Quitar asignaci�n de tag a la pierna izquierda
        left_Leg_AttackPoint.tag = BEU_Tags.UNTAGGED_TAG;
    }

    //Funci�n para que el enemigo se ponga de pie
    void EnemyStandUp()
    {
        //Llamar a la corrutina que levanta al enemigo
        StartCoroutine(StandUpAfertTimeCo());
    }
    
    void TagNoqueador()
    {
        //asignar el tag de pierna izquierda a su punto de ataque
        left_Leg_AttackPoint.tag = BEU_Tags.NoqueadorTAG;
        left_Arm_AttackPoint.tag = BEU_Tags.NoqueadorTAG;
    }

    //Funci�n para quitar el tag de pierna izquierda
    void UntagNoqueador()
    {
        //Quitar asignaci�n de tag a la pierna izquierda
        left_Leg_AttackPoint.tag = BEU_Tags.UNTAGGED_TAG;
        left_Arm_AttackPoint.tag = BEU_Tags.UNTAGGED_TAG;
    }

    //Corrutina que levanta al enemigo
    IEnumerator StandUpAfertTimeCo()
    {
        //Pausa que usa el temporizador
        yield return new WaitForSeconds(standUpTimer);

        //Llamar al metodo StandUp del script de animaciones
        animationScript.StandUp();
    }

    public void AttackSound()
    {
        audioSource.volume = 0.2f;
        audioSource.clip = wooshSound;
        audioSource.Play();
    }
    void KnockDown()
    {
        audioSource.clip = fallSound; 
        audioSource.Play();
    }
    
    void HitGround()
    {
        audioSource.clip = groundHitSound; 
        audioSource.Play();
    }

    void DisableMovement()
    {
        enemyMovement.enabled = false;

        transform.parent.gameObject.layer = 0;
    }
    
    void EnabledMovement()
    {
        enemyMovement.enabled = true;
        
        transform.parent.gameObject.layer = 7;
    }

    void CharacterDied()
    {
        audioSource.volume = 1f;
        audioSource.clip = deadSound;
        audioSource.Play();
        SceneManager.LoadScene(enemyMovement.nextLevel);
        Invoke("DesactivarGameObject", 5f);
    }
    
    void PlayerDied()
    {
        audioSource.volume = 1f;
        audioSource.clip = deadSound;
        audioSource.Play();
        SceneManager.LoadScene(nextLevel);
    }

    void noAtacar()
    {
        jugador.enabled = false;
        enemyMovement.attackPlayer = false;
    }
    void DesactivarGameObject()
    {
        colisionador.enabled = false;
        rb.isKinematic = true;
    }
}



