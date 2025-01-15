using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F22 RID: 12066
	[ChildElementInfo(typeof(TableLayout))]
	[ChildElementInfo(typeof(TableStyle))]
	[ChildElementInfo(typeof(TablePositionProperties))]
	[ChildElementInfo(typeof(TableOverlap))]
	[ChildElementInfo(typeof(BiDiVisual))]
	[ChildElementInfo(typeof(TableWidth))]
	[ChildElementInfo(typeof(TableJustification))]
	[ChildElementInfo(typeof(TableCellSpacing))]
	[ChildElementInfo(typeof(TableIndentation))]
	[ChildElementInfo(typeof(TableBorders))]
	[ChildElementInfo(typeof(Shading))]
	[ChildElementInfo(typeof(TableCellMarginDefault))]
	[ChildElementInfo(typeof(TableLook))]
	[ChildElementInfo(typeof(TableCaption), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(TableDescription), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class PreviousTableProperties : OpenXmlCompositeElement
	{
		// Token: 0x17008ECC RID: 36556
		// (get) Token: 0x06019D52 RID: 105810 RVA: 0x0030DFE2 File Offset: 0x0030C1E2
		public override string LocalName
		{
			get
			{
				return "tblPr";
			}
		}

		// Token: 0x17008ECD RID: 36557
		// (get) Token: 0x06019D53 RID: 105811 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008ECE RID: 36558
		// (get) Token: 0x06019D54 RID: 105812 RVA: 0x003572D7 File Offset: 0x003554D7
		internal override int ElementTypeId
		{
			get
			{
				return 11707;
			}
		}

		// Token: 0x06019D55 RID: 105813 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019D56 RID: 105814 RVA: 0x00293ECF File Offset: 0x002920CF
		public PreviousTableProperties()
		{
		}

		// Token: 0x06019D57 RID: 105815 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PreviousTableProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019D58 RID: 105816 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PreviousTableProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019D59 RID: 105817 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PreviousTableProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019D5A RID: 105818 RVA: 0x003572E0 File Offset: 0x003554E0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "tblStyle" == name)
			{
				return new TableStyle();
			}
			if (23 == namespaceId && "tblpPr" == name)
			{
				return new TablePositionProperties();
			}
			if (23 == namespaceId && "tblOverlap" == name)
			{
				return new TableOverlap();
			}
			if (23 == namespaceId && "bidiVisual" == name)
			{
				return new BiDiVisual();
			}
			if (23 == namespaceId && "tblW" == name)
			{
				return new TableWidth();
			}
			if (23 == namespaceId && "jc" == name)
			{
				return new TableJustification();
			}
			if (23 == namespaceId && "tblCellSpacing" == name)
			{
				return new TableCellSpacing();
			}
			if (23 == namespaceId && "tblInd" == name)
			{
				return new TableIndentation();
			}
			if (23 == namespaceId && "tblBorders" == name)
			{
				return new TableBorders();
			}
			if (23 == namespaceId && "shd" == name)
			{
				return new Shading();
			}
			if (23 == namespaceId && "tblLayout" == name)
			{
				return new TableLayout();
			}
			if (23 == namespaceId && "tblCellMar" == name)
			{
				return new TableCellMarginDefault();
			}
			if (23 == namespaceId && "tblLook" == name)
			{
				return new TableLook();
			}
			if (23 == namespaceId && "tblCaption" == name)
			{
				return new TableCaption();
			}
			if (23 == namespaceId && "tblDescription" == name)
			{
				return new TableDescription();
			}
			return null;
		}

		// Token: 0x17008ECF RID: 36559
		// (get) Token: 0x06019D5B RID: 105819 RVA: 0x00357456 File Offset: 0x00355656
		internal override string[] ElementTagNames
		{
			get
			{
				return PreviousTableProperties.eleTagNames;
			}
		}

		// Token: 0x17008ED0 RID: 36560
		// (get) Token: 0x06019D5C RID: 105820 RVA: 0x0035745D File Offset: 0x0035565D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PreviousTableProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17008ED1 RID: 36561
		// (get) Token: 0x06019D5D RID: 105821 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008ED2 RID: 36562
		// (get) Token: 0x06019D5E RID: 105822 RVA: 0x00357464 File Offset: 0x00355664
		// (set) Token: 0x06019D5F RID: 105823 RVA: 0x0035746D File Offset: 0x0035566D
		public TableStyle TableStyle
		{
			get
			{
				return base.GetElement<TableStyle>(0);
			}
			set
			{
				base.SetElement<TableStyle>(0, value);
			}
		}

		// Token: 0x17008ED3 RID: 36563
		// (get) Token: 0x06019D60 RID: 105824 RVA: 0x00357477 File Offset: 0x00355677
		// (set) Token: 0x06019D61 RID: 105825 RVA: 0x00357480 File Offset: 0x00355680
		public TablePositionProperties TablePositionProperties
		{
			get
			{
				return base.GetElement<TablePositionProperties>(1);
			}
			set
			{
				base.SetElement<TablePositionProperties>(1, value);
			}
		}

		// Token: 0x17008ED4 RID: 36564
		// (get) Token: 0x06019D62 RID: 105826 RVA: 0x0035748A File Offset: 0x0035568A
		// (set) Token: 0x06019D63 RID: 105827 RVA: 0x00357493 File Offset: 0x00355693
		public TableOverlap TableOverlap
		{
			get
			{
				return base.GetElement<TableOverlap>(2);
			}
			set
			{
				base.SetElement<TableOverlap>(2, value);
			}
		}

		// Token: 0x17008ED5 RID: 36565
		// (get) Token: 0x06019D64 RID: 105828 RVA: 0x0035749D File Offset: 0x0035569D
		// (set) Token: 0x06019D65 RID: 105829 RVA: 0x003574A6 File Offset: 0x003556A6
		public BiDiVisual BiDiVisual
		{
			get
			{
				return base.GetElement<BiDiVisual>(3);
			}
			set
			{
				base.SetElement<BiDiVisual>(3, value);
			}
		}

		// Token: 0x17008ED6 RID: 36566
		// (get) Token: 0x06019D66 RID: 105830 RVA: 0x003574B0 File Offset: 0x003556B0
		// (set) Token: 0x06019D67 RID: 105831 RVA: 0x003574B9 File Offset: 0x003556B9
		public TableWidth TableWidth
		{
			get
			{
				return base.GetElement<TableWidth>(4);
			}
			set
			{
				base.SetElement<TableWidth>(4, value);
			}
		}

		// Token: 0x17008ED7 RID: 36567
		// (get) Token: 0x06019D68 RID: 105832 RVA: 0x003574C3 File Offset: 0x003556C3
		// (set) Token: 0x06019D69 RID: 105833 RVA: 0x003574CC File Offset: 0x003556CC
		public TableJustification TableJustification
		{
			get
			{
				return base.GetElement<TableJustification>(5);
			}
			set
			{
				base.SetElement<TableJustification>(5, value);
			}
		}

		// Token: 0x17008ED8 RID: 36568
		// (get) Token: 0x06019D6A RID: 105834 RVA: 0x003574D6 File Offset: 0x003556D6
		// (set) Token: 0x06019D6B RID: 105835 RVA: 0x003574DF File Offset: 0x003556DF
		public TableCellSpacing TableCellSpacing
		{
			get
			{
				return base.GetElement<TableCellSpacing>(6);
			}
			set
			{
				base.SetElement<TableCellSpacing>(6, value);
			}
		}

		// Token: 0x17008ED9 RID: 36569
		// (get) Token: 0x06019D6C RID: 105836 RVA: 0x003574E9 File Offset: 0x003556E9
		// (set) Token: 0x06019D6D RID: 105837 RVA: 0x003574F2 File Offset: 0x003556F2
		public TableIndentation TableIndentation
		{
			get
			{
				return base.GetElement<TableIndentation>(7);
			}
			set
			{
				base.SetElement<TableIndentation>(7, value);
			}
		}

		// Token: 0x17008EDA RID: 36570
		// (get) Token: 0x06019D6E RID: 105838 RVA: 0x003574FC File Offset: 0x003556FC
		// (set) Token: 0x06019D6F RID: 105839 RVA: 0x00357505 File Offset: 0x00355705
		public TableBorders TableBorders
		{
			get
			{
				return base.GetElement<TableBorders>(8);
			}
			set
			{
				base.SetElement<TableBorders>(8, value);
			}
		}

		// Token: 0x17008EDB RID: 36571
		// (get) Token: 0x06019D70 RID: 105840 RVA: 0x0035750F File Offset: 0x0035570F
		// (set) Token: 0x06019D71 RID: 105841 RVA: 0x00357519 File Offset: 0x00355719
		public Shading Shading
		{
			get
			{
				return base.GetElement<Shading>(9);
			}
			set
			{
				base.SetElement<Shading>(9, value);
			}
		}

		// Token: 0x17008EDC RID: 36572
		// (get) Token: 0x06019D72 RID: 105842 RVA: 0x00357524 File Offset: 0x00355724
		// (set) Token: 0x06019D73 RID: 105843 RVA: 0x0035752E File Offset: 0x0035572E
		public TableLayout TableLayout
		{
			get
			{
				return base.GetElement<TableLayout>(10);
			}
			set
			{
				base.SetElement<TableLayout>(10, value);
			}
		}

		// Token: 0x17008EDD RID: 36573
		// (get) Token: 0x06019D74 RID: 105844 RVA: 0x00357539 File Offset: 0x00355739
		// (set) Token: 0x06019D75 RID: 105845 RVA: 0x00357543 File Offset: 0x00355743
		public TableCellMarginDefault TableCellMarginDefault
		{
			get
			{
				return base.GetElement<TableCellMarginDefault>(11);
			}
			set
			{
				base.SetElement<TableCellMarginDefault>(11, value);
			}
		}

		// Token: 0x17008EDE RID: 36574
		// (get) Token: 0x06019D76 RID: 105846 RVA: 0x0035754E File Offset: 0x0035574E
		// (set) Token: 0x06019D77 RID: 105847 RVA: 0x00357558 File Offset: 0x00355758
		public TableLook TableLook
		{
			get
			{
				return base.GetElement<TableLook>(12);
			}
			set
			{
				base.SetElement<TableLook>(12, value);
			}
		}

		// Token: 0x17008EDF RID: 36575
		// (get) Token: 0x06019D78 RID: 105848 RVA: 0x00357563 File Offset: 0x00355763
		// (set) Token: 0x06019D79 RID: 105849 RVA: 0x0035756D File Offset: 0x0035576D
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public TableCaption TableCaption
		{
			get
			{
				return base.GetElement<TableCaption>(13);
			}
			set
			{
				base.SetElement<TableCaption>(13, value);
			}
		}

		// Token: 0x17008EE0 RID: 36576
		// (get) Token: 0x06019D7A RID: 105850 RVA: 0x00357578 File Offset: 0x00355778
		// (set) Token: 0x06019D7B RID: 105851 RVA: 0x00357582 File Offset: 0x00355782
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public TableDescription TableDescription
		{
			get
			{
				return base.GetElement<TableDescription>(14);
			}
			set
			{
				base.SetElement<TableDescription>(14, value);
			}
		}

		// Token: 0x06019D7C RID: 105852 RVA: 0x0035758D File Offset: 0x0035578D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PreviousTableProperties>(deep);
		}

		// Token: 0x0400AA9D RID: 43677
		private const string tagName = "tblPr";

		// Token: 0x0400AA9E RID: 43678
		private const byte tagNsId = 23;

		// Token: 0x0400AA9F RID: 43679
		internal const int ElementTypeIdConst = 11707;

		// Token: 0x0400AAA0 RID: 43680
		private static readonly string[] eleTagNames = new string[]
		{
			"tblStyle", "tblpPr", "tblOverlap", "bidiVisual", "tblW", "jc", "tblCellSpacing", "tblInd", "tblBorders", "shd",
			"tblLayout", "tblCellMar", "tblLook", "tblCaption", "tblDescription"
		};

		// Token: 0x0400AAA1 RID: 43681
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23
		};
	}
}
