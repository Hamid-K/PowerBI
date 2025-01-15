using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models
{
	// Token: 0x020016C1 RID: 5825
	public class ArithmeticPair : IEquatable<ArithmeticPair>
	{
		// Token: 0x0600C23D RID: 49725 RVA: 0x0029DADC File Offset: 0x0029BCDC
		public ArithmeticPair(decimal left, decimal right)
		{
			this.Left = left;
			this.Right = right;
		}

		// Token: 0x170020F4 RID: 8436
		// (get) Token: 0x0600C23E RID: 49726 RVA: 0x0029DAF2 File Offset: 0x0029BCF2
		public decimal Left { get; }

		// Token: 0x170020F5 RID: 8437
		// (get) Token: 0x0600C23F RID: 49727 RVA: 0x0029DAFA File Offset: 0x0029BCFA
		public decimal Right { get; }

		// Token: 0x0600C240 RID: 49728 RVA: 0x0029DB04 File Offset: 0x0029BD04
		public bool Equals(ArithmeticPair other)
		{
			return other != null && this.Left.Equals(this.Right);
		}

		// Token: 0x0600C241 RID: 49729 RVA: 0x0029DB30 File Offset: 0x0029BD30
		public override bool Equals(object other)
		{
			return this.Equals(other as ArithmeticPair);
		}

		// Token: 0x0600C242 RID: 49730 RVA: 0x00218E7F File Offset: 0x0021707F
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x0600C243 RID: 49731 RVA: 0x0029DB3E File Offset: 0x0029BD3E
		public static bool operator ==(ArithmeticPair left, ArithmeticPair right)
		{
			return (left == null && right == null) || (left != null && left.Equals(right));
		}

		// Token: 0x0600C244 RID: 49732 RVA: 0x0029DB54 File Offset: 0x0029BD54
		public static bool operator !=(ArithmeticPair left, ArithmeticPair right)
		{
			return !(left == right);
		}

		// Token: 0x0600C245 RID: 49733 RVA: 0x0029DB60 File Offset: 0x0029BD60
		public override string ToString()
		{
			string text;
			if ((text = this._toString) == null)
			{
				text = (this._toString = string.Format("{{{0}, {1}}}", this.Left, this.Right));
			}
			return text;
		}

		// Token: 0x04004B55 RID: 19285
		private string _toString;
	}
}
