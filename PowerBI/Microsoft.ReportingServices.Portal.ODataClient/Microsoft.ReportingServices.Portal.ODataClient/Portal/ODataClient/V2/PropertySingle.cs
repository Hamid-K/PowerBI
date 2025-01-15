using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000014 RID: 20
	[OriginalName("PropertySingle")]
	public class PropertySingle : DataServiceQuerySingle<Property>
	{
		// Token: 0x060000C3 RID: 195 RVA: 0x00003175 File Offset: 0x00001375
		public PropertySingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000317F File Offset: 0x0000137F
		public PropertySingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000318A File Offset: 0x0000138A
		public PropertySingle(DataServiceQuerySingle<Property> query)
			: base(query)
		{
		}
	}
}
