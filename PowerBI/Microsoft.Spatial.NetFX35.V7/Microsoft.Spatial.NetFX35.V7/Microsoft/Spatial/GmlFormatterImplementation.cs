using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml;

namespace Microsoft.Spatial
{
	// Token: 0x02000044 RID: 68
	[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gml", Justification = "Gml is the accepted name in the industry")]
	internal class GmlFormatterImplementation : GmlFormatter
	{
		// Token: 0x060001B2 RID: 434 RVA: 0x00004BFF File Offset: 0x00002DFF
		internal GmlFormatterImplementation(SpatialImplementation creator)
			: base(creator)
		{
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00004C08 File Offset: 0x00002E08
		public override SpatialPipeline CreateWriter(XmlWriter target)
		{
			return new ForwardingSegment(new GmlWriter(target));
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00004C1A File Offset: 0x00002E1A
		protected override void ReadGeography(XmlReader readerStream, SpatialPipeline pipeline)
		{
			new GmlReader(pipeline).ReadGeography(readerStream);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00004C28 File Offset: 0x00002E28
		protected override void ReadGeometry(XmlReader readerStream, SpatialPipeline pipeline)
		{
			new GmlReader(pipeline).ReadGeometry(readerStream);
		}
	}
}
