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

        public static int GetPercentage(float value, float maxValue) => Mathf.RoundToInt(value / maxValue * 100);
    }
}