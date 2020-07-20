using System;
using System.Linq;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private static int trees;
    public static int Trees
    {
        get => trees;
        private set
        {
            trees = value;
            OnTreesCountChanged.Invoke();
        }
    }

    public static event Action OnTreesCountChanged = delegate { };


    private void OnEnable() => Trees++;
    private void OnDisable() => Trees--;

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
