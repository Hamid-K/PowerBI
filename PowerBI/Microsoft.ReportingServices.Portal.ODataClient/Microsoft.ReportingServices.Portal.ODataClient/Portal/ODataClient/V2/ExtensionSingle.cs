using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200002D RID: 45
	[OriginalName("ExtensionSingle")]
	public class ExtensionSingle : DataServiceQuerySingle<Extension>
	{
		// Token: 0x060001D9 RID: 473 RVA: 0x0000505E File Offset: 0x0000325E
		public ExtensionSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00005068 File Offset: 0x00003268
		public ExtensionSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00005073 File Offset: 0x00003273
		public ExtensionSingle(DataServiceQuerySingle<Extension> query)
			: base(query)
		{
		}
	}
}
