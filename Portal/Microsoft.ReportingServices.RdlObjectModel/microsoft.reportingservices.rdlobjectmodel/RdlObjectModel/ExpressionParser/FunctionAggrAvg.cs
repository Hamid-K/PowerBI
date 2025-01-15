using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000249 RID: 585
	[Serializable]
	internal sealed class FunctionAggrAvg : FunctionAggrStandard
	{
		// Token: 0x0600135D RID: 4957 RVA: 0x0002E663 File Offset: 0x0002C863
		public FunctionAggrAvg(List<IInternalExpression> args)
			: base(args)
		{
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x0002E66C File Offset: 0x0002C86C
		internal override string DisplayText()
		{
			return "Avg";
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x0002E674 File Offset: 0x0002C874
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
					"Avg(",
					this._Expr.WriteSource(nameChanges),
					", ",
					text,
					")"
				});
			}
			return "Avg(" + this._Expr.WriteSource(nameChanges) + ")";
		}
	}
}
