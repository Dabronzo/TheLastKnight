using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.GameCore
{
    public class ActionScheduler : MonoBehaviour
    {
        //varible to store the current action
        IAction currentAction;


        //this method will stop an current action and start the new one
        //using MonoBehaviour to make as commom parent to Mover and Combat
        public void StartAction(IAction action)
        {
            //in case the action did not change
            if (currentAction == action) return;

            if(currentAction != null)
            {
                //change the action
                print("canceling" + currentAction);
                currentAction.Cancel();
            }
            //set the current action
            currentAction = action;

        }
    }   
}
