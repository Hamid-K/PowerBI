using System;
using System.IO;

namespace Microsoft.Spatial
{
	// Token: 0x02000047 RID: 71
	internal class WellKnownTextSqlFormatterImplementation : WellKnownTextSqlFormatter
	{
		// Token: 0x06000211 RID: 529 RVA: 0x0000537C File Offset: 0x0000357C
		internal WellKnownTextSqlFormatterImplementation(SpatialImplementation creator)
			: base(creator)
		{
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00005385 File Offset: 0x00003585
		internal WellKnownTextSqlFormatterImplementation(SpatialImplementation creator, bool allowOnlyTwoDimensions)
			: base(creator)
		{
			this.allowOnlyTwoDimensions = allowOnlyTwoDimensions;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00005395 File Offset: 0x00003595
		public override SpatialPipeline CreateWriter(TextWriter target)
		{
			return new ForwardingSegment(new WellKnownTextSqlWriter(target, this.allowOnlyTwoDimensions));
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000053AD File Offset: 0x000035AD
		protected override void ReadGeography(TextReader readerStream, SpatialPipeline pipeline)
		{
			new WellKnownTextSqlReader(pipeline, this.allowOnlyTwoDimensions).ReadGeography(readerStream);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x000053C1 File Offset: 0x000035C1
		protected override void ReadGeometry(TextReader readerStream, SpatialPipeline pipeline)
		{
			new WellKnownTextSqlReader(pipeline, this.allowOnlyTwoDimensions).ReadGeometry(readerStream);
		}

		// Token: 0x0400004A RID: 74
		private readonly bool allowOnlyTwoDimensions;
	}
}
