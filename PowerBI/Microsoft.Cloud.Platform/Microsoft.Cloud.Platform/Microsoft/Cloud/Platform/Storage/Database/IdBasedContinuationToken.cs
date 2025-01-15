using System;
using System.Runtime.Serialization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x02000049 RID: 73
	[DataContract]
	public class IdBasedContinuationToken : IDatabaseContinuationToken
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001BA RID: 442 RVA: 0x0000635C File Offset: 0x0000455C
		// (set) Token: 0x060001BB RID: 443 RVA: 0x00006364 File Offset: 0x00004564
		[DataMember]
		public long RecordId { get; private set; }

		// Token: 0x060001BC RID: 444 RVA: 0x0000636D File Offset: 0x0000456D
		public IdBasedContinuationToken(long recordId)
		{
			ExtendedDiagnostics.EnsureArgumentIsNotNegative(recordId, "recordId");
			this.RecordId = recordId;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00006387 File Offset: 0x00004587
		public override string ToString()
		{
			return "The last record id that was read from the db = '{0}'".FormatWithInvariantCulture(new object[] { this.RecordId });
		}
	}
}
