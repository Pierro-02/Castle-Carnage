using UnityEngine;

public class Destination : MonoBehaviour {

    private const string OBJECT_LAYER = "Enemy";

    private void OnTriggerEnter(Collider collider) {
        int damage = collider.GetComponent<Enemy>().GetDamage();
        HealthManager.LifeLost(damage);
        Debug.Log(collider.name + " should be deleted");
    }

    private void OnTriggerStay(Collider collider) {
        if (collider.gameObject.tag == OBJECT_LAYER) {
            collider.GetComponent<Enemy>().Escaped();
            SoundSystem.PlayLifeLost();
        }
    }
}
