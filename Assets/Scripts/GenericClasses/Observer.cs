using System;

namespace DevelopersWork.ChestSystem.GenericClasses
{
    public abstract class Observer<T>: Singleton<T> where T : Observer<T>
    {
        static event Action ObserverQueue;

        public void AddListener(Action listener)
        {
            ObserverQueue += listener;
        }

        public void RemoveListener(Action listener)
        {
            ObserverQueue -= listener;
        }

        public void TriggerEvent()
        {
            ObserverQueue?.Invoke();
        }
    }
}

