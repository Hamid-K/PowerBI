using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000248 RID: 584
	[Serializable]
	internal sealed class FunctionAggrAggregate : FunctionAggr
	{
		// Token: 0x0600135A RID: 4954 RVA: 0x0002E5CF File Offset: 0x0002C7CF
		public FunctionAggrAggregate(List<IInternalExpression> args)
			: base(args)
		{
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x0002E5D8 File Offset: 0x0002C7D8
		internal override string DisplayText()
		{
			return "Aggregate";
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x0002E5E0 File Offset: 0x0002C7E0
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
					"Aggregate(",
					this._Expr.WriteSource(nameChanges),
					", ",
					text,
					")"
				});
			}
			return "Aggregate(" + this._Expr.WriteSource(nameChanges) + ")";
		}
	}
}
