using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000251 RID: 593
	[Serializable]
	internal sealed class FunctionAggrMax : FunctionAggrStandard
	{
		// Token: 0x06001377 RID: 4983 RVA: 0x0002EA11 File Offset: 0x0002CC11
		public FunctionAggrMax(List<IInternalExpression> args)
			: base(args)
		{
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x0002EA1A File Offset: 0x0002CC1A
		internal override string DisplayText()
		{
			return "Max";
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x0002EA24 File Offset: 0x0002CC24
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
					"Max(",
					this._Expr.WriteSource(nameChanges),
					", ",
					text,
					")"
				});
			}
			return "Max(" + this._Expr.WriteSource(nameChanges) + ")";
		}
	}
}
