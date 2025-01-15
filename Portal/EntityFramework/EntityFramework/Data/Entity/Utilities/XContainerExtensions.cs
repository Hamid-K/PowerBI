using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace System.Data.Entity.Utilities
{
	// Token: 0x02000085 RID: 133
	internal static class XContainerExtensions
	{
		// Token: 0x0600045D RID: 1117 RVA: 0x000103A8 File Offset: 0x0000E5A8
		public static XElement GetOrAddElement(this XContainer container, XName name)
		{
			XElement xelement = container.Element(name);
			if (xelement == null)
			{
				xelement = new XElement(name);
				container.Add(xelement);
			}
			return xelement;
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x000103CF File Offset: 0x0000E5CF
		public static IEnumerable<XElement> Descendants(this XContainer container, IEnumerable<XName> name)
		{
			return name.SelectMany(new Func<XName, IEnumerable<XElement>>(container.Descendants));
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x000103E3 File Offset: 0x0000E5E3
		public static IEnumerable<XElement> Elements(this XContainer container, IEnumerable<XName> name)
		{
			return name.SelectMany(new Func<XName, IEnumerable<XElement>>(container.Elements));
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x000103F8 File Offset: 0x0000E5F8
		public static IEnumerable<XElement> Descendants<T>(this IEnumerable<T> source, IEnumerable<XName> name) where T : XContainer
		{
			return name.SelectMany((XName n) => source.SelectMany((T c) => c.Descendants(n)));
		}
	}
}
