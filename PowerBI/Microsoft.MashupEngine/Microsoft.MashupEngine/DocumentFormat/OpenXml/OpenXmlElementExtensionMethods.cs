using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml
{
	// Token: 0x020030CF RID: 12495
	internal static class OpenXmlElementExtensionMethods
	{
		// Token: 0x0601B1E7 RID: 111079 RVA: 0x0036C664 File Offset: 0x0036A864
		internal static int GetXPathIndex(this OpenXmlElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (element.Parent == null)
			{
				return 1;
			}
			int num = 1;
			if (element is OpenXmlMiscNode)
			{
				return 1;
			}
			if (element is OpenXmlUnknownElement)
			{
				using (IEnumerator<OpenXmlElement> enumerator = element.Parent.ChildElements.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						OpenXmlElement openXmlElement = enumerator.Current;
						if (element == openXmlElement)
						{
							return num;
						}
						if (openXmlElement is OpenXmlUnknownElement && openXmlElement.NamespaceUri == element.NamespaceUri && openXmlElement.LocalName == element.LocalName)
						{
							num++;
						}
					}
					return num;
				}
			}
			Type type = element.GetType();
			foreach (OpenXmlElement openXmlElement2 in element.Parent.ChildElements)
			{
				if (element == openXmlElement2)
				{
					return num;
				}
				if (type == openXmlElement2.GetType())
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x0601B1E8 RID: 111080 RVA: 0x0036C780 File Offset: 0x0036A980
		internal static OpenXmlPart GetPart(this OpenXmlElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			OpenXmlPartRootElement partRootElement = element.GetPartRootElement();
			if (partRootElement != null && partRootElement.OpenXmlPart != null)
			{
				return partRootElement.OpenXmlPart;
			}
			return null;
		}

		// Token: 0x0601B1E9 RID: 111081 RVA: 0x0036C7B8 File Offset: 0x0036A9B8
		internal static Uri GetPartUri(this OpenXmlElement element)
		{
			OpenXmlPart part = element.GetPart();
			if (part != null)
			{
				return part.Uri;
			}
			return null;
		}

		// Token: 0x0601B1EA RID: 111082 RVA: 0x0036C7D7 File Offset: 0x0036A9D7
		internal static object CreateInstance(string typeName)
		{
			return Activator.CreateInstance(null, typeName);
		}

		// Token: 0x0601B1EB RID: 111083 RVA: 0x0036C7E0 File Offset: 0x0036A9E0
		internal static string GetAttributeValueEx(this OpenXmlElement element, string localName, string namespaceUri)
		{
			string text;
			try
			{
				text = element.GetAttribute(localName, namespaceUri).Value;
			}
			catch (KeyNotFoundException)
			{
				text = null;
			}
			return text;
		}

		// Token: 0x0601B1EC RID: 111084 RVA: 0x0036C818 File Offset: 0x0036AA18
		internal static XmlQualifiedName GetFixedAttributeQname(this OpenXmlElement element, int attriuteIndex)
		{
			string text = element.AttributeTagNames[attriuteIndex];
			string namespaceUri = NamespaceIdMap.GetNamespaceUri(element.AttributeNamespaceIds[attriuteIndex]);
			return new XmlQualifiedName(text, namespaceUri);
		}

		// Token: 0x0601B1ED RID: 111085 RVA: 0x0036C844 File Offset: 0x0036AA44
		internal static ICollection<OpenXmlElement> CreateChildrenElementsByIds(this OpenXmlElement parent, IEnumerable<int> elementIds)
		{
			List<OpenXmlElement> list = new List<OpenXmlElement>();
			if (elementIds.Count<int>() > 0)
			{
				Attribute[] customAttributes = Attribute.GetCustomAttributes(parent.GetType(), typeof(ChildElementInfoAttribute));
				foreach (ChildElementInfoAttribute childElementInfoAttribute in customAttributes)
				{
					Type elementType = childElementInfoAttribute.ElementType;
					OpenXmlElement openXmlElement = (OpenXmlElement)Activator.CreateInstance(elementType);
					foreach (int num in elementIds)
					{
						if (openXmlElement.ElementTypeId == num)
						{
							list.Add(openXmlElement);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0601B1EE RID: 111086 RVA: 0x0036C8FC File Offset: 0x0036AAFC
		internal static bool CanContainsChild(this OpenXmlElement parent, OpenXmlElement child)
		{
			if (parent is OpenXmlCompositeElement)
			{
				Attribute[] customAttributes = Attribute.GetCustomAttributes(parent.GetType(), typeof(ChildElementInfoAttribute));
				foreach (ChildElementInfoAttribute childElementInfoAttribute in customAttributes)
				{
					if (childElementInfoAttribute.ElementType.IsInstanceOfType(child))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0601B1EF RID: 111087 RVA: 0x0036C95C File Offset: 0x0036AB5C
		internal static OpenXmlElement TryCreateValidChild(this OpenXmlElement parent, FileFormatVersions fileFormat, string namespaceUri, string localName)
		{
			OpenXmlElement openXmlElement = parent.ElementFactory(string.Empty, localName, namespaceUri);
			if (openXmlElement is OpenXmlUnknownElement || !openXmlElement.IsInVersion(fileFormat))
			{
				return null;
			}
			return openXmlElement;
		}
	}
}
