using System;

namespace FiaMaze.Types {
    public static class EnumExtensions {
        public static string String(this ActionMapType str) {
            return str switch {
                ActionMapType.Game => "Player",
                ActionMapType.HUD => "HUD",
                _ => "Player"
            };
        }
    }
}