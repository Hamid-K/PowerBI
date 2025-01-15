using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02003000 RID: 12288
	[ChildElementInfo(typeof(TableWidth))]
	[ChildElementInfo(typeof(TableBorders))]
	[ChildElementInfo(typeof(TablePropertyExceptionsChange))]
	[ChildElementInfo(typeof(TableJustification))]
	[ChildElementInfo(typeof(TableCellSpacing))]
	[ChildElementInfo(typeof(TableIndentation))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Shading))]
	[ChildElementInfo(typeof(TableLayout))]
	[ChildElementInfo(typeof(TableCellMarginDefault))]
	[ChildElementInfo(typeof(TableLook))]
	internal class TablePropertyExceptions : OpenXmlCompositeElement
	{
		// Token: 0x170095E1 RID: 38369
		// (get) Token: 0x0601AC7D RID: 109693 RVA: 0x00356BF9 File Offset: 0x00354DF9
		public override string LocalName
		{
			get
			{
				return "tblPrEx";
			}
		}

		// Token: 0x170095E2 RID: 38370
		// (get) Token: 0x0601AC7E RID: 109694 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170095E3 RID: 38371
		// (get) Token: 0x0601AC7F RID: 109695 RVA: 0x0036778D File Offset: 0x0036598D
		internal override int ElementTypeId
		{
			get
			{
				return 12131;
			}
		}

		// Token: 0x0601AC80 RID: 109696 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601AC81 RID: 109697 RVA: 0x00293ECF File Offset: 0x002920CF
		public TablePropertyExceptions()
		{
		}

		// Token: 0x0601AC82 RID: 109698 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TablePropertyExceptions(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AC83 RID: 109699 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TablePropertyExceptions(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AC84 RID: 109700 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TablePropertyExceptions(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601AC85 RID: 109701 RVA: 0x00367794 File Offset: 0x00365994
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
			if (23 == namespaceId && "tblPrExChange" == name)
			{
				return new TablePropertyExceptionsChange();
			}
			return null;
		}

		// Token: 0x170095E4 RID: 38372
		// (get) Token: 0x0601AC86 RID: 109702 RVA: 0x00367892 File Offset: 0x00365A92
		internal override string[] ElementTagNames
		{
			get
			{
				return TablePropertyExceptions.eleTagNames;
			}
		}

		// Token: 0x170095E5 RID: 38373
		// (get) Token: 0x0601AC87 RID: 109703 RVA: 0x00367899 File Offset: 0x00365A99
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TablePropertyExceptions.eleNamespaceIds;
			}
		}

		// Token: 0x170095E6 RID: 38374
		// (get) Token: 0x0601AC88 RID: 109704 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170095E7 RID: 38375
		// (get) Token: 0x0601AC89 RID: 109705 RVA: 0x00356CFC File Offset: 0x00354EFC
		// (set) Token: 0x0601AC8A RID: 109706 RVA: 0x00356D05 File Offset: 0x00354F05
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

		// Token: 0x170095E8 RID: 38376
		// (get) Token: 0x0601AC8B RID: 109707 RVA: 0x00356D0F File Offset: 0x00354F0F
		// (set) Token: 0x0601AC8C RID: 109708 RVA: 0x00356D18 File Offset: 0x00354F18
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

		// Token: 0x170095E9 RID: 38377
		// (get) Token: 0x0601AC8D RID: 109709 RVA: 0x00356D22 File Offset: 0x00354F22
		// (set) Token: 0x0601AC8E RID: 109710 RVA: 0x00356D2B File Offset: 0x00354F2B
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

		// Token: 0x170095EA RID: 38378
		// (get) Token: 0x0601AC8F RID: 109711 RVA: 0x00356D35 File Offset: 0x00354F35
		// (set) Token: 0x0601AC90 RID: 109712 RVA: 0x00356D3E File Offset: 0x00354F3E
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

		// Token: 0x170095EB RID: 38379
		// (get) Token: 0x0601AC91 RID: 109713 RVA: 0x00356D48 File Offset: 0x00354F48
		// (set) Token: 0x0601AC92 RID: 109714 RVA: 0x00356D51 File Offset: 0x00354F51
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

		// Token: 0x170095EC RID: 38380
		// (get) Token: 0x0601AC93 RID: 109715 RVA: 0x00356D5B File Offset: 0x00354F5B
		// (set) Token: 0x0601AC94 RID: 109716 RVA: 0x00356D64 File Offset: 0x00354F64
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

		// Token: 0x170095ED RID: 38381
		// (get) Token: 0x0601AC95 RID: 109717 RVA: 0x00356D6E File Offset: 0x00354F6E
		// (set) Token: 0x0601AC96 RID: 109718 RVA: 0x00356D77 File Offset: 0x00354F77
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

		// Token: 0x170095EE RID: 38382
		// (get) Token: 0x0601AC97 RID: 109719 RVA: 0x00356D81 File Offset: 0x00354F81
		// (set) Token: 0x0601AC98 RID: 109720 RVA: 0x00356D8A File Offset: 0x00354F8A
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

		// Token: 0x170095EF RID: 38383
		// (get) Token: 0x0601AC99 RID: 109721 RVA: 0x00356D94 File Offset: 0x00354F94
		// (set) Token: 0x0601AC9A RID: 109722 RVA: 0x00356D9D File Offset: 0x00354F9D
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

		// Token: 0x170095F0 RID: 38384
		// (get) Token: 0x0601AC9B RID: 109723 RVA: 0x003678A0 File Offset: 0x00365AA0
		// (set) Token: 0x0601AC9C RID: 109724 RVA: 0x003678AA File Offset: 0x00365AAA
		public TablePropertyExceptionsChange TablePropertyExceptionsChange
		{
			get
			{
				return base.GetElement<TablePropertyExceptionsChange>(9);
			}
			set
			{
				base.SetElement<TablePropertyExceptionsChange>(9, value);
			}
		}

		// Token: 0x0601AC9D RID: 109725 RVA: 0x003678B5 File Offset: 0x00365AB5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TablePropertyExceptions>(deep);
		}

		// Token: 0x0400AE4D RID: 44621
		private const string tagName = "tblPrEx";

		// Token: 0x0400AE4E RID: 44622
		private const byte tagNsId = 23;

		// Token: 0x0400AE4F RID: 44623
		internal const int ElementTypeIdConst = 12131;

		// Token: 0x0400AE50 RID: 44624
		private static readonly string[] eleTagNames = new string[] { "tblW", "jc", "tblCellSpacing", "tblInd", "tblBorders", "shd", "tblLayout", "tblCellMar", "tblLook", "tblPrExChange" };

		// Token: 0x0400AE51 RID: 44625
		private static readonly byte[] eleNamespaceIds = new byte[] { 23, 23, 23, 23, 23, 23, 23, 23, 23, 23 };
	}
}
