using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Word;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02003001 RID: 12289
	[ChildElementInfo(typeof(Deleted))]
	[ChildElementInfo(typeof(TableRowHeight))]
	[ChildElementInfo(typeof(Hidden))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ConditionalFormatStyle))]
	[ChildElementInfo(typeof(DivId))]
	[ChildElementInfo(typeof(GridBefore))]
	[ChildElementInfo(typeof(GridAfter))]
	[ChildElementInfo(typeof(WidthBeforeTableRow))]
	[ChildElementInfo(typeof(WidthAfterTableRow))]
	[ChildElementInfo(typeof(CantSplit))]
	[ChildElementInfo(typeof(TableHeader))]
	[ChildElementInfo(typeof(TableCellSpacing))]
	[ChildElementInfo(typeof(TableJustification))]
	[ChildElementInfo(typeof(Inserted))]
	[ChildElementInfo(typeof(TableRowPropertiesChange))]
	[ChildElementInfo(typeof(ConflictInsertion), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ConflictDeletion), FileFormatVersions.Office2010)]
	internal class TableRowProperties : OpenXmlCompositeElement
	{
		// Token: 0x170095F1 RID: 38385
		// (get) Token: 0x0601AC9F RID: 109727 RVA: 0x00357191 File Offset: 0x00355391
		public override string LocalName
		{
			get
			{
				return "trPr";
			}
		}

		// Token: 0x170095F2 RID: 38386
		// (get) Token: 0x0601ACA0 RID: 109728 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170095F3 RID: 38387
		// (get) Token: 0x0601ACA1 RID: 109729 RVA: 0x00367943 File Offset: 0x00365B43
		internal override int ElementTypeId
		{
			get
			{
				return 12132;
			}
		}

		// Token: 0x0601ACA2 RID: 109730 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601ACA3 RID: 109731 RVA: 0x00293ECF File Offset: 0x002920CF
		public TableRowProperties()
		{
		}

		// Token: 0x0601ACA4 RID: 109732 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TableRowProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601ACA5 RID: 109733 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TableRowProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601ACA6 RID: 109734 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TableRowProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601ACA7 RID: 109735 RVA: 0x0036794C File Offset: 0x00365B4C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "cnfStyle" == name)
			{
				return new ConditionalFormatStyle();
			}
			if (23 == namespaceId && "divId" == name)
			{
				return new DivId();
			}
			if (23 == namespaceId && "gridBefore" == name)
			{
				return new GridBefore();
			}
			if (23 == namespaceId && "gridAfter" == name)
			{
				return new GridAfter();
			}
			if (23 == namespaceId && "wBefore" == name)
			{
				return new WidthBeforeTableRow();
			}
			if (23 == namespaceId && "wAfter" == name)
			{
				return new WidthAfterTableRow();
			}
			if (23 == namespaceId && "trHeight" == name)
			{
				return new TableRowHeight();
			}
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
			if (23 == namespaceId && "ins" == name)
			{
				return new Inserted();
			}
			if (23 == namespaceId && "del" == name)
			{
				return new Deleted();
			}
			if (23 == namespaceId && "trPrChange" == name)
			{
				return new TableRowPropertiesChange();
			}
			if (52 == namespaceId && "conflictIns" == name)
			{
				return new ConflictInsertion();
			}
			if (52 == namespaceId && "conflictDel" == name)
			{
				return new ConflictDeletion();
			}
			return null;
		}

		// Token: 0x0601ACA8 RID: 109736 RVA: 0x00367AF2 File Offset: 0x00365CF2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableRowProperties>(deep);
		}

		// Token: 0x0400AE52 RID: 44626
		private const string tagName = "trPr";

		// Token: 0x0400AE53 RID: 44627
		private const byte tagNsId = 23;

		// Token: 0x0400AE54 RID: 44628
		internal const int ElementTypeIdConst = 12132;
	}
}
