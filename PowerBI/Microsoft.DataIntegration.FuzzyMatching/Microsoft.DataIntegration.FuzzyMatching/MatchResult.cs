using System;
using System.Data;
using System.Diagnostics;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Data;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000054 RID: 84
	[DebuggerDisplay("Rid={RightRecordId} Similarity={ComparisonResult.Similarity}")]
	[Serializable]
	internal sealed class MatchResult : IMatchResult, IReset
	{
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000324 RID: 804 RVA: 0x000107BC File Offset: 0x0000E9BC
		IDataRecord IMatchResult.InputRecord
		{
			get
			{
				return this.LeftRecord;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000325 RID: 805 RVA: 0x000107C4 File Offset: 0x0000E9C4
		IDataRecord IMatchResult.RightRecord
		{
			get
			{
				return this.RightRecord;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000326 RID: 806 RVA: 0x000107CC File Offset: 0x0000E9CC
		int IMatchResult.RightRecordId
		{
			get
			{
				return this.RightRecordId;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000327 RID: 807 RVA: 0x000107D4 File Offset: 0x0000E9D4
		ComparisonResult IMatchResult.ComparisonResult
		{
			get
			{
				return this.ComparisonResult;
			}
		}

		// Token: 0x06000328 RID: 808 RVA: 0x000107DC File Offset: 0x0000E9DC
		public MatchResult()
		{
			this.RightRecord = new SimpleDataRecord();
		}

		// Token: 0x06000329 RID: 809 RVA: 0x000107EF File Offset: 0x0000E9EF
		public void Reset()
		{
			this.RightRecordId = -1;
			this.LeftRecord = null;
			this.RightRecord.Values = null;
			this.ComparisonResult = null;
		}

		// Token: 0x04000117 RID: 279
		public IDataRecord LeftRecord;

		// Token: 0x04000118 RID: 280
		public int RightRecordId;

		// Token: 0x04000119 RID: 281
		public SimpleDataRecord RightRecord;

		// Token: 0x0400011A RID: 282
		public object[] RightRecordValues;

		// Token: 0x0400011B RID: 283
		public ComparisonResult ComparisonResult;
	}
}
