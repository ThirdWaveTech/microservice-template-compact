using System;

namespace __NAME__.App.Infrastructure.Diagnostics
{
    public class MigrationRecord
    {
        public virtual long Version { get; set; }
        public virtual DateTime? AppliedOn { get; set; }
        public virtual string Description { get; set; }

        public override string ToString()
        {
            return $"Version {Version} applied on {AppliedOn}: {Description}";
        }
    }
}
