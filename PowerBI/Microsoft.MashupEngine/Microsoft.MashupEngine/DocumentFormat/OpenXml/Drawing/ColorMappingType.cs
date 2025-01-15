using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027E4 RID: 10212
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ExtensionList))]
	internal abstract class ColorMappingType : OpenXmlCompositeElement
	{
		// Token: 0x17006452 RID: 25682
		// (get) Token: 0x06013E52 RID: 81490 RVA: 0x0030CEC8 File Offset: 0x0030B0C8
		internal override string[] AttributeTagNames
		{
			get
			{
				return ColorMappingType.attributeTagNames;
			}
		}

		// Token: 0x17006453 RID: 25683
		// (get) Token: 0x06013E53 RID: 81491 RVA: 0x0030CECF File Offset: 0x0030B0CF
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ColorMappingType.attributeNamespaceIds;
			}
		}

		// Token: 0x17006454 RID: 25684
		// (get) Token: 0x06013E54 RID: 81492 RVA: 0x002FA667 File Offset: 0x002F8867
		// (set) Token: 0x06013E55 RID: 81493 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "bg1")]
		public EnumValue<ColorSchemeIndexValues> Background1
		{
			get
			{
				return (EnumValue<ColorSchemeIndexValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17006455 RID: 25685
		// (get) Token: 0x06013E56 RID: 81494 RVA: 0x002FA676 File Offset: 0x002F8876
		// (set) Token: 0x06013E57 RID: 81495 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "tx1")]
		public EnumValue<ColorSchemeIndexValues> Text1
		{
			get
			{
				return (EnumValue<ColorSchemeIndexValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17006456 RID: 25686
		// (get) Token: 0x06013E58 RID: 81496 RVA: 0x002FA685 File Offset: 0x002F8885
		// (set) Token: 0x06013E59 RID: 81497 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "bg2")]
		public EnumValue<ColorSchemeIndexValues> Background2
		{
			get
			{
				return (EnumValue<ColorSchemeIndexValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17006457 RID: 25687
		// (get) Token: 0x06013E5A RID: 81498 RVA: 0x002FA694 File Offset: 0x002F8894
		// (set) Token: 0x06013E5B RID: 81499 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "tx2")]
		public EnumValue<ColorSchemeIndexValues> Text2
		{
			get
			{
				return (EnumValue<ColorSchemeIndexValues>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17006458 RID: 25688
		// (get) Token: 0x06013E5C RID: 81500 RVA: 0x002FA6A3 File Offset: 0x002F88A3
		// (set) Token: 0x06013E5D RID: 81501 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "accent1")]
		public EnumValue<ColorSchemeIndexValues> Accent1
		{
			get
			{
				return (EnumValue<ColorSchemeIndexValues>)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17006459 RID: 25689
		// (get) Token: 0x06013E5E RID: 81502 RVA: 0x002FA6B2 File Offset: 0x002F88B2
		// (set) Token: 0x06013E5F RID: 81503 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "accent2")]
		public EnumValue<ColorSchemeIndexValues> Accent2
		{
			get
			{
				return (EnumValue<ColorSchemeIndexValues>)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x1700645A RID: 25690
		// (get) Token: 0x06013E60 RID: 81504 RVA: 0x002FA6C1 File Offset: 0x002F88C1
		// (set) Token: 0x06013E61 RID: 81505 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "accent3")]
		public EnumValue<ColorSchemeIndexValues> Accent3
		{
			get
			{
				return (EnumValue<ColorSchemeIndexValues>)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x1700645B RID: 25691
		// (get) Token: 0x06013E62 RID: 81506 RVA: 0x002FA6D0 File Offset: 0x002F88D0
		// (set) Token: 0x06013E63 RID: 81507 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "accent4")]
		public EnumValue<ColorSchemeIndexValues> Accent4
		{
			get
			{
				return (EnumValue<ColorSchemeIndexValues>)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x1700645C RID: 25692
		// (get) Token: 0x06013E64 RID: 81508 RVA: 0x002FA6DF File Offset: 0x002F88DF
		// (set) Token: 0x06013E65 RID: 81509 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "accent5")]
		public EnumValue<ColorSchemeIndexValues> Accent5
		{
			get
			{
				return (EnumValue<ColorSchemeIndexValues>)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x1700645D RID: 25693
		// (get) Token: 0x06013E66 RID: 81510 RVA: 0x002FA6EE File Offset: 0x002F88EE
		// (set) Token: 0x06013E67 RID: 81511 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "accent6")]
		public EnumValue<ColorSchemeIndexValues> Accent6
		{
			get
			{
				return (EnumValue<ColorSchemeIndexValues>)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x1700645E RID: 25694
		// (get) Token: 0x06013E68 RID: 81512 RVA: 0x002FA6FE File Offset: 0x002F88FE
		// (set) Token: 0x06013E69 RID: 81513 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "hlink")]
		public EnumValue<ColorSchemeIndexValues> Hyperlink
		{
			get
			{
				return (EnumValue<ColorSchemeIndexValues>)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x1700645F RID: 25695
		// (get) Token: 0x06013E6A RID: 81514 RVA: 0x002FA70E File Offset: 0x002F890E
		// (set) Token: 0x06013E6B RID: 81515 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "folHlink")]
		public EnumValue<ColorSchemeIndexValues> FollowedHyperlink
		{
			get
			{
				return (EnumValue<ColorSchemeIndexValues>)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x06013E6C RID: 81516 RVA: 0x002FA71E File Offset: 0x002F891E
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17006460 RID: 25696
		// (get) Token: 0x06013E6D RID: 81517 RVA: 0x0030CED6 File Offset: 0x0030B0D6
		internal override string[] ElementTagNames
		{
			get
			{
				return ColorMappingType.eleTagNames;
			}
		}

		// Token: 0x17006461 RID: 25697
		// (get) Token: 0x06013E6E RID: 81518 RVA: 0x0030CEDD File Offset: 0x0030B0DD
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ColorMappingType.eleNamespaceIds;
			}
		}

		// Token: 0x17006462 RID: 25698
		// (get) Token: 0x06013E6F RID: 81519 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006463 RID: 25699
		// (get) Token: 0x06013E70 RID: 81520 RVA: 0x002FA747 File Offset: 0x002F8947
		// (set) Token: 0x06013E71 RID: 81521 RVA: 0x002FA750 File Offset: 0x002F8950
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(0);
			}
			set
			{
				base.SetElement<ExtensionList>(0, value);
			}
		}

		// Token: 0x06013E72 RID: 81522 RVA: 0x0030CEE4 File Offset: 0x0030B0E4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "bg1" == name)
			{
				return new EnumValue<ColorSchemeIndexValues>();
			}
			if (namespaceId == 0 && "tx1" == name)
			{
				return new EnumValue<ColorSchemeIndexValues>();
			}
			if (namespaceId == 0 && "bg2" == name)
			{
				return new EnumValue<ColorSchemeIndexValues>();
			}
			if (namespaceId == 0 && "tx2" == name)
			{
				return new EnumValue<ColorSchemeIndexValues>();
			}
			if (namespaceId == 0 && "accent1" == name)
			{
				return new EnumValue<ColorSchemeIndexValues>();
			}
			if (namespaceId == 0 && "accent2" == name)
			{
				return new EnumValue<ColorSchemeIndexValues>();
			}
			if (namespaceId == 0 && "accent3" == name)
			{
				return new EnumValue<ColorSchemeIndexValues>();
			}
			if (namespaceId == 0 && "accent4" == name)
			{
				return new EnumValue<ColorSchemeIndexValues>();
			}
			if (namespaceId == 0 && "accent5" == name)
			{
				return new EnumValue<ColorSchemeIndexValues>();
			}
			if (namespaceId == 0 && "accent6" == name)
			{
				return new EnumValue<ColorSchemeIndexValues>();
			}
			if (namespaceId == 0 && "hlink" == name)
			{
				return new EnumValue<ColorSchemeIndexValues>();
			}
			if (namespaceId == 0 && "folHlink" == name)
			{
				return new EnumValue<ColorSchemeIndexValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013E73 RID: 81523 RVA: 0x00293ECF File Offset: 0x002920CF
		protected ColorMappingType()
		{
		}

		// Token: 0x06013E74 RID: 81524 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected ColorMappingType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013E75 RID: 81525 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected ColorMappingType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013E76 RID: 81526 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected ColorMappingType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013E77 RID: 81527 RVA: 0x0030D004 File Offset: 0x0030B204
		// Note: this type is marked as 'beforefieldinit'.
		static ColorMappingType()
		{
			byte[] array = new byte[12];
			ColorMappingType.attributeNamespaceIds = array;
			ColorMappingType.eleTagNames = new string[] { "extLst" };
			ColorMappingType.eleNamespaceIds = new byte[] { 10 };
		}

		// Token: 0x04008835 RID: 34869
		private static string[] attributeTagNames = new string[]
		{
			"bg1", "tx1", "bg2", "tx2", "accent1", "accent2", "accent3", "accent4", "accent5", "accent6",
			"hlink", "folHlink"
		};

		// Token: 0x04008836 RID: 34870
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008837 RID: 34871
		private static readonly string[] eleTagNames;

		// Token: 0x04008838 RID: 34872
		private static readonly byte[] eleNamespaceIds;
	}
}
