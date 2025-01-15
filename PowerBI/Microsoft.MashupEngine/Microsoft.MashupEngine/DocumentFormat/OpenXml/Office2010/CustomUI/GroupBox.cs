using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022DF RID: 8927
	[ChildElementInfo(typeof(BackstageEditBox), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(RadioGroup), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(BackstageComboBox), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Hyperlink), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(BackstageLabelControl), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(GroupBox), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(LayoutContainer), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ImageControl), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(BackstageDropDown), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(BackstageGroupButton), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(BackstageCheckBox), FileFormatVersions.Office2010)]
	internal class GroupBox : OpenXmlCompositeElement
	{
		// Token: 0x170045C3 RID: 17859
		// (get) Token: 0x0600FA53 RID: 64083 RVA: 0x002D9834 File Offset: 0x002D7A34
		public override string LocalName
		{
			get
			{
				return "groupBox";
			}
		}

		// Token: 0x170045C4 RID: 17860
		// (get) Token: 0x0600FA54 RID: 64084 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x170045C5 RID: 17861
		// (get) Token: 0x0600FA55 RID: 64085 RVA: 0x002D983B File Offset: 0x002D7A3B
		internal override int ElementTypeId
		{
			get
			{
				return 13072;
			}
		}

		// Token: 0x0600FA56 RID: 64086 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170045C6 RID: 17862
		// (get) Token: 0x0600FA57 RID: 64087 RVA: 0x002D9842 File Offset: 0x002D7A42
		internal override string[] AttributeTagNames
		{
			get
			{
				return GroupBox.attributeTagNames;
			}
		}

		// Token: 0x170045C7 RID: 17863
		// (get) Token: 0x0600FA58 RID: 64088 RVA: 0x002D9849 File Offset: 0x002D7A49
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return GroupBox.attributeNamespaceIds;
			}
		}

		// Token: 0x170045C8 RID: 17864
		// (get) Token: 0x0600FA59 RID: 64089 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600FA5A RID: 64090 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x170045C9 RID: 17865
		// (get) Token: 0x0600FA5B RID: 64091 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600FA5C RID: 64092 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x170045CA RID: 17866
		// (get) Token: 0x0600FA5D RID: 64093 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600FA5E RID: 64094 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x170045CB RID: 17867
		// (get) Token: 0x0600FA5F RID: 64095 RVA: 0x002D8849 File Offset: 0x002D6A49
		// (set) Token: 0x0600FA60 RID: 64096 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "expand")]
		public EnumValue<ExpandValues> Expand
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

		// Token: 0x170045CC RID: 17868
		// (get) Token: 0x0600FA61 RID: 64097 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600FA62 RID: 64098 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "label")]
		public StringValue Label
		{
			get
			{
				return (StringValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x170045CD RID: 17869
		// (get) Token: 0x0600FA63 RID: 64099 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600FA64 RID: 64100 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "getLabel")]
		public StringValue GetLabel
		{
			get
			{
				return (StringValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x0600FA65 RID: 64101 RVA: 0x00293ECF File Offset: 0x002920CF
		public GroupBox()
		{
		}

		// Token: 0x0600FA66 RID: 64102 RVA: 0x00293ED7 File Offset: 0x002920D7
		public GroupBox(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FA67 RID: 64103 RVA: 0x00293EE0 File Offset: 0x002920E0
		public GroupBox(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FA68 RID: 64104 RVA: 0x00293EE9 File Offset: 0x002920E9
		public GroupBox(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600FA69 RID: 64105 RVA: 0x002D9850 File Offset: 0x002D7A50
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

		// Token: 0x0600FA6A RID: 64106 RVA: 0x002D9968 File Offset: 0x002D7B68
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
			if (namespaceId == 0 && "expand" == name)
			{
				return new EnumValue<ExpandValues>();
			}
			if (namespaceId == 0 && "label" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getLabel" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600FA6B RID: 64107 RVA: 0x002D9A01 File Offset: 0x002D7C01
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GroupBox>(deep);
		}

		// Token: 0x0600FA6C RID: 64108 RVA: 0x002D9A0C File Offset: 0x002D7C0C
		// Note: this type is marked as 'beforefieldinit'.
		static GroupBox()
		{
			byte[] array = new byte[6];
			GroupBox.attributeNamespaceIds = array;
		}

		// Token: 0x04007188 RID: 29064
		private const string tagName = "groupBox";

		// Token: 0x04007189 RID: 29065
		private const byte tagNsId = 57;

		// Token: 0x0400718A RID: 29066
		internal const int ElementTypeIdConst = 13072;

		// Token: 0x0400718B RID: 29067
		private static string[] attributeTagNames = new string[] { "id", "idQ", "tag", "expand", "label", "getLabel" };

		// Token: 0x0400718C RID: 29068
		private static byte[] attributeNamespaceIds;
	}
}
