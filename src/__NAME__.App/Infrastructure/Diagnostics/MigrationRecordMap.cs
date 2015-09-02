using FluentNHibernate.Mapping;

namespace __NAME__.App.Infrastructure.Diagnostics
{
    class MigrationRecordMap: ClassMap<MigrationRecord>
    {
        public MigrationRecordMap()
        {
            Table("VersionInfo");
            Schema("dbo");
            LazyLoad();
            ReadOnly();
            Id(x => x.Version).Column("Version").Not.Nullable();
            Map(x => x.AppliedOn).Column("AppliedOn");
            Map(x => x.Description).Column("Description").Length(1024);
        }
    }
}
