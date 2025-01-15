using System;
using System.Linq;
using System.Xml.Linq;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x0200007D RID: 125
	internal static class XmlExtensions
	{
		// Token: 0x060004B0 RID: 1200 RVA: 0x0000C3D4 File Offset: 0x0000A5D4
		internal static string GetAttribute(this XElement element, XName attributeName)
		{
			XAttribute xattribute = element.Attribute(attributeName);
			if (xattribute != null)
			{
				return xattribute.Value;
			}
			return null;
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0000C3F4 File Offset: 0x0000A5F4
		internal static XElement GetOrAddFirst(this XElement element, XName childName)
		{
			XElement xelement = element.Element(childName);
			if (xelement == null)
			{
				xelement = new XElement(childName);
				element.AddFirst(xelement);
			}
			return xelement;
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0000C41B File Offset: 0x0000A61B
		internal static XAttribute GetXmlnsAttribute(this XElement element)
		{
			return element.Attribute("xmlns");
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0000C430 File Offset: 0x0000A630
		internal static string GetXText(this XElement element)
		{
			XText xtext = element.Nodes().OfType<XText>().FirstOrDefault<XText>();
			if (xtext == null)
			{
				return string.Empty;
			}
			return xtext.Value;
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0000C460 File Offset: 0x0000A660
		internal static void OverrideDefaultXmlNamespace(this XElement element, XNamespace xmlns)
		{
			XNamespace @namespace = element.Name.Namespace;
			XmlExtensions.OverrideDefaultXmlNamespaceImpl(element, xmlns);
			foreach (XElement xelement in element.Descendants())
			{
				if (xelement.Name.Namespace == @namespace)
				{
					XmlExtensions.OverrideDefaultXmlNamespaceImpl(xelement, xmlns);
				}
			}
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0000C4D4 File Offset: 0x0000A6D4
		private static void OverrideDefaultXmlNamespaceImpl(XElement element, XNamespace xmlns)
		{
			XAttribute xmlnsAttribute = element.GetXmlnsAttribute();
			if (xmlnsAttribute != null)
			{
				xmlnsAttribute.Value = xmlns.NamespaceName;
			}
			element.Name = xmlns + element.Name.LocalName;
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0000C510 File Offset: 0x0000A710
		internal static void RemoveAttributes(this XElement element, params XName[] attributeNames)
		{
			foreach (XName xname in attributeNames)
			{
				XAttribute xattribute = element.Attribute(xname);
				if (xattribute != null)
				{
					xattribute.Remove();
				}
			}
		}
	}
}
