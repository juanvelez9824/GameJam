using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   
 
        private Rigidbody2D rigidBody;
        public float speed = 10;
        public float jumpForce = 2;
        // Start is called before the first frame update
        void Start()
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            Vector2 dir = new Vector2(x, y);

            walk(dir);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

        }

        private void walk(Vector2 dir)
        {
            rigidBody.velocity = new Vector2(dir.x * speed, rigidBody.velocity.y);
        }

        private void Jump()
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
            rigidBody.velocity += Vector2.up * jumpForce;
        }
    
}
