using System;
using Microsoft.ReportingServices.Portal.Interfaces.Enums;
using Microsoft.ReportingServices.Portal.Interfaces.Models;

namespace Microsoft.ReportingServices.Portal.Services.Models
{
	// Token: 0x02000058 RID: 88
	internal sealed class ResourceCatalogItem : CatalogItem, IResourceCatalogItem, ICatalogItem
	{
		// Token: 0x060002CF RID: 719 RVA: 0x00012966 File Offset: 0x00010B66
		public ResourceCatalogItem()
			: base(CatalogItemType.Resource)
		{
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x0001296F File Offset: 0x00010B6F
		// (set) Token: 0x060002D1 RID: 721 RVA: 0x00012977 File Offset: 0x00010B77
		public byte[] Content { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x00012980 File Offset: 0x00010B80
		// (set) Token: 0x060002D3 RID: 723 RVA: 0x00012988 File Offset: 0x00010B88
		public string ContentType { get; set; }
	}
}
