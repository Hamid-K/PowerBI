using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000087 RID: 135
	[OriginalName("DeliveryExtensionSingle")]
	public class DeliveryExtensionSingle : DataServiceQuerySingle<DeliveryExtension>
	{
		// Token: 0x060005EB RID: 1515 RVA: 0x0000BAA5 File Offset: 0x00009CA5
		public DeliveryExtensionSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0000BAAF File Offset: 0x00009CAF
		public DeliveryExtensionSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0000BABA File Offset: 0x00009CBA
		public DeliveryExtensionSingle(DataServiceQuerySingle<DeliveryExtension> query)
			: base(query)
		{
		}
	}
}
