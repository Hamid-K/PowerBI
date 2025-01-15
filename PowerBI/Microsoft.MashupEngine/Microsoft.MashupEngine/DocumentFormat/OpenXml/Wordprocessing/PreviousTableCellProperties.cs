using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F20 RID: 12064
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NoWrap))]
	[ChildElementInfo(typeof(CellMerge))]
	[ChildElementInfo(typeof(TableCellWidth))]
	[ChildElementInfo(typeof(GridSpan))]
	[ChildElementInfo(typeof(HorizontalMerge))]
	[ChildElementInfo(typeof(VerticalMerge))]
	[ChildElementInfo(typeof(TableCellBorders))]
	[ChildElementInfo(typeof(Shading))]
	[ChildElementInfo(typeof(ConditionalFormatStyle))]
	[ChildElementInfo(typeof(TableCellMargin))]
	[ChildElementInfo(typeof(TextDirection))]
	[ChildElementInfo(typeof(TableCellFitText))]
	[ChildElementInfo(typeof(TableCellVerticalAlignment))]
	[ChildElementInfo(typeof(HideMark))]
	[ChildElementInfo(typeof(CellInsertion))]
	[ChildElementInfo(typeof(CellDeletion))]
	internal class PreviousTableCellProperties : OpenXmlCompositeElement
	{
		// Token: 0x17008EB6 RID: 36534
		// (get) Token: 0x06019D20 RID: 105760 RVA: 0x0030D556 File Offset: 0x0030B756
		public override string LocalName
		{
			get
			{
				return "tcPr";
			}
		}

		// Token: 0x17008EB7 RID: 36535
		// (get) Token: 0x06019D21 RID: 105761 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008EB8 RID: 36536
		// (get) Token: 0x06019D22 RID: 105762 RVA: 0x00356E2A File Offset: 0x0035502A
		internal override int ElementTypeId
		{
			get
			{
				return 11705;
			}
		}

		// Token: 0x06019D23 RID: 105763 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019D24 RID: 105764 RVA: 0x00293ECF File Offset: 0x002920CF
		public PreviousTableCellProperties()
		{
		}

		// Token: 0x06019D25 RID: 105765 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PreviousTableCellProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019D26 RID: 105766 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PreviousTableCellProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019D27 RID: 105767 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PreviousTableCellProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019D28 RID: 105768 RVA: 0x00356E34 File Offset: 0x00355034
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "cnfStyle" == name)
			{
				return new ConditionalFormatStyle();
			}
			if (23 == namespaceId && "tcW" == name)
			{
				return new TableCellWidth();
			}
			if (23 == namespaceId && "gridSpan" == name)
			{
				return new GridSpan();
			}
			if (23 == namespaceId && "hMerge" == name)
			{
				return new HorizontalMerge();
			}
			if (23 == namespaceId && "vMerge" == name)
			{
				return new VerticalMerge();
			}
			if (23 == namespaceId && "tcBorders" == name)
			{
				return new TableCellBorders();
			}
			if (23 == namespaceId && "shd" == name)
			{
				return new Shading();
			}
			if (23 == namespaceId && "noWrap" == name)
			{
				return new NoWrap();
			}
			if (23 == namespaceId && "tcMar" == name)
			{
				return new TableCellMargin();
			}
			if (23 == namespaceId && "textDirection" == name)
			{
				return new TextDirection();
			}
			if (23 == namespaceId && "tcFitText" == name)
			{
				return new TableCellFitText();
			}
			if (23 == namespaceId && "vAlign" == name)
			{
				return new TableCellVerticalAlignment();
			}
			if (23 == namespaceId && "hideMark" == name)
			{
				return new HideMark();
			}
			if (23 == namespaceId && "cellIns" == name)
			{
				return new CellInsertion();
			}
			if (23 == namespaceId && "cellDel" == name)
			{
				return new CellDeletion();
			}
			if (23 == namespaceId && "cellMerge" == name)
			{
				return new CellMerge();
			}
			return null;
		}

		// Token: 0x17008EB9 RID: 36537
		// (get) Token: 0x06019D29 RID: 105769 RVA: 0x00356FC2 File Offset: 0x003551C2
		internal override string[] ElementTagNames
		{
			get
			{
				return PreviousTableCellProperties.eleTagNames;
			}
		}

		// Token: 0x17008EBA RID: 36538
		// (get) Token: 0x06019D2A RID: 105770 RVA: 0x00356FC9 File Offset: 0x003551C9
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PreviousTableCellProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17008EBB RID: 36539
		// (get) Token: 0x06019D2B RID: 105771 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008EBC RID: 36540
		// (get) Token: 0x06019D2C RID: 105772 RVA: 0x00356FD0 File Offset: 0x003551D0
		// (set) Token: 0x06019D2D RID: 105773 RVA: 0x00356FD9 File Offset: 0x003551D9
		public ConditionalFormatStyle ConditionalFormatStyle
		{
			get
			{
				return base.GetElement<ConditionalFormatStyle>(0);
			}
			set
			{
				base.SetElement<ConditionalFormatStyle>(0, value);
			}
		}

		// Token: 0x17008EBD RID: 36541
		// (get) Token: 0x06019D2E RID: 105774 RVA: 0x00356FE3 File Offset: 0x003551E3
		// (set) Token: 0x06019D2F RID: 105775 RVA: 0x00356FEC File Offset: 0x003551EC
		public TableCellWidth TableCellWidth
		{
			get
			{
				return base.GetElement<TableCellWidth>(1);
			}
			set
			{
				base.SetElement<TableCellWidth>(1, value);
			}
		}

		// Token: 0x17008EBE RID: 36542
		// (get) Token: 0x06019D30 RID: 105776 RVA: 0x00356FF6 File Offset: 0x003551F6
		// (set) Token: 0x06019D31 RID: 105777 RVA: 0x00356FFF File Offset: 0x003551FF
		public GridSpan GridSpan
		{
			get
			{
				return base.GetElement<GridSpan>(2);
			}
			set
			{
				base.SetElement<GridSpan>(2, value);
			}
		}

		// Token: 0x17008EBF RID: 36543
		// (get) Token: 0x06019D32 RID: 105778 RVA: 0x00357009 File Offset: 0x00355209
		// (set) Token: 0x06019D33 RID: 105779 RVA: 0x00357012 File Offset: 0x00355212
		public HorizontalMerge HorizontalMerge
		{
			get
			{
				return base.GetElement<HorizontalMerge>(3);
			}
			set
			{
				base.SetElement<HorizontalMerge>(3, value);
			}
		}

		// Token: 0x17008EC0 RID: 36544
		// (get) Token: 0x06019D34 RID: 105780 RVA: 0x0035701C File Offset: 0x0035521C
		// (set) Token: 0x06019D35 RID: 105781 RVA: 0x00357025 File Offset: 0x00355225
		public VerticalMerge VerticalMerge
		{
			get
			{
				return base.GetElement<VerticalMerge>(4);
			}
			set
			{
				base.SetElement<VerticalMerge>(4, value);
			}
		}

		// Token: 0x17008EC1 RID: 36545
		// (get) Token: 0x06019D36 RID: 105782 RVA: 0x0035702F File Offset: 0x0035522F
		// (set) Token: 0x06019D37 RID: 105783 RVA: 0x00357038 File Offset: 0x00355238
		public TableCellBorders TableCellBorders
		{
			get
			{
				return base.GetElement<TableCellBorders>(5);
			}
			set
			{
				base.SetElement<TableCellBorders>(5, value);
			}
		}

		// Token: 0x17008EC2 RID: 36546
		// (get) Token: 0x06019D38 RID: 105784 RVA: 0x00357042 File Offset: 0x00355242
		// (set) Token: 0x06019D39 RID: 105785 RVA: 0x0035704B File Offset: 0x0035524B
		public Shading Shading
		{
			get
			{
				return base.GetElement<Shading>(6);
			}
			set
			{
				base.SetElement<Shading>(6, value);
			}
		}

		// Token: 0x17008EC3 RID: 36547
		// (get) Token: 0x06019D3A RID: 105786 RVA: 0x00357055 File Offset: 0x00355255
		// (set) Token: 0x06019D3B RID: 105787 RVA: 0x0035705E File Offset: 0x0035525E
		public NoWrap NoWrap
		{
			get
			{
				return base.GetElement<NoWrap>(7);
			}
			set
			{
				base.SetElement<NoWrap>(7, value);
			}
		}

		// Token: 0x17008EC4 RID: 36548
		// (get) Token: 0x06019D3C RID: 105788 RVA: 0x00357068 File Offset: 0x00355268
		// (set) Token: 0x06019D3D RID: 105789 RVA: 0x00357071 File Offset: 0x00355271
		public TableCellMargin TableCellMargin
		{
			get
			{
				return base.GetElement<TableCellMargin>(8);
			}
			set
			{
				base.SetElement<TableCellMargin>(8, value);
			}
		}

		// Token: 0x17008EC5 RID: 36549
		// (get) Token: 0x06019D3E RID: 105790 RVA: 0x0035707B File Offset: 0x0035527B
		// (set) Token: 0x06019D3F RID: 105791 RVA: 0x00357085 File Offset: 0x00355285
		public TextDirection TextDirection
		{
			get
			{
				return base.GetElement<TextDirection>(9);
			}
			set
			{
				base.SetElement<TextDirection>(9, value);
			}
		}

		// Token: 0x17008EC6 RID: 36550
		// (get) Token: 0x06019D40 RID: 105792 RVA: 0x00357090 File Offset: 0x00355290
		// (set) Token: 0x06019D41 RID: 105793 RVA: 0x0035709A File Offset: 0x0035529A
		public TableCellFitText TableCellFitText
		{
			get
			{
				return base.GetElement<TableCellFitText>(10);
			}
			set
			{
				base.SetElement<TableCellFitText>(10, value);
			}
		}

		// Token: 0x17008EC7 RID: 36551
		// (get) Token: 0x06019D42 RID: 105794 RVA: 0x003570A5 File Offset: 0x003552A5
		// (set) Token: 0x06019D43 RID: 105795 RVA: 0x003570AF File Offset: 0x003552AF
		public TableCellVerticalAlignment TableCellVerticalAlignment
		{
			get
			{
				return base.GetElement<TableCellVerticalAlignment>(11);
			}
			set
			{
				base.SetElement<TableCellVerticalAlignment>(11, value);
			}
		}

		// Token: 0x17008EC8 RID: 36552
		// (get) Token: 0x06019D44 RID: 105796 RVA: 0x003570BA File Offset: 0x003552BA
		// (set) Token: 0x06019D45 RID: 105797 RVA: 0x003570C4 File Offset: 0x003552C4
		public HideMark HideMark
		{
			get
			{
				return base.GetElement<HideMark>(12);
			}
			set
			{
				base.SetElement<HideMark>(12, value);
			}
		}

		// Token: 0x06019D46 RID: 105798 RVA: 0x003570CF File Offset: 0x003552CF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PreviousTableCellProperties>(deep);
		}

		// Token: 0x0400AA95 RID: 43669
		private const string tagName = "tcPr";

		// Token: 0x0400AA96 RID: 43670
		private const byte tagNsId = 23;

		// Token: 0x0400AA97 RID: 43671
		internal const int ElementTypeIdConst = 11705;

		// Token: 0x0400AA98 RID: 43672
		private static readonly string[] eleTagNames = new string[]
		{
			"cnfStyle", "tcW", "gridSpan", "hMerge", "vMerge", "tcBorders", "shd", "noWrap", "tcMar", "textDirection",
			"tcFitText", "vAlign", "hideMark", "cellIns", "cellDel", "cellMerge"
		};

		// Token: 0x0400AA99 RID: 43673
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23
		};
	}
}
