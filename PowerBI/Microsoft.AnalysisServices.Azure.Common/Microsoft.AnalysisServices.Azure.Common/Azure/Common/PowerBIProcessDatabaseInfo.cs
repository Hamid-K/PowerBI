using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000061 RID: 97
	[DataContract]
	public class PowerBIProcessDatabaseInfo
	{
		// Token: 0x06000491 RID: 1169 RVA: 0x00010184 File Offset: 0x0000E384
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				string.Format("{{ DatabaseMoniker: {0}, ", this.DatabaseMoniker),
				"UserPuid: ",
				this.UserPuid,
				", TenantId: ",
				this.TenantId,
				", ",
				string.Format("Connections: {0}, ", this.Connections),
				string.Format("MaxParallel: {0}, ", this.MaxParallel),
				string.Format("ContentProviderId: {0}, ", this.ContentProviderId),
				string.Format("RefreshIncrementally: {0}, ", this.RefreshIncrementally),
				string.Format("IncrementalRefreshEffectiveDate: {0}, ", this.IncrementalRefreshEffectiveDate),
				string.Format("IntendedUsage: {0}, ", this.IntendedUsage),
				string.Format("RefreshScheduleTime: {0}, ", this.RefreshScheduleTime),
				string.Format("HasV3Datasources: {0}, ", this.HasV3Datasources),
				string.Format("UseV3ExecutionFramework: {0} }}", this.UseV3ExecutionFrameworkForV1Models)
			});
		}

		// Token: 0x04000174 RID: 372
		[DataMember]
		public DatabaseMoniker DatabaseMoniker;

		// Token: 0x04000175 RID: 373
		[DataMember]
		public string UserPuid;

		// Token: 0x04000176 RID: 374
		[DataMember]
		public string TenantId;

		// Token: 0x04000177 RID: 375
		[DataMember]
		public IEnumerable<ASConnectionInfo> Connections;

		// Token: 0x04000178 RID: 376
		[DataMember]
		public int MaxParallel;

		// Token: 0x04000179 RID: 377
		[DataMember]
		public int? ContentProviderId;

		// Token: 0x0400017A RID: 378
		[DataMember]
		public bool? RefreshIncrementally;

		// Token: 0x0400017B RID: 379
		[DataMember]
		public DateTime? IncrementalRefreshEffectiveDate;

		// Token: 0x0400017C RID: 380
		[DataMember]
		public IntendedUsage IntendedUsage;

		// Token: 0x0400017D RID: 381
		[DataMember]
		public DateTime RefreshScheduleTime;

		// Token: 0x0400017E RID: 382
		[DataMember]
		public bool HasV3Datasources;

		// Token: 0x0400017F RID: 383
		[DataMember]
		public bool UseV3ExecutionFrameworkForV1Models;
	}
}
