using System;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation
{
	// Token: 0x020017F1 RID: 6129
	internal abstract class FormulaBinaryOperator : FormulaExpression, IFormulaBinaryOperator, IFormulaOperator, IFormulaTyped
	{
		// Token: 0x0600C9C7 RID: 51655 RVA: 0x002B2CE4 File Offset: 0x002B0EE4
		protected FormulaBinaryOperator(FormulaExpression left, FormulaExpression right, int precedence, bool compact, bool associative)
		{
			IFormulaBinaryOperator formulaBinaryOperator = left as IFormulaBinaryOperator;
			if (formulaBinaryOperator != null && formulaBinaryOperator.Precedence < precedence)
			{
				left = new FormulaParenthesis(left);
			}
			IFormulaBinaryOperator formulaBinaryOperator2 = right as IFormulaBinaryOperator;
			if (formulaBinaryOperator2 != null && (formulaBinaryOperator2.Precedence < precedence || (formulaBinaryOperator2.GetType() != base.GetType() && formulaBinaryOperator2.Precedence <= precedence) || (formulaBinaryOperator2.GetType() == base.GetType() && !formulaBinaryOperator2.Associative)))
			{
				right = new FormulaParenthesis(right);
			}
			base.Children = new FormulaExpression[] { left, right };
			this.Left = left;
			this.Right = right;
			this.Precedence = precedence;
			this.Associative = associative;
			this.Compact = compact;
		}

		// Token: 0x170021FC RID: 8700
		// (get) Token: 0x0600C9C8 RID: 51656 RVA: 0x002B2D9D File Offset: 0x002B0F9D
		public bool Associative { get; }

		// Token: 0x170021FD RID: 8701
		// (get) Token: 0x0600C9C9 RID: 51657 RVA: 0x002B2DA5 File Offset: 0x002B0FA5
		public bool Compact { get; }

		// Token: 0x170021FE RID: 8702
		// (get) Token: 0x0600C9CA RID: 51658 RVA: 0x002B2DAD File Offset: 0x002B0FAD
		public FormulaExpression Left { get; }

		// Token: 0x170021FF RID: 8703
		// (get) Token: 0x0600C9CB RID: 51659 RVA: 0x002B2DB5 File Offset: 0x002B0FB5
		public int Precedence { get; }

		// Token: 0x17002200 RID: 8704
		// (get) Token: 0x0600C9CC RID: 51660 RVA: 0x002B2DBD File Offset: 0x002B0FBD
		public FormulaExpression Right { get; }

		// Token: 0x17002201 RID: 8705
		// (get) Token: 0x0600C9CD RID: 51661
		public abstract string Symbol { get; }

		// Token: 0x17002202 RID: 8706
		// (get) Token: 0x0600C9CE RID: 51662 RVA: 0x002B2DC8 File Offset: 0x002B0FC8
		public virtual Type Type
		{
			get
			{
				Type type;
				if ((type = this.TypeCached) == null)
				{
					type = (this.TypeCached = this.ResolveType(false));
				}
				return type;
			}
		}

		// Token: 0x0600C9CF RID: 51663 RVA: 0x002B2DF0 File Offset: 0x002B0FF0
		protected Type ResolveType(bool forceDouble = false)
		{
			IFormulaTyped formulaTyped = this.Left as IFormulaTyped;
			if (formulaTyped != null)
			{
				IFormulaTyped formulaTyped2 = this.Right as IFormulaTyped;
				if (formulaTyped2 != null)
				{
					Type type = formulaTyped.Type;
					Type type2 = formulaTyped2.Type;
					bool flag = type == typeof(string);
					bool flag2 = type2 == typeof(string);
					bool flag3 = type == typeof(bool) || type == typeof(bool?);
					bool flag4 = type2 == typeof(bool) || type2 == typeof(bool?);
					bool flag5 = type == typeof(int) || type == typeof(int?);
					bool flag6 = type2 == typeof(int) || type2 == typeof(int?);
					bool flag7 = type == typeof(double) || type == typeof(double?);
					bool flag8 = type2 == typeof(double) || type2 == typeof(double?);
					bool flag9 = type == typeof(decimal) || type == typeof(decimal?);
					bool flag10 = type2 == typeof(decimal) || type2 == typeof(decimal?);
					bool flag11 = type == typeof(DateTime) || type == typeof(DateTime?);
					bool flag12 = type2 == typeof(DateTime) || type2 == typeof(DateTime?);
					bool flag13 = (type != null && type.IsNullable()) || (type2 != null && type2.IsNullable());
					if (!flag9 && !flag10)
					{
						if (!flag7 && !flag8)
						{
							if (!flag5 && !flag6)
							{
								if (!flag11 && !flag12)
								{
									if (flag || flag2)
									{
										return typeof(string);
									}
									if (!flag3 && !flag4)
									{
										return typeof(object);
									}
									if (!flag13)
									{
										return typeof(DateTime);
									}
									return typeof(DateTime?);
								}
								else
								{
									if (!flag13)
									{
										return typeof(DateTime);
									}
									return typeof(DateTime?);
								}
							}
							else if (!forceDouble)
							{
								if (!flag13)
								{
									return typeof(int);
								}
								return typeof(int?);
							}
							else
							{
								if (!flag13)
								{
									return typeof(double);
								}
								return typeof(double?);
							}
						}
						else
						{
							if (!flag13)
							{
								return typeof(double);
							}
							return typeof(double?);
						}
					}
					else
					{
						if (!flag13)
						{
							return typeof(decimal);
						}
						return typeof(decimal?);
					}
				}
			}
			return null;
		}

		// Token: 0x0600C9D0 RID: 51664 RVA: 0x002B30D4 File Offset: 0x002B12D4
		protected override string ToCodeString()
		{
			if (!this.Compact)
			{
				return string.Format("{0} {1} {2}", this.Left, this.Symbol, this.Right);
			}
			return string.Format("{0}{1}{2}", this.Left, this.Symbol, this.Right);
		}

		// Token: 0x04004F39 RID: 20281
		protected Type TypeCached;
	}
}
