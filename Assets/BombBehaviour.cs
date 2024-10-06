using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehaviour : MonoBehaviour
{
    public GameObject explosionParticle;
    private Vector3 explosionPos;
    public float explosionCycle;
    public int explosionCount;

    public Vector3 explosionSize;

    private BoxCollider bc;

    private void Start()
    {
        explosionPos = transform.position;
        bc = GetComponent<BoxCollider>();
    }

    private void OnEnable() => StartExplosionCycle();
    private void StartExplosionCycle() => StartCoroutine(Explode());

    private IEnumerator Explode() {
        for (; explosionCount > 0; explosionCount--) {
            yield return new WaitForSeconds(explosionCycle-0.2f);
            Explosion();
            yield return new WaitForSeconds(0.2f);
            Rearm();
        }
        gameObject.SetActive(false);
    }

    private void Explosion() {
        ParticleSystem ps = Instantiate(explosionParticle, explosionPos, Quaternion.identity).GetComponent<ParticleSystem>();
        ps.Play();
        bc.enabled = true;
        bc.size = explosionSize;
    }
    private void Rearm() {
        bc.enabled = false;
    }
}
