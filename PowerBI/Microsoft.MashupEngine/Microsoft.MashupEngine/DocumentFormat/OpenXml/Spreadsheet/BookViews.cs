using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C54 RID: 11348
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(WorkbookView))]
	internal class BookViews : OpenXmlCompositeElement
	{
		// Token: 0x17008237 RID: 33335
		// (get) Token: 0x06018192 RID: 98706 RVA: 0x0033E82D File Offset: 0x0033CA2D
		public override string LocalName
		{
			get
			{
				return "bookViews";
			}
		}

		// Token: 0x17008238 RID: 33336
		// (get) Token: 0x06018193 RID: 98707 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008239 RID: 33337
		// (get) Token: 0x06018194 RID: 98708 RVA: 0x0033E834 File Offset: 0x0033CA34
		internal override int ElementTypeId
		{
			get
			{
				return 11329;
			}
		}

		// Token: 0x06018195 RID: 98709 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018196 RID: 98710 RVA: 0x00293ECF File Offset: 0x002920CF
		public BookViews()
		{
		}

		// Token: 0x06018197 RID: 98711 RVA: 0x00293ED7 File Offset: 0x002920D7
		public BookViews(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018198 RID: 98712 RVA: 0x00293EE0 File Offset: 0x002920E0
		public BookViews(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018199 RID: 98713 RVA: 0x00293EE9 File Offset: 0x002920E9
		public BookViews(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601819A RID: 98714 RVA: 0x0033E83B File Offset: 0x0033CA3B
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "workbookView" == name)
			{
				return new WorkbookView();
			}
			return null;
		}

		// Token: 0x0601819B RID: 98715 RVA: 0x0033E856 File Offset: 0x0033CA56
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BookViews>(deep);
		}

		// Token: 0x04009ED5 RID: 40661
		private const string tagName = "bookViews";

		// Token: 0x04009ED6 RID: 40662
		private const byte tagNsId = 22;

		// Token: 0x04009ED7 RID: 40663
		internal const int ElementTypeIdConst = 11329;
	}
}
