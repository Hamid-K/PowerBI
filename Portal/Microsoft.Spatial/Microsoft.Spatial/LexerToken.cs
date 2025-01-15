using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000069 RID: 105
	internal class LexerToken
	{
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x00006BAA File Offset: 0x00004DAA
		// (set) Token: 0x060002D2 RID: 722 RVA: 0x00006BB2 File Offset: 0x00004DB2
		public string Text { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x00006BBB File Offset: 0x00004DBB
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x00006BC3 File Offset: 0x00004DC3
		public int Type { get; set; }

		// Token: 0x060002D5 RID: 725 RVA: 0x00006BCC File Offset: 0x00004DCC
		public bool MatchToken(int targetType, string targetText, StringComparison comparison)
		{
			return this.Type == targetType && (string.IsNullOrEmpty(targetText) || this.Text.Equals(targetText, comparison));
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00006BF0 File Offset: 0x00004DF0
		public override string ToString()
		{
			return string.Concat(new object[] { "Type:[", this.Type, "] Text:[", this.Text, "]" });
		}
	}
}
