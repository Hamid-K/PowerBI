using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Edm.Csdl.Parsing.Common
{
	// Token: 0x020001B1 RID: 433
	internal class XmlElementInfo : IXmlElementAttributes
	{
		// Token: 0x06000C20 RID: 3104 RVA: 0x000232B0 File Offset: 0x000214B0
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

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000C21 RID: 3105 RVA: 0x00023338 File Offset: 0x00021538
		IEnumerable<XmlAttributeInfo> IXmlElementAttributes.Unused
		{
			get
			{
				if (this.attributes != null)
				{
					foreach (XmlAttributeInfo xmlAttributeInfo in Enumerable.Where<XmlAttributeInfo>(this.attributes.Values, (XmlAttributeInfo attr) => !attr.IsUsed))
					{
						yield return xmlAttributeInfo;
					}
					IEnumerator<XmlAttributeInfo> enumerator = null;
				}
				yield break;
				yield break;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000C22 RID: 3106 RVA: 0x00023355 File Offset: 0x00021555
		// (set) Token: 0x06000C23 RID: 3107 RVA: 0x0002335D File Offset: 0x0002155D
		internal string Name { get; private set; }

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x00023366 File Offset: 0x00021566
		// (set) Token: 0x06000C25 RID: 3109 RVA: 0x0002336E File Offset: 0x0002156E
		internal CsdlLocation Location { get; private set; }

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000C26 RID: 3110 RVA: 0x0001402B File Offset: 0x0001222B
		internal IXmlElementAttributes Attributes
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000C27 RID: 3111 RVA: 0x00023378 File Offset: 0x00021578
		internal IList<XmlAnnotationInfo> Annotations
		{
			get
			{
				IList<XmlAnnotationInfo> list = this.annotations;
				return list ?? new XmlAnnotationInfo[0];
			}
		}

		// Token: 0x170003DF RID: 991
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

		// Token: 0x06000C29 RID: 3113 RVA: 0x000233CB File Offset: 0x000215CB
		internal void AddAnnotation(XmlAnnotationInfo annotation)
		{
			if (this.annotations == null)
			{
				this.annotations = new List<XmlAnnotationInfo>();
			}
			this.annotations.Add(annotation);
		}

		// Token: 0x040006AD RID: 1709
		private readonly Dictionary<string, XmlAttributeInfo> attributes;

		// Token: 0x040006AE RID: 1710
		private List<XmlAnnotationInfo> annotations;
	}
}
