using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x020029F6 RID: 10742
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class ColorMap : OpenXmlCompositeElement
	{
		// Token: 0x17006E88 RID: 28296
		// (get) Token: 0x06015604 RID: 87556 RVA: 0x0030D0F1 File Offset: 0x0030B2F1
		public override string LocalName
		{
			get
			{
				return "clrMap";
			}
		}

		// Token: 0x17006E89 RID: 28297
		// (get) Token: 0x06015605 RID: 87557 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006E8A RID: 28298
		// (get) Token: 0x06015606 RID: 87558 RVA: 0x0031E4CB File Offset: 0x0031C6CB
		internal override int ElementTypeId
		{
			get
			{
				return 12169;
			}
		}

		// Token: 0x06015607 RID: 87559 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006E8B RID: 28299
		// (get) Token: 0x06015608 RID: 87560 RVA: 0x0031E4D2 File Offset: 0x0031C6D2
		internal override string[] AttributeTagNames
		{
			get
			{
				return ColorMap.attributeTagNames;
			}
		}

		// Token: 0x17006E8C RID: 28300
		// (get) Token: 0x06015609 RID: 87561 RVA: 0x0031E4D9 File Offset: 0x0031C6D9
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ColorMap.attributeNamespaceIds;
			}
		}

		// Token: 0x17006E8D RID: 28301
		// (get) Token: 0x0601560A RID: 87562 RVA: 0x002FA667 File Offset: 0x002F8867
		// (set) Token: 0x0601560B RID: 87563 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17006E8E RID: 28302
		// (get) Token: 0x0601560C RID: 87564 RVA: 0x002FA676 File Offset: 0x002F8876
		// (set) Token: 0x0601560D RID: 87565 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17006E8F RID: 28303
		// (get) Token: 0x0601560E RID: 87566 RVA: 0x002FA685 File Offset: 0x002F8885
		// (set) Token: 0x0601560F RID: 87567 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17006E90 RID: 28304
		// (get) Token: 0x06015610 RID: 87568 RVA: 0x002FA694 File Offset: 0x002F8894
		// (set) Token: 0x06015611 RID: 87569 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x17006E91 RID: 28305
		// (get) Token: 0x06015612 RID: 87570 RVA: 0x002FA6A3 File Offset: 0x002F88A3
		// (set) Token: 0x06015613 RID: 87571 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x17006E92 RID: 28306
		// (get) Token: 0x06015614 RID: 87572 RVA: 0x002FA6B2 File Offset: 0x002F88B2
		// (set) Token: 0x06015615 RID: 87573 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
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

		// Token: 0x17006E93 RID: 28307
		// (get) Token: 0x06015616 RID: 87574 RVA: 0x002FA6C1 File Offset: 0x002F88C1
		// (set) Token: 0x06015617 RID: 87575 RVA: 0x002BD4FC File Offset: 0x002BB6FC
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

		// Token: 0x17006E94 RID: 28308
		// (get) Token: 0x06015618 RID: 87576 RVA: 0x002FA6D0 File Offset: 0x002F88D0
		// (set) Token: 0x06015619 RID: 87577 RVA: 0x002BD516 File Offset: 0x002BB716
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

		// Token: 0x17006E95 RID: 28309
		// (get) Token: 0x0601561A RID: 87578 RVA: 0x002FA6DF File Offset: 0x002F88DF
		// (set) Token: 0x0601561B RID: 87579 RVA: 0x002BD530 File Offset: 0x002BB730
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

		// Token: 0x17006E96 RID: 28310
		// (get) Token: 0x0601561C RID: 87580 RVA: 0x002FA6EE File Offset: 0x002F88EE
		// (set) Token: 0x0601561D RID: 87581 RVA: 0x002BD54B File Offset: 0x002BB74B
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

		// Token: 0x17006E97 RID: 28311
		// (get) Token: 0x0601561E RID: 87582 RVA: 0x002FA6FE File Offset: 0x002F88FE
		// (set) Token: 0x0601561F RID: 87583 RVA: 0x002BDB45 File Offset: 0x002BBD45
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

		// Token: 0x17006E98 RID: 28312
		// (get) Token: 0x06015620 RID: 87584 RVA: 0x002FA70E File Offset: 0x002F890E
		// (set) Token: 0x06015621 RID: 87585 RVA: 0x002BDB61 File Offset: 0x002BBD61
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

		// Token: 0x06015622 RID: 87586 RVA: 0x00293ECF File Offset: 0x002920CF
		public ColorMap()
		{
		}

		// Token: 0x06015623 RID: 87587 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ColorMap(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015624 RID: 87588 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ColorMap(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015625 RID: 87589 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ColorMap(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015626 RID: 87590 RVA: 0x002FA71E File Offset: 0x002F891E
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17006E99 RID: 28313
		// (get) Token: 0x06015627 RID: 87591 RVA: 0x0031E4E0 File Offset: 0x0031C6E0
		internal override string[] ElementTagNames
		{
			get
			{
				return ColorMap.eleTagNames;
			}
		}

		// Token: 0x17006E9A RID: 28314
		// (get) Token: 0x06015628 RID: 87592 RVA: 0x0031E4E7 File Offset: 0x0031C6E7
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ColorMap.eleNamespaceIds;
			}
		}

		// Token: 0x17006E9B RID: 28315
		// (get) Token: 0x06015629 RID: 87593 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006E9C RID: 28316
		// (get) Token: 0x0601562A RID: 87594 RVA: 0x002FA747 File Offset: 0x002F8947
		// (set) Token: 0x0601562B RID: 87595 RVA: 0x002FA750 File Offset: 0x002F8950
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

		// Token: 0x0601562C RID: 87596 RVA: 0x0031E4F0 File Offset: 0x0031C6F0
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

		// Token: 0x0601562D RID: 87597 RVA: 0x0031E60D File Offset: 0x0031C80D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ColorMap>(deep);
		}

		// Token: 0x0601562E RID: 87598 RVA: 0x0031E618 File Offset: 0x0031C818
		// Note: this type is marked as 'beforefieldinit'.
		static ColorMap()
		{
			byte[] array = new byte[12];
			ColorMap.attributeNamespaceIds = array;
			ColorMap.eleTagNames = new string[] { "extLst" };
			ColorMap.eleNamespaceIds = new byte[] { 10 };
		}

		// Token: 0x04009343 RID: 37699
		private const string tagName = "clrMap";

		// Token: 0x04009344 RID: 37700
		private const byte tagNsId = 24;

		// Token: 0x04009345 RID: 37701
		internal const int ElementTypeIdConst = 12169;

		// Token: 0x04009346 RID: 37702
		private static string[] attributeTagNames = new string[]
		{
			"bg1", "tx1", "bg2", "tx2", "accent1", "accent2", "accent3", "accent4", "accent5", "accent6",
			"hlink", "folHlink"
		};

		// Token: 0x04009347 RID: 37703
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009348 RID: 37704
		private static readonly string[] eleTagNames;

		// Token: 0x04009349 RID: 37705
		private static readonly byte[] eleNamespaceIds;
	}
}
