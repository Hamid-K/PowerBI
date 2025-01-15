using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.CustomUI
{
	// Token: 0x0200228D RID: 8845
	[ChildElementInfo(typeof(Gallery))]
	[ChildElementInfo(typeof(DynamicMenu))]
	[ChildElementInfo(typeof(SplitButton))]
	[ChildElementInfo(typeof(Box))]
	[ChildElementInfo(typeof(ButtonGroup))]
	[ChildElementInfo(typeof(VerticalSeparator))]
	[ChildElementInfo(typeof(DialogBoxLauncher))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(CheckBox))]
	[ChildElementInfo(typeof(Menu))]
	[ChildElementInfo(typeof(TextLabel))]
	[ChildElementInfo(typeof(Button))]
	[ChildElementInfo(typeof(ToggleButton))]
	[ChildElementInfo(typeof(ControlClone))]
	[ChildElementInfo(typeof(EditBox))]
	[ChildElementInfo(typeof(ComboBox))]
	[ChildElementInfo(typeof(DropDown))]
	internal class Group : OpenXmlCompositeElement
	{
		// Token: 0x17004063 RID: 16483
		// (get) Token: 0x0600EEF2 RID: 61170 RVA: 0x002C29FF File Offset: 0x002C0BFF
		public override string LocalName
		{
			get
			{
				return "group";
			}
		}

		// Token: 0x17004064 RID: 16484
		// (get) Token: 0x0600EEF3 RID: 61171 RVA: 0x002C8749 File Offset: 0x002C6949
		internal override byte NamespaceId
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x17004065 RID: 16485
		// (get) Token: 0x0600EEF4 RID: 61172 RVA: 0x002CF6F8 File Offset: 0x002CD8F8
		internal override int ElementTypeId
		{
			get
			{
				return 12604;
			}
		}

		// Token: 0x0600EEF5 RID: 61173 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17004066 RID: 16486
		// (get) Token: 0x0600EEF6 RID: 61174 RVA: 0x002CF6FF File Offset: 0x002CD8FF
		internal override string[] AttributeTagNames
		{
			get
			{
				return Group.attributeTagNames;
			}
		}

		// Token: 0x17004067 RID: 16487
		// (get) Token: 0x0600EEF7 RID: 61175 RVA: 0x002CF706 File Offset: 0x002CD906
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Group.attributeNamespaceIds;
			}
		}

		// Token: 0x17004068 RID: 16488
		// (get) Token: 0x0600EEF8 RID: 61176 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600EEF9 RID: 61177 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004069 RID: 16489
		// (get) Token: 0x0600EEFA RID: 61178 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600EEFB RID: 61179 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "idQ")]
		public StringValue IdQ
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

		// Token: 0x1700406A RID: 16490
		// (get) Token: 0x0600EEFC RID: 61180 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600EEFD RID: 61181 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "idMso")]
		public StringValue IdMso
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

		// Token: 0x1700406B RID: 16491
		// (get) Token: 0x0600EEFE RID: 61182 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600EEFF RID: 61183 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "tag")]
		public StringValue Tag
		{
			get
			{
				return (StringValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x1700406C RID: 16492
		// (get) Token: 0x0600EF00 RID: 61184 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600EF01 RID: 61185 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x1700406D RID: 16493
		// (get) Token: 0x0600EF02 RID: 61186 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600EF03 RID: 61187 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
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

		// Token: 0x1700406E RID: 16494
		// (get) Token: 0x0600EF04 RID: 61188 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600EF05 RID: 61189 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "image")]
		public StringValue Image
		{
			get
			{
				return (StringValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x1700406F RID: 16495
		// (get) Token: 0x0600EF06 RID: 61190 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600EF07 RID: 61191 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "imageMso")]
		public StringValue ImageMso
		{
			get
			{
				return (StringValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17004070 RID: 16496
		// (get) Token: 0x0600EF08 RID: 61192 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600EF09 RID: 61193 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "getImage")]
		public StringValue GetImage
		{
			get
			{
				return (StringValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17004071 RID: 16497
		// (get) Token: 0x0600EF0A RID: 61194 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600EF0B RID: 61195 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "insertAfterMso")]
		public StringValue InsertAfterMso
		{
			get
			{
				return (StringValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x17004072 RID: 16498
		// (get) Token: 0x0600EF0C RID: 61196 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600EF0D RID: 61197 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "insertBeforeMso")]
		public StringValue InsertBeforeMso
		{
			get
			{
				return (StringValue)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x17004073 RID: 16499
		// (get) Token: 0x0600EF0E RID: 61198 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600EF0F RID: 61199 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQ
		{
			get
			{
				return (StringValue)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x17004074 RID: 16500
		// (get) Token: 0x0600EF10 RID: 61200 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600EF11 RID: 61201 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQ
		{
			get
			{
				return (StringValue)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x17004075 RID: 16501
		// (get) Token: 0x0600EF12 RID: 61202 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600EF13 RID: 61203 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "screentip")]
		public StringValue Screentip
		{
			get
			{
				return (StringValue)base.Attributes[13];
			}
			set
			{
				base.Attributes[13] = value;
			}
		}

		// Token: 0x17004076 RID: 16502
		// (get) Token: 0x0600EF14 RID: 61204 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600EF15 RID: 61205 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "getScreentip")]
		public StringValue GetScreentip
		{
			get
			{
				return (StringValue)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x17004077 RID: 16503
		// (get) Token: 0x0600EF16 RID: 61206 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600EF17 RID: 61207 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "supertip")]
		public StringValue Supertip
		{
			get
			{
				return (StringValue)base.Attributes[15];
			}
			set
			{
				base.Attributes[15] = value;
			}
		}

		// Token: 0x17004078 RID: 16504
		// (get) Token: 0x0600EF18 RID: 61208 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600EF19 RID: 61209 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "getSupertip")]
		public StringValue GetSupertip
		{
			get
			{
				return (StringValue)base.Attributes[16];
			}
			set
			{
				base.Attributes[16] = value;
			}
		}

		// Token: 0x17004079 RID: 16505
		// (get) Token: 0x0600EF1A RID: 61210 RVA: 0x002CDD31 File Offset: 0x002CBF31
		// (set) Token: 0x0600EF1B RID: 61211 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
		{
			get
			{
				return (BooleanValue)base.Attributes[17];
			}
			set
			{
				base.Attributes[17] = value;
			}
		}

		// Token: 0x1700407A RID: 16506
		// (get) Token: 0x0600EF1C RID: 61212 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600EF1D RID: 61213 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
		{
			get
			{
				return (StringValue)base.Attributes[18];
			}
			set
			{
				base.Attributes[18] = value;
			}
		}

		// Token: 0x1700407B RID: 16507
		// (get) Token: 0x0600EF1E RID: 61214 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600EF1F RID: 61215 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
		{
			get
			{
				return (StringValue)base.Attributes[19];
			}
			set
			{
				base.Attributes[19] = value;
			}
		}

		// Token: 0x1700407C RID: 16508
		// (get) Token: 0x0600EF20 RID: 61216 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0600EF21 RID: 61217 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
		{
			get
			{
				return (StringValue)base.Attributes[20];
			}
			set
			{
				base.Attributes[20] = value;
			}
		}

		// Token: 0x0600EF22 RID: 61218 RVA: 0x00293ECF File Offset: 0x002920CF
		public Group()
		{
		}

		// Token: 0x0600EF23 RID: 61219 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Group(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600EF24 RID: 61220 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Group(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600EF25 RID: 61221 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Group(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600EF26 RID: 61222 RVA: 0x002CF710 File Offset: 0x002CD910
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (34 == namespaceId && "control" == name)
			{
				return new ControlClone();
			}
			if (34 == namespaceId && "labelControl" == name)
			{
				return new TextLabel();
			}
			if (34 == namespaceId && "button" == name)
			{
				return new Button();
			}
			if (34 == namespaceId && "toggleButton" == name)
			{
				return new ToggleButton();
			}
			if (34 == namespaceId && "checkBox" == name)
			{
				return new CheckBox();
			}
			if (34 == namespaceId && "editBox" == name)
			{
				return new EditBox();
			}
			if (34 == namespaceId && "comboBox" == name)
			{
				return new ComboBox();
			}
			if (34 == namespaceId && "dropDown" == name)
			{
				return new DropDown();
			}
			if (34 == namespaceId && "gallery" == name)
			{
				return new Gallery();
			}
			if (34 == namespaceId && "menu" == name)
			{
				return new Menu();
			}
			if (34 == namespaceId && "dynamicMenu" == name)
			{
				return new DynamicMenu();
			}
			if (34 == namespaceId && "splitButton" == name)
			{
				return new SplitButton();
			}
			if (34 == namespaceId && "box" == name)
			{
				return new Box();
			}
			if (34 == namespaceId && "buttonGroup" == name)
			{
				return new ButtonGroup();
			}
			if (34 == namespaceId && "separator" == name)
			{
				return new VerticalSeparator();
			}
			if (34 == namespaceId && "dialogBoxLauncher" == name)
			{
				return new DialogBoxLauncher();
			}
			return null;
		}

		// Token: 0x0600EF27 RID: 61223 RVA: 0x002CF8A0 File Offset: 0x002CDAA0
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
			if (namespaceId == 0 && "idMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "tag" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "label" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getLabel" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "image" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "imageMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getImage" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insertAfterMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insertBeforeMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insertAfterQ" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insertBeforeQ" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "screentip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getScreentip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "supertip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getSupertip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "visible" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "getVisible" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "keytip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getKeytip" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600EF28 RID: 61224 RVA: 0x002CFA83 File Offset: 0x002CDC83
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Group>(deep);
		}

		// Token: 0x0600EF29 RID: 61225 RVA: 0x002CFA8C File Offset: 0x002CDC8C
		// Note: this type is marked as 'beforefieldinit'.
		static Group()
		{
			byte[] array = new byte[21];
			Group.attributeNamespaceIds = array;
		}

		// Token: 0x04007022 RID: 28706
		private const string tagName = "group";

		// Token: 0x04007023 RID: 28707
		private const byte tagNsId = 34;

		// Token: 0x04007024 RID: 28708
		internal const int ElementTypeIdConst = 12604;

		// Token: 0x04007025 RID: 28709
		private static string[] attributeTagNames = new string[]
		{
			"id", "idQ", "idMso", "tag", "label", "getLabel", "image", "imageMso", "getImage", "insertAfterMso",
			"insertBeforeMso", "insertAfterQ", "insertBeforeQ", "screentip", "getScreentip", "supertip", "getSupertip", "visible", "getVisible", "keytip",
			"getKeytip"
		};

		// Token: 0x04007026 RID: 28710
		private static byte[] attributeNamespaceIds;
	}
}
