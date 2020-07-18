using System.Linq;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public void DestroyTree()
    {
        if (GetComponent<Palm>() != null)
        {
            Destroy(gameObject);
            return;
        }

        Collider[] trees = Physics.OverlapSphere(transform.position, Palm.SacrificeRadius, 1 << LayerMask.NameToLayer("Plant"));
        var palm = trees
            .Where(x => x.GetComponent<Palm>() != null)
            .OrderBy(x => (x.transform.position - transform.position).sqrMagnitude)
            .FirstOrDefault();

        SowingManager.Instance.AddSeeds(2);

        if (palm != null)
            Destroy(palm.gameObject);
        else
            Destroy(gameObject);
    }
}
