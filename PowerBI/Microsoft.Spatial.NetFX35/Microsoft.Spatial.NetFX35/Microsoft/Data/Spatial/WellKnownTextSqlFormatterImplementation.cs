using System;
using System.IO;
using Microsoft.Spatial;

namespace Microsoft.Data.Spatial
{
	// Token: 0x02000046 RID: 70
	internal class WellKnownTextSqlFormatterImplementation : WellKnownTextSqlFormatter
	{
		// Token: 0x060001CD RID: 461 RVA: 0x000051B9 File Offset: 0x000033B9
		internal WellKnownTextSqlFormatterImplementation(SpatialImplementation creator)
			: base(creator)
		{
		}

		// Token: 0x060001CE RID: 462 RVA: 0x000051C2 File Offset: 0x000033C2
		internal WellKnownTextSqlFormatterImplementation(SpatialImplementation creator, bool allowOnlyTwoDimensions)
			: base(creator)
		{
			this.allowOnlyTwoDimensions = allowOnlyTwoDimensions;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x000051D2 File Offset: 0x000033D2
		public override SpatialPipeline CreateWriter(TextWriter target)
		{
			return new ForwardingSegment(new WellKnownTextSqlWriter(target, this.allowOnlyTwoDimensions));
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x000051EA File Offset: 0x000033EA
		protected override void ReadGeography(TextReader readerStream, SpatialPipeline pipeline)
		{
			new WellKnownTextSqlReader(pipeline, this.allowOnlyTwoDimensions).ReadGeography(readerStream);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000051FE File Offset: 0x000033FE
		protected override void ReadGeometry(TextReader readerStream, SpatialPipeline pipeline)
		{
			new WellKnownTextSqlReader(pipeline, this.allowOnlyTwoDimensions).ReadGeometry(readerStream);
		}

		// Token: 0x04000043 RID: 67
		private readonly bool allowOnlyTwoDimensions;
	}
}
