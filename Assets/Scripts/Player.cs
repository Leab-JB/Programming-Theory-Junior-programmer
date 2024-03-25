using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject playerTroop;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 screenPosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(screenPosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Restrict to instantiate troop inside the Enemy
                if(!hit.collider.CompareTag("Enemy"))
                    Instantiate(playerTroop, hit.point, Quaternion.identity);
            }

        }
    }


}
