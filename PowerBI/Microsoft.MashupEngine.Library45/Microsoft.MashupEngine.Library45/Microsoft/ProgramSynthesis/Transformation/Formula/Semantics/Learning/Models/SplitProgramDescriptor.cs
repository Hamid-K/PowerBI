using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models
{
	// Token: 0x020016EA RID: 5866
	public class SplitProgramDescriptor : IEquatable<SplitProgramDescriptor>
	{
		// Token: 0x1700215A RID: 8538
		// (get) Token: 0x0600C3A3 RID: 50083 RVA: 0x002A1911 File Offset: 0x0029FB11
		// (set) Token: 0x0600C3A4 RID: 50084 RVA: 0x002A1919 File Offset: 0x0029FB19
		public char Delimiter { get; set; }

		// Token: 0x1700215B RID: 8539
		// (get) Token: 0x0600C3A5 RID: 50085 RVA: 0x002A1922 File Offset: 0x0029FB22
		// (set) Token: 0x0600C3A6 RID: 50086 RVA: 0x002A192A File Offset: 0x0029FB2A
		public int Instance { get; set; }

		// Token: 0x1700215C RID: 8540
		// (get) Token: 0x0600C3A7 RID: 50087 RVA: 0x002A1933 File Offset: 0x0029FB33
		// (set) Token: 0x0600C3A8 RID: 50088 RVA: 0x002A193B File Offset: 0x0029FB3B
		public int NegativeInstance { get; set; }

		// Token: 0x0600C3A9 RID: 50089 RVA: 0x002A1944 File Offset: 0x0029FB44
		public bool Equals(SplitProgramDescriptor other)
		{
			return other != null && this.ToString() == other.ToString();
		}

		// Token: 0x0600C3AA RID: 50090 RVA: 0x002A1962 File Offset: 0x0029FB62
		public override bool Equals(object other)
		{
			return this.Equals(other as SplitProgramDescriptor);
		}

		// Token: 0x0600C3AB RID: 50091 RVA: 0x00218E7F File Offset: 0x0021707F
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x0600C3AC RID: 50092 RVA: 0x002A1970 File Offset: 0x0029FB70
		public static bool operator ==(SplitProgramDescriptor left, SplitProgramDescriptor right)
		{
			return (left == null && right == null) || (left != null && left.Equals(right));
		}

		// Token: 0x0600C3AD RID: 50093 RVA: 0x002A1986 File Offset: 0x0029FB86
		public static bool operator !=(SplitProgramDescriptor left, SplitProgramDescriptor right)
		{
			return !(left == right);
		}

		// Token: 0x0600C3AE RID: 50094 RVA: 0x002A1994 File Offset: 0x0029FB94
		public override string ToString()
		{
			string text;
			if ((text = this._toString) == null)
			{
				text = (this._toString = string.Format("Split({0}, {{{1},{2}}})", this.Delimiter.ToCSharpPseudoLiteral(), this.Instance, this.NegativeInstance));
			}
			return text;
		}

		// Token: 0x04004C36 RID: 19510
		private string _toString;
	}
}
