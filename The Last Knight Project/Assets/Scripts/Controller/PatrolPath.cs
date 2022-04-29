using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Controller
{
    public class PatrolPath : MonoBehaviour
    {
        [SerializeField] float sphereRadius = 0.5f;
        private void OnDrawGizmos()
        {
            //cicle on the waypoints on the childs
            for (int i =0; i < transform.childCount; i++)
            {
                int next = GetNextIndex(i);
                Gizmos.color = Color.white;
                Gizmos.DrawSphere(GetWaypont(i), sphereRadius);
                Gizmos.DrawLine(GetWaypont(i), GetWaypont(next));

            }
        }

        public Vector3 GetWaypont(int i)
        {
            return transform.GetChild(i).position;
        }

        public int GetNextIndex(int index)
        {
            if (index + 1 == transform.childCount)
            {
                return 0;
            }
            return index + 1;
        }
    }
}
