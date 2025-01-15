using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200024A RID: 586
	[Serializable]
	internal sealed class FunctionAggrCount : FunctionAggrStandard
	{
		// Token: 0x06001360 RID: 4960 RVA: 0x0002E6F7 File Offset: 0x0002C8F7
		public FunctionAggrCount(List<IInternalExpression> args)
			: base(args)
		{
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x0002E700 File Offset: 0x0002C900
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Int32;
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x0002E704 File Offset: 0x0002C904
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
					"Count(",
					this._Expr.WriteSource(nameChanges),
					", ",
					text,
					")"
				});
			}
			return "Count(" + this._Expr.WriteSource(nameChanges) + ")";
		}
	}
}
