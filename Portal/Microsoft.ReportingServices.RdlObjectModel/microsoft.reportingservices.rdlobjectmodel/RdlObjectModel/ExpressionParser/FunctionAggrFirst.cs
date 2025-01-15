using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200024D RID: 589
	[Serializable]
	internal sealed class FunctionAggrFirst : FunctionAggr
	{
		// Token: 0x0600136B RID: 4971 RVA: 0x0002E86D File Offset: 0x0002CA6D
		public FunctionAggrFirst(List<IInternalExpression> args)
			: base(args)
		{
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x0002E876 File Offset: 0x0002CA76
		internal override string DisplayText()
		{
			return "First";
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x0002E880 File Offset: 0x0002CA80
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
					"First(",
					this._Expr.WriteSource(nameChanges),
					", ",
					text,
					")"
				});
			}
			return "First(" + this._Expr.WriteSource(nameChanges) + ")";
		}
	}
}
