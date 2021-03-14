using System;
using System.Collections.Generic;

namespace DotNetJobMap
{
    public interface IJobContainer
    {
        void Add(params IJob[] jobs);

        void Remove(params IJob[] jobs);

        bool TryGet(Type key, out IJob job);
    }

    public class JobContainer : IJobContainer
    {
        private readonly IJobIndexer _indexer;
        private readonly IDictionary<Type, IJob> _storage = new Dictionary<Type, IJob>();

        public JobContainer(IJobIndexer indexer)
            : this(indexer, null)
        {
        }

        public JobContainer(IJobIndexer indexer, IDictionary<Type, IJob> storage)
        {
            _indexer = indexer;
            _storage = storage ?? new Dictionary<Type, IJob>();
        }

        public void Add(params IJob[] jobs)
        {
            if (jobs != null)
            {
                foreach (var job in jobs)
                {
                    var reflector = _indexer.GetIndex(job);

                    _storage.Add(reflector.Type, job);
                }
            }
        }

        public void Remove(params IJob[] jobs)
        {
            if (jobs != null)
            {
                foreach (var job in jobs)
                {
                    var reflector = _indexer.GetIndex(job);

                    _storage.Remove(reflector.Type);
                }
            }
        }

        public bool TryGet(Type key, out IJob job)
        {
            return _storage.TryGetValue(key, out job);
        }
    }
}
