using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022EB RID: 8939
	[ChildElementInfo(typeof(ComboBox), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(DropDownRegular), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Gallery), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Menu), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(DynamicMenu), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(SplitButton), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Box), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ButtonGroup), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Separator), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(DialogBoxLauncher), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(LabelControl), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Button), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ToggleButton), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(CheckBox), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(EditBox), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ControlClone), FileFormatVersions.Office2010)]
	internal class Group : OpenXmlCompositeElement
	{
		// Token: 0x17004691 RID: 18065
		// (get) Token: 0x0600FC09 RID: 64521 RVA: 0x002C29FF File Offset: 0x002C0BFF
		public override string LocalName
		{
			get
			{
				return "group";
			}
		}

		// Token: 0x17004692 RID: 18066
		// (get) Token: 0x0600FC0A RID: 64522 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x17004693 RID: 18067
		// (get) Token: 0x0600FC0B RID: 64523 RVA: 0x002DB19C File Offset: 0x002D939C
		internal override int ElementTypeId
		{
			get
			{
				return 13084;
			}
		}

		// Token: 0x0600FC0C RID: 64524 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004694 RID: 18068
		// (get) Token: 0x0600FC0D RID: 64525 RVA: 0x002DB1A3 File Offset: 0x002D93A3
		internal override string[] AttributeTagNames
		{
			get
			{
				return Group.attributeTagNames;
			}
		}

		// Token: 0x17004695 RID: 18069
		// (get) Token: 0x0600FC0E RID: 64526 RVA: 0x002DB1AA File Offset: 0x002D93AA
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Group.attributeNamespaceIds;
			}
		}

		// Token: 0x17004696 RID: 18070
		// (get) Token: 0x0600FC0F RID: 64527 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600FC10 RID: 64528 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004697 RID: 18071
		// (get) Token: 0x0600FC11 RID: 64529 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600FC12 RID: 64530 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17004698 RID: 18072
		// (get) Token: 0x0600FC13 RID: 64531 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600FC14 RID: 64532 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17004699 RID: 18073
		// (get) Token: 0x0600FC15 RID: 64533 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600FC16 RID: 64534 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "idMso")]
		public StringValue IdMso
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

		// Token: 0x1700469A RID: 18074
		// (get) Token: 0x0600FC17 RID: 64535 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600FC18 RID: 64536 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x1700469B RID: 18075
		// (get) Token: 0x0600FC19 RID: 64537 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600FC1A RID: 64538 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
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

		// Token: 0x1700469C RID: 18076
		// (get) Token: 0x0600FC1B RID: 64539 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600FC1C RID: 64540 RVA: 0x002BD4FC File Offset: 0x002BB6FC
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

		// Token: 0x1700469D RID: 18077
		// (get) Token: 0x0600FC1D RID: 64541 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600FC1E RID: 64542 RVA: 0x002BD516 File Offset: 0x002BB716
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

		// Token: 0x1700469E RID: 18078
		// (get) Token: 0x0600FC1F RID: 64543 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600FC20 RID: 64544 RVA: 0x002BD530 File Offset: 0x002BB730
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

		// Token: 0x1700469F RID: 18079
		// (get) Token: 0x0600FC21 RID: 64545 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600FC22 RID: 64546 RVA: 0x002BD54B File Offset: 0x002BB74B
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

		// Token: 0x170046A0 RID: 18080
		// (get) Token: 0x0600FC23 RID: 64547 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600FC24 RID: 64548 RVA: 0x002BDB45 File Offset: 0x002BBD45
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

		// Token: 0x170046A1 RID: 18081
		// (get) Token: 0x0600FC25 RID: 64549 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600FC26 RID: 64550 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQulifiedId
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

		// Token: 0x170046A2 RID: 18082
		// (get) Token: 0x0600FC27 RID: 64551 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600FC28 RID: 64552 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQulifiedId
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

		// Token: 0x170046A3 RID: 18083
		// (get) Token: 0x0600FC29 RID: 64553 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600FC2A RID: 64554 RVA: 0x002BE209 File Offset: 0x002BC409
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

		// Token: 0x170046A4 RID: 18084
		// (get) Token: 0x0600FC2B RID: 64555 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600FC2C RID: 64556 RVA: 0x002BE225 File Offset: 0x002BC425
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

		// Token: 0x170046A5 RID: 18085
		// (get) Token: 0x0600FC2D RID: 64557 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600FC2E RID: 64558 RVA: 0x002BE241 File Offset: 0x002BC441
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

		// Token: 0x170046A6 RID: 18086
		// (get) Token: 0x0600FC2F RID: 64559 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600FC30 RID: 64560 RVA: 0x002BE25D File Offset: 0x002BC45D
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

		// Token: 0x170046A7 RID: 18087
		// (get) Token: 0x0600FC31 RID: 64561 RVA: 0x002CDD31 File Offset: 0x002CBF31
		// (set) Token: 0x0600FC32 RID: 64562 RVA: 0x002BE279 File Offset: 0x002BC479
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

		// Token: 0x170046A8 RID: 18088
		// (get) Token: 0x0600FC33 RID: 64563 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600FC34 RID: 64564 RVA: 0x002BE295 File Offset: 0x002BC495
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

		// Token: 0x170046A9 RID: 18089
		// (get) Token: 0x0600FC35 RID: 64565 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600FC36 RID: 64566 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
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

		// Token: 0x170046AA RID: 18090
		// (get) Token: 0x0600FC37 RID: 64567 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0600FC38 RID: 64568 RVA: 0x002BE2CD File Offset: 0x002BC4CD
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

		// Token: 0x170046AB RID: 18091
		// (get) Token: 0x0600FC39 RID: 64569 RVA: 0x002DB1B1 File Offset: 0x002D93B1
		// (set) Token: 0x0600FC3A RID: 64570 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(0, "autoScale")]
		public BooleanValue AutoScale
		{
			get
			{
				return (BooleanValue)base.Attributes[21];
			}
			set
			{
				base.Attributes[21] = value;
			}
		}

		// Token: 0x170046AC RID: 18092
		// (get) Token: 0x0600FC3B RID: 64571 RVA: 0x002C8792 File Offset: 0x002C6992
		// (set) Token: 0x0600FC3C RID: 64572 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(0, "centerVertically")]
		public BooleanValue CenterVertically
		{
			get
			{
				return (BooleanValue)base.Attributes[22];
			}
			set
			{
				base.Attributes[22] = value;
			}
		}

		// Token: 0x0600FC3D RID: 64573 RVA: 0x00293ECF File Offset: 0x002920CF
		public Group()
		{
		}

		// Token: 0x0600FC3E RID: 64574 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Group(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FC3F RID: 64575 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Group(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FC40 RID: 64576 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Group(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600FC41 RID: 64577 RVA: 0x002DB1C4 File Offset: 0x002D93C4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (57 == namespaceId && "control" == name)
			{
				return new ControlClone();
			}
			if (57 == namespaceId && "labelControl" == name)
			{
				return new LabelControl();
			}
			if (57 == namespaceId && "button" == name)
			{
				return new Button();
			}
			if (57 == namespaceId && "toggleButton" == name)
			{
				return new ToggleButton();
			}
			if (57 == namespaceId && "checkBox" == name)
			{
				return new CheckBox();
			}
			if (57 == namespaceId && "editBox" == name)
			{
				return new EditBox();
			}
			if (57 == namespaceId && "comboBox" == name)
			{
				return new ComboBox();
			}
			if (57 == namespaceId && "dropDown" == name)
			{
				return new DropDownRegular();
			}
			if (57 == namespaceId && "gallery" == name)
			{
				return new Gallery();
			}
			if (57 == namespaceId && "menu" == name)
			{
				return new Menu();
			}
			if (57 == namespaceId && "dynamicMenu" == name)
			{
				return new DynamicMenu();
			}
			if (57 == namespaceId && "splitButton" == name)
			{
				return new SplitButton();
			}
			if (57 == namespaceId && "box" == name)
			{
				return new Box();
			}
			if (57 == namespaceId && "buttonGroup" == name)
			{
				return new ButtonGroup();
			}
			if (57 == namespaceId && "separator" == name)
			{
				return new Separator();
			}
			if (57 == namespaceId && "dialogBoxLauncher" == name)
			{
				return new DialogBoxLauncher();
			}
			return null;
		}

		// Token: 0x0600FC42 RID: 64578 RVA: 0x002DB354 File Offset: 0x002D9554
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
			if (namespaceId == 0 && "idMso" == name)
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
			if (namespaceId == 0 && "autoScale" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "centerVertically" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600FC43 RID: 64579 RVA: 0x002DB563 File Offset: 0x002D9763
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Group>(deep);
		}

		// Token: 0x0600FC44 RID: 64580 RVA: 0x002DB56C File Offset: 0x002D976C
		// Note: this type is marked as 'beforefieldinit'.
		static Group()
		{
			byte[] array = new byte[23];
			Group.attributeNamespaceIds = array;
		}

		// Token: 0x040071C6 RID: 29126
		private const string tagName = "group";

		// Token: 0x040071C7 RID: 29127
		private const byte tagNsId = 57;

		// Token: 0x040071C8 RID: 29128
		internal const int ElementTypeIdConst = 13084;

		// Token: 0x040071C9 RID: 29129
		private static string[] attributeTagNames = new string[]
		{
			"id", "idQ", "tag", "idMso", "label", "getLabel", "image", "imageMso", "getImage", "insertAfterMso",
			"insertBeforeMso", "insertAfterQ", "insertBeforeQ", "screentip", "getScreentip", "supertip", "getSupertip", "visible", "getVisible", "keytip",
			"getKeytip", "autoScale", "centerVertically"
		};

		// Token: 0x040071CA RID: 29130
		private static byte[] attributeNamespaceIds;
	}
}
