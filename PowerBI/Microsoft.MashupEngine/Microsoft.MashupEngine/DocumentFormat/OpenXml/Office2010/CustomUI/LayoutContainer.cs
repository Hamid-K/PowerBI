using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022E0 RID: 8928
	[ChildElementInfo(typeof(GroupBox), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(BackstageEditBox), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(BackstageComboBox), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Hyperlink), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(BackstageLabelControl), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(LayoutContainer), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ImageControl), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(BackstageCheckBox), FileFormatVersions.Office2010)]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(BackstageDropDown), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(RadioGroup), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(BackstageGroupButton), FileFormatVersions.Office2010)]
	internal class LayoutContainer : OpenXmlCompositeElement
	{
		// Token: 0x170045CE RID: 17870
		// (get) Token: 0x0600FA6D RID: 64109 RVA: 0x002D9A63 File Offset: 0x002D7C63
		public override string LocalName
		{
			get
			{
				return "layoutContainer";
			}
		}

		// Token: 0x170045CF RID: 17871
		// (get) Token: 0x0600FA6E RID: 64110 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x170045D0 RID: 17872
		// (get) Token: 0x0600FA6F RID: 64111 RVA: 0x002D9A6A File Offset: 0x002D7C6A
		internal override int ElementTypeId
		{
			get
			{
				return 13073;
			}
		}

		// Token: 0x0600FA70 RID: 64112 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170045D1 RID: 17873
		// (get) Token: 0x0600FA71 RID: 64113 RVA: 0x002D9A71 File Offset: 0x002D7C71
		internal override string[] AttributeTagNames
		{
			get
			{
				return LayoutContainer.attributeTagNames;
			}
		}

		// Token: 0x170045D2 RID: 17874
		// (get) Token: 0x0600FA72 RID: 64114 RVA: 0x002D9A78 File Offset: 0x002D7C78
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return LayoutContainer.attributeNamespaceIds;
			}
		}

		// Token: 0x170045D3 RID: 17875
		// (get) Token: 0x0600FA73 RID: 64115 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600FA74 RID: 64116 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x170045D4 RID: 17876
		// (get) Token: 0x0600FA75 RID: 64117 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600FA76 RID: 64118 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "idQ")]
		public StringValue QualifiedId
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

		// Token: 0x170045D5 RID: 17877
		// (get) Token: 0x0600FA77 RID: 64119 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600FA78 RID: 64120 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "tag")]
		public StringValue Tag
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x170045D6 RID: 17878
		// (get) Token: 0x0600FA79 RID: 64121 RVA: 0x002D8849 File Offset: 0x002D6A49
		// (set) Token: 0x0600FA7A RID: 64122 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "align")]
		public EnumValue<ExpandValues> Align
		{
			get
			{
				return (EnumValue<ExpandValues>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x170045D7 RID: 17879
		// (get) Token: 0x0600FA7B RID: 64123 RVA: 0x002D8858 File Offset: 0x002D6A58
		// (set) Token: 0x0600FA7C RID: 64124 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "expand")]
		public EnumValue<ExpandValues> Expand
		{
			get
			{
				return (EnumValue<ExpandValues>)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x170045D8 RID: 17880
		// (get) Token: 0x0600FA7D RID: 64125 RVA: 0x002D9A7F File Offset: 0x002D7C7F
		// (set) Token: 0x0600FA7E RID: 64126 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "layoutChildren")]
		public EnumValue<LayoutChildrenValues> LayoutChildren
		{
			get
			{
				return (EnumValue<LayoutChildrenValues>)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x0600FA7F RID: 64127 RVA: 0x00293ECF File Offset: 0x002920CF
		public LayoutContainer()
		{
		}

		// Token: 0x0600FA80 RID: 64128 RVA: 0x00293ED7 File Offset: 0x002920D7
		public LayoutContainer(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FA81 RID: 64129 RVA: 0x00293EE0 File Offset: 0x002920E0
		public LayoutContainer(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FA82 RID: 64130 RVA: 0x00293EE9 File Offset: 0x002920E9
		public LayoutContainer(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600FA83 RID: 64131 RVA: 0x002D9A90 File Offset: 0x002D7C90
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (57 == namespaceId && "button" == name)
			{
				return new BackstageGroupButton();
			}
			if (57 == namespaceId && "checkBox" == name)
			{
				return new BackstageCheckBox();
			}
			if (57 == namespaceId && "editBox" == name)
			{
				return new BackstageEditBox();
			}
			if (57 == namespaceId && "dropDown" == name)
			{
				return new BackstageDropDown();
			}
			if (57 == namespaceId && "radioGroup" == name)
			{
				return new RadioGroup();
			}
			if (57 == namespaceId && "comboBox" == name)
			{
				return new BackstageComboBox();
			}
			if (57 == namespaceId && "hyperlink" == name)
			{
				return new Hyperlink();
			}
			if (57 == namespaceId && "labelControl" == name)
			{
				return new BackstageLabelControl();
			}
			if (57 == namespaceId && "groupBox" == name)
			{
				return new GroupBox();
			}
			if (57 == namespaceId && "layoutContainer" == name)
			{
				return new LayoutContainer();
			}
			if (57 == namespaceId && "imageControl" == name)
			{
				return new ImageControl();
			}
			return null;
		}

		// Token: 0x0600FA84 RID: 64132 RVA: 0x002D9BA8 File Offset: 0x002D7DA8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "idQ" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "tag" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "align" == name)
			{
				return new EnumValue<ExpandValues>();
			}
			if (namespaceId == 0 && "expand" == name)
			{
				return new EnumValue<ExpandValues>();
			}
			if (namespaceId == 0 && "layoutChildren" == name)
			{
				return new EnumValue<LayoutChildrenValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600FA85 RID: 64133 RVA: 0x002D9C41 File Offset: 0x002D7E41
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LayoutContainer>(deep);
		}

		// Token: 0x0600FA86 RID: 64134 RVA: 0x002D9C4C File Offset: 0x002D7E4C
		// Note: this type is marked as 'beforefieldinit'.
		static LayoutContainer()
		{
			byte[] array = new byte[6];
			LayoutContainer.attributeNamespaceIds = array;
		}

		// Token: 0x0400718D RID: 29069
		private const string tagName = "layoutContainer";

		// Token: 0x0400718E RID: 29070
		private const byte tagNsId = 57;

		// Token: 0x0400718F RID: 29071
		internal const int ElementTypeIdConst = 13073;

		// Token: 0x04007190 RID: 29072
		private static string[] attributeTagNames = new string[] { "id", "idQ", "tag", "align", "expand", "layoutChildren" };

		// Token: 0x04007191 RID: 29073
		private static byte[] attributeNamespaceIds;
	}
}
