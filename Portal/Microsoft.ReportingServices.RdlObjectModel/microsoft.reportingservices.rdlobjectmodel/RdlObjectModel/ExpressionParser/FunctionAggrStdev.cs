using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000257 RID: 599
	[Serializable]
	internal sealed class FunctionAggrStdev : FunctionAggrStandard
	{
		// Token: 0x06001388 RID: 5000 RVA: 0x0002ECB9 File Offset: 0x0002CEB9
		public FunctionAggrStdev(List<IInternalExpression> args)
			: base(args)
		{
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x0002ECC2 File Offset: 0x0002CEC2
		internal override string DisplayText()
		{
			return "StDev";
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x0002ECC9 File Offset: 0x0002CEC9
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Double;
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x0002ECD0 File Offset: 0x0002CED0
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
					"StDev(",
					this._Expr.WriteSource(nameChanges),
					", ",
					text,
					")"
				});
			}
			return "StDev(" + this._Expr.WriteSource(nameChanges) + ")";
		}
	}
}
