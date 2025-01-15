using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000258 RID: 600
	[Serializable]
	internal sealed class FunctionAggrStdevp : FunctionAggrStandard
	{
		// Token: 0x0600138C RID: 5004 RVA: 0x0002ED53 File Offset: 0x0002CF53
		public FunctionAggrStdevp(List<IInternalExpression> args)
			: base(args)
		{
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x0002ED5C File Offset: 0x0002CF5C
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Double;
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x0002ED60 File Offset: 0x0002CF60
		internal override string DisplayText()
		{
			return "StDevP";
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x0002ED68 File Offset: 0x0002CF68
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
					"StDevP(",
					this._Expr.WriteSource(nameChanges),
					", ",
					text,
					")"
				});
			}
			return "StDevP(" + this._Expr.WriteSource(nameChanges) + ")";
		}
	}
}
