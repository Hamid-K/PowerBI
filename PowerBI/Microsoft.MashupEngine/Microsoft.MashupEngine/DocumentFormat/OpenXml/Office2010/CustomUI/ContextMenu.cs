using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022F6 RID: 8950
	[ChildElementInfo(typeof(MenuSeparatorNoTitle), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(SplitButtonRegular), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(DynamicMenuRegular), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ControlCloneRegular), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(GalleryRegular), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(MenuRegular), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ToggleButtonRegular), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ButtonRegular), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(CheckBox), FileFormatVersions.Office2010)]
	internal class ContextMenu : OpenXmlCompositeElement
	{
		// Token: 0x17004708 RID: 18184
		// (get) Token: 0x0600FD18 RID: 64792 RVA: 0x002DBFBC File Offset: 0x002DA1BC
		public override string LocalName
		{
			get
			{
				return "contextMenu";
			}
		}

		// Token: 0x17004709 RID: 18185
		// (get) Token: 0x0600FD19 RID: 64793 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x1700470A RID: 18186
		// (get) Token: 0x0600FD1A RID: 64794 RVA: 0x002DBFC3 File Offset: 0x002DA1C3
		internal override int ElementTypeId
		{
			get
			{
				return 13094;
			}
		}

		// Token: 0x0600FD1B RID: 64795 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x1700470B RID: 18187
		// (get) Token: 0x0600FD1C RID: 64796 RVA: 0x002DBFCA File Offset: 0x002DA1CA
		internal override string[] AttributeTagNames
		{
			get
			{
				return ContextMenu.attributeTagNames;
			}
		}

		// Token: 0x1700470C RID: 18188
		// (get) Token: 0x0600FD1D RID: 64797 RVA: 0x002DBFD1 File Offset: 0x002DA1D1
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ContextMenu.attributeNamespaceIds;
			}
		}

		// Token: 0x1700470D RID: 18189
		// (get) Token: 0x0600FD1E RID: 64798 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600FD1F RID: 64799 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "idMso")]
		public StringValue IdMso
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

		// Token: 0x0600FD20 RID: 64800 RVA: 0x00293ECF File Offset: 0x002920CF
		public ContextMenu()
		{
		}

		// Token: 0x0600FD21 RID: 64801 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ContextMenu(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FD22 RID: 64802 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ContextMenu(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FD23 RID: 64803 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ContextMenu(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600FD24 RID: 64804 RVA: 0x002DBFD8 File Offset: 0x002DA1D8
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
			if (57 == namespaceId && "menuSeparator" == name)
			{
				return new MenuSeparatorNoTitle();
			}
			return null;
		}

		// Token: 0x0600FD25 RID: 64805 RVA: 0x002DC0BE File Offset: 0x002DA2BE
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "idMso" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600FD26 RID: 64806 RVA: 0x002DC0DE File Offset: 0x002DA2DE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ContextMenu>(deep);
		}

		// Token: 0x0600FD27 RID: 64807 RVA: 0x002DC0E8 File Offset: 0x002DA2E8
		// Note: this type is marked as 'beforefieldinit'.
		static ContextMenu()
		{
			byte[] array = new byte[1];
			ContextMenu.attributeNamespaceIds = array;
		}

		// Token: 0x040071F0 RID: 29168
		private const string tagName = "contextMenu";

		// Token: 0x040071F1 RID: 29169
		private const byte tagNsId = 57;

		// Token: 0x040071F2 RID: 29170
		internal const int ElementTypeIdConst = 13094;

		// Token: 0x040071F3 RID: 29171
		private static string[] attributeTagNames = new string[] { "idMso" };

		// Token: 0x040071F4 RID: 29172
		private static byte[] attributeNamespaceIds;
	}
}
