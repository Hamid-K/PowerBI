using System;
using Microsoft.DataShaping.Processing;
using Microsoft.ReportingServices.DataShapeQueryTranslation;

namespace Microsoft.DataShaping.Engine
{
	// Token: 0x02000009 RID: 9
	internal sealed class EngineComponentsFactory
	{
		// Token: 0x0400002E RID: 46
		internal static readonly IDataShapeQueryTranslator DefaultDsqTranslator = new DataShapeQueryTranslator();

		// Token: 0x0400002F RID: 47
		internal static readonly IDataShapeProcessor DefaultProcessor = new DataShapeProcessor();
	}
}
