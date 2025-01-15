using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022D1 RID: 8913
	[ChildElementInfo(typeof(Gallery), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(LabelControl), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ToggleButton), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(CheckBox), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(EditBox), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ComboBox), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(DropDownRegular), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Menu), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(DynamicMenu), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(SplitButton), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Box), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ButtonGroup), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Button), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ControlClone), FileFormatVersions.Office2010)]
	internal class Box : OpenXmlCompositeElement
	{
		// Token: 0x1700448D RID: 17549
		// (get) Token: 0x0600F7CF RID: 63439 RVA: 0x002CE46E File Offset: 0x002CC66E
		public override string LocalName
		{
			get
			{
				return "box";
			}
		}

		// Token: 0x1700448E RID: 17550
		// (get) Token: 0x0600F7D0 RID: 63440 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x1700448F RID: 17551
		// (get) Token: 0x0600F7D1 RID: 63441 RVA: 0x002D737A File Offset: 0x002D557A
		internal override int ElementTypeId
		{
			get
			{
				return 13058;
			}
		}

		// Token: 0x0600F7D2 RID: 63442 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004490 RID: 17552
		// (get) Token: 0x0600F7D3 RID: 63443 RVA: 0x002D7381 File Offset: 0x002D5581
		internal override string[] AttributeTagNames
		{
			get
			{
				return Box.attributeTagNames;
			}
		}

		// Token: 0x17004491 RID: 17553
		// (get) Token: 0x0600F7D4 RID: 63444 RVA: 0x002D7388 File Offset: 0x002D5588
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Box.attributeNamespaceIds;
			}
		}

		// Token: 0x17004492 RID: 17554
		// (get) Token: 0x0600F7D5 RID: 63445 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600F7D6 RID: 63446 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004493 RID: 17555
		// (get) Token: 0x0600F7D7 RID: 63447 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600F7D8 RID: 63448 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17004494 RID: 17556
		// (get) Token: 0x0600F7D9 RID: 63449 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600F7DA RID: 63450 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17004495 RID: 17557
		// (get) Token: 0x0600F7DB RID: 63451 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x0600F7DC RID: 63452 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
		{
			get
			{
				return (BooleanValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17004496 RID: 17558
		// (get) Token: 0x0600F7DD RID: 63453 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600F7DE RID: 63454 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
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

		// Token: 0x17004497 RID: 17559
		// (get) Token: 0x0600F7DF RID: 63455 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600F7E0 RID: 63456 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "insertAfterMso")]
		public StringValue InsertAfterMso
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

		// Token: 0x17004498 RID: 17560
		// (get) Token: 0x0600F7E1 RID: 63457 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600F7E2 RID: 63458 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "insertBeforeMso")]
		public StringValue InsertBeforeMso
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

		// Token: 0x17004499 RID: 17561
		// (get) Token: 0x0600F7E3 RID: 63459 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600F7E4 RID: 63460 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQulifiedId
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

		// Token: 0x1700449A RID: 17562
		// (get) Token: 0x0600F7E5 RID: 63461 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600F7E6 RID: 63462 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQulifiedId
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

		// Token: 0x1700449B RID: 17563
		// (get) Token: 0x0600F7E7 RID: 63463 RVA: 0x002D738F File Offset: 0x002D558F
		// (set) Token: 0x0600F7E8 RID: 63464 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "boxStyle")]
		public EnumValue<BoxStyleValues> BoxStyle
		{
			get
			{
				return (EnumValue<BoxStyleValues>)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x0600F7E9 RID: 63465 RVA: 0x00293ECF File Offset: 0x002920CF
		public Box()
		{
		}

		// Token: 0x0600F7EA RID: 63466 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Box(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F7EB RID: 63467 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Box(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F7EC RID: 63468 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Box(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600F7ED RID: 63469 RVA: 0x002D73A0 File Offset: 0x002D55A0
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
			return null;
		}

		// Token: 0x0600F7EE RID: 63470 RVA: 0x002D7500 File Offset: 0x002D5700
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
			if (namespaceId == 0 && "visible" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "getVisible" == name)
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
			if (namespaceId == 0 && "boxStyle" == name)
			{
				return new EnumValue<BoxStyleValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600F7EF RID: 63471 RVA: 0x002D75F1 File Offset: 0x002D57F1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Box>(deep);
		}

		// Token: 0x0600F7F0 RID: 63472 RVA: 0x002D75FC File Offset: 0x002D57FC
		// Note: this type is marked as 'beforefieldinit'.
		static Box()
		{
			byte[] array = new byte[10];
			Box.attributeNamespaceIds = array;
		}

		// Token: 0x04007142 RID: 28994
		private const string tagName = "box";

		// Token: 0x04007143 RID: 28995
		private const byte tagNsId = 57;

		// Token: 0x04007144 RID: 28996
		internal const int ElementTypeIdConst = 13058;

		// Token: 0x04007145 RID: 28997
		private static string[] attributeTagNames = new string[] { "id", "idQ", "tag", "visible", "getVisible", "insertAfterMso", "insertBeforeMso", "insertAfterQ", "insertBeforeQ", "boxStyle" };

		// Token: 0x04007146 RID: 28998
		private static byte[] attributeNamespaceIds;
	}
}
