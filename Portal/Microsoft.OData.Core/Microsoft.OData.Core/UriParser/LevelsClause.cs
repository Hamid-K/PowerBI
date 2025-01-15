using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000187 RID: 391
	public sealed class LevelsClause
	{
		// Token: 0x0600133F RID: 4927 RVA: 0x00039432 File Offset: 0x00037632
		public LevelsClause(bool isMaxLevel, long level)
		{
			this.isMaxLevel = isMaxLevel;
			this.level = level;
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06001340 RID: 4928 RVA: 0x00039448 File Offset: 0x00037648
		public bool IsMaxLevel
		{
			get
			{
				return this.isMaxLevel;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06001341 RID: 4929 RVA: 0x00039450 File Offset: 0x00037650
		public long Level
		{
			get
			{
				return this.level;
			}
		}

		// Token: 0x0400089E RID: 2206
		private bool isMaxLevel;

		// Token: 0x0400089F RID: 2207
		private long level;
	}
}
