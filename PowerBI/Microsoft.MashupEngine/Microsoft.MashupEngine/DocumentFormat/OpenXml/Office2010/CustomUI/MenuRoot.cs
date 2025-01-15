using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022E4 RID: 8932
	[ChildElementInfo(typeof(GalleryRegular), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(SplitButtonRegular), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(MenuRegular), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(DynamicMenuRegular), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ToggleButtonRegular), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(MenuSeparator), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ControlCloneRegular), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ButtonRegular), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(CheckBox), FileFormatVersions.Office2010)]
	internal class MenuRoot : OpenXmlCompositeElement
	{
		// Token: 0x17004617 RID: 17943
		// (get) Token: 0x0600FB0B RID: 64267 RVA: 0x002CA224 File Offset: 0x002C8424
		public override string LocalName
		{
			get
			{
				return "menu";
			}
		}

		// Token: 0x17004618 RID: 17944
		// (get) Token: 0x0600FB0C RID: 64268 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x17004619 RID: 17945
		// (get) Token: 0x0600FB0D RID: 64269 RVA: 0x002DA3F9 File Offset: 0x002D85F9
		internal override int ElementTypeId
		{
			get
			{
				return 13077;
			}
		}

		// Token: 0x0600FB0E RID: 64270 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x1700461A RID: 17946
		// (get) Token: 0x0600FB0F RID: 64271 RVA: 0x002DA400 File Offset: 0x002D8600
		internal override string[] AttributeTagNames
		{
			get
			{
				return MenuRoot.attributeTagNames;
			}
		}

		// Token: 0x1700461B RID: 17947
		// (get) Token: 0x0600FB10 RID: 64272 RVA: 0x002DA407 File Offset: 0x002D8607
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MenuRoot.attributeNamespaceIds;
			}
		}

		// Token: 0x1700461C RID: 17948
		// (get) Token: 0x0600FB11 RID: 64273 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600FB12 RID: 64274 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "title")]
		public StringValue Title
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

		// Token: 0x1700461D RID: 17949
		// (get) Token: 0x0600FB13 RID: 64275 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600FB14 RID: 64276 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "getTitle")]
		public StringValue GetTitle
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

		// Token: 0x1700461E RID: 17950
		// (get) Token: 0x0600FB15 RID: 64277 RVA: 0x002D6730 File Offset: 0x002D4930
		// (set) Token: 0x0600FB16 RID: 64278 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "itemSize")]
		public EnumValue<ItemSizeValues> ItemSize
		{
			get
			{
				return (EnumValue<ItemSizeValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x0600FB17 RID: 64279 RVA: 0x00293ECF File Offset: 0x002920CF
		public MenuRoot()
		{
		}

		// Token: 0x0600FB18 RID: 64280 RVA: 0x00293ED7 File Offset: 0x002920D7
		public MenuRoot(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FB19 RID: 64281 RVA: 0x00293EE0 File Offset: 0x002920E0
		public MenuRoot(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FB1A RID: 64282 RVA: 0x00293EE9 File Offset: 0x002920E9
		public MenuRoot(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600FB1B RID: 64283 RVA: 0x002DA410 File Offset: 0x002D8610
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (57 == namespaceId && "control" == name)
			{
				return new ControlCloneRegular();
			}
			if (57 == namespaceId && "button" == name)
			{
				return new ButtonRegular();
			}
			if (57 == namespaceId && "checkBox" == name)
			{
				return new CheckBox();
			}
			if (57 == namespaceId && "gallery" == name)
			{
				return new GalleryRegular();
			}
			if (57 == namespaceId && "toggleButton" == name)
			{
				return new ToggleButtonRegular();
			}
			if (57 == namespaceId && "menuSeparator" == name)
			{
				return new MenuSeparator();
			}
			if (57 == namespaceId && "splitButton" == name)
			{
				return new SplitButtonRegular();
			}
			if (57 == namespaceId && "menu" == name)
			{
				return new MenuRegular();
			}
			if (57 == namespaceId && "dynamicMenu" == name)
			{
				return new DynamicMenuRegular();
			}
			return null;
		}

		// Token: 0x0600FB1C RID: 64284 RVA: 0x002DA4F8 File Offset: 0x002D86F8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "title" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getTitle" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "itemSize" == name)
			{
				return new EnumValue<ItemSizeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600FB1D RID: 64285 RVA: 0x002DA54F File Offset: 0x002D874F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MenuRoot>(deep);
		}

		// Token: 0x0600FB1E RID: 64286 RVA: 0x002DA558 File Offset: 0x002D8758
		// Note: this type is marked as 'beforefieldinit'.
		static MenuRoot()
		{
			byte[] array = new byte[3];
			MenuRoot.attributeNamespaceIds = array;
		}

		// Token: 0x040071A1 RID: 29089
		private const string tagName = "menu";

		// Token: 0x040071A2 RID: 29090
		private const byte tagNsId = 57;

		// Token: 0x040071A3 RID: 29091
		internal const int ElementTypeIdConst = 13077;

		// Token: 0x040071A4 RID: 29092
		private static string[] attributeTagNames = new string[] { "title", "getTitle", "itemSize" };

		// Token: 0x040071A5 RID: 29093
		private static byte[] attributeNamespaceIds;
	}
}
