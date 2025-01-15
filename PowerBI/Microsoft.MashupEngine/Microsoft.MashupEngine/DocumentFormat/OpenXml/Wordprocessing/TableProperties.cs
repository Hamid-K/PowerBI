using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F63 RID: 12131
	[ChildElementInfo(typeof(TableJustification))]
	[ChildElementInfo(typeof(TableBorders))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(TableStyle))]
	[ChildElementInfo(typeof(TablePositionProperties))]
	[ChildElementInfo(typeof(TableOverlap))]
	[ChildElementInfo(typeof(BiDiVisual))]
	[ChildElementInfo(typeof(TableWidth))]
	[ChildElementInfo(typeof(TableCellSpacing))]
	[ChildElementInfo(typeof(TableIndentation))]
	[ChildElementInfo(typeof(Shading))]
	[ChildElementInfo(typeof(TableLayout))]
	[ChildElementInfo(typeof(TableCellMarginDefault))]
	[ChildElementInfo(typeof(TableLook))]
	[ChildElementInfo(typeof(TableCaption), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(TableDescription), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(TablePropertiesChange))]
	internal class TableProperties : OpenXmlCompositeElement
	{
		// Token: 0x1700909C RID: 37020
		// (get) Token: 0x0601A14D RID: 106829 RVA: 0x0030DFE2 File Offset: 0x0030C1E2
		public override string LocalName
		{
			get
			{
				return "tblPr";
			}
		}

		// Token: 0x1700909D RID: 37021
		// (get) Token: 0x0601A14E RID: 106830 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700909E RID: 37022
		// (get) Token: 0x0601A14F RID: 106831 RVA: 0x0035D3F7 File Offset: 0x0035B5F7
		internal override int ElementTypeId
		{
			get
			{
				return 11789;
			}
		}

		// Token: 0x0601A150 RID: 106832 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A151 RID: 106833 RVA: 0x00293ECF File Offset: 0x002920CF
		public TableProperties()
		{
		}

		// Token: 0x0601A152 RID: 106834 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TableProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A153 RID: 106835 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TableProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A154 RID: 106836 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TableProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A155 RID: 106837 RVA: 0x0035D400 File Offset: 0x0035B600
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
			if (23 == namespaceId && "tblPrChange" == name)
			{
				return new TablePropertiesChange();
			}
			return null;
		}

		// Token: 0x1700909F RID: 37023
		// (get) Token: 0x0601A156 RID: 106838 RVA: 0x0035D58E File Offset: 0x0035B78E
		internal override string[] ElementTagNames
		{
			get
			{
				return TableProperties.eleTagNames;
			}
		}

		// Token: 0x170090A0 RID: 37024
		// (get) Token: 0x0601A157 RID: 106839 RVA: 0x0035D595 File Offset: 0x0035B795
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TableProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170090A1 RID: 37025
		// (get) Token: 0x0601A158 RID: 106840 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170090A2 RID: 37026
		// (get) Token: 0x0601A159 RID: 106841 RVA: 0x00357464 File Offset: 0x00355664
		// (set) Token: 0x0601A15A RID: 106842 RVA: 0x0035746D File Offset: 0x0035566D
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

		// Token: 0x170090A3 RID: 37027
		// (get) Token: 0x0601A15B RID: 106843 RVA: 0x00357477 File Offset: 0x00355677
		// (set) Token: 0x0601A15C RID: 106844 RVA: 0x00357480 File Offset: 0x00355680
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

		// Token: 0x170090A4 RID: 37028
		// (get) Token: 0x0601A15D RID: 106845 RVA: 0x0035748A File Offset: 0x0035568A
		// (set) Token: 0x0601A15E RID: 106846 RVA: 0x00357493 File Offset: 0x00355693
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

		// Token: 0x170090A5 RID: 37029
		// (get) Token: 0x0601A15F RID: 106847 RVA: 0x0035749D File Offset: 0x0035569D
		// (set) Token: 0x0601A160 RID: 106848 RVA: 0x003574A6 File Offset: 0x003556A6
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

		// Token: 0x170090A6 RID: 37030
		// (get) Token: 0x0601A161 RID: 106849 RVA: 0x003574B0 File Offset: 0x003556B0
		// (set) Token: 0x0601A162 RID: 106850 RVA: 0x003574B9 File Offset: 0x003556B9
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

		// Token: 0x170090A7 RID: 37031
		// (get) Token: 0x0601A163 RID: 106851 RVA: 0x003574C3 File Offset: 0x003556C3
		// (set) Token: 0x0601A164 RID: 106852 RVA: 0x003574CC File Offset: 0x003556CC
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

		// Token: 0x170090A8 RID: 37032
		// (get) Token: 0x0601A165 RID: 106853 RVA: 0x003574D6 File Offset: 0x003556D6
		// (set) Token: 0x0601A166 RID: 106854 RVA: 0x003574DF File Offset: 0x003556DF
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

		// Token: 0x170090A9 RID: 37033
		// (get) Token: 0x0601A167 RID: 106855 RVA: 0x003574E9 File Offset: 0x003556E9
		// (set) Token: 0x0601A168 RID: 106856 RVA: 0x003574F2 File Offset: 0x003556F2
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

		// Token: 0x170090AA RID: 37034
		// (get) Token: 0x0601A169 RID: 106857 RVA: 0x003574FC File Offset: 0x003556FC
		// (set) Token: 0x0601A16A RID: 106858 RVA: 0x00357505 File Offset: 0x00355705
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

		// Token: 0x170090AB RID: 37035
		// (get) Token: 0x0601A16B RID: 106859 RVA: 0x0035750F File Offset: 0x0035570F
		// (set) Token: 0x0601A16C RID: 106860 RVA: 0x00357519 File Offset: 0x00355719
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

		// Token: 0x170090AC RID: 37036
		// (get) Token: 0x0601A16D RID: 106861 RVA: 0x00357524 File Offset: 0x00355724
		// (set) Token: 0x0601A16E RID: 106862 RVA: 0x0035752E File Offset: 0x0035572E
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

		// Token: 0x170090AD RID: 37037
		// (get) Token: 0x0601A16F RID: 106863 RVA: 0x00357539 File Offset: 0x00355739
		// (set) Token: 0x0601A170 RID: 106864 RVA: 0x00357543 File Offset: 0x00355743
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

		// Token: 0x170090AE RID: 37038
		// (get) Token: 0x0601A171 RID: 106865 RVA: 0x0035754E File Offset: 0x0035574E
		// (set) Token: 0x0601A172 RID: 106866 RVA: 0x00357558 File Offset: 0x00355758
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

		// Token: 0x170090AF RID: 37039
		// (get) Token: 0x0601A173 RID: 106867 RVA: 0x00357563 File Offset: 0x00355763
		// (set) Token: 0x0601A174 RID: 106868 RVA: 0x0035756D File Offset: 0x0035576D
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

		// Token: 0x170090B0 RID: 37040
		// (get) Token: 0x0601A175 RID: 106869 RVA: 0x00357578 File Offset: 0x00355778
		// (set) Token: 0x0601A176 RID: 106870 RVA: 0x00357582 File Offset: 0x00355782
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

		// Token: 0x170090B1 RID: 37041
		// (get) Token: 0x0601A177 RID: 106871 RVA: 0x0035D59C File Offset: 0x0035B79C
		// (set) Token: 0x0601A178 RID: 106872 RVA: 0x0035D5A6 File Offset: 0x0035B7A6
		public TablePropertiesChange TablePropertiesChange
		{
			get
			{
				return base.GetElement<TablePropertiesChange>(15);
			}
			set
			{
				base.SetElement<TablePropertiesChange>(15, value);
			}
		}

		// Token: 0x0601A179 RID: 106873 RVA: 0x0035D5B1 File Offset: 0x0035B7B1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableProperties>(deep);
		}

		// Token: 0x0400ABB4 RID: 43956
		private const string tagName = "tblPr";

		// Token: 0x0400ABB5 RID: 43957
		private const byte tagNsId = 23;

		// Token: 0x0400ABB6 RID: 43958
		internal const int ElementTypeIdConst = 11789;

		// Token: 0x0400ABB7 RID: 43959
		private static readonly string[] eleTagNames = new string[]
		{
			"tblStyle", "tblpPr", "tblOverlap", "bidiVisual", "tblW", "jc", "tblCellSpacing", "tblInd", "tblBorders", "shd",
			"tblLayout", "tblCellMar", "tblLook", "tblCaption", "tblDescription", "tblPrChange"
		};

		// Token: 0x0400ABB8 RID: 43960
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23
		};
	}
}
