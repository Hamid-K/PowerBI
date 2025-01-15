using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200013D RID: 317
	public sealed class LevelsClause
	{
		// Token: 0x06000E39 RID: 3641 RVA: 0x0002976A File Offset: 0x0002796A
		public LevelsClause(bool isMaxLevel, long level)
		{
			this.isMaxLevel = isMaxLevel;
			this.level = level;
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000E3A RID: 3642 RVA: 0x00029780 File Offset: 0x00027980
		public bool IsMaxLevel
		{
			get
			{
				return this.isMaxLevel;
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000E3B RID: 3643 RVA: 0x00029788 File Offset: 0x00027988
		public long Level
		{
			get
			{
				return this.level;
			}
		}

		// Token: 0x04000769 RID: 1897
		private bool isMaxLevel;

		// Token: 0x0400076A RID: 1898
		private long level;
	}
}
