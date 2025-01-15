using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData.V4.Write
{
	// Token: 0x020008A4 RID: 2212
	internal abstract class ODataWriteRequestExecuter
	{
		// Token: 0x06003F51 RID: 16209 RVA: 0x000D0800 File Offset: 0x000CEA00
		public ODataWriteRequestExecuter(ODataEnvironment environment)
		{
			this.odataEnvironment = environment;
		}

		// Token: 0x1700149E RID: 5278
		// (get) Token: 0x06003F52 RID: 16210 RVA: 0x000D080F File Offset: 0x000CEA0F
		public ODataEnvironment OdataEnvironment
		{
			get
			{
				return this.odataEnvironment;
			}
		}

		// Token: 0x06003F53 RID: 16211
		public abstract List<IValueReference> ExecuteODataWriteRequests(List<ODataWriteRequest> crudRequests);

		// Token: 0x06003F54 RID: 16212 RVA: 0x000D0817 File Offset: 0x000CEA17
		public static ODataWriteRequestExecuter New(ODataEnvironment environment)
		{
			if (environment.Annotations.SupportsBatch)
			{
				return new ODataBatchOperationWriter(environment);
			}
			return new ODataOperationWriter(environment);
		}

		// Token: 0x04002144 RID: 8516
		private readonly ODataEnvironment odataEnvironment;
	}
}
