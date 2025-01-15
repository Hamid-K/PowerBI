using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C7F RID: 11391
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ChartSheetView))]
	internal class ChartSheetViews : OpenXmlCompositeElement
	{
		// Token: 0x1700834A RID: 33610
		// (get) Token: 0x06018432 RID: 99378 RVA: 0x0033FE6E File Offset: 0x0033E06E
		public override string LocalName
		{
			get
			{
				return "sheetViews";
			}
		}

		// Token: 0x1700834B RID: 33611
		// (get) Token: 0x06018433 RID: 99379 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700834C RID: 33612
		// (get) Token: 0x06018434 RID: 99380 RVA: 0x0033FE75 File Offset: 0x0033E075
		internal override int ElementTypeId
		{
			get
			{
				return 11371;
			}
		}

		// Token: 0x06018435 RID: 99381 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018436 RID: 99382 RVA: 0x00293ECF File Offset: 0x002920CF
		public ChartSheetViews()
		{
		}

		// Token: 0x06018437 RID: 99383 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ChartSheetViews(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018438 RID: 99384 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ChartSheetViews(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018439 RID: 99385 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ChartSheetViews(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601843A RID: 99386 RVA: 0x0033FE7C File Offset: 0x0033E07C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "sheetView" == name)
			{
				return new ChartSheetView();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x0601843B RID: 99387 RVA: 0x0033FEAF File Offset: 0x0033E0AF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ChartSheetViews>(deep);
		}

		// Token: 0x04009F8B RID: 40843
		private const string tagName = "sheetViews";

		// Token: 0x04009F8C RID: 40844
		private const byte tagNsId = 22;

		// Token: 0x04009F8D RID: 40845
		internal const int ElementTypeIdConst = 11371;
	}
}
