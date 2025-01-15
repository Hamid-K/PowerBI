using System;
using System.Collections.Generic;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000050 RID: 80
	public interface IDelta
	{
		// Token: 0x0600021A RID: 538
		IEnumerable<string> GetChangedPropertyNames();

		// Token: 0x0600021B RID: 539
		IEnumerable<string> GetUnchangedPropertyNames();

		// Token: 0x0600021C RID: 540
		bool TrySetPropertyValue(string name, object value);

		// Token: 0x0600021D RID: 541
		bool TryGetPropertyValue(string name, out object value);

		// Token: 0x0600021E RID: 542
		bool TryGetPropertyType(string name, out Type type);

		// Token: 0x0600021F RID: 543
		void Clear();
	}
}
