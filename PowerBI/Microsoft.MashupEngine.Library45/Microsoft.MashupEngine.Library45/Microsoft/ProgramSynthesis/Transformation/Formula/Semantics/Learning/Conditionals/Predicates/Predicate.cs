using System;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.Predicates
{
	// Token: 0x02001739 RID: 5945
	public abstract class Predicate : IEquatable<Predicate>, IPredicateEvaluator
	{
		// Token: 0x17002199 RID: 8601
		// (get) Token: 0x0600C583 RID: 50563 RVA: 0x002A7B5A File Offset: 0x002A5D5A
		// (set) Token: 0x0600C584 RID: 50564 RVA: 0x002A7B62 File Offset: 0x002A5D62
		public double? Score { get; set; }

		// Token: 0x0600C585 RID: 50565 RVA: 0x002A7B6B File Offset: 0x002A5D6B
		public bool Equals(Predicate other)
		{
			return other != null && this.ToEqualString() == other.ToEqualString();
		}

		// Token: 0x0600C586 RID: 50566 RVA: 0x002A7B89 File Offset: 0x002A5D89
		public override bool Equals(object other)
		{
			return this.Equals(other as Predicate);
		}

		// Token: 0x0600C587 RID: 50567
		public abstract bool Evaluate(IRow row);

		// Token: 0x0600C588 RID: 50568 RVA: 0x002A7B97 File Offset: 0x002A5D97
		public override int GetHashCode()
		{
			return this.ToEqualString().GetHashCode();
		}

		// Token: 0x0600C589 RID: 50569 RVA: 0x002A7BA4 File Offset: 0x002A5DA4
		public static bool operator ==(Predicate left, Predicate right)
		{
			return (left == null && right == null) || (left != null && left.Equals(right));
		}

		// Token: 0x0600C58A RID: 50570 RVA: 0x002A7BBA File Offset: 0x002A5DBA
		public static bool operator !=(Predicate left, Predicate right)
		{
			return !(left == right);
		}

		// Token: 0x0600C58B RID: 50571
		public abstract string ToEqualString();

		// Token: 0x0600C58C RID: 50572 RVA: 0x002A7BC8 File Offset: 0x002A5DC8
		public override string ToString()
		{
			string text;
			if ((text = this._toString) == null)
			{
				text = (this._toString = this.ToEqualString());
			}
			return text;
		}

		// Token: 0x04004D54 RID: 19796
		private string _toString;
	}
}
