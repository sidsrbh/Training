using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _speed = 5f;
    Animator animator;

    private void Awake()
    {
     //   animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        var horizonatal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizonatal, 0f, vertical);
        movement *= Time.deltaTime * _speed;
        transform.Translate(movement, Space.World);
      //  animator.SetFloat("VelocityX", horizonatal, 0.1f, Time.deltaTime);
       // animator.SetFloat("VelocityZ", vertical, 0.1f, Time.deltaTime);
    }
}
