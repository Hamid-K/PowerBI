using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C99 RID: 11417
	[ChildElementInfo(typeof(CustomSheetView))]
	[GeneratedCode("DomGen", "2.0")]
	internal class CustomSheetViews : OpenXmlCompositeElement
	{
		// Token: 0x17008417 RID: 33815
		// (get) Token: 0x06018603 RID: 99843 RVA: 0x0033FFEB File Offset: 0x0033E1EB
		public override string LocalName
		{
			get
			{
				return "customSheetViews";
			}
		}

		// Token: 0x17008418 RID: 33816
		// (get) Token: 0x06018604 RID: 99844 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008419 RID: 33817
		// (get) Token: 0x06018605 RID: 99845 RVA: 0x0034115E File Offset: 0x0033F35E
		internal override int ElementTypeId
		{
			get
			{
				return 11397;
			}
		}

		// Token: 0x06018606 RID: 99846 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018607 RID: 99847 RVA: 0x00293ECF File Offset: 0x002920CF
		public CustomSheetViews()
		{
		}

		// Token: 0x06018608 RID: 99848 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CustomSheetViews(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018609 RID: 99849 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CustomSheetViews(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601860A RID: 99850 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CustomSheetViews(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601860B RID: 99851 RVA: 0x00341165 File Offset: 0x0033F365
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "customSheetView" == name)
			{
				return new CustomSheetView();
			}
			return null;
		}

		// Token: 0x0601860C RID: 99852 RVA: 0x00341180 File Offset: 0x0033F380
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomSheetViews>(deep);
		}

		// Token: 0x04009FFA RID: 40954
		private const string tagName = "customSheetViews";

		// Token: 0x04009FFB RID: 40955
		private const byte tagNsId = 22;

		// Token: 0x04009FFC RID: 40956
		internal const int ElementTypeIdConst = 11397;
	}
}
