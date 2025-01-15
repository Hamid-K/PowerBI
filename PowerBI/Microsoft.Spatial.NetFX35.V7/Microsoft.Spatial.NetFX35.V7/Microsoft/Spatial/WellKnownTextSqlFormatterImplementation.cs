using System;
using System.IO;

namespace Microsoft.Spatial
{
	// Token: 0x02000042 RID: 66
	internal class WellKnownTextSqlFormatterImplementation : WellKnownTextSqlFormatter
	{
		// Token: 0x0600019B RID: 411 RVA: 0x000046A8 File Offset: 0x000028A8
		internal WellKnownTextSqlFormatterImplementation(SpatialImplementation creator)
			: base(creator)
		{
		}

		// Token: 0x0600019C RID: 412 RVA: 0x000046B1 File Offset: 0x000028B1
		internal WellKnownTextSqlFormatterImplementation(SpatialImplementation creator, bool allowOnlyTwoDimensions)
			: base(creator)
		{
			this.allowOnlyTwoDimensions = allowOnlyTwoDimensions;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x000046C1 File Offset: 0x000028C1
		public override SpatialPipeline CreateWriter(TextWriter target)
		{
			return new ForwardingSegment(new WellKnownTextSqlWriter(target, this.allowOnlyTwoDimensions));
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000046D9 File Offset: 0x000028D9
		protected override void ReadGeography(TextReader readerStream, SpatialPipeline pipeline)
		{
			new WellKnownTextSqlReader(pipeline, this.allowOnlyTwoDimensions).ReadGeography(readerStream);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x000046ED File Offset: 0x000028ED
		protected override void ReadGeometry(TextReader readerStream, SpatialPipeline pipeline)
		{
			new WellKnownTextSqlReader(pipeline, this.allowOnlyTwoDimensions).ReadGeometry(readerStream);
		}

		// Token: 0x0400003D RID: 61
		private readonly bool allowOnlyTwoDimensions;
	}
}
