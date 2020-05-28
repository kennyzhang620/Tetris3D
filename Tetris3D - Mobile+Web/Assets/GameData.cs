using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSettings
{
    public class PlayerStats
    {
        private string Username = "User";
        private int Score = 0;
        private int UID;

        // Initalizes the class
        public PlayerStats()
        {
            var rand = new System.Random();
            UID = rand.Next(0, 2147483647);
        }

        public PlayerStats(string User)
        {
            Username = User;
            var rand = new System.Random();
            UID = rand.Next(0, 2147483647);
        }

        public void AddScore()
        {
            Score++;
        }

        public int ViewScore()
        {
            return Score;
        }

        public void SubtractScore()
        {
            Score--;
        }

        public string GetUser()
        {
            return Username;
        }

        public void SetUser(string user)
        {
            Username = user;
        }

        public void GenerateUID()
        {
            var rand = new System.Random();
            UID = rand.Next(0, 2147483647);
        }

        public int GetScore()
        {
            return Score;
        }

        public void ResetScore()
        {
            Score = 0;
        }
    };
    public class GameData : MonoBehaviour
    {
        public static int SpawnerMode = 0;
        public static int ClearLevel = -1;
        public static int ClearLines = 0;
        public static int ClearLinesA = 0;
        public static PlayerStats CurrentUser = new PlayerStats("Unknown");
        public static int Level = 0;
        public static float prevX;
        public static RigidbodyConstraints defaultcst = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
        public static Vector3 defaultCoords;
        public static bool PhysicsOn = true;
        public static int inX;
        public static int inY;
        public static int inZ;

        public static float touchX;
        public static int IterationCount = 0;

        public static bool ScanLines;
        public static List<PlayerStats> Leaderboards;




    }
}
