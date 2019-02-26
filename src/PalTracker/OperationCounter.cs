using System.Collections.Generic;
using System.Collections.Immutable;

namespace PalTracker{
    public class OperationCounter<T> : IOperationCounter<T>
    {
        private readonly IDictionary<TrackedOperation, int> count;
        public IDictionary<TrackedOperation, int> GetCounts => count.ToImmutableDictionary(); 

        public string Name => $"{typeof(T).Name}Operations";

        public OperationCounter()
        {
            count = new Dictionary<TrackedOperation,int>();
            foreach(var action in System.Enum.GetValues(typeof(TrackedOperation))){
                count.Add((TrackedOperation)action,0);
            }            
        }

        public void Increment(TrackedOperation operation)
        {
            count[operation] += 1;
        }
    }
}