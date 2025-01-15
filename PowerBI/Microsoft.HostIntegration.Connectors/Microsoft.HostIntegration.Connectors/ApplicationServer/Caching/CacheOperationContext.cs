using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000AE RID: 174
	public sealed class CacheOperationContext
	{
		// Token: 0x06000411 RID: 1041 RVA: 0x00014244 File Offset: 0x00012444
		private CacheOperationContext()
		{
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x00014257 File Offset: 0x00012457
		internal IDictionary Properties
		{
			get
			{
				return this._propertyBag;
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0001425F File Offset: 0x0001245F
		internal static CacheOperationContext GetUnique()
		{
			return new CacheOperationContext();
		}

		// Token: 0x04000329 RID: 809
		private IDictionary _propertyBag = new Dictionary<object, object>();
	}
}
