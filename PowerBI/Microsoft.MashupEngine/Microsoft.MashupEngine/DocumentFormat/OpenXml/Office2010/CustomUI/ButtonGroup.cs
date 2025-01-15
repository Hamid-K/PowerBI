using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022D2 RID: 8914
	[ChildElementInfo(typeof(MenuRegular), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ToggleButtonRegular), FileFormatVersions.Office2010)]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ButtonRegular), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(SplitButtonRegular), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Separator), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(DynamicMenuRegular), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(GalleryRegular), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ControlCloneRegular), FileFormatVersions.Office2010)]
	internal class ButtonGroup : OpenXmlCompositeElement
	{
		// Token: 0x1700449C RID: 17564
		// (get) Token: 0x0600F7F1 RID: 63473 RVA: 0x002CE751 File Offset: 0x002CC951
		public override string LocalName
		{
			get
			{
				return "buttonGroup";
			}
		}

		// Token: 0x1700449D RID: 17565
		// (get) Token: 0x0600F7F2 RID: 63474 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x1700449E RID: 17566
		// (get) Token: 0x0600F7F3 RID: 63475 RVA: 0x002D7676 File Offset: 0x002D5876
		internal override int ElementTypeId
		{
			get
			{
				return 13059;
			}
		}

		// Token: 0x0600F7F4 RID: 63476 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x1700449F RID: 17567
		// (get) Token: 0x0600F7F5 RID: 63477 RVA: 0x002D767D File Offset: 0x002D587D
		internal override string[] AttributeTagNames
		{
			get
			{
				return ButtonGroup.attributeTagNames;
			}
		}

		// Token: 0x170044A0 RID: 17568
		// (get) Token: 0x0600F7F6 RID: 63478 RVA: 0x002D7684 File Offset: 0x002D5884
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ButtonGroup.attributeNamespaceIds;
			}
		}

		// Token: 0x170044A1 RID: 17569
		// (get) Token: 0x0600F7F7 RID: 63479 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600F7F8 RID: 63480 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x170044A2 RID: 17570
		// (get) Token: 0x0600F7F9 RID: 63481 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600F7FA RID: 63482 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x170044A3 RID: 17571
		// (get) Token: 0x0600F7FB RID: 63483 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600F7FC RID: 63484 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x170044A4 RID: 17572
		// (get) Token: 0x0600F7FD RID: 63485 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x0600F7FE RID: 63486 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x170044A5 RID: 17573
		// (get) Token: 0x0600F7FF RID: 63487 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600F800 RID: 63488 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x170044A6 RID: 17574
		// (get) Token: 0x0600F801 RID: 63489 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600F802 RID: 63490 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
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

		// Token: 0x170044A7 RID: 17575
		// (get) Token: 0x0600F803 RID: 63491 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600F804 RID: 63492 RVA: 0x002BD4FC File Offset: 0x002BB6FC
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

		// Token: 0x170044A8 RID: 17576
		// (get) Token: 0x0600F805 RID: 63493 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600F806 RID: 63494 RVA: 0x002BD516 File Offset: 0x002BB716
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

		// Token: 0x170044A9 RID: 17577
		// (get) Token: 0x0600F807 RID: 63495 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600F808 RID: 63496 RVA: 0x002BD530 File Offset: 0x002BB730
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

		// Token: 0x0600F809 RID: 63497 RVA: 0x00293ECF File Offset: 0x002920CF
		public ButtonGroup()
		{
		}

		// Token: 0x0600F80A RID: 63498 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ButtonGroup(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F80B RID: 63499 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ButtonGroup(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F80C RID: 63500 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ButtonGroup(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600F80D RID: 63501 RVA: 0x002D768C File Offset: 0x002D588C
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
			if (57 == namespaceId && "toggleButton" == name)
			{
				return new ToggleButtonRegular();
			}
			if (57 == namespaceId && "gallery" == name)
			{
				return new GalleryRegular();
			}
			if (57 == namespaceId && "menu" == name)
			{
				return new MenuRegular();
			}
			if (57 == namespaceId && "dynamicMenu" == name)
			{
				return new DynamicMenuRegular();
			}
			if (57 == namespaceId && "splitButton" == name)
			{
				return new SplitButtonRegular();
			}
			if (57 == namespaceId && "separator" == name)
			{
				return new Separator();
			}
			return null;
		}

		// Token: 0x0600F80E RID: 63502 RVA: 0x002D775C File Offset: 0x002D595C
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
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600F80F RID: 63503 RVA: 0x002D7837 File Offset: 0x002D5A37
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ButtonGroup>(deep);
		}

		// Token: 0x0600F810 RID: 63504 RVA: 0x002D7840 File Offset: 0x002D5A40
		// Note: this type is marked as 'beforefieldinit'.
		static ButtonGroup()
		{
			byte[] array = new byte[9];
			ButtonGroup.attributeNamespaceIds = array;
		}

		// Token: 0x04007147 RID: 28999
		private const string tagName = "buttonGroup";

		// Token: 0x04007148 RID: 29000
		private const byte tagNsId = 57;

		// Token: 0x04007149 RID: 29001
		internal const int ElementTypeIdConst = 13059;

		// Token: 0x0400714A RID: 29002
		private static string[] attributeTagNames = new string[] { "id", "idQ", "tag", "visible", "getVisible", "insertAfterMso", "insertBeforeMso", "insertAfterQ", "insertBeforeQ" };

		// Token: 0x0400714B RID: 29003
		private static byte[] attributeNamespaceIds;
	}
}
