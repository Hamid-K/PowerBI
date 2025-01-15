using System;
using System.IO;
using System.Spatial;

namespace Microsoft.Data.Spatial
{
	// Token: 0x02000045 RID: 69
	internal class WellKnownTextSqlFormatterImplementation : WellKnownTextSqlFormatter
	{
		// Token: 0x060001C3 RID: 451 RVA: 0x00005249 File Offset: 0x00003449
		internal WellKnownTextSqlFormatterImplementation(SpatialImplementation creator)
			: base(creator)
		{
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00005252 File Offset: 0x00003452
		internal WellKnownTextSqlFormatterImplementation(SpatialImplementation creator, bool allowOnlyTwoDimensions)
			: base(creator)
		{
			this.allowOnlyTwoDimensions = allowOnlyTwoDimensions;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00005262 File Offset: 0x00003462
		public override SpatialPipeline CreateWriter(TextWriter target)
		{
			return new ForwardingSegment(new WellKnownTextSqlWriter(target, this.allowOnlyTwoDimensions));
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000527A File Offset: 0x0000347A
		protected override void ReadGeography(TextReader readerStream, SpatialPipeline pipeline)
		{
			new WellKnownTextSqlReader(pipeline, this.allowOnlyTwoDimensions).ReadGeography(readerStream);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000528E File Offset: 0x0000348E
		protected override void ReadGeometry(TextReader readerStream, SpatialPipeline pipeline)
		{
			new WellKnownTextSqlReader(pipeline, this.allowOnlyTwoDimensions).ReadGeometry(readerStream);
		}

		// Token: 0x04000041 RID: 65
		private readonly bool allowOnlyTwoDimensions;
	}
}
