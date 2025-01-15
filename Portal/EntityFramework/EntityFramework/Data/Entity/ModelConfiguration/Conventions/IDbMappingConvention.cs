using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020001A3 RID: 419
	internal interface IDbMappingConvention : IConvention
	{
		// Token: 0x06001764 RID: 5988
		void Apply(DbDatabaseMapping databaseMapping);
	}
}
