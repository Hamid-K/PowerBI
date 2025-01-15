using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FA4 RID: 12196
	[ChildElementInfo(typeof(TableHeader))]
	[ChildElementInfo(typeof(Hidden))]
	[ChildElementInfo(typeof(CantSplit))]
	[ChildElementInfo(typeof(TableCellSpacing))]
	[ChildElementInfo(typeof(TableJustification))]
	[GeneratedCode("DomGen", "2.0")]
	internal class TableStyleConditionalFormattingTableRowProperties : OpenXmlCompositeElement
	{
		// Token: 0x170092D5 RID: 37589
		// (get) Token: 0x0601A5FA RID: 108026 RVA: 0x00357191 File Offset: 0x00355391
		public override string LocalName
		{
			get
			{
				return "trPr";
			}
		}

		// Token: 0x170092D6 RID: 37590
		// (get) Token: 0x0601A5FB RID: 108027 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170092D7 RID: 37591
		// (get) Token: 0x0601A5FC RID: 108028 RVA: 0x003615B8 File Offset: 0x0035F7B8
		internal override int ElementTypeId
		{
			get
			{
				return 11890;
			}
		}

		// Token: 0x0601A5FD RID: 108029 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A5FE RID: 108030 RVA: 0x00293ECF File Offset: 0x002920CF
		public TableStyleConditionalFormattingTableRowProperties()
		{
		}

		// Token: 0x0601A5FF RID: 108031 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TableStyleConditionalFormattingTableRowProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A600 RID: 108032 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TableStyleConditionalFormattingTableRowProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A601 RID: 108033 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TableStyleConditionalFormattingTableRowProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A602 RID: 108034 RVA: 0x003615C0 File Offset: 0x0035F7C0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "hidden" == name)
			{
				return new Hidden();
			}
			if (23 == namespaceId && "cantSplit" == name)
			{
				return new CantSplit();
			}
			if (23 == namespaceId && "tblHeader" == name)
			{
				return new TableHeader();
			}
			if (23 == namespaceId && "tblCellSpacing" == name)
			{
				return new TableCellSpacing();
			}
			if (23 == namespaceId && "jc" == name)
			{
				return new TableJustification();
			}
			return null;
		}

		// Token: 0x0601A603 RID: 108035 RVA: 0x00361646 File Offset: 0x0035F846
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableStyleConditionalFormattingTableRowProperties>(deep);
		}

		// Token: 0x0400ACC7 RID: 44231
		private const string tagName = "trPr";

		// Token: 0x0400ACC8 RID: 44232
		private const byte tagNsId = 23;

		// Token: 0x0400ACC9 RID: 44233
		internal const int ElementTypeIdConst = 11890;
	}
}
