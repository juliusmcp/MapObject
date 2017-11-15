using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapObject.DataTypes
{
    public class RegisteredMappingKey
    {

        public Type To{ get; private set; }

        public Type From { get; private set; }
        public string NamedMapping { get; private set; }

        public RegisteredMappingKey(Type From,Type To, string NamedMapping = "")
        {
            this.To = To;
            this.From = From;
            this.NamedMapping = NamedMapping;
        }
        public RegisteredMappingKey(Type To, string NamedMapping="")
        {
            this.To= To;
            this.NamedMapping = NamedMapping;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var other = obj as RegisteredMappingKey;

            if (ReferenceEquals(this, other))
                return true;

            if (other == null)
                return false;

            return To == other.To && string.Equals(this.NamedMapping, other.NamedMapping, StringComparison.InvariantCultureIgnoreCase);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                const int multiplier = 31;
                int hash = GetType().GetHashCode();

                hash = hash * multiplier + this.To.GetHashCode();
                hash = hash * multiplier + (NamedMapping == null ? 0 : NamedMapping.GetHashCode());

                return hash;
            }
        }

    }
}
