using System;
using System.Collections.Generic;
using System.Linq;

namespace ShortcutRunner
{
    public interface IShortcutCollection
    {
        void Add(ShortcutDescription shortcutDescription, Action action);
        IEnumerable<Action> GetActions(ShortcutDescription shortcutDescription);
    }

    public class ShortcutCollection : IShortcutCollection
    {
        private readonly IList<KeyValuePair<ShortcutDescription, Action>> _actions =
            new List<KeyValuePair<ShortcutDescription, Action>>();

        public void Add(ShortcutDescription shortcutDescription, Action action)
        {
            _actions.Add(new KeyValuePair<ShortcutDescription, Action>(shortcutDescription, action));
        }

        public IEnumerable<Action> GetActions(ShortcutDescription shortcutDescription)
        {
            return _actions
                .Where(k => shortcutDescription.Equals(k.Key))
                .Select(k => k.Value)
                .ToArray();
        }
    }
}