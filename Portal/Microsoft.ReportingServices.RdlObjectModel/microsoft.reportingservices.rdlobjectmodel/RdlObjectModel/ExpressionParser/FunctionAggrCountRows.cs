using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200024C RID: 588
	[Serializable]
	internal sealed class FunctionAggrCountRows : FunctionAggrStandard
	{
		// Token: 0x06001367 RID: 4967 RVA: 0x0002E81F File Offset: 0x0002CA1F
		public FunctionAggrCountRows(List<IInternalExpression> args)
			: base(args)
		{
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x0002E828 File Offset: 0x0002CA28
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Int32;
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x0002E82C File Offset: 0x0002CA2C
		internal override string DisplayText()
		{
			return "CountRows";
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x0002E834 File Offset: 0x0002CA34
		public override string WriteSource(NameChanges nameChanges)
		{
			string text = string.Empty;
			if (base.Scope != null)
			{
				text = base.GetScopeAsStringForWrite(nameChanges);
				return "CountRows(" + text + ")";
			}
			return "CountRows()";
		}
	}
}
