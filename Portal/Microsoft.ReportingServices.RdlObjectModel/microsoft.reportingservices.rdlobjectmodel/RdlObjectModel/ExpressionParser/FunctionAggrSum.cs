using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000259 RID: 601
	[Serializable]
	internal sealed class FunctionAggrSum : FunctionAggrStandard
	{
		// Token: 0x06001390 RID: 5008 RVA: 0x0002EDEB File Offset: 0x0002CFEB
		public FunctionAggrSum(List<IInternalExpression> args)
			: base(args)
		{
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x0002EDF4 File Offset: 0x0002CFF4
		internal override string DisplayText()
		{
			return "Sum";
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x0002EDFC File Offset: 0x0002CFFC
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
					"Sum(",
					this._Expr.WriteSource(nameChanges),
					", ",
					text,
					")"
				});
			}
			return "Sum(" + this._Expr.WriteSource(nameChanges) + ")";
		}
	}
}
