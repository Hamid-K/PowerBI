using System;
using Microsoft.SqlServer.Server;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000D1 RID: 209
	[SqlUserDefinedAggregate(1, Name = "Populate", IsInvariantToDuplicates = false, IsInvariantToNulls = true)]
	[Serializable]
	public struct Populate
	{
		// Token: 0x060007E6 RID: 2022 RVA: 0x0002676D File Offset: 0x0002496D
		public void Init()
		{
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x0002676F File Offset: 0x0002496F
		public void Accumulate(Record record)
		{
			this.m_rowCount += 1L;
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x00026780 File Offset: 0x00024980
		public void Merge(Populate Group)
		{
			this.m_rowCount += Group.m_rowCount;
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00026795 File Offset: 0x00024995
		public long Terminate()
		{
			return this.m_rowCount;
		}

		// Token: 0x0400034E RID: 846
		private long m_rowCount;
	}
}
