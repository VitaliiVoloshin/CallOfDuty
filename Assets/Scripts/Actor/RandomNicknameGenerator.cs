using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShooterFeatures
{
    public class RandomNicknameGenerator
    {
        public string GetRandomNickname()
        {
            string[] nicknameParts1 = new string[] { "Ge", "Me", "Ta", "Bo", "Ke", "Ra", "Ne", "Mi" };
            string[] nicknameParts2 = new string[] { "oo", "ue", "as", "to", "ra", "me", "io", "so" };
            string[] nicknameParts3 = new string[] { "se", "matt", "lace", "fo", "cake", "end" };
            string nicknamePart1 = nicknameParts1[Random.Range(0, nicknameParts1.Length)];
            string nicknamePart2 = nicknameParts2[Random.Range(0, nicknameParts2.Length)];
            string nicknamePart3 = nicknameParts3[Random.Range(0, nicknameParts3.Length)];
            return nicknamePart1 + nicknamePart2 + nicknamePart3;
        }
    }
}
