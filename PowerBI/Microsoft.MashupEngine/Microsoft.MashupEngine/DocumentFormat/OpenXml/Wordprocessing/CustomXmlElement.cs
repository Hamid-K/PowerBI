using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EB8 RID: 11960
	internal abstract class CustomXmlElement : OpenXmlCompositeElement
	{
		// Token: 0x0601974B RID: 104267 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected CustomXmlElement(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601974C RID: 104268 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected CustomXmlElement(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601974D RID: 104269 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected CustomXmlElement(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x17008C21 RID: 35873
		// (get) Token: 0x0601974E RID: 104270 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601974F RID: 104271 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "uri")]
		public StringValue Uri
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17008C22 RID: 35874
		// (get) Token: 0x06019750 RID: 104272 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06019751 RID: 104273 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "element")]
		public StringValue Element
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06019752 RID: 104274 RVA: 0x0034A40F File Offset: 0x0034860F
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "uri" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "element" == name)
			{
				return new StringValue();
			}
			return null;
		}

		// Token: 0x17008C23 RID: 35875
		// (get) Token: 0x06019753 RID: 104275 RVA: 0x0034A442 File Offset: 0x00348642
		// (set) Token: 0x06019754 RID: 104276 RVA: 0x0034A44B File Offset: 0x0034864B
		public CustomXmlProperties CustomXmlProperties
		{
			get
			{
				return base.GetElement<CustomXmlProperties>(0);
			}
			set
			{
				base.SetElement<CustomXmlProperties>(0, value);
			}
		}
	}
}
