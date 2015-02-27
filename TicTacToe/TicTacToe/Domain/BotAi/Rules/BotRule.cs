using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe.Domain.BotAi.Rules
{
    public abstract class BotRule
    {
        protected readonly BotProcessor Processor;
        protected Random Random = new Random();

        protected BotRule(BotProcessor processor)
        {
            Processor = processor;
        }

        protected List<MoveLocation> MadeMoves
        {
            get { return Processor.Moves.ConvertAll(x => x.Location); }
        }

        protected List<MoveLocation> NotMadeMoves
        {
            get { return Constants.AllLocations.Except(MadeMoves).ToList(); }
        }

        protected List<MoveLocation> EnemyMoves
        {
            get
            {
                return Processor.Moves
                    .Where(x => x.Player.Id == Processor.Game.PlayerWhoLastMoved.Id)
                    .Select(x => x.Location)
                    .ToList();
                ;
            }
        }

        protected List<MoveLocation> BotMoves
        {
            get
            {
                return Processor.Moves
                    .Where(x => x.Player.Id == Processor.Game.PlayerWhoMoves.Id)
                    .Select(x => x.Location)
                    .ToList();
                ;
            }
        }

        public abstract bool CheckCondition();
        public abstract MoveLocation CalcLocation();
    }
}