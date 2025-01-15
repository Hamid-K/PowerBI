using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000250 RID: 592
	[Serializable]
	internal sealed class FunctionAggrLevel : FunctionAggr
	{
		// Token: 0x06001374 RID: 4980 RVA: 0x0002E9CB File Offset: 0x0002CBCB
		public FunctionAggrLevel(List<IInternalExpression> args)
			: base(args)
		{
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x0002E9D4 File Offset: 0x0002CBD4
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Double;
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x0002E9D8 File Offset: 0x0002CBD8
		public override string WriteSource(NameChanges nameChanges)
		{
			string text = string.Empty;
			if (base.Scope == null)
			{
				text = base.GetScopeAsStringForWrite(nameChanges);
				return "Level(" + text + ")";
			}
			return "Level()";
		}
	}
}
