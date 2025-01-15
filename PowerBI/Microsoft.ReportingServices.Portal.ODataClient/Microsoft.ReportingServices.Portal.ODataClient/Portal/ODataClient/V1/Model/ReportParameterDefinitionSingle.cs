using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000BB RID: 187
	[OriginalName("ReportParameterDefinitionSingle")]
	public class ReportParameterDefinitionSingle : DataServiceQuerySingle<ReportParameterDefinition>
	{
		// Token: 0x060007FC RID: 2044 RVA: 0x00010254 File Offset: 0x0000E454
		public ReportParameterDefinitionSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0001025E File Offset: 0x0000E45E
		public ReportParameterDefinitionSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x00010269 File Offset: 0x0000E469
		public ReportParameterDefinitionSingle(DataServiceQuerySingle<ReportParameterDefinition> query)
			: base(query)
		{
		}
	}
}
