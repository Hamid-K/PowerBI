using System;

namespace Microsoft.Data.Spatial
{
	// Token: 0x0200007D RID: 125
	internal class LexerToken
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x000086C2 File Offset: 0x000068C2
		// (set) Token: 0x060002F7 RID: 759 RVA: 0x000086CA File Offset: 0x000068CA
		public string Text { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x000086D3 File Offset: 0x000068D3
		// (set) Token: 0x060002F9 RID: 761 RVA: 0x000086DB File Offset: 0x000068DB
		public int Type { get; set; }

		// Token: 0x060002FA RID: 762 RVA: 0x000086E4 File Offset: 0x000068E4
		public bool MatchToken(int targetType, string targetText, StringComparison comparison)
		{
			return this.Type == targetType && (string.IsNullOrEmpty(targetText) || this.Text.Equals(targetText, comparison));
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00008708 File Offset: 0x00006908
		public override string ToString()
		{
			return string.Concat(new object[] { "Type:[", this.Type, "] Text:[", this.Text, "]" });
		}
	}
}
