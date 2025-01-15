using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Edm.Csdl.Parsing.Common
{
	// Token: 0x02000195 RID: 405
	internal class XmlElementInfo : IXmlElementAttributes
	{
		// Token: 0x060007C6 RID: 1990 RVA: 0x000131E4 File Offset: 0x000113E4
		internal XmlElementInfo(string elementName, CsdlLocation elementLocation, IList<XmlAttributeInfo> attributes, List<XmlAnnotationInfo> annotations)
		{
			this.Name = elementName;
			this.Location = elementLocation;
			if (attributes != null && attributes.Count > 0)
			{
				this.attributes = new Dictionary<string, XmlAttributeInfo>();
				foreach (XmlAttributeInfo xmlAttributeInfo in attributes)
				{
					this.attributes.Add(xmlAttributeInfo.Name, xmlAttributeInfo);
				}
			}
			this.annotations = annotations;
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x060007C7 RID: 1991 RVA: 0x00013444 File Offset: 0x00011644
		IEnumerable<XmlAttributeInfo> IXmlElementAttributes.Unused
		{
			get
			{
				if (this.attributes != null)
				{
					foreach (XmlAttributeInfo attr2 in Enumerable.Where<XmlAttributeInfo>(this.attributes.Values, (XmlAttributeInfo attr) => !attr.IsUsed))
					{
						yield return attr2;
					}
				}
				yield break;
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x060007C8 RID: 1992 RVA: 0x00013461 File Offset: 0x00011661
		// (set) Token: 0x060007C9 RID: 1993 RVA: 0x00013469 File Offset: 0x00011669
		internal string Name { get; private set; }

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x00013472 File Offset: 0x00011672
		// (set) Token: 0x060007CB RID: 1995 RVA: 0x0001347A File Offset: 0x0001167A
		internal CsdlLocation Location { get; private set; }

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x00013483 File Offset: 0x00011683
		internal IXmlElementAttributes Attributes
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x00013486 File Offset: 0x00011686
		internal IList<XmlAnnotationInfo> Annotations
		{
			get
			{
				return this.annotations ?? ((IList<XmlAnnotationInfo>)new XmlAnnotationInfo[0]);
			}
		}

		// Token: 0x17000337 RID: 823
		XmlAttributeInfo IXmlElementAttributes.this[string attributeName]
		{
			get
			{
				XmlAttributeInfo xmlAttributeInfo;
				if (this.attributes != null && this.attributes.TryGetValue(attributeName, ref xmlAttributeInfo))
				{
					xmlAttributeInfo.IsUsed = true;
					return xmlAttributeInfo;
				}
				return XmlAttributeInfo.Missing;
			}
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x000134D3 File Offset: 0x000116D3
		internal void AddAnnotation(XmlAnnotationInfo annotation)
		{
			if (this.annotations == null)
			{
				this.annotations = new List<XmlAnnotationInfo>();
			}
			this.annotations.Add(annotation);
		}

		// Token: 0x04000404 RID: 1028
		private readonly Dictionary<string, XmlAttributeInfo> attributes;

		// Token: 0x04000405 RID: 1029
		private List<XmlAnnotationInfo> annotations;
	}
}
