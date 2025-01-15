using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000045 RID: 69
	[OriginalName("ReportParameterDefinitionSingle")]
	public class ReportParameterDefinitionSingle : DataServiceQuerySingle<ReportParameterDefinition>
	{
		// Token: 0x060002FD RID: 765 RVA: 0x000077A6 File Offset: 0x000059A6
		public ReportParameterDefinitionSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x060002FE RID: 766 RVA: 0x000077B0 File Offset: 0x000059B0
		public ReportParameterDefinitionSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x060002FF RID: 767 RVA: 0x000077BB File Offset: 0x000059BB
		public ReportParameterDefinitionSingle(DataServiceQuerySingle<ReportParameterDefinition> query)
			: base(query)
		{
		}
	}
}
