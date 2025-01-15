using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F1F RID: 12063
	[ChildElementInfo(typeof(TableWidth))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(TableBorders))]
	[ChildElementInfo(typeof(TableLook))]
	[ChildElementInfo(typeof(TableCellSpacing))]
	[ChildElementInfo(typeof(TableIndentation))]
	[ChildElementInfo(typeof(TableJustification))]
	[ChildElementInfo(typeof(Shading))]
	[ChildElementInfo(typeof(TableLayout))]
	[ChildElementInfo(typeof(TableCellMarginDefault))]
	internal class PreviousTablePropertyExceptions : OpenXmlCompositeElement
	{
		// Token: 0x17008EA7 RID: 36519
		// (get) Token: 0x06019D00 RID: 105728 RVA: 0x00356BF9 File Offset: 0x00354DF9
		public override string LocalName
		{
			get
			{
				return "tblPrEx";
			}
		}

		// Token: 0x17008EA8 RID: 36520
		// (get) Token: 0x06019D01 RID: 105729 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008EA9 RID: 36521
		// (get) Token: 0x06019D02 RID: 105730 RVA: 0x00356C00 File Offset: 0x00354E00
		internal override int ElementTypeId
		{
			get
			{
				return 11704;
			}
		}

		// Token: 0x06019D03 RID: 105731 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019D04 RID: 105732 RVA: 0x00293ECF File Offset: 0x002920CF
		public PreviousTablePropertyExceptions()
		{
		}

		// Token: 0x06019D05 RID: 105733 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PreviousTablePropertyExceptions(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019D06 RID: 105734 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PreviousTablePropertyExceptions(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019D07 RID: 105735 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PreviousTablePropertyExceptions(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019D08 RID: 105736 RVA: 0x00356C08 File Offset: 0x00354E08
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
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
			return null;
		}

		// Token: 0x17008EAA RID: 36522
		// (get) Token: 0x06019D09 RID: 105737 RVA: 0x00356CEE File Offset: 0x00354EEE
		internal override string[] ElementTagNames
		{
			get
			{
				return PreviousTablePropertyExceptions.eleTagNames;
			}
		}

		// Token: 0x17008EAB RID: 36523
		// (get) Token: 0x06019D0A RID: 105738 RVA: 0x00356CF5 File Offset: 0x00354EF5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PreviousTablePropertyExceptions.eleNamespaceIds;
			}
		}

		// Token: 0x17008EAC RID: 36524
		// (get) Token: 0x06019D0B RID: 105739 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008EAD RID: 36525
		// (get) Token: 0x06019D0C RID: 105740 RVA: 0x00356CFC File Offset: 0x00354EFC
		// (set) Token: 0x06019D0D RID: 105741 RVA: 0x00356D05 File Offset: 0x00354F05
		public TableWidth TableWidth
		{
			get
			{
				return base.GetElement<TableWidth>(0);
			}
			set
			{
				base.SetElement<TableWidth>(0, value);
			}
		}

		// Token: 0x17008EAE RID: 36526
		// (get) Token: 0x06019D0E RID: 105742 RVA: 0x00356D0F File Offset: 0x00354F0F
		// (set) Token: 0x06019D0F RID: 105743 RVA: 0x00356D18 File Offset: 0x00354F18
		public TableJustification TableJustification
		{
			get
			{
				return base.GetElement<TableJustification>(1);
			}
			set
			{
				base.SetElement<TableJustification>(1, value);
			}
		}

		// Token: 0x17008EAF RID: 36527
		// (get) Token: 0x06019D10 RID: 105744 RVA: 0x00356D22 File Offset: 0x00354F22
		// (set) Token: 0x06019D11 RID: 105745 RVA: 0x00356D2B File Offset: 0x00354F2B
		public TableCellSpacing TableCellSpacing
		{
			get
			{
				return base.GetElement<TableCellSpacing>(2);
			}
			set
			{
				base.SetElement<TableCellSpacing>(2, value);
			}
		}

		// Token: 0x17008EB0 RID: 36528
		// (get) Token: 0x06019D12 RID: 105746 RVA: 0x00356D35 File Offset: 0x00354F35
		// (set) Token: 0x06019D13 RID: 105747 RVA: 0x00356D3E File Offset: 0x00354F3E
		public TableIndentation TableIndentation
		{
			get
			{
				return base.GetElement<TableIndentation>(3);
			}
			set
			{
				base.SetElement<TableIndentation>(3, value);
			}
		}

		// Token: 0x17008EB1 RID: 36529
		// (get) Token: 0x06019D14 RID: 105748 RVA: 0x00356D48 File Offset: 0x00354F48
		// (set) Token: 0x06019D15 RID: 105749 RVA: 0x00356D51 File Offset: 0x00354F51
		public TableBorders TableBorders
		{
			get
			{
				return base.GetElement<TableBorders>(4);
			}
			set
			{
				base.SetElement<TableBorders>(4, value);
			}
		}

		// Token: 0x17008EB2 RID: 36530
		// (get) Token: 0x06019D16 RID: 105750 RVA: 0x00356D5B File Offset: 0x00354F5B
		// (set) Token: 0x06019D17 RID: 105751 RVA: 0x00356D64 File Offset: 0x00354F64
		public Shading Shading
		{
			get
			{
				return base.GetElement<Shading>(5);
			}
			set
			{
				base.SetElement<Shading>(5, value);
			}
		}

		// Token: 0x17008EB3 RID: 36531
		// (get) Token: 0x06019D18 RID: 105752 RVA: 0x00356D6E File Offset: 0x00354F6E
		// (set) Token: 0x06019D19 RID: 105753 RVA: 0x00356D77 File Offset: 0x00354F77
		public TableLayout TableLayout
		{
			get
			{
				return base.GetElement<TableLayout>(6);
			}
			set
			{
				base.SetElement<TableLayout>(6, value);
			}
		}

		// Token: 0x17008EB4 RID: 36532
		// (get) Token: 0x06019D1A RID: 105754 RVA: 0x00356D81 File Offset: 0x00354F81
		// (set) Token: 0x06019D1B RID: 105755 RVA: 0x00356D8A File Offset: 0x00354F8A
		public TableCellMarginDefault TableCellMarginDefault
		{
			get
			{
				return base.GetElement<TableCellMarginDefault>(7);
			}
			set
			{
				base.SetElement<TableCellMarginDefault>(7, value);
			}
		}

		// Token: 0x17008EB5 RID: 36533
		// (get) Token: 0x06019D1C RID: 105756 RVA: 0x00356D94 File Offset: 0x00354F94
		// (set) Token: 0x06019D1D RID: 105757 RVA: 0x00356D9D File Offset: 0x00354F9D
		public TableLook TableLook
		{
			get
			{
				return base.GetElement<TableLook>(8);
			}
			set
			{
				base.SetElement<TableLook>(8, value);
			}
		}

		// Token: 0x06019D1E RID: 105758 RVA: 0x00356DA7 File Offset: 0x00354FA7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PreviousTablePropertyExceptions>(deep);
		}

		// Token: 0x0400AA90 RID: 43664
		private const string tagName = "tblPrEx";

		// Token: 0x0400AA91 RID: 43665
		private const byte tagNsId = 23;

		// Token: 0x0400AA92 RID: 43666
		internal const int ElementTypeIdConst = 11704;

		// Token: 0x0400AA93 RID: 43667
		private static readonly string[] eleTagNames = new string[] { "tblW", "jc", "tblCellSpacing", "tblInd", "tblBorders", "shd", "tblLayout", "tblCellMar", "tblLook" };

		// Token: 0x0400AA94 RID: 43668
		private static readonly byte[] eleNamespaceIds = new byte[] { 23, 23, 23, 23, 23, 23, 23, 23, 23 };
	}
}
