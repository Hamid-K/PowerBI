using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml;

namespace Microsoft.Spatial
{
	// Token: 0x0200003C RID: 60
	[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gml", Justification = "Gml is the accepted name in the industry")]
	public abstract class GmlFormatter : SpatialFormatter<XmlReader, XmlWriter>
	{
		// Token: 0x06000190 RID: 400 RVA: 0x000048E8 File Offset: 0x00002AE8
		protected GmlFormatter(SpatialImplementation creator)
			: base(creator)
		{
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000048F1 File Offset: 0x00002AF1
		public static GmlFormatter Create()
		{
			return SpatialImplementation.CurrentImplementation.CreateGmlFormatter();
		}
	}
}
