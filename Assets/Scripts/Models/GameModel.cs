using System;

namespace Snake3D.Models
{
    [Serializable]
    public class GameModel
    {
        public float pizzaStayTime;
        public float resetGameTimer;
    }

    [Serializable]
    public static class GameTags
    {
        public const string snakeHeadTag = "SnakeHead";
        public const string wallsTag = "Walls";
        public const string pizzaTag = "Pizza";
    }

    [Serializable]
    public enum GameState
    {
        MENU,
        GAME,

    }
}

