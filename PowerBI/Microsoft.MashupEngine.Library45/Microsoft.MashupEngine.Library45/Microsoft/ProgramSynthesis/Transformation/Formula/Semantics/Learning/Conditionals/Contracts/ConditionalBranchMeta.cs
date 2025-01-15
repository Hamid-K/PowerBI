using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.Contracts
{
	// Token: 0x02001752 RID: 5970
	public class ConditionalBranchMeta : IEquatable<ConditionalBranchMeta>
	{
		// Token: 0x170021B0 RID: 8624
		// (get) Token: 0x0600C620 RID: 50720 RVA: 0x002A9B71 File Offset: 0x002A7D71
		// (set) Token: 0x0600C621 RID: 50721 RVA: 0x002A9B79 File Offset: 0x002A7D79
		public IReadOnlyList<ConditionalBranchDelimiterInfo> Delimiters { get; set; }

		// Token: 0x170021B1 RID: 8625
		// (get) Token: 0x0600C622 RID: 50722 RVA: 0x002A9B82 File Offset: 0x002A7D82
		// (set) Token: 0x0600C623 RID: 50723 RVA: 0x002A9B8A File Offset: 0x002A7D8A
		public bool HasWholeColumnOutput { get; set; }

		// Token: 0x170021B2 RID: 8626
		// (get) Token: 0x0600C624 RID: 50724 RVA: 0x002A9B93 File Offset: 0x002A7D93
		// (set) Token: 0x0600C625 RID: 50725 RVA: 0x002A9B9B File Offset: 0x002A7D9B
		public IProgram Program { get; set; }

		// Token: 0x170021B3 RID: 8627
		// (get) Token: 0x0600C626 RID: 50726 RVA: 0x002A9BA4 File Offset: 0x002A7DA4
		// (set) Token: 0x0600C627 RID: 50727 RVA: 0x002A9BAC File Offset: 0x002A7DAC
		public IReadOnlyList<string> UsedColumnNames { get; set; }

		// Token: 0x0600C628 RID: 50728 RVA: 0x002A9BB5 File Offset: 0x002A7DB5
		public bool Equals(ConditionalBranchMeta other)
		{
			return other != null && this.ToEqualString() == other.ToEqualString();
		}

		// Token: 0x0600C629 RID: 50729 RVA: 0x002A9BD3 File Offset: 0x002A7DD3
		public override bool Equals(object other)
		{
			return this.Equals(other as ConditionalBranchMeta);
		}

		// Token: 0x0600C62A RID: 50730 RVA: 0x002A9BE1 File Offset: 0x002A7DE1
		public override int GetHashCode()
		{
			return this.ToEqualString().GetHashCode();
		}

		// Token: 0x0600C62B RID: 50731 RVA: 0x002A9BEE File Offset: 0x002A7DEE
		public static bool operator ==(ConditionalBranchMeta left, ConditionalBranchMeta right)
		{
			return (left == null && right == null) || (left != null && left.Equals(right));
		}

		// Token: 0x0600C62C RID: 50732 RVA: 0x002A9C04 File Offset: 0x002A7E04
		public static bool operator !=(ConditionalBranchMeta left, ConditionalBranchMeta right)
		{
			return !(left == right);
		}

		// Token: 0x0600C62D RID: 50733 RVA: 0x002A9C10 File Offset: 0x002A7E10
		public string ToEqualString()
		{
			return string.Format("{0}{1}{2}", this.Program, Environment.NewLine, this.ToString());
		}

		// Token: 0x0600C62E RID: 50734 RVA: 0x002A9C30 File Offset: 0x002A7E30
		public override string ToString()
		{
			IReadOnlyList<string> usedColumnNames = this.UsedColumnNames;
			string text = ((usedColumnNames != null && usedColumnNames.Any<string>()) ? ("Used Columns" + Environment.NewLine + "  " + string.Join(", ", this.UsedColumnNames)) : string.Empty);
			string text2;
			if (!this.Delimiters.Any<ConditionalBranchDelimiterInfo>())
			{
				text2 = string.Empty;
			}
			else
			{
				string[] array = new string[6];
				array[0] = Environment.NewLine;
				array[1] = Environment.NewLine;
				array[2] = "Delimiters";
				array[3] = Environment.NewLine;
				array[4] = "  ";
				array[5] = string.Join(Environment.NewLine + "  ", this.Delimiters.Select((ConditionalBranchDelimiterInfo d) => d.ToString()));
				text2 = string.Concat(array);
			}
			string text3 = text2;
			return text + text3;
		}
	}
}
