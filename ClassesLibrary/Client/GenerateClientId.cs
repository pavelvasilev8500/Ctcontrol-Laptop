using System;

namespace ClassesLibrary.Client
{
    public static class GenerateClientId
    {
        public static string Id()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
