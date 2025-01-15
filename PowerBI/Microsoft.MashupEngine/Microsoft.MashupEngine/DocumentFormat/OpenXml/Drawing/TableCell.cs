using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027EE RID: 10222
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(TextBody))]
	[ChildElementInfo(typeof(TableCellProperties))]
	[GeneratedCode("DomGen", "2.0")]
	internal class TableCell : OpenXmlCompositeElement
	{
		// Token: 0x170064AF RID: 25775
		// (get) Token: 0x06013F24 RID: 81700 RVA: 0x0030D955 File Offset: 0x0030BB55
		public override string LocalName
		{
			get
			{
				return "tc";
			}
		}

		// Token: 0x170064B0 RID: 25776
		// (get) Token: 0x06013F25 RID: 81701 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170064B1 RID: 25777
		// (get) Token: 0x06013F26 RID: 81702 RVA: 0x0030D95C File Offset: 0x0030BB5C
		internal override int ElementTypeId
		{
			get
			{
				return 10260;
			}
		}

		// Token: 0x06013F27 RID: 81703 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170064B2 RID: 25778
		// (get) Token: 0x06013F28 RID: 81704 RVA: 0x0030D963 File Offset: 0x0030BB63
		internal override string[] AttributeTagNames
		{
			get
			{
				return TableCell.attributeTagNames;
			}
		}

		// Token: 0x170064B3 RID: 25779
		// (get) Token: 0x06013F29 RID: 81705 RVA: 0x0030D96A File Offset: 0x0030BB6A
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TableCell.attributeNamespaceIds;
			}
		}

		// Token: 0x170064B4 RID: 25780
		// (get) Token: 0x06013F2A RID: 81706 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06013F2B RID: 81707 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "rowSpan")]
		public Int32Value RowSpan
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

		// Token: 0x170064B5 RID: 25781
		// (get) Token: 0x06013F2C RID: 81708 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x06013F2D RID: 81709 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "gridSpan")]
		public Int32Value GridSpan
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

		// Token: 0x170064B6 RID: 25782
		// (get) Token: 0x06013F2E RID: 81710 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06013F2F RID: 81711 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "hMerge")]
		public BooleanValue HorizontalMerge
		{
			get
			{
				return (BooleanValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x170064B7 RID: 25783
		// (get) Token: 0x06013F30 RID: 81712 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06013F31 RID: 81713 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "vMerge")]
		public BooleanValue VerticalMerge
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

		// Token: 0x06013F32 RID: 81714 RVA: 0x00293ECF File Offset: 0x002920CF
		public TableCell()
		{
		}

		// Token: 0x06013F33 RID: 81715 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TableCell(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013F34 RID: 81716 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TableCell(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013F35 RID: 81717 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TableCell(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013F36 RID: 81718 RVA: 0x0030D974 File Offset: 0x0030BB74
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "txBody" == name)
			{
				return new TextBody();
			}
			if (10 == namespaceId && "tcPr" == name)
			{
				return new TableCellProperties();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170064B8 RID: 25784
		// (get) Token: 0x06013F37 RID: 81719 RVA: 0x0030D9CA File Offset: 0x0030BBCA
		internal override string[] ElementTagNames
		{
			get
			{
				return TableCell.eleTagNames;
			}
		}

		// Token: 0x170064B9 RID: 25785
		// (get) Token: 0x06013F38 RID: 81720 RVA: 0x0030D9D1 File Offset: 0x0030BBD1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TableCell.eleNamespaceIds;
			}
		}

		// Token: 0x170064BA RID: 25786
		// (get) Token: 0x06013F39 RID: 81721 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170064BB RID: 25787
		// (get) Token: 0x06013F3A RID: 81722 RVA: 0x0030AAA4 File Offset: 0x00308CA4
		// (set) Token: 0x06013F3B RID: 81723 RVA: 0x0030AAAD File Offset: 0x00308CAD
		public TextBody TextBody
		{
			get
			{
				return base.GetElement<TextBody>(0);
			}
			set
			{
				base.SetElement<TextBody>(0, value);
			}
		}

		// Token: 0x170064BC RID: 25788
		// (get) Token: 0x06013F3C RID: 81724 RVA: 0x0030D9D8 File Offset: 0x0030BBD8
		// (set) Token: 0x06013F3D RID: 81725 RVA: 0x0030D9E1 File Offset: 0x0030BBE1
		public TableCellProperties TableCellProperties
		{
			get
			{
				return base.GetElement<TableCellProperties>(1);
			}
			set
			{
				base.SetElement<TableCellProperties>(1, value);
			}
		}

		// Token: 0x170064BD RID: 25789
		// (get) Token: 0x06013F3E RID: 81726 RVA: 0x003012C6 File Offset: 0x002FF4C6
		// (set) Token: 0x06013F3F RID: 81727 RVA: 0x003012CF File Offset: 0x002FF4CF
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(2);
			}
			set
			{
				base.SetElement<ExtensionList>(2, value);
			}
		}

		// Token: 0x06013F40 RID: 81728 RVA: 0x0030D9EC File Offset: 0x0030BBEC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "rowSpan" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "gridSpan" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "hMerge" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "vMerge" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013F41 RID: 81729 RVA: 0x0030DA59 File Offset: 0x0030BC59
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableCell>(deep);
		}

		// Token: 0x06013F42 RID: 81730 RVA: 0x0030DA64 File Offset: 0x0030BC64
		// Note: this type is marked as 'beforefieldinit'.
		static TableCell()
		{
			byte[] array = new byte[4];
			TableCell.attributeNamespaceIds = array;
			TableCell.eleTagNames = new string[] { "txBody", "tcPr", "extLst" };
			TableCell.eleNamespaceIds = new byte[] { 10, 10, 10 };
		}

		// Token: 0x04008862 RID: 34914
		private const string tagName = "tc";

		// Token: 0x04008863 RID: 34915
		private const byte tagNsId = 10;

		// Token: 0x04008864 RID: 34916
		internal const int ElementTypeIdConst = 10260;

		// Token: 0x04008865 RID: 34917
		private static string[] attributeTagNames = new string[] { "rowSpan", "gridSpan", "hMerge", "vMerge" };

		// Token: 0x04008866 RID: 34918
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008867 RID: 34919
		private static readonly string[] eleTagNames;

		// Token: 0x04008868 RID: 34920
		private static readonly byte[] eleNamespaceIds;
	}
}
