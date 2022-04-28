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
        //using IAction as interface to Mover and Combat
        public void StartAction(IAction action)
        {
            //in case the action did not change
            if (currentAction == action) return;

            if(currentAction != null)
            {
                //change the action
                currentAction.Cancel();
            }
            //set the current action
            currentAction = action;

        }

        //Because Health should be able to let this script knows that the character is dead
        //so the Scheduler can cancel the action
        public void CancelCurrentAction()
        {
            //setting the current action as null has the effect to cancel 
            StartAction(null);
        }
    }   
}
