using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200013F RID: 319
	internal static class MapMapperFactory
	{
		// Token: 0x06000DDF RID: 3551 RVA: 0x0003B085 File Offset: 0x00039285
		internal static IMapMapper CreateMapMapperInstance(Map map, string defaultFontFamily)
		{
			return new MapMapper(map, defaultFontFamily);
		}
	}
}
