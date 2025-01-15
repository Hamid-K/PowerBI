using System;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel.Serialization
{
	// Token: 0x020002E7 RID: 743
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	internal sealed class XmlChildAttributeAttribute : XmlAttributeAttribute
	{
		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x060016D1 RID: 5841 RVA: 0x00036010 File Offset: 0x00034210
		public string ElementName
		{
			get
			{
				return this.m_elementName;
			}
		}

		// Token: 0x060016D2 RID: 5842 RVA: 0x00036018 File Offset: 0x00034218
		public XmlChildAttributeAttribute(string elementName, string attributeName)
			: this(elementName, attributeName, null)
		{
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x00036023 File Offset: 0x00034223
		public XmlChildAttributeAttribute(string elementName, string attributeName, string namespaceUri)
			: base(attributeName)
		{
			this.m_elementName = elementName;
			base.Namespace = namespaceUri;
		}

		// Token: 0x04000710 RID: 1808
		private readonly string m_elementName;
	}
}
