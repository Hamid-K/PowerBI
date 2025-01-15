using System;
using System.Runtime.Serialization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common.DataContracts
{
	// Token: 0x02000155 RID: 341
	[DataContract]
	public sealed class BindDatabaseResult
	{
		// Token: 0x060011E2 RID: 4578 RVA: 0x000494A0 File Offset: 0x000476A0
		public BindDatabaseResult(ServiceEntity serviceEntity, DatabaseEntity databaseEntity)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<ServiceEntity>(serviceEntity, "serviceEntity");
			ExtendedDiagnostics.EnsureArgumentNotNull<DatabaseEntity>(databaseEntity, "databaseEntity");
			this.ServiceEntity = serviceEntity;
			this.DatabaseEntity = databaseEntity;
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060011E3 RID: 4579 RVA: 0x000494CC File Offset: 0x000476CC
		// (set) Token: 0x060011E4 RID: 4580 RVA: 0x000494D4 File Offset: 0x000476D4
		[DataMember]
		public ServiceEntity ServiceEntity { get; private set; }

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060011E5 RID: 4581 RVA: 0x000494DD File Offset: 0x000476DD
		// (set) Token: 0x060011E6 RID: 4582 RVA: 0x000494E5 File Offset: 0x000476E5
		[DataMember]
		public DatabaseEntity DatabaseEntity { get; private set; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060011E7 RID: 4583 RVA: 0x000494EE File Offset: 0x000476EE
		// (set) Token: 0x060011E8 RID: 4584 RVA: 0x000494F6 File Offset: 0x000476F6
		[DataMember]
		public Uri DatabaseUri { get; set; }

		// Token: 0x060011E9 RID: 4585 RVA: 0x000494FF File Offset: 0x000476FF
		public override string ToString()
		{
			return "[DatabaseEntity='{0}',ServiceEntity='{1}']".FormatWithInvariantCulture(new object[] { this.DatabaseEntity, this.ServiceEntity });
		}
	}
}
