using System;
using System.Collections;
using UnityEngine;

namespace Common
{
    public static class Utils
    {
        public static IEnumerator MakeActionDelay(Action action, float time)
        {
            yield return new WaitForSeconds(time);
            action.Invoke();
        }
    }
}