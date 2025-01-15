using System;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Microsoft.HostIntegration.TI.Linq
{
	// Token: 0x0200075A RID: 1882
	public class HIPDatabase : DataContext
	{
		// Token: 0x06003BAF RID: 15279 RVA: 0x000CC1CD File Offset: 0x000CA3CD
		public HIPDatabase(string connection)
			: base(connection, HIPDatabase.mappingSource)
		{
		}

		// Token: 0x06003BB0 RID: 15280 RVA: 0x000CC1DB File Offset: 0x000CA3DB
		public HIPDatabase(IDbConnection connection)
			: base(connection, HIPDatabase.mappingSource)
		{
		}

		// Token: 0x06003BB1 RID: 15281 RVA: 0x000CC1E9 File Offset: 0x000CA3E9
		public HIPDatabase(string connection, MappingSource mappingSource)
			: base(connection, mappingSource)
		{
		}

		// Token: 0x06003BB2 RID: 15282 RVA: 0x000CC1F3 File Offset: 0x000CA3F3
		public HIPDatabase(IDbConnection connection, MappingSource mappingSource)
			: base(connection, mappingSource)
		{
		}

		// Token: 0x17000DAE RID: 3502
		// (get) Token: 0x06003BB3 RID: 15283 RVA: 0x000CC1FD File Offset: 0x000CA3FD
		public Table<AffiliatedApplication> AffiliatedApplications
		{
			get
			{
				return base.GetTable<AffiliatedApplication>();
			}
		}

		// Token: 0x17000DAF RID: 3503
		// (get) Token: 0x06003BB4 RID: 15284 RVA: 0x000CC205 File Offset: 0x000CA405
		public Table<Application> Applications
		{
			get
			{
				return base.GetTable<Application>();
			}
		}

		// Token: 0x17000DB0 RID: 3504
		// (get) Token: 0x06003BB5 RID: 15285 RVA: 0x000CC20D File Offset: 0x000CA40D
		public Table<Computer> Computers
		{
			get
			{
				return base.GetTable<Computer>();
			}
		}

		// Token: 0x17000DB1 RID: 3505
		// (get) Token: 0x06003BB6 RID: 15286 RVA: 0x000CC215 File Offset: 0x000CA415
		public Table<Determinant> Determinants
		{
			get
			{
				return base.GetTable<Determinant>();
			}
		}

		// Token: 0x17000DB2 RID: 3506
		// (get) Token: 0x06003BB7 RID: 15287 RVA: 0x000CC21D File Offset: 0x000CA41D
		public Table<HEPermission> HEPermissions
		{
			get
			{
				return base.GetTable<HEPermission>();
			}
		}

		// Token: 0x17000DB3 RID: 3507
		// (get) Token: 0x06003BB8 RID: 15288 RVA: 0x000CC225 File Offset: 0x000CA425
		public Table<LinqHostEnvironment> HostEnvironments
		{
			get
			{
				return base.GetTable<LinqHostEnvironment>();
			}
		}

		// Token: 0x17000DB4 RID: 3508
		// (get) Token: 0x06003BB9 RID: 15289 RVA: 0x000CC22D File Offset: 0x000CA42D
		public Table<LEEndpoint> LEEndpoints
		{
			get
			{
				return base.GetTable<LEEndpoint>();
			}
		}

		// Token: 0x17000DB5 RID: 3509
		// (get) Token: 0x06003BBA RID: 15290 RVA: 0x000CC235 File Offset: 0x000CA435
		public Table<Listener> Listeners
		{
			get
			{
				return base.GetTable<Listener>();
			}
		}

		// Token: 0x17000DB6 RID: 3510
		// (get) Token: 0x06003BBB RID: 15291 RVA: 0x000CC23D File Offset: 0x000CA43D
		public Table<LocalEnvironment> LocalEnvironments
		{
			get
			{
				return base.GetTable<LocalEnvironment>();
			}
		}

		// Token: 0x17000DB7 RID: 3511
		// (get) Token: 0x06003BBC RID: 15292 RVA: 0x000CC245 File Offset: 0x000CA445
		public Table<Method> Methods
		{
			get
			{
				return base.GetTable<Method>();
			}
		}

		// Token: 0x17000DB8 RID: 3512
		// (get) Token: 0x06003BBD RID: 15293 RVA: 0x000CC24D File Offset: 0x000CA44D
		public Table<Object> Objects
		{
			get
			{
				return base.GetTable<Object>();
			}
		}

		// Token: 0x17000DB9 RID: 3513
		// (get) Token: 0x06003BBE RID: 15294 RVA: 0x000CC255 File Offset: 0x000CA455
		public Table<SecurityPolicy> SecurityPolicies
		{
			get
			{
				return base.GetTable<SecurityPolicy>();
			}
		}

		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x06003BBF RID: 15295 RVA: 0x000CC25D File Offset: 0x000CA45D
		public Table<TIMFile> TIMFiles
		{
			get
			{
				return base.GetTable<TIMFile>();
			}
		}

		// Token: 0x17000DBB RID: 3515
		// (get) Token: 0x06003BC0 RID: 15296 RVA: 0x000CC265 File Offset: 0x000CA465
		public Table<View> Views
		{
			get
			{
				return base.GetTable<View>();
			}
		}

		// Token: 0x040023CC RID: 9164
		private static MappingSource mappingSource = new AttributeMappingSource();
	}
}
