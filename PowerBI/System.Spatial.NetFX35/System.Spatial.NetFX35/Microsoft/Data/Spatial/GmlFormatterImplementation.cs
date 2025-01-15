using System;
using System.Spatial;
using System.Xml;

namespace Microsoft.Data.Spatial
{
	// Token: 0x02000047 RID: 71
	internal class GmlFormatterImplementation : GmlFormatter
	{
		// Token: 0x060001DA RID: 474 RVA: 0x000057B5 File Offset: 0x000039B5
		internal GmlFormatterImplementation(SpatialImplementation creator)
			: base(creator)
		{
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000057BE File Offset: 0x000039BE
		public override SpatialPipeline CreateWriter(XmlWriter target)
		{
			return new ForwardingSegment(new GmlWriter(target));
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000057D0 File Offset: 0x000039D0
		protected override void ReadGeography(XmlReader readerStream, SpatialPipeline pipeline)
		{
			new GmlReader(pipeline).ReadGeography(readerStream);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000057DE File Offset: 0x000039DE
		protected override void ReadGeometry(XmlReader readerStream, SpatialPipeline pipeline)
		{
			new GmlReader(pipeline).ReadGeometry(readerStream);
		}
	}
}
