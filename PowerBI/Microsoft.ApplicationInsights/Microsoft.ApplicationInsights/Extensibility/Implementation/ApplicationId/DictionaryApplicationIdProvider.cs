using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.ApplicationId
{
	// Token: 0x020000C6 RID: 198
	public class DictionaryApplicationIdProvider : IApplicationIdProvider
	{
		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x000179CA File Offset: 0x00015BCA
		// (set) Token: 0x0600067D RID: 1661 RVA: 0x000179D2 File Offset: 0x00015BD2
		public IReadOnlyDictionary<string, string> Defined { get; set; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x000179DB File Offset: 0x00015BDB
		// (set) Token: 0x0600067F RID: 1663 RVA: 0x000179E3 File Offset: 0x00015BE3
		public IApplicationIdProvider Next { get; set; }

		// Token: 0x06000680 RID: 1664 RVA: 0x000179EC File Offset: 0x00015BEC
		public bool TryGetApplicationId(string instrumentationKey, out string applicationId)
		{
			applicationId = null;
			IReadOnlyDictionary<string, string> defined = this.Defined;
			bool flag = defined != null && defined.TryGetValue(instrumentationKey, out applicationId);
			if (!flag)
			{
				IApplicationIdProvider next = this.Next;
				flag = next != null && next.TryGetApplicationId(instrumentationKey, out applicationId);
			}
			return flag;
		}
	}
}
