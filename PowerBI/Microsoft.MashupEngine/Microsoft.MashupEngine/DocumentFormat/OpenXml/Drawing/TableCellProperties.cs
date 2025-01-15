using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027ED RID: 10221
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(BottomLeftToTopRightBorderLineProperties))]
	[ChildElementInfo(typeof(LeftBorderLineProperties))]
	[ChildElementInfo(typeof(RightBorderLineProperties))]
	[ChildElementInfo(typeof(TopBorderLineProperties))]
	[ChildElementInfo(typeof(BottomBorderLineProperties))]
	[ChildElementInfo(typeof(TopLeftToBottomRightBorderLineProperties))]
	[ChildElementInfo(typeof(SolidFill))]
	[ChildElementInfo(typeof(Cell3DProperties))]
	[ChildElementInfo(typeof(NoFill))]
	[ChildElementInfo(typeof(BlipFill))]
	[ChildElementInfo(typeof(GroupFill))]
	[ChildElementInfo(typeof(GradientFill))]
	[ChildElementInfo(typeof(PatternFill))]
	internal class TableCellProperties : OpenXmlCompositeElement
	{
		// Token: 0x17006498 RID: 25752
		// (get) Token: 0x06013EF5 RID: 81653 RVA: 0x0030D556 File Offset: 0x0030B756
		public override string LocalName
		{
			get
			{
				return "tcPr";
			}
		}

		// Token: 0x17006499 RID: 25753
		// (get) Token: 0x06013EF6 RID: 81654 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700649A RID: 25754
		// (get) Token: 0x06013EF7 RID: 81655 RVA: 0x0030D55D File Offset: 0x0030B75D
		internal override int ElementTypeId
		{
			get
			{
				return 10259;
			}
		}

		// Token: 0x06013EF8 RID: 81656 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700649B RID: 25755
		// (get) Token: 0x06013EF9 RID: 81657 RVA: 0x0030D564 File Offset: 0x0030B764
		internal override string[] AttributeTagNames
		{
			get
			{
				return TableCellProperties.attributeTagNames;
			}
		}

		// Token: 0x1700649C RID: 25756
		// (get) Token: 0x06013EFA RID: 81658 RVA: 0x0030D56B File Offset: 0x0030B76B
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TableCellProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x1700649D RID: 25757
		// (get) Token: 0x06013EFB RID: 81659 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06013EFC RID: 81660 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "marL")]
		public Int32Value LeftMargin
		{
			get
			{
				return (Int32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x1700649E RID: 25758
		// (get) Token: 0x06013EFD RID: 81661 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x06013EFE RID: 81662 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "marR")]
		public Int32Value RightMargin
		{
			get
			{
				return (Int32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x1700649F RID: 25759
		// (get) Token: 0x06013EFF RID: 81663 RVA: 0x002E1683 File Offset: 0x002DF883
		// (set) Token: 0x06013F00 RID: 81664 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "marT")]
		public Int32Value TopMargin
		{
			get
			{
				return (Int32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x170064A0 RID: 25760
		// (get) Token: 0x06013F01 RID: 81665 RVA: 0x002BFA76 File Offset: 0x002BDC76
		// (set) Token: 0x06013F02 RID: 81666 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "marB")]
		public Int32Value BottomMargin
		{
			get
			{
				return (Int32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x170064A1 RID: 25761
		// (get) Token: 0x06013F03 RID: 81667 RVA: 0x002F0948 File Offset: 0x002EEB48
		// (set) Token: 0x06013F04 RID: 81668 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "vert")]
		public EnumValue<TextVerticalValues> Vertical
		{
			get
			{
				return (EnumValue<TextVerticalValues>)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x170064A2 RID: 25762
		// (get) Token: 0x06013F05 RID: 81669 RVA: 0x0030D572 File Offset: 0x0030B772
		// (set) Token: 0x06013F06 RID: 81670 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "anchor")]
		public EnumValue<TextAnchoringTypeValues> Anchor
		{
			get
			{
				return (EnumValue<TextAnchoringTypeValues>)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x170064A3 RID: 25763
		// (get) Token: 0x06013F07 RID: 81671 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x06013F08 RID: 81672 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "anchorCtr")]
		public BooleanValue AnchorCenter
		{
			get
			{
				return (BooleanValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x170064A4 RID: 25764
		// (get) Token: 0x06013F09 RID: 81673 RVA: 0x0030D581 File Offset: 0x0030B781
		// (set) Token: 0x06013F0A RID: 81674 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "horzOverflow")]
		public EnumValue<TextHorizontalOverflowValues> HorizontalOverflow
		{
			get
			{
				return (EnumValue<TextHorizontalOverflowValues>)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x06013F0B RID: 81675 RVA: 0x00293ECF File Offset: 0x002920CF
		public TableCellProperties()
		{
		}

		// Token: 0x06013F0C RID: 81676 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TableCellProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013F0D RID: 81677 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TableCellProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013F0E RID: 81678 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TableCellProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013F0F RID: 81679 RVA: 0x0030D590 File Offset: 0x0030B790
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "lnL" == name)
			{
				return new LeftBorderLineProperties();
			}
			if (10 == namespaceId && "lnR" == name)
			{
				return new RightBorderLineProperties();
			}
			if (10 == namespaceId && "lnT" == name)
			{
				return new TopBorderLineProperties();
			}
			if (10 == namespaceId && "lnB" == name)
			{
				return new BottomBorderLineProperties();
			}
			if (10 == namespaceId && "lnTlToBr" == name)
			{
				return new TopLeftToBottomRightBorderLineProperties();
			}
			if (10 == namespaceId && "lnBlToTr" == name)
			{
				return new BottomLeftToTopRightBorderLineProperties();
			}
			if (10 == namespaceId && "cell3D" == name)
			{
				return new Cell3DProperties();
			}
			if (10 == namespaceId && "noFill" == name)
			{
				return new NoFill();
			}
			if (10 == namespaceId && "solidFill" == name)
			{
				return new SolidFill();
			}
			if (10 == namespaceId && "gradFill" == name)
			{
				return new GradientFill();
			}
			if (10 == namespaceId && "blipFill" == name)
			{
				return new BlipFill();
			}
			if (10 == namespaceId && "pattFill" == name)
			{
				return new PatternFill();
			}
			if (10 == namespaceId && "grpFill" == name)
			{
				return new GroupFill();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170064A5 RID: 25765
		// (get) Token: 0x06013F10 RID: 81680 RVA: 0x0030D6EE File Offset: 0x0030B8EE
		internal override string[] ElementTagNames
		{
			get
			{
				return TableCellProperties.eleTagNames;
			}
		}

		// Token: 0x170064A6 RID: 25766
		// (get) Token: 0x06013F11 RID: 81681 RVA: 0x0030D6F5 File Offset: 0x0030B8F5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TableCellProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170064A7 RID: 25767
		// (get) Token: 0x06013F12 RID: 81682 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170064A8 RID: 25768
		// (get) Token: 0x06013F13 RID: 81683 RVA: 0x0030D6FC File Offset: 0x0030B8FC
		// (set) Token: 0x06013F14 RID: 81684 RVA: 0x0030D705 File Offset: 0x0030B905
		public LeftBorderLineProperties LeftBorderLineProperties
		{
			get
			{
				return base.GetElement<LeftBorderLineProperties>(0);
			}
			set
			{
				base.SetElement<LeftBorderLineProperties>(0, value);
			}
		}

		// Token: 0x170064A9 RID: 25769
		// (get) Token: 0x06013F15 RID: 81685 RVA: 0x0030D70F File Offset: 0x0030B90F
		// (set) Token: 0x06013F16 RID: 81686 RVA: 0x0030D718 File Offset: 0x0030B918
		public RightBorderLineProperties RightBorderLineProperties
		{
			get
			{
				return base.GetElement<RightBorderLineProperties>(1);
			}
			set
			{
				base.SetElement<RightBorderLineProperties>(1, value);
			}
		}

		// Token: 0x170064AA RID: 25770
		// (get) Token: 0x06013F17 RID: 81687 RVA: 0x0030D722 File Offset: 0x0030B922
		// (set) Token: 0x06013F18 RID: 81688 RVA: 0x0030D72B File Offset: 0x0030B92B
		public TopBorderLineProperties TopBorderLineProperties
		{
			get
			{
				return base.GetElement<TopBorderLineProperties>(2);
			}
			set
			{
				base.SetElement<TopBorderLineProperties>(2, value);
			}
		}

		// Token: 0x170064AB RID: 25771
		// (get) Token: 0x06013F19 RID: 81689 RVA: 0x0030D735 File Offset: 0x0030B935
		// (set) Token: 0x06013F1A RID: 81690 RVA: 0x0030D73E File Offset: 0x0030B93E
		public BottomBorderLineProperties BottomBorderLineProperties
		{
			get
			{
				return base.GetElement<BottomBorderLineProperties>(3);
			}
			set
			{
				base.SetElement<BottomBorderLineProperties>(3, value);
			}
		}

		// Token: 0x170064AC RID: 25772
		// (get) Token: 0x06013F1B RID: 81691 RVA: 0x0030D748 File Offset: 0x0030B948
		// (set) Token: 0x06013F1C RID: 81692 RVA: 0x0030D751 File Offset: 0x0030B951
		public TopLeftToBottomRightBorderLineProperties TopLeftToBottomRightBorderLineProperties
		{
			get
			{
				return base.GetElement<TopLeftToBottomRightBorderLineProperties>(4);
			}
			set
			{
				base.SetElement<TopLeftToBottomRightBorderLineProperties>(4, value);
			}
		}

		// Token: 0x170064AD RID: 25773
		// (get) Token: 0x06013F1D RID: 81693 RVA: 0x0030D75B File Offset: 0x0030B95B
		// (set) Token: 0x06013F1E RID: 81694 RVA: 0x0030D764 File Offset: 0x0030B964
		public BottomLeftToTopRightBorderLineProperties BottomLeftToTopRightBorderLineProperties
		{
			get
			{
				return base.GetElement<BottomLeftToTopRightBorderLineProperties>(5);
			}
			set
			{
				base.SetElement<BottomLeftToTopRightBorderLineProperties>(5, value);
			}
		}

		// Token: 0x170064AE RID: 25774
		// (get) Token: 0x06013F1F RID: 81695 RVA: 0x0030D76E File Offset: 0x0030B96E
		// (set) Token: 0x06013F20 RID: 81696 RVA: 0x0030D777 File Offset: 0x0030B977
		public Cell3DProperties Cell3DProperties
		{
			get
			{
				return base.GetElement<Cell3DProperties>(6);
			}
			set
			{
				base.SetElement<Cell3DProperties>(6, value);
			}
		}

		// Token: 0x06013F21 RID: 81697 RVA: 0x0030D784 File Offset: 0x0030B984
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "marL" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "marR" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "marT" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "marB" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "vert" == name)
			{
				return new EnumValue<TextVerticalValues>();
			}
			if (namespaceId == 0 && "anchor" == name)
			{
				return new EnumValue<TextAnchoringTypeValues>();
			}
			if (namespaceId == 0 && "anchorCtr" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "horzOverflow" == name)
			{
				return new EnumValue<TextHorizontalOverflowValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013F22 RID: 81698 RVA: 0x0030D849 File Offset: 0x0030BA49
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableCellProperties>(deep);
		}

		// Token: 0x06013F23 RID: 81699 RVA: 0x0030D854 File Offset: 0x0030BA54
		// Note: this type is marked as 'beforefieldinit'.
		static TableCellProperties()
		{
			byte[] array = new byte[8];
			TableCellProperties.attributeNamespaceIds = array;
			TableCellProperties.eleTagNames = new string[]
			{
				"lnL", "lnR", "lnT", "lnB", "lnTlToBr", "lnBlToTr", "cell3D", "noFill", "solidFill", "gradFill",
				"blipFill", "pattFill", "grpFill", "extLst"
			};
			TableCellProperties.eleNamespaceIds = new byte[]
			{
				10, 10, 10, 10, 10, 10, 10, 10, 10, 10,
				10, 10, 10, 10
			};
		}

		// Token: 0x0400885B RID: 34907
		private const string tagName = "tcPr";

		// Token: 0x0400885C RID: 34908
		private const byte tagNsId = 10;

		// Token: 0x0400885D RID: 34909
		internal const int ElementTypeIdConst = 10259;

		// Token: 0x0400885E RID: 34910
		private static string[] attributeTagNames = new string[] { "marL", "marR", "marT", "marB", "vert", "anchor", "anchorCtr", "horzOverflow" };

		// Token: 0x0400885F RID: 34911
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008860 RID: 34912
		private static readonly string[] eleTagNames;

		// Token: 0x04008861 RID: 34913
		private static readonly byte[] eleNamespaceIds;
	}
}
