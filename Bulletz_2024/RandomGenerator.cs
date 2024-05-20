using System;

namespace Boidz
{
    static class RandomGenerator
    {
        private static Random rand;

        static RandomGenerator()
        {
            rand = new Random();
        }

        public static int GetRandomInt(int min, int max)
        {
            return rand.Next(min, max);
        }

        public static float GetRandomFloat()
        {
            return (float)rand.NextDouble();
        }

        public static float GetRandomFloat(int min, int max)
        {
            float r = (float)(rand.Next(min * 100, max * 100)) * 0.01f;
            return r;
        }
    }
}
