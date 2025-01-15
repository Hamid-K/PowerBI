using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C90 RID: 11408
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(SheetView))]
	internal class SheetViews : OpenXmlCompositeElement
	{
		// Token: 0x170083C6 RID: 33734
		// (get) Token: 0x0601854C RID: 99660 RVA: 0x0033FE6E File Offset: 0x0033E06E
		public override string LocalName
		{
			get
			{
				return "sheetViews";
			}
		}

		// Token: 0x170083C7 RID: 33735
		// (get) Token: 0x0601854D RID: 99661 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170083C8 RID: 33736
		// (get) Token: 0x0601854E RID: 99662 RVA: 0x00340963 File Offset: 0x0033EB63
		internal override int ElementTypeId
		{
			get
			{
				return 11388;
			}
		}

		// Token: 0x0601854F RID: 99663 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018550 RID: 99664 RVA: 0x00293ECF File Offset: 0x002920CF
		public SheetViews()
		{
		}

		// Token: 0x06018551 RID: 99665 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SheetViews(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018552 RID: 99666 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SheetViews(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018553 RID: 99667 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SheetViews(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018554 RID: 99668 RVA: 0x0034096A File Offset: 0x0033EB6A
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "sheetView" == name)
			{
				return new SheetView();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x06018555 RID: 99669 RVA: 0x0034099D File Offset: 0x0033EB9D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SheetViews>(deep);
		}

		// Token: 0x04009FD3 RID: 40915
		private const string tagName = "sheetViews";

		// Token: 0x04009FD4 RID: 40916
		private const byte tagNsId = 22;

		// Token: 0x04009FD5 RID: 40917
		internal const int ElementTypeIdConst = 11388;
	}
}
