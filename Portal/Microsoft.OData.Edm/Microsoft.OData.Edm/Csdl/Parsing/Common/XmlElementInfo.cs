using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Edm.Csdl.Parsing.Common
{
	// Token: 0x020001BE RID: 446
	internal class XmlElementInfo : IXmlElementAttributes
	{
		// Token: 0x06000CD2 RID: 3282 RVA: 0x00025478 File Offset: 0x00023678
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

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000CD3 RID: 3283 RVA: 0x00025500 File Offset: 0x00023700
		IEnumerable<XmlAttributeInfo> IXmlElementAttributes.Unused
		{
			get
			{
				if (this.attributes != null)
				{
					foreach (XmlAttributeInfo xmlAttributeInfo in this.attributes.Values.Where((XmlAttributeInfo attr) => !attr.IsUsed))
					{
						yield return xmlAttributeInfo;
					}
					IEnumerator<XmlAttributeInfo> enumerator = null;
				}
				yield break;
				yield break;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000CD4 RID: 3284 RVA: 0x0002551D File Offset: 0x0002371D
		// (set) Token: 0x06000CD5 RID: 3285 RVA: 0x00025525 File Offset: 0x00023725
		internal string Name { get; private set; }

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x0002552E File Offset: 0x0002372E
		// (set) Token: 0x06000CD7 RID: 3287 RVA: 0x00025536 File Offset: 0x00023736
		internal CsdlLocation Location { get; private set; }

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000CD8 RID: 3288 RVA: 0x0001250B File Offset: 0x0001070B
		internal IXmlElementAttributes Attributes
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x00025540 File Offset: 0x00023740
		internal IList<XmlAnnotationInfo> Annotations
		{
			get
			{
				IList<XmlAnnotationInfo> list = this.annotations;
				return list ?? new XmlAnnotationInfo[0];
			}
		}

		// Token: 0x17000429 RID: 1065
		XmlAttributeInfo IXmlElementAttributes.this[string attributeName]
		{
			get
			{
				XmlAttributeInfo xmlAttributeInfo;
				if (this.attributes != null && this.attributes.TryGetValue(attributeName, out xmlAttributeInfo))
				{
					xmlAttributeInfo.IsUsed = true;
					return xmlAttributeInfo;
				}
				return XmlAttributeInfo.Missing;
			}
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x00025593 File Offset: 0x00023793
		internal void AddAnnotation(XmlAnnotationInfo annotation)
		{
			if (this.annotations == null)
			{
				this.annotations = new List<XmlAnnotationInfo>();
			}
			this.annotations.Add(annotation);
		}

		// Token: 0x04000726 RID: 1830
		private readonly Dictionary<string, XmlAttributeInfo> attributes;

		// Token: 0x04000727 RID: 1831
		private List<XmlAnnotationInfo> annotations;
	}
}
