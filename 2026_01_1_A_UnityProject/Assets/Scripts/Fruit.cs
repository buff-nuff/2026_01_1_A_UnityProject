using Unity.VisualScripting;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public int fruitType;
    public bool hasMerged = false;

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (hasMerged)
            return;

            Fruit otherFruit = collision.gameObject.GetComponent<Fruit>();

            if (otherFruit != null && !otherFruit.hasMerged && otherFruit.fruitType == fruitType)
            {
                hasMerged = true;
                Vector3 mergePosition = (transform.position + otherFruit.transform.position) / 2f;

            Fruitgame gameManager = FindAnyObjectByType<Fruitgame>();

            if (gameManager != null)
            {
                gameManager.MergeFruits(fruitType, mergePosition);
            }
            {
                
            }

            Destroy(otherFruit.gameObject);
                Destroy(gameObject);
            }
        
    }

}
