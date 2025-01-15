using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Lucia.Hosting.Analytics;

namespace Microsoft.PowerBI.ExploreHost.Lucia
{
	// Token: 0x02000072 RID: 114
	[DataContract]
	public sealed class UtteranceHistoryResult
	{
		// Token: 0x0600032B RID: 811 RVA: 0x0000A3EC File Offset: 0x000085EC
		public UtteranceHistoryResult(IEnumerable<UtteranceViewItem> utterances = null, DateTime? timestamp = null, string datasetName = null, string workspaceName = null)
		{
			this.Utterances = utterances;
			this.Timestamp = timestamp;
			this.DatasetName = datasetName;
			this.WorkspaceName = workspaceName;
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600032C RID: 812 RVA: 0x0000A411 File Offset: 0x00008611
		[DataMember(EmitDefaultValue = false, Order = 10)]
		public IEnumerable<UtteranceViewItem> Utterances { get; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600032D RID: 813 RVA: 0x0000A419 File Offset: 0x00008619
		[DataMember(EmitDefaultValue = false, Order = 20)]
		public DateTime? Timestamp { get; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0000A421 File Offset: 0x00008621
		[DataMember(EmitDefaultValue = false, Order = 30)]
		public string DatasetName { get; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0000A429 File Offset: 0x00008629
		[DataMember(EmitDefaultValue = false, Order = 40)]
		public string WorkspaceName { get; }
	}
}
