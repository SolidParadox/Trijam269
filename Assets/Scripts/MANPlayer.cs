using UnityEngine;

public class MANPlayer : MANEntity {
    public Transform visualRig;

    public RadarCore deathRadar;

  void LateUpdate () {
    mika.AddThrusterOutput ( new Vector2 ( Input.GetAxis ("Horizontal"), Input.GetAxis("Vertical") ) );
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f; // Ensure the mouse position is at the same depth as the player
        Vector2 direction = (mouseWorldPosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        visualRig.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (deathRadar.breached)
        {
            SceneCore.Instance.gameover = true;
            mika.enabled = false;
            enabled = false;
        }
    }

    private void FixedUpdate()
    {
        transform.rotation = visualRig.rotation;
    }
}
