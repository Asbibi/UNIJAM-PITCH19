using UnityEngine;

public class InteractionBalcon : MonoBehaviour
{
    [SerializeField] private InteractionBalcon otherPoint = null;

    public Vector3 GetOtherPointPosition()
    {
        if (otherPoint == null)
            return transform.position;
        else
            return otherPoint.transform.position;
    }
}
