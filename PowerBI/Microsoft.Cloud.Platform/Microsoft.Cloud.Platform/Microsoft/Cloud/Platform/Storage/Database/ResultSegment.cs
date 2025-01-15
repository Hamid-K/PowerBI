using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x02000047 RID: 71
	[DataContract]
	public sealed class ResultSegment<TRecord>
	{
		// Token: 0x060001B5 RID: 437 RVA: 0x00006324 File Offset: 0x00004524
		public ResultSegment(List<TRecord> results, IDatabaseContinuationToken continuationToken)
		{
			this.Results = results;
			this.ContinuationToken = continuationToken;
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x0000633A File Offset: 0x0000453A
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x00006342 File Offset: 0x00004542
		[DataMember]
		public List<TRecord> Results { get; private set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x0000634B File Offset: 0x0000454B
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x00006353 File Offset: 0x00004553
		[DataMember]
		public IDatabaseContinuationToken ContinuationToken { get; private set; }
	}
}
