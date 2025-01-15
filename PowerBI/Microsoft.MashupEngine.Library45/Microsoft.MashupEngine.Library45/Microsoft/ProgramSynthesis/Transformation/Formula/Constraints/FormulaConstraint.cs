using System;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Constraints
{
	// Token: 0x020019D1 RID: 6609
	public abstract class FormulaConstraint : Constraint<IRow, object>, IEquatable<FormulaConstraint>
	{
		// Token: 0x0600D7AC RID: 55212 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override bool ConflictsWith(Constraint<IRow, object> other)
		{
			return false;
		}

		// Token: 0x0600D7AD RID: 55213 RVA: 0x002DD527 File Offset: 0x002DB727
		public bool Equals(FormulaConstraint other)
		{
			return other != null && this.ToString() == other.ToString();
		}

		// Token: 0x0600D7AE RID: 55214 RVA: 0x002DD545 File Offset: 0x002DB745
		public override bool Equals(Constraint<IRow, object> other)
		{
			return other != null && this.ToString() == other.ToString();
		}

		// Token: 0x0600D7AF RID: 55215 RVA: 0x002DD563 File Offset: 0x002DB763
		public override bool Equals(object other)
		{
			return this.Equals(other as FormulaConstraint);
		}

		// Token: 0x0600D7B0 RID: 55216 RVA: 0x00218E7F File Offset: 0x0021707F
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x0600D7B1 RID: 55217 RVA: 0x002DD571 File Offset: 0x002DB771
		public static bool operator ==(FormulaConstraint left, FormulaConstraint right)
		{
			return (left == null && right == null) || (left != null && left.Equals(right));
		}

		// Token: 0x0600D7B2 RID: 55218 RVA: 0x002DD587 File Offset: 0x002DB787
		public static bool operator !=(FormulaConstraint left, FormulaConstraint right)
		{
			return !(left == right);
		}

		// Token: 0x0600D7B3 RID: 55219 RVA: 0x002DD594 File Offset: 0x002DB794
		public override string ToString()
		{
			string text;
			if ((text = this._equalString) == null)
			{
				text = (this._equalString = this.ToEqualString());
			}
			return text;
		}

		// Token: 0x0600D7B4 RID: 55220 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<IRow, object> program)
		{
			return true;
		}

		// Token: 0x0600D7B5 RID: 55221
		internal abstract string ToEqualString();

		// Token: 0x040052D0 RID: 21200
		private string _equalString;
	}
}
