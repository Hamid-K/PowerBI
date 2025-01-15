using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.CustomUI
{
	// Token: 0x02002286 RID: 8838
	[ChildElementInfo(typeof(MenuSeparator))]
	[ChildElementInfo(typeof(UnsizedMenu))]
	[ChildElementInfo(typeof(UnsizedDynamicMenu))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(UnsizedButton))]
	[ChildElementInfo(typeof(CheckBox))]
	[ChildElementInfo(typeof(UnsizedGallery))]
	[ChildElementInfo(typeof(UnsizedToggleButton))]
	[ChildElementInfo(typeof(UnsizedControlClone))]
	[ChildElementInfo(typeof(UnsizedSplitButton))]
	internal class MenuRoot : OpenXmlCompositeElement
	{
		// Token: 0x17003FEC RID: 16364
		// (get) Token: 0x0600EDFA RID: 60922 RVA: 0x002CA224 File Offset: 0x002C8424
		public override string LocalName
		{
			get
			{
				return "menu";
			}
		}

		// Token: 0x17003FED RID: 16365
		// (get) Token: 0x0600EDFB RID: 60923 RVA: 0x002C8749 File Offset: 0x002C6949
		internal override byte NamespaceId
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x17003FEE RID: 16366
		// (get) Token: 0x0600EDFC RID: 60924 RVA: 0x002CE95F File Offset: 0x002CCB5F
		internal override int ElementTypeId
		{
			get
			{
				return 12597;
			}
		}

		// Token: 0x0600EDFD RID: 60925 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003FEF RID: 16367
		// (get) Token: 0x0600EDFE RID: 60926 RVA: 0x002CE966 File Offset: 0x002CCB66
		internal override string[] AttributeTagNames
		{
			get
			{
				return MenuRoot.attributeTagNames;
			}
		}

		// Token: 0x17003FF0 RID: 16368
		// (get) Token: 0x0600EDFF RID: 60927 RVA: 0x002CE96D File Offset: 0x002CCB6D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MenuRoot.attributeNamespaceIds;
			}
		}

		// Token: 0x17003FF1 RID: 16369
		// (get) Token: 0x0600EE00 RID: 60928 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600EE01 RID: 60929 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17003FF2 RID: 16370
		// (get) Token: 0x0600EE02 RID: 60930 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600EE03 RID: 60931 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17003FF3 RID: 16371
		// (get) Token: 0x0600EE04 RID: 60932 RVA: 0x002CD803 File Offset: 0x002CBA03
		// (set) Token: 0x0600EE05 RID: 60933 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x0600EE06 RID: 60934 RVA: 0x00293ECF File Offset: 0x002920CF
		public MenuRoot()
		{
		}

		// Token: 0x0600EE07 RID: 60935 RVA: 0x00293ED7 File Offset: 0x002920D7
		public MenuRoot(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600EE08 RID: 60936 RVA: 0x00293EE0 File Offset: 0x002920E0
		public MenuRoot(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600EE09 RID: 60937 RVA: 0x00293EE9 File Offset: 0x002920E9
		public MenuRoot(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600EE0A RID: 60938 RVA: 0x002CE974 File Offset: 0x002CCB74
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (34 == namespaceId && "control" == name)
			{
				return new UnsizedControlClone();
			}
			if (34 == namespaceId && "button" == name)
			{
				return new UnsizedButton();
			}
			if (34 == namespaceId && "checkBox" == name)
			{
				return new CheckBox();
			}
			if (34 == namespaceId && "gallery" == name)
			{
				return new UnsizedGallery();
			}
			if (34 == namespaceId && "toggleButton" == name)
			{
				return new UnsizedToggleButton();
			}
			if (34 == namespaceId && "menuSeparator" == name)
			{
				return new MenuSeparator();
			}
			if (34 == namespaceId && "splitButton" == name)
			{
				return new UnsizedSplitButton();
			}
			if (34 == namespaceId && "menu" == name)
			{
				return new UnsizedMenu();
			}
			if (34 == namespaceId && "dynamicMenu" == name)
			{
				return new UnsizedDynamicMenu();
			}
			return null;
		}

		// Token: 0x0600EE0B RID: 60939 RVA: 0x002CEA5C File Offset: 0x002CCC5C
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

		// Token: 0x0600EE0C RID: 60940 RVA: 0x002CEAB3 File Offset: 0x002CCCB3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MenuRoot>(deep);
		}

		// Token: 0x0600EE0D RID: 60941 RVA: 0x002CEABC File Offset: 0x002CCCBC
		// Note: this type is marked as 'beforefieldinit'.
		static MenuRoot()
		{
			byte[] array = new byte[3];
			MenuRoot.attributeNamespaceIds = array;
		}

		// Token: 0x04006FFD RID: 28669
		private const string tagName = "menu";

		// Token: 0x04006FFE RID: 28670
		private const byte tagNsId = 34;

		// Token: 0x04006FFF RID: 28671
		internal const int ElementTypeIdConst = 12597;

		// Token: 0x04007000 RID: 28672
		private static string[] attributeTagNames = new string[] { "title", "getTitle", "itemSize" };

		// Token: 0x04007001 RID: 28673
		private static byte[] attributeNamespaceIds;
	}
}
