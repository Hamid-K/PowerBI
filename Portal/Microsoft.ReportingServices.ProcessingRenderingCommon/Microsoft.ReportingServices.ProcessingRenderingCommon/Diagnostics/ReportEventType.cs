using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200007E RID: 126
	public enum ReportEventType
	{
		// Token: 0x040001CB RID: 459
		Render = 1,
		// Token: 0x040001CC RID: 460
		BookmarkNavigation,
		// Token: 0x040001CD RID: 461
		DocumentMapNavigation,
		// Token: 0x040001CE RID: 462
		DrillThrough,
		// Token: 0x040001CF RID: 463
		FindString,
		// Token: 0x040001D0 RID: 464
		GetDocumentMap,
		// Token: 0x040001D1 RID: 465
		Toggle,
		// Token: 0x040001D2 RID: 466
		Sort,
		// Token: 0x040001D3 RID: 467
		Execute,
		// Token: 0x040001D4 RID: 468
		RenderEdit,
		// Token: 0x040001D5 RID: 469
		ExecuteDataShapeQuery,
		// Token: 0x040001D6 RID: 470
		RenderMobileReport,
		// Token: 0x040001D7 RID: 471
		ConceptualSchema,
		// Token: 0x040001D8 RID: 472
		QueryData,
		// Token: 0x040001D9 RID: 473
		ASModelStream,
		// Token: 0x040001DA RID: 474
		RenderExcelWorkbook,
		// Token: 0x040001DB RID: 475
		GetExcelWorkbookInfo,
		// Token: 0x040001DC RID: 476
		SaveToCatalog,
		// Token: 0x040001DD RID: 477
		DataRefresh
	}
}
