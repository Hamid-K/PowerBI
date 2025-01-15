using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.Contracts
{
	// Token: 0x02001754 RID: 5972
	public class ConditionalBranchDelimiterInfo : IEquatable<ConditionalBranchDelimiterInfo>
	{
		// Token: 0x170021B4 RID: 8628
		// (get) Token: 0x0600C633 RID: 50739 RVA: 0x002A9D19 File Offset: 0x002A7F19
		// (set) Token: 0x0600C634 RID: 50740 RVA: 0x002A9D21 File Offset: 0x002A7F21
		public string ColumnName { get; set; }

		// Token: 0x170021B5 RID: 8629
		// (get) Token: 0x0600C635 RID: 50741 RVA: 0x002A9D2A File Offset: 0x002A7F2A
		// (set) Token: 0x0600C636 RID: 50742 RVA: 0x002A9D32 File Offset: 0x002A7F32
		public int MinimumCount { get; set; }

		// Token: 0x170021B6 RID: 8630
		// (get) Token: 0x0600C637 RID: 50743 RVA: 0x002A9D3B File Offset: 0x002A7F3B
		// (set) Token: 0x0600C638 RID: 50744 RVA: 0x002A9D43 File Offset: 0x002A7F43
		public string Value { get; set; }

		// Token: 0x0600C639 RID: 50745 RVA: 0x002A9D4C File Offset: 0x002A7F4C
		public bool Equals(ConditionalBranchDelimiterInfo other)
		{
			return other != null && this.ToString() == other.ToString();
		}

		// Token: 0x0600C63A RID: 50746 RVA: 0x002A9D6A File Offset: 0x002A7F6A
		public override bool Equals(object other)
		{
			return this.Equals(other as ConditionalBranchDelimiterInfo);
		}

		// Token: 0x0600C63B RID: 50747 RVA: 0x00218E7F File Offset: 0x0021707F
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x0600C63C RID: 50748 RVA: 0x002A9D78 File Offset: 0x002A7F78
		public static bool operator ==(ConditionalBranchDelimiterInfo left, ConditionalBranchDelimiterInfo right)
		{
			return (left == null && right == null) || (left != null && left.Equals(right));
		}

		// Token: 0x0600C63D RID: 50749 RVA: 0x002A9D8E File Offset: 0x002A7F8E
		public static bool operator !=(ConditionalBranchDelimiterInfo left, ConditionalBranchDelimiterInfo right)
		{
			return !(left == right);
		}

		// Token: 0x0600C63E RID: 50750 RVA: 0x002A9D9A File Offset: 0x002A7F9A
		public override string ToString()
		{
			return string.Format("{0}:{1}[{2:N0}]", this.ColumnName, this.Value.ToCSharpLiteral(), this.MinimumCount);
		}
	}
}
