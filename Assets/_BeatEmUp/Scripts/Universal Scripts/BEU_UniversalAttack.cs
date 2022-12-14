using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BEU_UniversalAttack : MonoBehaviour
{
    //Variables Publicas
    public LayerMask collisionLater; //Later que evalua las colisiones de ataque 
    public float radius = 1f; //Radio de esfera de detecci�n
    public float damage = 2f; //Cantidad de da�o a realizar
    public bool isPlayer; //El jugador tiene este script Y/N?
    public bool isEnemy; //El enemigo tiene este script Y/N?
    public GameObject hitFXPrefab; //Prefab para efecto de imapcto

    private void Update()
    {
        //LLamar al metodo que detecta colisiones
        DetectColission();
    }

    //Metodo que detecta colisiones
    void DetectColission()
    {
        //Arreglo de colisiones que se forma por medio de una esfera hecha...
        //por fisicas que dececta objetos en cierto layer
        //Esta esfera es invisible en el juego
        Collider[] _hit = Physics.OverlapSphere(transform.position, radius, collisionLater);

        //Prueba de detecci�n
        if(_hit.Length > 0)
        {
            //Esto aplica si impactamos a un enemigo

            //Checar si el jugador es quien ataca
            if(isPlayer)
            {
                //Obtener la posicion del impacto una colision en el arreglo
                //Esta variable local nos ayudara a posicionar el FX de impacto
                Vector3 _hitFXPos = _hit[0].transform.position;

                //Posicion del FX de impacto en Y
                //Subir el efecto de impacto
                _hitFXPos.y += 1.3f;

                //Posici�n del efecto en X
                //Checar si el enemigo se encuentra volteando a la derecha
                if(_hit[0].transform.forward.x > 0)
                {
                    //Moveremos el FX de impacto en +X
                    _hitFXPos.x += 0.3f;
                }
                else if(_hit[0].transform.forward.x < 0)
                {
                    //Moveremos el FX de impacto en -X
                    _hitFXPos.x -= 0.3f;
                }

                //Instanciar las particulas de Impacto
                Instantiate(hitFXPrefab, _hitFXPos, Quaternion.identity);

                //Comparacion de Tags del GO del jugador
                if(gameObject.CompareTag(BEU_Tags.NoqueadorTAG))
                {
                    //Obtener el script de vida que este asignado al enemigo
                    //Esto es para bajarle vida
                    //El jugador activa el parametro knockdown
                    _hit[0].GetComponent<BEU_HealthScript>().ApplyDamage(damage, true);
                }
                else
                {
                    //Esto es si el ataque no cuenta con los tags contemplados
                    //llamar a la funci�n ApplpyDamage del script de vida del enemigo
                    //El jugador no activa el bool knockdown
                    _hit[0].GetComponent<BEU_HealthScript>().ApplyDamage(damage, false);
                }
            }
            else
            {
                //Obtener la posicion del impacto una colision en el arreglo
                //Esta variable local nos ayudara a posicionar el FX de impacto
                Vector3 _hitFXPos = _hit[0].transform.position;

                //Posicion del FX de impacto en Y
                //Subir el efecto de impacto
                _hitFXPos.y += 1.3f;

                //Posici�n del efecto en X
                //Checar si el enemigo se encuentra volteando a la derecha
                if (_hit[0].transform.forward.x > 0)
                {
                    //Moveremos el FX de impacto en +X
                    _hitFXPos.x += 0.3f;
                }
                else if (_hit[0].transform.forward.x < 0)
                {
                    //Moveremos el FX de impacto en -X
                    _hitFXPos.x -= 0.3f;
                }

                //Instanciar las particulas de Impacto
                Instantiate(hitFXPrefab, _hitFXPos, Quaternion.identity);

                //Comparacion de Tags del GO del jugador
                if (gameObject.CompareTag(BEU_Tags.NoqueadorTAG))
                {
                    //Obtener el script de vida que este asignado al enemigo
                    //Esto es para bajarle vida
                    //El jugador activa el parametro knockdown
                    _hit[0].GetComponent<BEU_HealthScript>().ApplyDamage(damage, true);
                }
                else
                {
                    //Esto es si el ataque no cuenta con los tags contemplados
                    //llamar a la funci�n ApplpyDamage del script de vida del enemigo
                    //El jugador no activa el bool knockdown
                    _hit[0].GetComponent<BEU_HealthScript>().ApplyDamage(damage, false);
                }
            }

            //Desactivar GO
            gameObject.SetActive(false);
        }
    }
}
