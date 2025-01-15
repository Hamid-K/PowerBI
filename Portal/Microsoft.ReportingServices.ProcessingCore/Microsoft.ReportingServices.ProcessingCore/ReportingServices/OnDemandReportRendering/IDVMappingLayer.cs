using System;
using System.IO;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200013A RID: 314
	internal interface IDVMappingLayer : IDisposable
	{
		// Token: 0x17000814 RID: 2068
		// (set) Token: 0x06000DD3 RID: 3539
		float DpiX { set; }

		// Token: 0x17000815 RID: 2069
		// (set) Token: 0x06000DD4 RID: 3540
		float DpiY { set; }

		// Token: 0x17000816 RID: 2070
		// (set) Token: 0x06000DD5 RID: 3541
		double? WidthOverride { set; }

		// Token: 0x17000817 RID: 2071
		// (set) Token: 0x06000DD6 RID: 3542
		double? HeightOverride { set; }

		// Token: 0x06000DD7 RID: 3543
		Stream GetImage(DynamicImageInstance.ImageType type);

		// Token: 0x06000DD8 RID: 3544
		ActionInfoWithDynamicImageMapCollection GetImageMaps();

		// Token: 0x06000DD9 RID: 3545
		Stream GetCoreXml();
	}
}
