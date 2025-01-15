using System;
using System.IO;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002BA RID: 698
	public interface IDynamicImageInstance
	{
		// Token: 0x06001A9F RID: 6815
		void SetDpi(int xDpi, int yDpi);

		// Token: 0x06001AA0 RID: 6816
		void SetSize(double width, double height);

		// Token: 0x06001AA1 RID: 6817
		Stream GetImage(DynamicImageInstance.ImageType type, out ActionInfoWithDynamicImageMapCollection actionImageMaps);
	}
}
