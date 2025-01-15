using System;
using System.ComponentModel;
using Microsoft.Lucia.Core.TermIndex;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000040 RID: 64
	[ImmutableObject(true)]
	public sealed class BuildDataIndexResult
	{
		// Token: 0x060000F7 RID: 247 RVA: 0x00003D5E File Offset: 0x00001F5E
		public BuildDataIndexResult(DataIndex dataIndex, DataIndexStatistics statistics, BuildDataIndexWarnings warnings = BuildDataIndexWarnings.None)
		{
			this.DataIndex = dataIndex;
			this.Statistics = statistics;
			this.Warnings = warnings;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00003D7B File Offset: 0x00001F7B
		public BuildDataIndexResult(BuildDataIndexWarnings warnings)
		{
			this.Warnings = warnings;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00003D8A File Offset: 0x00001F8A
		public DataIndex DataIndex { get; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00003D92 File Offset: 0x00001F92
		public DataIndexStatistics Statistics { get; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00003D9A File Offset: 0x00001F9A
		public BuildDataIndexWarnings Warnings { get; }
	}
}
