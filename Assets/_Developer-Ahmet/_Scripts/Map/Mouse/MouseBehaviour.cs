using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MouseBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    RandomPositionInSphere myRandomPositionCalculator;
    private NavMeshAgent _agent; // NPC'nin hareketini kontrol eden bile�en
    private Rigidbody _rb;
    private BoxCollider _boxCollider;

    private bool isJump = false;
    private float jumpMaxValue = 3f; // Z�plama aral��� (�rne�in 5 saniye)
    private float jumpTime = 0f; // Z�plama aral��� (�rne�in 5 saniye)
    private float jumpTimer = 0f;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _rb = GetComponent<Rigidbody>();
        myRandomPositionCalculator = new RandomPositionInSphere();
        jumpTime = Random.Range(1f, jumpMaxValue);
    }

    void Update()
    {
        if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
        {
            // E�er NPC hedefine ula�t�ysa yeni bir rastgele konum belirle
            Vector3 randomPosition = myRandomPositionCalculator.GetRandomPositionInSphere(transform);
            _agent.SetDestination(randomPosition);
        }
        jumpTimer += Time.deltaTime;
        
        if (jumpTimer >= jumpTime && !isJump)
        {
            // Belirli bir s�re ge�ti�inde ve zaten z�plam�yorsa z�pla
            Jump();
            // Z�plama i�leminden sonra saya�� s�f�rla
            jumpTimer = 0f;
        }
    }
    void Jump()
    {
        jumpTime = Random.Range(1f, jumpMaxValue);
        isJump = true;
        // Y�ksekli�i ayarlay�n, burada sadece �rnek bir y�kseklik eklenmi�tir
        float jumpForce = Random.Range(2, 5);
        transform.position += Vector3.up * jumpForce;
        // Z�plama tamamland�ktan sonra tekrar z�plamay� etkinle�tir
        StartCoroutine(ResetJump());
    }
    IEnumerator ResetJump()
    {
        yield return new WaitForSeconds(1f); // Z�plama s�resi
        
        isJump = false;
    }

}
public class RandomPositionInSphere
{
    public float sphereRadius = 5f; // K�renin yar��ap�

    // K�renin i�erisinde rastgele bir konum �reten fonksiyon
    public Vector3 GetRandomPositionInSphere(Transform _who)
    {
        Vector3 randomDirection = Random.insideUnitSphere.normalized;
        Vector3 randomPosition = _who.position + randomDirection * Random.Range(0f, sphereRadius);
        return randomPosition;
    }
}

