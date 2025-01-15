using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000246 RID: 582
	[Serializable]
	internal sealed class CreateDrillthroughContext : FunctionAggr
	{
		// Token: 0x0600134C RID: 4940 RVA: 0x0002E3E0 File Offset: 0x0002C5E0
		public CreateDrillthroughContext(List<IInternalExpression> args)
			: base(args)
		{
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x0002E3E9 File Offset: 0x0002C5E9
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Boolean;
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x0002E3EC File Offset: 0x0002C5EC
		public override string WriteSource(NameChanges nameChanges)
		{
			return "CreateDrillthroughContext()";
		}
	}
}
