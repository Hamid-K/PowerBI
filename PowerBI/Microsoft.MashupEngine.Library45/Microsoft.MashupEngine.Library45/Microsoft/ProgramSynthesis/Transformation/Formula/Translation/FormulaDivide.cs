using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation
{
	// Token: 0x020017F5 RID: 6133
	internal abstract class FormulaDivide : FormulaBinaryOperator
	{
		// Token: 0x0600C9D7 RID: 51671 RVA: 0x002B3122 File Offset: 0x002B1322
		protected FormulaDivide(FormulaExpression left, FormulaExpression right, int precedence = 6)
			: base(left, right, precedence, false, false)
		{
		}

		// Token: 0x17002206 RID: 8710
		// (get) Token: 0x0600C9D8 RID: 51672 RVA: 0x002B314A File Offset: 0x002B134A
		public override string Symbol
		{
			get
			{
				return "/";
			}
		}

		// Token: 0x17002207 RID: 8711
		// (get) Token: 0x0600C9D9 RID: 51673 RVA: 0x002B3154 File Offset: 0x002B1354
		public override Type Type
		{
			get
			{
				Type type;
				if ((type = this.TypeCached) == null)
				{
					type = (this.TypeCached = base.ResolveType(true));
				}
				return type;
			}
		}
	}
}
