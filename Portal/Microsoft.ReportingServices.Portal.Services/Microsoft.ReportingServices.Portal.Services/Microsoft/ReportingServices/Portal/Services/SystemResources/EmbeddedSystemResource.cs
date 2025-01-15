using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ReportingServices.Library;

namespace Microsoft.ReportingServices.Portal.Services.SystemResources
{
	// Token: 0x02000034 RID: 52
	internal sealed class EmbeddedSystemResource : SystemResource
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000ED8F File Offset: 0x0000CF8F
		// (set) Token: 0x0600022B RID: 555 RVA: 0x0000ED97 File Offset: 0x0000CF97
		internal string PackageName { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000EDA0 File Offset: 0x0000CFA0
		// (set) Token: 0x0600022D RID: 557 RVA: 0x0000EDA8 File Offset: 0x0000CFA8
		internal IDictionary<string, SystemResourceContentItem> Contents { get; private set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600022E RID: 558 RVA: 0x0000EDB4 File Offset: 0x0000CFB4
		internal override IDictionary<string, string> Items
		{
			get
			{
				return this.Contents.ToDictionary((KeyValuePair<string, SystemResourceContentItem> x) => x.Key, (KeyValuePair<string, SystemResourceContentItem> x) => Guid.Empty.ToString());
			}
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000EE0A File Offset: 0x0000D00A
		internal EmbeddedSystemResource()
		{
			this.Contents = new Dictionary<string, SystemResourceContentItem>();
		}
	}
}
