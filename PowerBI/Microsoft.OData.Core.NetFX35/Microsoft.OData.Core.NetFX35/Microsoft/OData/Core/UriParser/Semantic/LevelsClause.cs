using System;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000243 RID: 579
	public sealed class LevelsClause
	{
		// Token: 0x060014AF RID: 5295 RVA: 0x00049CC7 File Offset: 0x00047EC7
		public LevelsClause(bool isMaxLevel, long level)
		{
			this.isMaxLevel = isMaxLevel;
			this.level = level;
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x060014B0 RID: 5296 RVA: 0x00049CDD File Offset: 0x00047EDD
		public bool IsMaxLevel
		{
			get
			{
				return this.isMaxLevel;
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x060014B1 RID: 5297 RVA: 0x00049CE5 File Offset: 0x00047EE5
		public long Level
		{
			get
			{
				return this.level;
			}
		}

		// Token: 0x040008B0 RID: 2224
		private bool isMaxLevel;

		// Token: 0x040008B1 RID: 2225
		private long level;
	}
}
