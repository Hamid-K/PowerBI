using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F60 RID: 12128
	[ChildElementInfo(typeof(TableCellWidth))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(TableCellMargin))]
	[ChildElementInfo(typeof(TableCellPropertiesChange))]
	[ChildElementInfo(typeof(GridSpan))]
	[ChildElementInfo(typeof(HorizontalMerge))]
	[ChildElementInfo(typeof(VerticalMerge))]
	[ChildElementInfo(typeof(TableCellBorders))]
	[ChildElementInfo(typeof(Shading))]
	[ChildElementInfo(typeof(NoWrap))]
	[ChildElementInfo(typeof(ConditionalFormatStyle))]
	[ChildElementInfo(typeof(TextDirection))]
	[ChildElementInfo(typeof(TableCellFitText))]
	[ChildElementInfo(typeof(TableCellVerticalAlignment))]
	[ChildElementInfo(typeof(HideMark))]
	[ChildElementInfo(typeof(CellInsertion))]
	[ChildElementInfo(typeof(CellDeletion))]
	[ChildElementInfo(typeof(CellMerge))]
	internal class TableCellProperties : OpenXmlCompositeElement
	{
		// Token: 0x17009071 RID: 36977
		// (get) Token: 0x0601A0F3 RID: 106739 RVA: 0x0030D556 File Offset: 0x0030B756
		public override string LocalName
		{
			get
			{
				return "tcPr";
			}
		}

		// Token: 0x17009072 RID: 36978
		// (get) Token: 0x0601A0F4 RID: 106740 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009073 RID: 36979
		// (get) Token: 0x0601A0F5 RID: 106741 RVA: 0x0035CF0B File Offset: 0x0035B10B
		internal override int ElementTypeId
		{
			get
			{
				return 11784;
			}
		}

		// Token: 0x0601A0F6 RID: 106742 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A0F7 RID: 106743 RVA: 0x00293ECF File Offset: 0x002920CF
		public TableCellProperties()
		{
		}

		// Token: 0x0601A0F8 RID: 106744 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TableCellProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A0F9 RID: 106745 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TableCellProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A0FA RID: 106746 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TableCellProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A0FB RID: 106747 RVA: 0x0035CF14 File Offset: 0x0035B114
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
			if (23 == namespaceId && "tcPrChange" == name)
			{
				return new TableCellPropertiesChange();
			}
			return null;
		}

		// Token: 0x17009074 RID: 36980
		// (get) Token: 0x0601A0FC RID: 106748 RVA: 0x0035D0BA File Offset: 0x0035B2BA
		internal override string[] ElementTagNames
		{
			get
			{
				return TableCellProperties.eleTagNames;
			}
		}

		// Token: 0x17009075 RID: 36981
		// (get) Token: 0x0601A0FD RID: 106749 RVA: 0x0035D0C1 File Offset: 0x0035B2C1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TableCellProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17009076 RID: 36982
		// (get) Token: 0x0601A0FE RID: 106750 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17009077 RID: 36983
		// (get) Token: 0x0601A0FF RID: 106751 RVA: 0x00356FD0 File Offset: 0x003551D0
		// (set) Token: 0x0601A100 RID: 106752 RVA: 0x00356FD9 File Offset: 0x003551D9
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

		// Token: 0x17009078 RID: 36984
		// (get) Token: 0x0601A101 RID: 106753 RVA: 0x00356FE3 File Offset: 0x003551E3
		// (set) Token: 0x0601A102 RID: 106754 RVA: 0x00356FEC File Offset: 0x003551EC
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

		// Token: 0x17009079 RID: 36985
		// (get) Token: 0x0601A103 RID: 106755 RVA: 0x00356FF6 File Offset: 0x003551F6
		// (set) Token: 0x0601A104 RID: 106756 RVA: 0x00356FFF File Offset: 0x003551FF
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

		// Token: 0x1700907A RID: 36986
		// (get) Token: 0x0601A105 RID: 106757 RVA: 0x00357009 File Offset: 0x00355209
		// (set) Token: 0x0601A106 RID: 106758 RVA: 0x00357012 File Offset: 0x00355212
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

		// Token: 0x1700907B RID: 36987
		// (get) Token: 0x0601A107 RID: 106759 RVA: 0x0035701C File Offset: 0x0035521C
		// (set) Token: 0x0601A108 RID: 106760 RVA: 0x00357025 File Offset: 0x00355225
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

		// Token: 0x1700907C RID: 36988
		// (get) Token: 0x0601A109 RID: 106761 RVA: 0x0035702F File Offset: 0x0035522F
		// (set) Token: 0x0601A10A RID: 106762 RVA: 0x00357038 File Offset: 0x00355238
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

		// Token: 0x1700907D RID: 36989
		// (get) Token: 0x0601A10B RID: 106763 RVA: 0x00357042 File Offset: 0x00355242
		// (set) Token: 0x0601A10C RID: 106764 RVA: 0x0035704B File Offset: 0x0035524B
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

		// Token: 0x1700907E RID: 36990
		// (get) Token: 0x0601A10D RID: 106765 RVA: 0x00357055 File Offset: 0x00355255
		// (set) Token: 0x0601A10E RID: 106766 RVA: 0x0035705E File Offset: 0x0035525E
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

		// Token: 0x1700907F RID: 36991
		// (get) Token: 0x0601A10F RID: 106767 RVA: 0x00357068 File Offset: 0x00355268
		// (set) Token: 0x0601A110 RID: 106768 RVA: 0x00357071 File Offset: 0x00355271
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

		// Token: 0x17009080 RID: 36992
		// (get) Token: 0x0601A111 RID: 106769 RVA: 0x0035707B File Offset: 0x0035527B
		// (set) Token: 0x0601A112 RID: 106770 RVA: 0x00357085 File Offset: 0x00355285
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

		// Token: 0x17009081 RID: 36993
		// (get) Token: 0x0601A113 RID: 106771 RVA: 0x00357090 File Offset: 0x00355290
		// (set) Token: 0x0601A114 RID: 106772 RVA: 0x0035709A File Offset: 0x0035529A
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

		// Token: 0x17009082 RID: 36994
		// (get) Token: 0x0601A115 RID: 106773 RVA: 0x003570A5 File Offset: 0x003552A5
		// (set) Token: 0x0601A116 RID: 106774 RVA: 0x003570AF File Offset: 0x003552AF
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

		// Token: 0x17009083 RID: 36995
		// (get) Token: 0x0601A117 RID: 106775 RVA: 0x003570BA File Offset: 0x003552BA
		// (set) Token: 0x0601A118 RID: 106776 RVA: 0x003570C4 File Offset: 0x003552C4
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

		// Token: 0x0601A119 RID: 106777 RVA: 0x0035D0C8 File Offset: 0x0035B2C8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableCellProperties>(deep);
		}

		// Token: 0x0400ABA1 RID: 43937
		private const string tagName = "tcPr";

		// Token: 0x0400ABA2 RID: 43938
		private const byte tagNsId = 23;

		// Token: 0x0400ABA3 RID: 43939
		internal const int ElementTypeIdConst = 11784;

		// Token: 0x0400ABA4 RID: 43940
		private static readonly string[] eleTagNames = new string[]
		{
			"cnfStyle", "tcW", "gridSpan", "hMerge", "vMerge", "tcBorders", "shd", "noWrap", "tcMar", "textDirection",
			"tcFitText", "vAlign", "hideMark", "cellIns", "cellDel", "cellMerge", "tcPrChange"
		};

		// Token: 0x0400ABA5 RID: 43941
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23
		};
	}
}
