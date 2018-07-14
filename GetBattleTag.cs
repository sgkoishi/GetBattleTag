using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Plugins;

namespace GetBattleTag {
    public class GetBattleTag : IPlugin {
        private const string HearthstoneLogsPath = @"C:\Program Files (x86)\Hearthstone\Logs\Power.log";

        public void OnLoad() { }
        public void OnUnload() { }
        public void OnUpdate() { }

        public void OnButtonPress() {
            CopyOpponentBattleTag();
        }

        public string Name => "Get BattleTag";
        public string Description => "Get BattleTag of your current/last opponent.";
        public string ButtonText => "Get BattleTag";
        public string Author => "KimTranjan & Kno010";
        public Version Version => new Version(1, 0, 0);

        public MenuItem MenuItem {
            get {
                var menu = new MenuItem {
                    Header = "Get BattleTag"
                };

                menu.Click += (sender, e) => { CopyOpponentBattleTag(); };

                return menu;
            }
        }

        private static void CopyOpponentBattleTag() {
            if (string.IsNullOrEmpty(Core.Game.Player.Name)) {
                MessageBox.Show("You must play at least one game to get your last opponent's BattleTag.");

                return;
            }

            Clipboard.SetText(GetLastOpponentBattleTag());
        }

        private static string GetLastOpponentBattleTag() {
            var opponents = new List<string>();

            using (var fs = File.Open(HearthstoneLogsPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var bs = new BufferedStream(fs))
            using (var sr = new StreamReader(bs)) {
                string line;

                while ((line = sr.ReadLine()) != null) {
                    if (line.Contains("GameState.DebugPrintGame() - PlayerID=") && !line.Contains(Core.Game.Player.Name)) {
                        opponents.Add(line);
                    }
                }
            }

            return Regex.Match(opponents[opponents.Count - 1], "(?<=PlayerName=).*").Value;
        }
    }
}
