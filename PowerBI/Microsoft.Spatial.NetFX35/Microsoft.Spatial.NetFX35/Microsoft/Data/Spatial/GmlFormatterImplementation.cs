using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml;
using Microsoft.Spatial;

namespace Microsoft.Data.Spatial
{
	// Token: 0x02000048 RID: 72
	[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gml", Justification = "Gml is the accepted name in the industry")]
	internal class GmlFormatterImplementation : GmlFormatter
	{
		// Token: 0x060001E4 RID: 484 RVA: 0x00005725 File Offset: 0x00003925
		internal GmlFormatterImplementation(SpatialImplementation creator)
			: base(creator)
		{
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000572E File Offset: 0x0000392E
		public override SpatialPipeline CreateWriter(XmlWriter target)
		{
			return new ForwardingSegment(new GmlWriter(target));
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00005740 File Offset: 0x00003940
		protected override void ReadGeography(XmlReader readerStream, SpatialPipeline pipeline)
		{
			new GmlReader(pipeline).ReadGeography(readerStream);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000574E File Offset: 0x0000394E
		protected override void ReadGeometry(XmlReader readerStream, SpatialPipeline pipeline)
		{
			new GmlReader(pipeline).ReadGeometry(readerStream);
		}
	}
}
