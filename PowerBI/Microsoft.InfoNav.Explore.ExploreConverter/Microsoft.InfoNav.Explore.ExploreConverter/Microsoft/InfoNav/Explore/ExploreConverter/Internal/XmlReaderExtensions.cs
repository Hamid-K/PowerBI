using System;
using System.Xml;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x020000B9 RID: 185
	internal static class XmlReaderExtensions
	{
		// Token: 0x060003E5 RID: 997 RVA: 0x00014376 File Offset: 0x00012576
		public static XmlReader ReadSubtreeAndMoveToContent(this XmlReader parent)
		{
			XmlReader xmlReader = parent.ReadSubtree();
			xmlReader.MoveToContent();
			return xmlReader;
		}
	}
}
