using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe.Domain
{
    public static class Constants
    {
        private static readonly List<List<MoveLocation>> _winCoditions = new List<List<MoveLocation>>
        {
            new List<MoveLocation> {MoveLocation.TopLeft, MoveLocation.TopCenter, MoveLocation.TopRight},
            new List<MoveLocation> {MoveLocation.CenterLeft, MoveLocation.Center, MoveLocation.CenterRight},
            new List<MoveLocation> {MoveLocation.BottomLeft, MoveLocation.BottomCenter, MoveLocation.BottomRight},
            new List<MoveLocation> {MoveLocation.TopLeft, MoveLocation.CenterLeft, MoveLocation.BottomLeft},
            new List<MoveLocation> {MoveLocation.TopCenter, MoveLocation.Center, MoveLocation.BottomCenter},
            new List<MoveLocation> {MoveLocation.TopRight, MoveLocation.CenterRight, MoveLocation.BottomRight},
            new List<MoveLocation> {MoveLocation.TopLeft, MoveLocation.Center, MoveLocation.BottomRight},
            new List<MoveLocation> {MoveLocation.BottomLeft, MoveLocation.Center, MoveLocation.TopRight}
        };

        private static readonly List<MoveLocation> _allLocations = Enum.GetValues(typeof (MoveLocation))
            .Cast<MoveLocation>()
            .ToList();

        public static List<List<MoveLocation>> WinConditions
        {
            get { return _winCoditions; }
        }

        public static List<MoveLocation> AllLocations
        {
            get { return _allLocations; }
        }
    }
}