using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml;

namespace Microsoft.Spatial
{
	// Token: 0x02000036 RID: 54
	[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gml", Justification = "Gml is the accepted name in the industry")]
	public abstract class GmlFormatter : SpatialFormatter<XmlReader, XmlWriter>
	{
		// Token: 0x06000148 RID: 328 RVA: 0x00003E74 File Offset: 0x00002074
		protected GmlFormatter(SpatialImplementation creator)
			: base(creator)
		{
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00003E7D File Offset: 0x0000207D
		public static GmlFormatter Create()
		{
			return SpatialImplementation.CurrentImplementation.CreateGmlFormatter();
		}
	}
}
