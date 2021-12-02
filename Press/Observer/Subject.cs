using System;
using System.Collections.Generic;

namespace Observer
{
    public class Subject : ISubject
    {
        public int State { get; set; } = -0;

        private List<IObserver> ListObservers = new();

        public void Attach(IObserver observer)
        {
            ListObservers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            ListObservers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in ListObservers)
            {
                observer.Update(this);
            }
        }

        public void logic()
        { 
        
        }
    }
}
