using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200025B RID: 603
	[Serializable]
	internal sealed class FunctionAggrVarp : FunctionAggrStandard
	{
		// Token: 0x06001397 RID: 5015 RVA: 0x0002EF17 File Offset: 0x0002D117
		public FunctionAggrVarp(List<IInternalExpression> args)
			: base(args)
		{
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x0002EF20 File Offset: 0x0002D120
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Double;
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x0002EF24 File Offset: 0x0002D124
		internal override string DisplayText()
		{
			return "VarP";
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x0002EF2C File Offset: 0x0002D12C
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
					"VarP(",
					this._Expr.WriteSource(nameChanges),
					", ",
					text,
					")"
				});
			}
			return "VarP(" + this._Expr.WriteSource(nameChanges) + ")";
		}
	}
}
