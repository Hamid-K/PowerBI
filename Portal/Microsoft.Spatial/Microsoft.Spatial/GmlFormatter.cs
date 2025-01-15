using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml;

namespace Microsoft.Spatial
{
	// Token: 0x0200003B RID: 59
	[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gml", Justification = "Gml is the accepted name in the industry")]
	public abstract class GmlFormatter : SpatialFormatter<XmlReader, XmlWriter>
	{
		// Token: 0x060001BE RID: 446 RVA: 0x00004B48 File Offset: 0x00002D48
		protected GmlFormatter(SpatialImplementation creator)
			: base(creator)
		{
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00004B51 File Offset: 0x00002D51
		public static GmlFormatter Create()
		{
			return SpatialImplementation.CurrentImplementation.CreateGmlFormatter();
		}
	}
}
