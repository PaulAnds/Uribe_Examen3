using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BEU_CharacterAnimation : MonoBehaviour
{
    // Variables privadas
    private Animator anim; // Referencia del componente animator del componente

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Metodo para animar movimiento
    public void Walk(bool _move)
    {
        // Cambiar el valor del parametro movement del animator
        anim.SetBool(BEU_AnimationTags.MOVEMENT, _move);
    }

    // Metodo para animar primer golpe
    public void Punch_1()
    {
        //  Activar trigger Punch1 del animator
        anim.SetTrigger(BEU_AnimationTags.PUNCH_1_TRIGGER);
    }

    // Metodo para animar segundo golpe
    public void Punch_2()
    {
        //  Activar trigger Punch2 del animator
        anim.SetTrigger(BEU_AnimationTags.PUNCH_2_TRIGGER);
    }

    // Metodo para animar tercer golpe
    public void Punch_3()
    {
        //  Activar trigger Punch3 del animator
        anim.SetTrigger(BEU_AnimationTags.PUNCH_3_TRIGGER);
    }

    // Metodo para animar primera patada
    public void Kick_1()
    {
        //  Activar trigger Kick1 del animator
        anim.SetTrigger(BEU_AnimationTags.KICK_1_TRIGGER);
    }

    // Metodo para animar segunda patada
    public void Kick_2()
    {
        //  Activar trigger Kick2 del animator
        anim.SetTrigger(BEU_AnimationTags.KICK_2_TRIGGER);
    }

    // Animaciones de ataque
    public void EnemyAttack(int _attack)
    {
        // Activar triggers del animator basados en un indice de ataque
        if (_attack == 0)
        {
            // Activar el trigger del primer ataque el enemigo
            anim.SetTrigger(BEU_AnimationTags.ATTACK_1_TRIGGER);
        }

        if (_attack == 1)
        {
            // Activar el trigger del segundo ataque el enemigo
            anim.SetTrigger(BEU_AnimationTags.ATTACK_2_TRIGGER);
        }

        if (_attack == 2)
        {
            // Activar el trigger del tercer ataque el enemigo
            anim.SetTrigger(BEU_AnimationTags.ATTACK_3_TRIGGER);
        }
    }

    // Animacion de Idle
    public void PlayIdleAnimation()
    {
        // Reproducir animacion Idle del Enemigo
        anim.Play(BEU_AnimationTags.IDLE_ANIMATION);
    }

    // Animaciones de Knockdown
    public void Knockdown()
    {
        // Activar el trigger knockdown del enemigo
        anim.SetTrigger(BEU_AnimationTags.KNOCKDOWN_TRIGGER);
    }

    // Animacion de StandUp
    public void StandUp()
    {
        // Activar el triger StandUp del enemigo
        anim.SetTrigger(BEU_AnimationTags.STAND_UP);
    }

    // Animacion de Hit
    public void Hit()
    {
        // Activar el trigger hit del enemigo
        anim.SetTrigger(BEU_AnimationTags.HIT_TRIGGER);
    }

    // Animacion de muerte
    public void Death()
    {
        // Activar el trigger Death del enemigo
        anim.SetTrigger(BEU_AnimationTags.DEATH_TRIGGER);
    }
}
