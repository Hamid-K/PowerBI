using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000278 RID: 632
	[Serializable]
	internal class FunctionDataSourcesCollection : BaseInternalExpression
	{
		// Token: 0x06001413 RID: 5139 RVA: 0x0002F9ED File Offset: 0x0002DBED
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Object;
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x0002F9F0 File Offset: 0x0002DBF0
		public override string WriteSource()
		{
			return "DataSources";
		}
	}
}
