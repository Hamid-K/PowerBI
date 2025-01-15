using System;
using System.Xml;

namespace System.Spatial
{
	// Token: 0x0200003A RID: 58
	public abstract class GmlFormatter : SpatialFormatter<XmlReader, XmlWriter>
	{
		// Token: 0x0600017F RID: 383 RVA: 0x000047D0 File Offset: 0x000029D0
		protected GmlFormatter(SpatialImplementation creator)
			: base(creator)
		{
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000047D9 File Offset: 0x000029D9
		public static GmlFormatter Create()
		{
			return SpatialImplementation.CurrentImplementation.CreateGmlFormatter();
		}
	}
}
