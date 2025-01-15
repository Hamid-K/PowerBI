using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200024B RID: 587
	[Serializable]
	internal sealed class FunctionAggrCountDistinct : FunctionAggrStandard
	{
		// Token: 0x06001363 RID: 4963 RVA: 0x0002E787 File Offset: 0x0002C987
		public FunctionAggrCountDistinct(List<IInternalExpression> args)
			: base(args)
		{
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x0002E790 File Offset: 0x0002C990
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Int32;
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x0002E794 File Offset: 0x0002C994
		internal override string DisplayText()
		{
			return "CountDistinct";
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x0002E79C File Offset: 0x0002C99C
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
					"CountDistinct(",
					this._Expr.WriteSource(nameChanges),
					", ",
					text,
					")"
				});
			}
			return "CountDistinct(" + this._Expr.WriteSource(nameChanges) + ")";
		}
	}
}
