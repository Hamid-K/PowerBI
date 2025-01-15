using System;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x02000112 RID: 274
	internal class ReturnedEntitySetAnnotation
	{
		// Token: 0x06000968 RID: 2408 RVA: 0x000279AC File Offset: 0x00025BAC
		public ReturnedEntitySetAnnotation(string entitySetName)
		{
			if (string.IsNullOrEmpty(entitySetName))
			{
				throw Error.ArgumentNullOrEmpty("entitySetName");
			}
			this.EntitySetName = entitySetName;
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x000279CE File Offset: 0x00025BCE
		// (set) Token: 0x0600096A RID: 2410 RVA: 0x000279D6 File Offset: 0x00025BD6
		public string EntitySetName { get; private set; }
	}
}
