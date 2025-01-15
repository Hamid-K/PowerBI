using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C5B RID: 11355
	[ChildElementInfo(typeof(CustomWorkbookView))]
	[GeneratedCode("DomGen", "2.0")]
	internal class CustomWorkbookViews : OpenXmlCompositeElement
	{
		// Token: 0x17008261 RID: 33377
		// (get) Token: 0x060181FA RID: 98810 RVA: 0x0033EBDB File Offset: 0x0033CDDB
		public override string LocalName
		{
			get
			{
				return "customWorkbookViews";
			}
		}

		// Token: 0x17008262 RID: 33378
		// (get) Token: 0x060181FB RID: 98811 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008263 RID: 33379
		// (get) Token: 0x060181FC RID: 98812 RVA: 0x0033EBE2 File Offset: 0x0033CDE2
		internal override int ElementTypeId
		{
			get
			{
				return 11336;
			}
		}

		// Token: 0x060181FD RID: 98813 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060181FE RID: 98814 RVA: 0x00293ECF File Offset: 0x002920CF
		public CustomWorkbookViews()
		{
		}

		// Token: 0x060181FF RID: 98815 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CustomWorkbookViews(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018200 RID: 98816 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CustomWorkbookViews(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018201 RID: 98817 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CustomWorkbookViews(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018202 RID: 98818 RVA: 0x0033EBE9 File Offset: 0x0033CDE9
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "customWorkbookView" == name)
			{
				return new CustomWorkbookView();
			}
			return null;
		}

		// Token: 0x06018203 RID: 98819 RVA: 0x0033EC04 File Offset: 0x0033CE04
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomWorkbookViews>(deep);
		}

		// Token: 0x04009EF0 RID: 40688
		private const string tagName = "customWorkbookViews";

		// Token: 0x04009EF1 RID: 40689
		private const byte tagNsId = 22;

		// Token: 0x04009EF2 RID: 40690
		internal const int ElementTypeIdConst = 11336;
	}
}
