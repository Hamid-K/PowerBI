using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200024F RID: 591
	[Serializable]
	internal sealed class FunctionAggrLast : FunctionAggr
	{
		// Token: 0x06001371 RID: 4977 RVA: 0x0002E935 File Offset: 0x0002CB35
		public FunctionAggrLast(List<IInternalExpression> args)
			: base(args)
		{
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x0002E93E File Offset: 0x0002CB3E
		internal override string DisplayText()
		{
			return "Last";
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x0002E948 File Offset: 0x0002CB48
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
					"Last(",
					this._Expr.WriteSource(nameChanges),
					", ",
					text,
					")"
				});
			}
			return "Last(" + this._Expr.WriteSource(nameChanges) + ")";
		}
	}
}
