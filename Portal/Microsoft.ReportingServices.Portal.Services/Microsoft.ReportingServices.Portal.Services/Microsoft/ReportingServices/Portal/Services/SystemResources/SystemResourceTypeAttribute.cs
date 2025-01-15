using System;
using Model;

namespace Microsoft.ReportingServices.Portal.Services.SystemResources
{
	// Token: 0x02000031 RID: 49
	[AttributeUsage(AttributeTargets.Class)]
	internal sealed class SystemResourceTypeAttribute : Attribute
	{
		// Token: 0x06000220 RID: 544 RVA: 0x0000EB8F File Offset: 0x0000CD8F
		internal SystemResourceTypeAttribute(SystemResourceType type)
		{
			this.Type = type;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000221 RID: 545 RVA: 0x0000EB9E File Offset: 0x0000CD9E
		// (set) Token: 0x06000222 RID: 546 RVA: 0x0000EBA6 File Offset: 0x0000CDA6
		internal SystemResourceType Type { get; private set; }
	}
}
