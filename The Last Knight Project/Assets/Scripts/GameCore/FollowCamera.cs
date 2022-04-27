using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.GameCore
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] Transform target;

        //Using LateUpdate to make sure that the camera will always wait for the player move
        //to then start to follow
        void LateUpdate()
        {
            transform.position = target.position;
        }
    }
}
