using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;
using UnityEngine.AI;

public class ShootingScript : MonoBehaviour
{
    public float shootGFXDelay = 0.1f;
    public float rayDistance = 16.0f;
    public Transform shootingPoint;
    //public LayerMask enemyMask;

     
    [SerializeField] private GameObject shootingEffect;
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private Transform scoreUI;
    [SerializeField] private AudioSource shootingSound;

    private LineRenderer lineRenderer;
    private Vector3 fireDirection;
    private float lastShootTime;
    private ScoreScript scoreScript;
    private EnemySound enemySound;


    private void Awake()
    {
        lineRenderer = transform.GetComponent<LineRenderer>();
    }

    private void Update() 
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Shoot();
        }

        if (Time.time - lastShootTime > shootGFXDelay) 
        {
            lineRenderer.enabled = false;
        }
    }

    private void Shoot()
    {
        lastShootTime = Time.time;

        shootingSound.Play();

        lineRenderer.enabled = true;
        fireDirection = transform.forward;
        GameObject effect1Ins = Instantiate(shootingEffect, shootingPoint.transform.position, Quaternion.identity);
        ParticleSystem particle1 = effect1Ins.GetComponent<ParticleSystem>();
        particle1.Play();
        Destroy(effect1Ins, 0.5f);

        Vector3 startPos = shootingPoint.transform.position;
        Vector3 endPos = transform.position + transform.forward * rayDistance;

        RaycastHit hit;

        if (Physics.Raycast(startPos, fireDirection, out hit, rayDistance))
        {
            //if (hit.transform.tag == "Enviroment") 
            //{
            //    GameObject effect2Ins = Instantiate(impactEffect, hit.transform.position, Quaternion.identity);
            //    ParticleSystem particle2 = effect2Ins.GetComponent<ParticleSystem>();
            //    particle2.Play();
            //    Destroy(effect2Ins, 0.5f);
            //    endPos = hit.point;
            //}
            if (hit.transform.tag == "Enemy")
            {
                enemySound = hit.transform.GetComponent<EnemySound>();
                enemySound.EnemyIsHit();
                GameObject effect2Ins = Instantiate(impactEffect, hit.transform.position, Quaternion.identity);
                ParticleSystem particle2 = effect2Ins.GetComponent<ParticleSystem>();
                particle2.Play();
                Destroy(effect2Ins, 0.5f);
                endPos = hit.point;
                int damageDealt = Random.Range(5, 15);


                //EnemyController enemyScript = GetComponent<EnemyController>();
                EnemyHealth healthScript = hit.transform.GetComponent<EnemyHealth>();
                Animator anim = hit.transform.GetComponent<Animator>();
                FollowPlayerNavMesh followPlayerNavMeshScript = hit.transform.GetComponent<FollowPlayerNavMesh>();
                Collider collider = hit.transform.GetComponent<Collider>();
                healthScript.TakeDamage(damageDealt);

                if (healthScript.currentHealth <= 0)
                {
                    enemySound.EnemyDies();
                    scoreUI.GetComponent<ScoreScript>().scoreValue += 5;
                    hit.transform.GetChild(1).gameObject.SetActive(false);
                    collider.gameObject.GetComponent<Collider>().enabled = false;
                    anim.SetTrigger("GetKilled");
                    followPlayerNavMeshScript.navMeshAgent.isStopped = true;
                    Destroy(hit.transform.gameObject, 1.5f);

                }
            }
        }

        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }

    
}
