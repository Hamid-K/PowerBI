using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FA3 RID: 12195
	[ChildElementInfo(typeof(TableJustification))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(TableCellSpacing))]
	[ChildElementInfo(typeof(TableIndentation))]
	[ChildElementInfo(typeof(TableBorders))]
	[ChildElementInfo(typeof(Shading))]
	[ChildElementInfo(typeof(TableCellMarginDefault))]
	internal class TableStyleConditionalFormattingTableProperties : OpenXmlCompositeElement
	{
		// Token: 0x170092C9 RID: 37577
		// (get) Token: 0x0601A5E0 RID: 108000 RVA: 0x0030DFE2 File Offset: 0x0030C1E2
		public override string LocalName
		{
			get
			{
				return "tblPr";
			}
		}

		// Token: 0x170092CA RID: 37578
		// (get) Token: 0x0601A5E1 RID: 108001 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170092CB RID: 37579
		// (get) Token: 0x0601A5E2 RID: 108002 RVA: 0x00361428 File Offset: 0x0035F628
		internal override int ElementTypeId
		{
			get
			{
				return 11889;
			}
		}

		// Token: 0x0601A5E3 RID: 108003 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A5E4 RID: 108004 RVA: 0x00293ECF File Offset: 0x002920CF
		public TableStyleConditionalFormattingTableProperties()
		{
		}

		// Token: 0x0601A5E5 RID: 108005 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TableStyleConditionalFormattingTableProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A5E6 RID: 108006 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TableStyleConditionalFormattingTableProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A5E7 RID: 108007 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TableStyleConditionalFormattingTableProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A5E8 RID: 108008 RVA: 0x00361430 File Offset: 0x0035F630
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
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
			if (23 == namespaceId && "tblCellMar" == name)
			{
				return new TableCellMarginDefault();
			}
			return null;
		}

		// Token: 0x170092CC RID: 37580
		// (get) Token: 0x0601A5E9 RID: 108009 RVA: 0x003614CE File Offset: 0x0035F6CE
		internal override string[] ElementTagNames
		{
			get
			{
				return TableStyleConditionalFormattingTableProperties.eleTagNames;
			}
		}

		// Token: 0x170092CD RID: 37581
		// (get) Token: 0x0601A5EA RID: 108010 RVA: 0x003614D5 File Offset: 0x0035F6D5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TableStyleConditionalFormattingTableProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170092CE RID: 37582
		// (get) Token: 0x0601A5EB RID: 108011 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170092CF RID: 37583
		// (get) Token: 0x0601A5EC RID: 108012 RVA: 0x003614DC File Offset: 0x0035F6DC
		// (set) Token: 0x0601A5ED RID: 108013 RVA: 0x003614E5 File Offset: 0x0035F6E5
		public TableJustification TableJustification
		{
			get
			{
				return base.GetElement<TableJustification>(0);
			}
			set
			{
				base.SetElement<TableJustification>(0, value);
			}
		}

		// Token: 0x170092D0 RID: 37584
		// (get) Token: 0x0601A5EE RID: 108014 RVA: 0x003614EF File Offset: 0x0035F6EF
		// (set) Token: 0x0601A5EF RID: 108015 RVA: 0x003614F8 File Offset: 0x0035F6F8
		public TableCellSpacing TableCellSpacing
		{
			get
			{
				return base.GetElement<TableCellSpacing>(1);
			}
			set
			{
				base.SetElement<TableCellSpacing>(1, value);
			}
		}

		// Token: 0x170092D1 RID: 37585
		// (get) Token: 0x0601A5F0 RID: 108016 RVA: 0x00361502 File Offset: 0x0035F702
		// (set) Token: 0x0601A5F1 RID: 108017 RVA: 0x0036150B File Offset: 0x0035F70B
		public TableIndentation TableIndentation
		{
			get
			{
				return base.GetElement<TableIndentation>(2);
			}
			set
			{
				base.SetElement<TableIndentation>(2, value);
			}
		}

		// Token: 0x170092D2 RID: 37586
		// (get) Token: 0x0601A5F2 RID: 108018 RVA: 0x00361515 File Offset: 0x0035F715
		// (set) Token: 0x0601A5F3 RID: 108019 RVA: 0x0036151E File Offset: 0x0035F71E
		public TableBorders TableBorders
		{
			get
			{
				return base.GetElement<TableBorders>(3);
			}
			set
			{
				base.SetElement<TableBorders>(3, value);
			}
		}

		// Token: 0x170092D3 RID: 37587
		// (get) Token: 0x0601A5F4 RID: 108020 RVA: 0x00361528 File Offset: 0x0035F728
		// (set) Token: 0x0601A5F5 RID: 108021 RVA: 0x00361531 File Offset: 0x0035F731
		public Shading Shading
		{
			get
			{
				return base.GetElement<Shading>(4);
			}
			set
			{
				base.SetElement<Shading>(4, value);
			}
		}

		// Token: 0x170092D4 RID: 37588
		// (get) Token: 0x0601A5F6 RID: 108022 RVA: 0x0036153B File Offset: 0x0035F73B
		// (set) Token: 0x0601A5F7 RID: 108023 RVA: 0x00361544 File Offset: 0x0035F744
		public TableCellMarginDefault TableCellMarginDefault
		{
			get
			{
				return base.GetElement<TableCellMarginDefault>(5);
			}
			set
			{
				base.SetElement<TableCellMarginDefault>(5, value);
			}
		}

		// Token: 0x0601A5F8 RID: 108024 RVA: 0x0036154E File Offset: 0x0035F74E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableStyleConditionalFormattingTableProperties>(deep);
		}

		// Token: 0x0400ACC2 RID: 44226
		private const string tagName = "tblPr";

		// Token: 0x0400ACC3 RID: 44227
		private const byte tagNsId = 23;

		// Token: 0x0400ACC4 RID: 44228
		internal const int ElementTypeIdConst = 11889;

		// Token: 0x0400ACC5 RID: 44229
		private static readonly string[] eleTagNames = new string[] { "jc", "tblCellSpacing", "tblInd", "tblBorders", "shd", "tblCellMar" };

		// Token: 0x0400ACC6 RID: 44230
		private static readonly byte[] eleNamespaceIds = new byte[] { 23, 23, 23, 23, 23, 23 };
	}
}
