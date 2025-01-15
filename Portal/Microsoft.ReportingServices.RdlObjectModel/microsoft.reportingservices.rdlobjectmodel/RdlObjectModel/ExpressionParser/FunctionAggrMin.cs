using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000252 RID: 594
	[Serializable]
	internal sealed class FunctionAggrMin : FunctionAggrStandard
	{
		// Token: 0x0600137A RID: 4986 RVA: 0x0002EAA7 File Offset: 0x0002CCA7
		public FunctionAggrMin(List<IInternalExpression> args)
			: base(args)
		{
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x0002EAB0 File Offset: 0x0002CCB0
		internal override string DisplayText()
		{
			return "Min";
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x0002EAB8 File Offset: 0x0002CCB8
		public override string WriteSource(NameChanges nameChanges)
		{
			if (this._Expr == null)
			{
				return "";
			}
			string text = string.Empty;
			if (base.Scope != null)
			{
				text = base.GetScopeAsStringForWrite(nameChanges);
				return string.Concat(new string[]
				{
					"Min(",
					this._Expr.WriteSource(nameChanges),
					", ",
					text,
					")"
				});
			}
			return "Min(" + this._Expr.WriteSource(nameChanges) + ")";
		}
	}
}
