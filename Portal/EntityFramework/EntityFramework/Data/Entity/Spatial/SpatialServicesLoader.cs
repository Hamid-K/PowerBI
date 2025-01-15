using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;

namespace System.Data.Entity.Spatial
{
	// Token: 0x02000099 RID: 153
	internal class SpatialServicesLoader
	{
		// Token: 0x06000671 RID: 1649 RVA: 0x00012E47 File Offset: 0x00011047
		public SpatialServicesLoader(IDbDependencyResolver resolver)
		{
			this._resolver = resolver;
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00012E58 File Offset: 0x00011058
		public virtual DbSpatialServices LoadDefaultServices()
		{
			DbSpatialServices dbSpatialServices = this._resolver.GetService<DbSpatialServices>();
			if (dbSpatialServices != null)
			{
				return dbSpatialServices;
			}
			dbSpatialServices = this._resolver.GetService(new DbProviderInfo("System.Data.SqlClient", "2012"));
			if (dbSpatialServices != null && dbSpatialServices.NativeTypesAvailable)
			{
				return dbSpatialServices;
			}
			return DefaultSpatialServices.Instance;
		}

		// Token: 0x0400012B RID: 299
		private readonly IDbDependencyResolver _resolver;
	}
}
