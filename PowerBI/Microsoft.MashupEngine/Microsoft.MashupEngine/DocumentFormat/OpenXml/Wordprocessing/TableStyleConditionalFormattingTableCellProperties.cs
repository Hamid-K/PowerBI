using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FA5 RID: 12197
	[ChildElementInfo(typeof(TableCellBorders))]
	[ChildElementInfo(typeof(TableCellMargin))]
	[ChildElementInfo(typeof(Shading))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NoWrap))]
	[ChildElementInfo(typeof(TableCellVerticalAlignment))]
	internal class TableStyleConditionalFormattingTableCellProperties : OpenXmlCompositeElement
	{
		// Token: 0x170092D8 RID: 37592
		// (get) Token: 0x0601A604 RID: 108036 RVA: 0x0030D556 File Offset: 0x0030B756
		public override string LocalName
		{
			get
			{
				return "tcPr";
			}
		}

		// Token: 0x170092D9 RID: 37593
		// (get) Token: 0x0601A605 RID: 108037 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170092DA RID: 37594
		// (get) Token: 0x0601A606 RID: 108038 RVA: 0x0036164F File Offset: 0x0035F84F
		internal override int ElementTypeId
		{
			get
			{
				return 11891;
			}
		}

		// Token: 0x0601A607 RID: 108039 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A608 RID: 108040 RVA: 0x00293ECF File Offset: 0x002920CF
		public TableStyleConditionalFormattingTableCellProperties()
		{
		}

		// Token: 0x0601A609 RID: 108041 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TableStyleConditionalFormattingTableCellProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A60A RID: 108042 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TableStyleConditionalFormattingTableCellProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A60B RID: 108043 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TableStyleConditionalFormattingTableCellProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A60C RID: 108044 RVA: 0x00361658 File Offset: 0x0035F858
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
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
			if (23 == namespaceId && "vAlign" == name)
			{
				return new TableCellVerticalAlignment();
			}
			return null;
		}

		// Token: 0x170092DB RID: 37595
		// (get) Token: 0x0601A60D RID: 108045 RVA: 0x003616DE File Offset: 0x0035F8DE
		internal override string[] ElementTagNames
		{
			get
			{
				return TableStyleConditionalFormattingTableCellProperties.eleTagNames;
			}
		}

		// Token: 0x170092DC RID: 37596
		// (get) Token: 0x0601A60E RID: 108046 RVA: 0x003616E5 File Offset: 0x0035F8E5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TableStyleConditionalFormattingTableCellProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170092DD RID: 37597
		// (get) Token: 0x0601A60F RID: 108047 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170092DE RID: 37598
		// (get) Token: 0x0601A610 RID: 108048 RVA: 0x003616EC File Offset: 0x0035F8EC
		// (set) Token: 0x0601A611 RID: 108049 RVA: 0x003616F5 File Offset: 0x0035F8F5
		public TableCellBorders TableCellBorders
		{
			get
			{
				return base.GetElement<TableCellBorders>(0);
			}
			set
			{
				base.SetElement<TableCellBorders>(0, value);
			}
		}

		// Token: 0x170092DF RID: 37599
		// (get) Token: 0x0601A612 RID: 108050 RVA: 0x003616FF File Offset: 0x0035F8FF
		// (set) Token: 0x0601A613 RID: 108051 RVA: 0x00361708 File Offset: 0x0035F908
		public Shading Shading
		{
			get
			{
				return base.GetElement<Shading>(1);
			}
			set
			{
				base.SetElement<Shading>(1, value);
			}
		}

		// Token: 0x170092E0 RID: 37600
		// (get) Token: 0x0601A614 RID: 108052 RVA: 0x00361712 File Offset: 0x0035F912
		// (set) Token: 0x0601A615 RID: 108053 RVA: 0x0036171B File Offset: 0x0035F91B
		public NoWrap NoWrap
		{
			get
			{
				return base.GetElement<NoWrap>(2);
			}
			set
			{
				base.SetElement<NoWrap>(2, value);
			}
		}

		// Token: 0x170092E1 RID: 37601
		// (get) Token: 0x0601A616 RID: 108054 RVA: 0x00361725 File Offset: 0x0035F925
		// (set) Token: 0x0601A617 RID: 108055 RVA: 0x0036172E File Offset: 0x0035F92E
		public TableCellMargin TableCellMargin
		{
			get
			{
				return base.GetElement<TableCellMargin>(3);
			}
			set
			{
				base.SetElement<TableCellMargin>(3, value);
			}
		}

		// Token: 0x170092E2 RID: 37602
		// (get) Token: 0x0601A618 RID: 108056 RVA: 0x00361738 File Offset: 0x0035F938
		// (set) Token: 0x0601A619 RID: 108057 RVA: 0x00361741 File Offset: 0x0035F941
		public TableCellVerticalAlignment TableCellVerticalAlignment
		{
			get
			{
				return base.GetElement<TableCellVerticalAlignment>(4);
			}
			set
			{
				base.SetElement<TableCellVerticalAlignment>(4, value);
			}
		}

		// Token: 0x0601A61A RID: 108058 RVA: 0x0036174B File Offset: 0x0035F94B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableStyleConditionalFormattingTableCellProperties>(deep);
		}

		// Token: 0x0400ACCA RID: 44234
		private const string tagName = "tcPr";

		// Token: 0x0400ACCB RID: 44235
		private const byte tagNsId = 23;

		// Token: 0x0400ACCC RID: 44236
		internal const int ElementTypeIdConst = 11891;

		// Token: 0x0400ACCD RID: 44237
		private static readonly string[] eleTagNames = new string[] { "tcBorders", "shd", "noWrap", "tcMar", "vAlign" };

		// Token: 0x0400ACCE RID: 44238
		private static readonly byte[] eleNamespaceIds = new byte[] { 23, 23, 23, 23, 23 };
	}
}
