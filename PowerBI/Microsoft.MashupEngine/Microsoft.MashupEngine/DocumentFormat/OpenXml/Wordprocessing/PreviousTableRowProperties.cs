using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F21 RID: 12065
	[ChildElementInfo(typeof(TableHeader))]
	[ChildElementInfo(typeof(ConditionalFormatStyle))]
	[ChildElementInfo(typeof(DivId))]
	[ChildElementInfo(typeof(GridBefore))]
	[ChildElementInfo(typeof(GridAfter))]
	[ChildElementInfo(typeof(WidthBeforeTableRow))]
	[ChildElementInfo(typeof(WidthAfterTableRow))]
	[ChildElementInfo(typeof(TableRowHeight))]
	[ChildElementInfo(typeof(Hidden))]
	[ChildElementInfo(typeof(CantSplit))]
	[ChildElementInfo(typeof(TableCellSpacing))]
	[ChildElementInfo(typeof(TableJustification))]
	[GeneratedCode("DomGen", "2.0")]
	internal class PreviousTableRowProperties : OpenXmlCompositeElement
	{
		// Token: 0x17008EC9 RID: 36553
		// (get) Token: 0x06019D48 RID: 105800 RVA: 0x00357191 File Offset: 0x00355391
		public override string LocalName
		{
			get
			{
				return "trPr";
			}
		}

		// Token: 0x17008ECA RID: 36554
		// (get) Token: 0x06019D49 RID: 105801 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008ECB RID: 36555
		// (get) Token: 0x06019D4A RID: 105802 RVA: 0x00357198 File Offset: 0x00355398
		internal override int ElementTypeId
		{
			get
			{
				return 11706;
			}
		}

		// Token: 0x06019D4B RID: 105803 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019D4C RID: 105804 RVA: 0x00293ECF File Offset: 0x002920CF
		public PreviousTableRowProperties()
		{
		}

		// Token: 0x06019D4D RID: 105805 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PreviousTableRowProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019D4E RID: 105806 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PreviousTableRowProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019D4F RID: 105807 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PreviousTableRowProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019D50 RID: 105808 RVA: 0x003571A0 File Offset: 0x003553A0
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
			return null;
		}

		// Token: 0x06019D51 RID: 105809 RVA: 0x003572CE File Offset: 0x003554CE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PreviousTableRowProperties>(deep);
		}

		// Token: 0x0400AA9A RID: 43674
		private const string tagName = "trPr";

		// Token: 0x0400AA9B RID: 43675
		private const byte tagNsId = 23;

		// Token: 0x0400AA9C RID: 43676
		internal const int ElementTypeIdConst = 11706;
	}
}
