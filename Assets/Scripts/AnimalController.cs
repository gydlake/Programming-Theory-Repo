using System.Collections;
using UnityEngine;

namespace EasyPrimitiveAnimals
{
    public class AnimalController : MonoBehaviour
    {
        // Leg and body object variables
        public GameObject FrontLegL;
        public GameObject FrontLegR;
        public GameObject RearLegL;
        public GameObject RearLegR;

        // Leg and body rotation variables
        private Vector3 legStartPosA = new Vector3(10.0f, 0f, 0f);
        private Vector3 legEndPosA = new Vector3(-10.0f, 0f, 0f);

        private Vector3 legStartPosB = new Vector3(-10.0f, 0f, 0f);
        private Vector3 legEndPosB = new Vector3(10.0f, 0f, 0f);

        private float rotSpeed;

        // Wander variables.
        public float moveAngle = 90f; // Define angle the animal turns after a collision.
        public float movSpeed = 1f; // Define speed that animal moves. This is also used to calculate leg movement speed.

        private bool canRotate = true;
        
        private void Start()
        {
                // Ensure child objects of legs are named EPA_FL, EPA_FR, EPA_RL and EPA_RR so the searches below can assign them to leg variables.
                FrontLegL = transform.Find("BaseAnimal").transform.Find("Legs").transform.Find("EPA_FL").gameObject;    // Find child object for front left leg.
                FrontLegR = transform.Find("BaseAnimal").transform.Find("Legs").transform.Find("EPA_FR").gameObject;    // Find child object for front right leg.
                RearLegL = transform.Find("BaseAnimal").transform.Find("Legs").transform.Find("EPA_RL").gameObject;    // Find child object for rear left leg.
                RearLegR = transform.Find("BaseAnimal").transform.Find("Legs").transform.Find("EPA_RR").gameObject;    // Find child object for rear right leg.

                rotSpeed = movSpeed * 4; // Set legs to move relative to animal moving speed.


            
        }

        private void Update()
        {

            SheepLegMovement();

            // Wander
            transform.Translate((Vector3.forward * Time.deltaTime) * movSpeed);

        }

        private void SheepLegMovement()
        {
            Quaternion legAngleFromA = Quaternion.Euler(this.legStartPosA);         // Set first start angle of leg.
            Quaternion legAngleToA = Quaternion.Euler(this.legEndPosA);             // Set first end angle of leg.

            Quaternion legAngleFromB = Quaternion.Euler(this.legStartPosB);         // Set second start angle of leg.
            Quaternion legAngleToB = Quaternion.Euler(this.legEndPosB);             // Set second end angle of leg.

            float lerp = 0.5f * (1.0f + Mathf.Sin(Mathf.PI * Time.realtimeSinceStartup * this.rotSpeed));

            FrontLegL.transform.localRotation = Quaternion.Lerp(legAngleFromA, legAngleToA, lerp);
            FrontLegR.transform.localRotation = Quaternion.Lerp(legAngleFromB, legAngleToB, lerp);

            RearLegL.transform.localRotation = Quaternion.Lerp(legAngleFromB, legAngleToB, lerp);
            RearLegR.transform.localRotation = Quaternion.Lerp(legAngleFromA, legAngleToA, lerp);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.CompareTag("Ground") && canRotate) // If the animal collides with something that is not the ground, spin it around.
            {
                StartCoroutine(SpinMeRound());
            }
        }

        private IEnumerator SpinMeRound()
        {
            // Disable option to rotate.
            canRotate = false;

            // Rotate animal.
            this.transform.rotation *= Quaternion.Euler(0, moveAngle, 0);

            // Wait...
            yield return new WaitForSeconds(1f);

            // Enable option to rotate.
            canRotate = true;
        }
    }
}
