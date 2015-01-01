using System;
using System.Collections.Generic;
using System.Linq;
using ShortcutRunner.HotkeyRegistration;

namespace ShortcutRunner.ShortcutManagement
{
    public interface IShortcutCollection
    {
        void Add(ShortcutDescription shortcutDescription, Action action);
        IEnumerable<Action> GetActions(ShortcutDescription shortcutDescription);
    }

    public class ShortcutCollection : IShortcutCollection
    {
        private readonly IDictionary<ShortcutDescription, IList<Action>> _actions =
            new Dictionary<ShortcutDescription, IList<Action>>();

        public void Add(ShortcutDescription shortcutDescription, Action action)
        {
            if (_actions.ContainsKey(shortcutDescription))
            {
                _actions[shortcutDescription].Add(action);
            }
            else
            {
                _actions.Add(shortcutDescription, new List<Action> { action });
            }
        }

        public IEnumerable<Action> GetActions(ShortcutDescription shortcutDescription)
        {
            return _actions.ContainsKey(shortcutDescription)
                ? _actions[shortcutDescription].ToArray()
                : new Action[] {};
        }
    }
}