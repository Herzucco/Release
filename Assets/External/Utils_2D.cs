using UnityEngine;

public static class Utils_2D {    

    public static void LookAt2D ( this Transform self, Transform target, Vector2 forward ) {
        LookAt2D( self, target.position, forward );
    }

    public static void LookAt2D ( this Transform self, Vector3 target, Vector2 forward ) {
        float forward_diff = forward == Vector2.up ? 90f : 0f;
        Vector3 direction = target - self.position;
        float angle = Mathf.Atan2( direction.y, direction.x ) * Mathf.Rad2Deg;
        self.rotation = Quaternion.AngleAxis( angle - forward_diff, Vector3.forward );
    }

}
