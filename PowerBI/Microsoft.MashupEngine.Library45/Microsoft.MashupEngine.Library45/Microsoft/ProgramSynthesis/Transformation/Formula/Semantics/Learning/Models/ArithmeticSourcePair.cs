using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models
{
	// Token: 0x020016C6 RID: 5830
	public class ArithmeticSourcePair : IEquatable<ArithmeticSourcePair>
	{
		// Token: 0x0600C256 RID: 49750 RVA: 0x0029DD99 File Offset: 0x0029BF99
		public ArithmeticSourcePair(NumberSource left, NumberSource right)
		{
			this.Left = left;
			this.Right = right;
		}

		// Token: 0x170020FD RID: 8445
		// (get) Token: 0x0600C257 RID: 49751 RVA: 0x0029DDB0 File Offset: 0x0029BFB0
		public decimal? Add
		{
			get
			{
				decimal? add = this._add;
				if (add == null)
				{
					return this._add = Operators.Add(this.Left.Value, this.Right.Value);
				}
				return add;
			}
		}

		// Token: 0x170020FE RID: 8446
		// (get) Token: 0x0600C258 RID: 49752 RVA: 0x0029DDF4 File Offset: 0x0029BFF4
		public decimal? Divide
		{
			get
			{
				decimal? divide = this._divide;
				if (divide == null)
				{
					return this._divide = Operators.Divide(this.Left.Value, this.Right.Value);
				}
				return divide;
			}
		}

		// Token: 0x170020FF RID: 8447
		// (get) Token: 0x0600C259 RID: 49753 RVA: 0x0029DE37 File Offset: 0x0029C037
		public NumberSource Left { get; }

		// Token: 0x17002100 RID: 8448
		// (get) Token: 0x0600C25A RID: 49754 RVA: 0x0029DE40 File Offset: 0x0029C040
		public decimal? Multiply
		{
			get
			{
				decimal? multiply = this._multiply;
				if (multiply == null)
				{
					return this._multiply = Operators.Multiply(this.Left.Value, this.Right.Value);
				}
				return multiply;
			}
		}

		// Token: 0x17002101 RID: 8449
		// (get) Token: 0x0600C25B RID: 49755 RVA: 0x0029DE83 File Offset: 0x0029C083
		public NumberSource Right { get; }

		// Token: 0x17002102 RID: 8450
		// (get) Token: 0x0600C25C RID: 49756 RVA: 0x0029DE8C File Offset: 0x0029C08C
		public decimal? Subtract
		{
			get
			{
				decimal? subtract = this._subtract;
				if (subtract == null)
				{
					return this._subtract = Operators.Subtract(this.Left.Value, this.Right.Value);
				}
				return subtract;
			}
		}

		// Token: 0x0600C25D RID: 49757 RVA: 0x0029DECF File Offset: 0x0029C0CF
		public bool Equals(ArithmeticSourcePair other)
		{
			return other != null && this.ToString() == other.ToString();
		}

		// Token: 0x0600C25E RID: 49758 RVA: 0x0029DEED File Offset: 0x0029C0ED
		public override bool Equals(object other)
		{
			return this.Equals(other as ArithmeticSourcePair);
		}

		// Token: 0x0600C25F RID: 49759 RVA: 0x00218E7F File Offset: 0x0021707F
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x0600C260 RID: 49760 RVA: 0x0029DEFB File Offset: 0x0029C0FB
		public static bool operator ==(ArithmeticSourcePair left, ArithmeticSourcePair right)
		{
			return (left == null && right == null) || (left != null && left.Equals(right));
		}

		// Token: 0x0600C261 RID: 49761 RVA: 0x0029DF11 File Offset: 0x0029C111
		public static bool operator !=(ArithmeticSourcePair left, ArithmeticSourcePair right)
		{
			return !(left == right);
		}

		// Token: 0x0600C262 RID: 49762 RVA: 0x0029DF20 File Offset: 0x0029C120
		public override string ToString()
		{
			string text;
			if ((text = this._toString) == null)
			{
				text = (this._toString = string.Format("Left: {0}  Right: {1}", this.Left, this.Right));
			}
			return text;
		}

		// Token: 0x04004B62 RID: 19298
		private decimal? _add;

		// Token: 0x04004B63 RID: 19299
		private decimal? _divide;

		// Token: 0x04004B64 RID: 19300
		private decimal? _multiply;

		// Token: 0x04004B65 RID: 19301
		private decimal? _subtract;

		// Token: 0x04004B66 RID: 19302
		private string _toString;
	}
}
