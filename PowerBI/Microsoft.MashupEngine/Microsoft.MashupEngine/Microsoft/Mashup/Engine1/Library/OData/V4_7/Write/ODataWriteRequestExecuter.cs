using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.Write
{
	// Token: 0x02000789 RID: 1929
	internal abstract class ODataWriteRequestExecuter
	{
		// Token: 0x060038A5 RID: 14501 RVA: 0x000B6BF0 File Offset: 0x000B4DF0
		public ODataWriteRequestExecuter(ODataEnvironment environment)
		{
			this.odataEnvironment = environment;
		}

		// Token: 0x1700133E RID: 4926
		// (get) Token: 0x060038A6 RID: 14502 RVA: 0x000B6BFF File Offset: 0x000B4DFF
		public ODataEnvironment OdataEnvironment
		{
			get
			{
				return this.odataEnvironment;
			}
		}

		// Token: 0x060038A7 RID: 14503
		public abstract List<IValueReference> ExecuteODataWriteRequests(List<ODataWriteRequest> crudRequests);

		// Token: 0x060038A8 RID: 14504 RVA: 0x000B6C07 File Offset: 0x000B4E07
		public static ODataWriteRequestExecuter New(ODataEnvironment environment)
		{
			if (environment.Annotations.SupportsBatch)
			{
				return new ODataBatchOperationWriter(environment);
			}
			return new ODataOperationWriter(environment);
		}

		// Token: 0x04001D4F RID: 7503
		private readonly ODataEnvironment odataEnvironment;
	}
}
