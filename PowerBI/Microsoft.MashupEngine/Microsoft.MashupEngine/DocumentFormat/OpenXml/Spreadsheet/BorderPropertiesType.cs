using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C67 RID: 11367
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Color))]
	internal abstract class BorderPropertiesType : OpenXmlCompositeElement
	{
		// Token: 0x170082AC RID: 33452
		// (get) Token: 0x060182AC RID: 98988 RVA: 0x0033F233 File Offset: 0x0033D433
		internal override string[] AttributeTagNames
		{
			get
			{
				return BorderPropertiesType.attributeTagNames;
			}
		}

		// Token: 0x170082AD RID: 33453
		// (get) Token: 0x060182AD RID: 98989 RVA: 0x0033F23A File Offset: 0x0033D43A
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BorderPropertiesType.attributeNamespaceIds;
			}
		}

		// Token: 0x170082AE RID: 33454
		// (get) Token: 0x060182AE RID: 98990 RVA: 0x0033F241 File Offset: 0x0033D441
		// (set) Token: 0x060182AF RID: 98991 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "style")]
		public EnumValue<BorderStyleValues> Style
		{
			get
			{
				return (EnumValue<BorderStyleValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060182B0 RID: 98992 RVA: 0x0033A76B File Offset: 0x0033896B
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "color" == name)
			{
				return new Color();
			}
			return null;
		}

		// Token: 0x170082AF RID: 33455
		// (get) Token: 0x060182B1 RID: 98993 RVA: 0x0033F250 File Offset: 0x0033D450
		internal override string[] ElementTagNames
		{
			get
			{
				return BorderPropertiesType.eleTagNames;
			}
		}

		// Token: 0x170082B0 RID: 33456
		// (get) Token: 0x060182B2 RID: 98994 RVA: 0x0033F257 File Offset: 0x0033D457
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return BorderPropertiesType.eleNamespaceIds;
			}
		}

		// Token: 0x170082B1 RID: 33457
		// (get) Token: 0x060182B3 RID: 98995 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170082B2 RID: 33458
		// (get) Token: 0x060182B4 RID: 98996 RVA: 0x0033A794 File Offset: 0x00338994
		// (set) Token: 0x060182B5 RID: 98997 RVA: 0x0033A79D File Offset: 0x0033899D
		public Color Color
		{
			get
			{
				return base.GetElement<Color>(0);
			}
			set
			{
				base.SetElement<Color>(0, value);
			}
		}

		// Token: 0x060182B6 RID: 98998 RVA: 0x0033F25E File Offset: 0x0033D45E
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "style" == name)
			{
				return new EnumValue<BorderStyleValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060182B7 RID: 98999 RVA: 0x00293ECF File Offset: 0x002920CF
		protected BorderPropertiesType()
		{
		}

		// Token: 0x060182B8 RID: 99000 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected BorderPropertiesType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060182B9 RID: 99001 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected BorderPropertiesType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060182BA RID: 99002 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected BorderPropertiesType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060182BB RID: 99003 RVA: 0x0033F280 File Offset: 0x0033D480
		// Note: this type is marked as 'beforefieldinit'.
		static BorderPropertiesType()
		{
			byte[] array = new byte[1];
			BorderPropertiesType.attributeNamespaceIds = array;
			BorderPropertiesType.eleTagNames = new string[] { "color" };
			BorderPropertiesType.eleNamespaceIds = new byte[] { 22 };
		}

		// Token: 0x04009F22 RID: 40738
		private static string[] attributeTagNames = new string[] { "style" };

		// Token: 0x04009F23 RID: 40739
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009F24 RID: 40740
		private static readonly string[] eleTagNames;

		// Token: 0x04009F25 RID: 40741
		private static readonly byte[] eleNamespaceIds;
	}
}
