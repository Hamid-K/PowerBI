using System;
using System.IO;

namespace Microsoft.Spatial
{
	// Token: 0x0200003C RID: 60
	public abstract class WellKnownTextSqlFormatter : SpatialFormatter<TextReader, TextWriter>
	{
		// Token: 0x060001C0 RID: 448 RVA: 0x00004B5D File Offset: 0x00002D5D
		protected WellKnownTextSqlFormatter(SpatialImplementation creator)
			: base(creator)
		{
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00004B66 File Offset: 0x00002D66
		public static WellKnownTextSqlFormatter Create()
		{
			return SpatialImplementation.CurrentImplementation.CreateWellKnownTextSqlFormatter();
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00004B72 File Offset: 0x00002D72
		public static WellKnownTextSqlFormatter Create(bool allowOnlyTwoDimensions)
		{
			return SpatialImplementation.CurrentImplementation.CreateWellKnownTextSqlFormatter(allowOnlyTwoDimensions);
		}
	}
}
