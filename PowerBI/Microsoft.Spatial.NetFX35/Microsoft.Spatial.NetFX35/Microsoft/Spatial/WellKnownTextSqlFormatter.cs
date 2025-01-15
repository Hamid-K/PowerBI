using System;
using System.IO;

namespace Microsoft.Spatial
{
	// Token: 0x0200003D RID: 61
	public abstract class WellKnownTextSqlFormatter : SpatialFormatter<TextReader, TextWriter>
	{
		// Token: 0x06000192 RID: 402 RVA: 0x000048FD File Offset: 0x00002AFD
		protected WellKnownTextSqlFormatter(SpatialImplementation creator)
			: base(creator)
		{
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00004906 File Offset: 0x00002B06
		public static WellKnownTextSqlFormatter Create()
		{
			return SpatialImplementation.CurrentImplementation.CreateWellKnownTextSqlFormatter();
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00004912 File Offset: 0x00002B12
		public static WellKnownTextSqlFormatter Create(bool allowOnlyTwoDimensions)
		{
			return SpatialImplementation.CurrentImplementation.CreateWellKnownTextSqlFormatter(allowOnlyTwoDimensions);
		}
	}
}
