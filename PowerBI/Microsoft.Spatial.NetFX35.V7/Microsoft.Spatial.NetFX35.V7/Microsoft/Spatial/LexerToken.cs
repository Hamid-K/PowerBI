using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000064 RID: 100
	internal class LexerToken
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600025B RID: 603 RVA: 0x00005EE2 File Offset: 0x000040E2
		// (set) Token: 0x0600025C RID: 604 RVA: 0x00005EEA File Offset: 0x000040EA
		public string Text { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600025D RID: 605 RVA: 0x00005EF3 File Offset: 0x000040F3
		// (set) Token: 0x0600025E RID: 606 RVA: 0x00005EFB File Offset: 0x000040FB
		public int Type { get; set; }

		// Token: 0x0600025F RID: 607 RVA: 0x00005F04 File Offset: 0x00004104
		public bool MatchToken(int targetType, string targetText, StringComparison comparison)
		{
			return this.Type == targetType && (string.IsNullOrEmpty(targetText) || this.Text.Equals(targetText, comparison));
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00005F28 File Offset: 0x00004128
		public override string ToString()
		{
			return string.Concat(new object[] { "Type:[", this.Type, "] Text:[", this.Text, "]" });
		}
	}
}
