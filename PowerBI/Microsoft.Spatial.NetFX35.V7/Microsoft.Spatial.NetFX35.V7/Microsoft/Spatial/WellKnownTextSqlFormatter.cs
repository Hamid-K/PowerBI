using System;
using System.IO;

namespace Microsoft.Spatial
{
	// Token: 0x02000037 RID: 55
	public abstract class WellKnownTextSqlFormatter : SpatialFormatter<TextReader, TextWriter>
	{
		// Token: 0x0600014A RID: 330 RVA: 0x00003E89 File Offset: 0x00002089
		protected WellKnownTextSqlFormatter(SpatialImplementation creator)
			: base(creator)
		{
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00003E92 File Offset: 0x00002092
		public static WellKnownTextSqlFormatter Create()
		{
			return SpatialImplementation.CurrentImplementation.CreateWellKnownTextSqlFormatter();
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00003E9E File Offset: 0x0000209E
		public static WellKnownTextSqlFormatter Create(bool allowOnlyTwoDimensions)
		{
			return SpatialImplementation.CurrentImplementation.CreateWellKnownTextSqlFormatter(allowOnlyTwoDimensions);
		}
	}
}
