using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200025A RID: 602
	[Serializable]
	internal sealed class FunctionAggrVar : FunctionAggrStandard
	{
		// Token: 0x06001393 RID: 5011 RVA: 0x0002EE7F File Offset: 0x0002D07F
		public FunctionAggrVar(List<IInternalExpression> args)
			: base(args)
		{
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x0002EE88 File Offset: 0x0002D088
		internal override string DisplayText()
		{
			return "Var";
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x0002EE8F File Offset: 0x0002D08F
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Double;
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x0002EE94 File Offset: 0x0002D094
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
					"Var(",
					this._Expr.WriteSource(nameChanges),
					", ",
					text,
					")"
				});
			}
			return "Var(" + this._Expr.WriteSource(nameChanges) + ")";
		}
	}
}
