using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoodManager
{

    public static class Calculate

    {

        //Take all the values as a float array and return the sum
        public static float ProductValue(params float[] ingredients)
        {
            float sum = 0;

            for (int i = 0; i < ingredients.Length; i++)
            {
                sum += ingredients[i];
            }
            return sum;
        }

    }
}
