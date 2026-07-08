using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public static class EventBus
    {
        private static readonly Dictionary<Type, Delegate> events = new();

        public static void Subscribe<T>(Action<T> listener) where T : IGameEvent
        {
            var eventType = typeof(T);
            events[eventType] = events.TryGetValue(eventType, out var existing)
                ? Delegate.Combine(existing, listener)
                : listener;
        }

        public static void Unsubscribe<T>(Action<T> listener) where T : IGameEvent
        {
            var eventType = typeof(T);
            if (!events.TryGetValue(eventType, out var existing)) return;

            var updated = Delegate.Remove(existing, listener);
            if (updated == null)
                events.Remove(eventType);
            else
                events[eventType] = updated;
        }

        public static void Publish<T>(T eventData) where T : IGameEvent
        {
            if (!events.TryGetValue(typeof(T), out var del)) return;

            foreach (var handler in del.GetInvocationList())
            {
                try
                {
                    ((Action<T>)handler).Invoke(eventData);
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                }
            }
        }

        public static void Clear() => events.Clear();
    }
}