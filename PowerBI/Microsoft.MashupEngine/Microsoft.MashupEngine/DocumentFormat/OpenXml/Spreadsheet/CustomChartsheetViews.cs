using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C81 RID: 11393
	[ChildElementInfo(typeof(CustomChartsheetView))]
	[GeneratedCode("DomGen", "2.0")]
	internal class CustomChartsheetViews : OpenXmlCompositeElement
	{
		// Token: 0x17008359 RID: 33625
		// (get) Token: 0x06018454 RID: 99412 RVA: 0x0033FFEB File Offset: 0x0033E1EB
		public override string LocalName
		{
			get
			{
				return "customSheetViews";
			}
		}

		// Token: 0x1700835A RID: 33626
		// (get) Token: 0x06018455 RID: 99413 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700835B RID: 33627
		// (get) Token: 0x06018456 RID: 99414 RVA: 0x0033FFF2 File Offset: 0x0033E1F2
		internal override int ElementTypeId
		{
			get
			{
				return 11373;
			}
		}

		// Token: 0x06018457 RID: 99415 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018458 RID: 99416 RVA: 0x00293ECF File Offset: 0x002920CF
		public CustomChartsheetViews()
		{
		}

		// Token: 0x06018459 RID: 99417 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CustomChartsheetViews(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601845A RID: 99418 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CustomChartsheetViews(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601845B RID: 99419 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CustomChartsheetViews(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601845C RID: 99420 RVA: 0x0033FFF9 File Offset: 0x0033E1F9
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "customSheetView" == name)
			{
				return new CustomChartsheetView();
			}
			return null;
		}

		// Token: 0x0601845D RID: 99421 RVA: 0x00340014 File Offset: 0x0033E214
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomChartsheetViews>(deep);
		}

		// Token: 0x04009F93 RID: 40851
		private const string tagName = "customSheetViews";

		// Token: 0x04009F94 RID: 40852
		private const byte tagNsId = 22;

		// Token: 0x04009F95 RID: 40853
		internal const int ElementTypeIdConst = 11373;
	}
}
