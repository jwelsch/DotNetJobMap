using System;
using System.Collections.Generic;

namespace DotNetJobMap
{
    public interface IJobManager
    {
        int JobCount { get; }

        void AddJob(IJob job);

        bool RemoveJob(IJobAddress address);

        IJob GetJob(IJobAddress address);
    }

    public class JobManager : IJobManager
    {
        private readonly Dictionary<IJobAddress, IJob> _jobs = new Dictionary<IJobAddress, IJob>();

        public int JobCount => _jobs.Count;

        public void AddJob(IJob job)
        {
            if (_jobs.TryGetValue(job.Address, out IJob _))
            {
                throw new ArgumentException($"A job with address '{job.Address}' already exists.", nameof(job));
            }

            _jobs.Add(job.Address, job);
        }

        public bool RemoveJob(IJobAddress address)
        {
            return _jobs.Remove(address);
        }

        public IJob GetJob(IJobAddress address)
        {
            if (!_jobs.TryGetValue(address, out IJob job))
            {
                throw new ArgumentException($"A job with address '{address}' was not found.", nameof(address));
            }

            return job;
        }
    }
}
