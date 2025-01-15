using System;

namespace Microsoft.Data.Spatial
{
	// Token: 0x0200007E RID: 126
	internal class LexerToken
	{
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000300 RID: 768 RVA: 0x00008632 File Offset: 0x00006832
		// (set) Token: 0x06000301 RID: 769 RVA: 0x0000863A File Offset: 0x0000683A
		public string Text { get; set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000302 RID: 770 RVA: 0x00008643 File Offset: 0x00006843
		// (set) Token: 0x06000303 RID: 771 RVA: 0x0000864B File Offset: 0x0000684B
		public int Type { get; set; }

		// Token: 0x06000304 RID: 772 RVA: 0x00008654 File Offset: 0x00006854
		public bool MatchToken(int targetType, string targetText, StringComparison comparison)
		{
			return this.Type == targetType && (string.IsNullOrEmpty(targetText) || this.Text.Equals(targetText, comparison));
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00008678 File Offset: 0x00006878
		public override string ToString()
		{
			return string.Concat(new object[] { "Type:[", this.Type, "] Text:[", this.Text, "]" });
		}
	}
}
