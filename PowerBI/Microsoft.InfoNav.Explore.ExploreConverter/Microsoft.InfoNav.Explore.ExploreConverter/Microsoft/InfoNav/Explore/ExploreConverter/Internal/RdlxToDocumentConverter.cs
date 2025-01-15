using System;
using System.IO;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000059 RID: 89
	public class RdlxToDocumentConverter
	{
		// Token: 0x060001CE RID: 462 RVA: 0x0000A0EC File Offset: 0x000082EC
		internal static PVDocumentRoot ConvertRdlxToDocument(Stream rdlxStream)
		{
			return RdmToDocumentConverter.Convert(ReportArchive.Load(rdlxStream));
		}

		// Token: 0x0400014C RID: 332
		public static readonly string TelemetryPropertyImageCountPerVisual = "ImageCountPerVisual";

		// Token: 0x0400014D RID: 333
		public static readonly string TelemetryPropertyDatabaseImage = "DatabaseImage";

		// Token: 0x0400014E RID: 334
		public static readonly string TelemetryPropertyEmbededImage = "EmbeddedImage";

		// Token: 0x0400014F RID: 335
		public static readonly string TelemetryPropertyExternalImage = "ExternalImage";

		// Token: 0x04000150 RID: 336
		public static readonly string TelemetryPropertyKPI = "KPIImage";
	}
}
