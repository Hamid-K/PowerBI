using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml;

namespace Microsoft.Spatial
{
	// Token: 0x02000049 RID: 73
	[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gml", Justification = "Gml is the accepted name in the industry")]
	internal class GmlFormatterImplementation : GmlFormatter
	{
		// Token: 0x06000228 RID: 552 RVA: 0x000058C7 File Offset: 0x00003AC7
		internal GmlFormatterImplementation(SpatialImplementation creator)
			: base(creator)
		{
		}

		// Token: 0x06000229 RID: 553 RVA: 0x000058D0 File Offset: 0x00003AD0
		public override SpatialPipeline CreateWriter(XmlWriter target)
		{
			return new ForwardingSegment(new GmlWriter(target));
		}

		// Token: 0x0600022A RID: 554 RVA: 0x000058E2 File Offset: 0x00003AE2
		protected override void ReadGeography(XmlReader readerStream, SpatialPipeline pipeline)
		{
			new GmlReader(pipeline).ReadGeography(readerStream);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x000058F0 File Offset: 0x00003AF0
		protected override void ReadGeometry(XmlReader readerStream, SpatialPipeline pipeline)
		{
			new GmlReader(pipeline).ReadGeometry(readerStream);
		}
	}
}
