using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Kit.Data.Tools.Dispatcher
{
    public static class Dispatcher
    {
        private static List<KeyValuePair<IEvent, IEventListener>> list = new List<KeyValuePair<IEvent, IEventListener>>();

        public static void AddEventListener<TEvent, TEventListener>() where TEvent : IEvent where TEventListener : IEventListener
        {
            var newEvent = Activator.CreateInstance<TEvent>();
            var newEventListener = Activator.CreateInstance<TEventListener>();

            list.Add(new KeyValuePair<IEvent, IEventListener>(newEvent, newEventListener));
        }

        public static void Dispatch<T>(T event1) where T : IEvent
        {
            var eventType = typeof(T);

            var listenersToNotify = list.Where(t => t.Key.GetType() == eventType).Select(t => t.Value).ToList();

            foreach(var listener in listenersToNotify)
            {
                listener.HandleEvent();
            }
        }
    }
}
