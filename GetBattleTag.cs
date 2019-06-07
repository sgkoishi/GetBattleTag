using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Plugins;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace GetBattleTag
{
    public class GetBattleTag : IPlugin
    {
        private static string _LastPlayerBattleTag;
        public string Name => "Get BattleTag";

        public string Description => "Get BattleTag of your opponent.";

        public string ButtonText => "Get BattleTag";

        public string Author => "SGKoishi";

        public Version Version => Assembly.GetExecutingAssembly().GetName().Version;

        public MenuItem MenuItem
        {
            get
            {
                var menu = new MenuItem
                {
                    Header = "Get BattleTag"
                };
                menu.Click += (s, e) => this.OnButtonPress();
                return menu;
            }
        }

        public void OnButtonPress()
        {
            Clipboard.SetText(_LastPlayerBattleTag);
        }

        public void OnLoad()
        {
        }

        public void OnUnload()
        {
        }

        public void OnUpdate()
        {
            if (!string.IsNullOrWhiteSpace(Core.Game.Opponent.Name))
            {
                _LastPlayerBattleTag = Core.Game.Opponent.Name;
            }
        }
    }
}
