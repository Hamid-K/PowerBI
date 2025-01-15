using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000253 RID: 595
	[Serializable]
	internal sealed class FunctionAggrRowNumber : FunctionAggr
	{
		// Token: 0x0600137D RID: 4989 RVA: 0x0002EB3B File Offset: 0x0002CD3B
		public FunctionAggrRowNumber(List<IInternalExpression> args)
			: base(args)
		{
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x0002EB44 File Offset: 0x0002CD44
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Int32;
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x0002EB48 File Offset: 0x0002CD48
		internal override string DisplayText()
		{
			return "RowNumber";
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x0002EB50 File Offset: 0x0002CD50
		public override string WriteSource(NameChanges nameChanges)
		{
			string text = string.Empty;
			if (base.Scope != null)
			{
				text = base.GetScopeAsStringForWrite(nameChanges);
				return "RowNumber(" + text + ")";
			}
			return "RowNumber(Nothing)";
		}
	}
}
