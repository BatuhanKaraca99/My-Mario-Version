using UnityEngine;

public static class Extensions 
{
    private static LayerMask layerMask = LayerMask.GetMask("Default");
    public static bool Raycast(this Rigidbody2D rigidbody,Vector2 direction)
    {
        if (rigidbody.isKinematic) // Physics Object is not controlling
        {
            return false;
        }

        float radius = 0.25f;
        float distance = 0.375f;

        RaycastHit2D hit = Physics2D.CircleCast(rigidbody.position, radius, direction.normalized, distance,layerMask);
        return hit.collider != null && hit.rigidbody != rigidbody; //not our rigidbody
        //return true if there is the object we hit it's collider
    }

    public static bool DotTest(this Transform transform, Transform other, Vector2 testDirection)
    {
        Vector2 direction = other.position - transform.position; //Colliding Object - Character
        return Vector2.Dot(direction.normalized, testDirection) > 0.25f; //Almost 0
    }
}
