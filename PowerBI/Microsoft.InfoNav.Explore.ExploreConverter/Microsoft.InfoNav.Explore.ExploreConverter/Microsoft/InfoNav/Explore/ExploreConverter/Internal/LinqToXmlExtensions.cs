using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000058 RID: 88
	internal static class LinqToXmlExtensions
	{
		// Token: 0x060001C9 RID: 457 RVA: 0x00009FD8 File Offset: 0x000081D8
		public static IEnumerable<XElement> ElementsByLocalName<T>(this T source, string localName) where T : XContainer
		{
			return from e in source.Elements()
				where e.Name.LocalName == localName
				select e;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000A010 File Offset: 0x00008210
		public static XElement ElementByLocalName<T>(this T source, string localName) where T : XContainer
		{
			return source.Elements().FirstOrDefault((XElement e) => e.Name.LocalName == localName);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000A048 File Offset: 0x00008248
		public static XAttribute AttributeByLocalName(this XElement source, string localName)
		{
			return source.Attributes().FirstOrDefault((XAttribute e) => e.Name.LocalName == localName);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000A07C File Offset: 0x0000827C
		public static XElement RequiredElementByLocalName<T>(this T source, string localName) where T : XContainer
		{
			XElement xelement = source.ElementByLocalName(localName);
			Contract.Check(xelement != null, string.Format(CultureInfo.InvariantCulture, "Expect to find a {0} node", new object[] { localName }));
			return xelement;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000A0B4 File Offset: 0x000082B4
		public static XAttribute RequiredAttributeByLocalName(this XElement source, string localName)
		{
			XAttribute xattribute = source.AttributeByLocalName(localName);
			Contract.Check(xattribute != null, string.Format(CultureInfo.InvariantCulture, "Expect to find a {0} attribute", new object[] { localName }));
			return xattribute;
		}
	}
}
