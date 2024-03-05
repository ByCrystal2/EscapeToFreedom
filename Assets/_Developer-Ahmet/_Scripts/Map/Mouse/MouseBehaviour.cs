using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MouseBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    RandomPositionInSphere myRandomPositionCalculator;
    private NavMeshAgent _agent; // NPC'nin hareketini kontrol eden bileþen
    private Rigidbody _rb;
    private BoxCollider _boxCollider;

    private bool isJump = false;
    private float jumpMaxValue = 3f; // Zýplama aralýðý (örneðin 5 saniye)
    private float jumpTime = 0f; // Zýplama aralýðý (örneðin 5 saniye)
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
            // Eðer NPC hedefine ulaþtýysa yeni bir rastgele konum belirle
            Vector3 randomPosition = myRandomPositionCalculator.GetRandomPositionInSphere(transform);
            _agent.SetDestination(randomPosition);
        }
        jumpTimer += Time.deltaTime;
        
        if (jumpTimer >= jumpTime && !isJump)
        {
            // Belirli bir süre geçtiðinde ve zaten zýplamýyorsa zýpla
            Jump();
            // Zýplama iþleminden sonra sayaçý sýfýrla
            jumpTimer = 0f;
        }
    }
    void Jump()
    {
        jumpTime = Random.Range(1f, jumpMaxValue);
        isJump = true;
        // Yüksekliði ayarlayýn, burada sadece örnek bir yükseklik eklenmiþtir
        float jumpForce = Random.Range(2, 5);
        transform.position += Vector3.up * jumpForce;
        // Zýplama tamamlandýktan sonra tekrar zýplamayý etkinleþtir
        StartCoroutine(ResetJump());
    }
    IEnumerator ResetJump()
    {
        yield return new WaitForSeconds(1f); // Zýplama süresi
        
        isJump = false;
    }

}
public class RandomPositionInSphere
{
    public float sphereRadius = 5f; // Kürenin yarýçapý

    // Kürenin içerisinde rastgele bir konum üreten fonksiyon
    public Vector3 GetRandomPositionInSphere(Transform _who)
    {
        Vector3 randomDirection = Random.insideUnitSphere.normalized;
        Vector3 randomPosition = _who.position + randomDirection * Random.Range(0f, sphereRadius);
        return randomPosition;
    }
}

