using System;

namespace TicTacToe.Domain
{
    public sealed class Player
    {
        private readonly Guid _id = Guid.NewGuid();

        public Player(string name, bool isBot = false)
        {
            Name = name;
            IsBot = isBot;
        }

        public Guid Id
        {
            get { return _id; }
        }

        public string Name { get; private set; }
        public bool IsBot { get; private set; }
    }
}