using System;

namespace DotNetJobMap
{
    public interface IJobAddress : IEquatable<IJobAddress>
    {
    }

    public class JobAddress : IJobAddress
    {
        private readonly int _id;

        public JobAddress(int id)
        {
            _id = id;
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }

        public override bool Equals(object comparison)
        {
            if (!(comparison is JobAddress address))
            {
                return false;
            }

            return Equals(address);
        }

        public bool Equals(IJobAddress comparison)
        {
            if (!(comparison is JobAddress address))
            {
                return false;
            }

            return Equals(address);
        }

        public bool Equals(JobAddress comparison)
        {
            if (comparison == null)
            {
                return false;
            }

            return _id == comparison._id;
        }

        public override string ToString()
        {
            return $"{{ Address: {_id} }}";
        }
    }
}
