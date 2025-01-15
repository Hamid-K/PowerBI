using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B30 RID: 11056
	[ChildElementInfo(typeof(Author))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Authors : OpenXmlCompositeElement
	{
		// Token: 0x1700776C RID: 30572
		// (get) Token: 0x06016A09 RID: 92681 RVA: 0x0032D5EB File Offset: 0x0032B7EB
		public override string LocalName
		{
			get
			{
				return "authors";
			}
		}

		// Token: 0x1700776D RID: 30573
		// (get) Token: 0x06016A0A RID: 92682 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700776E RID: 30574
		// (get) Token: 0x06016A0B RID: 92683 RVA: 0x0032D5F2 File Offset: 0x0032B7F2
		internal override int ElementTypeId
		{
			get
			{
				return 11054;
			}
		}

		// Token: 0x06016A0C RID: 92684 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016A0D RID: 92685 RVA: 0x00293ECF File Offset: 0x002920CF
		public Authors()
		{
		}

		// Token: 0x06016A0E RID: 92686 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Authors(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016A0F RID: 92687 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Authors(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016A10 RID: 92688 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Authors(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016A11 RID: 92689 RVA: 0x0032D5F9 File Offset: 0x0032B7F9
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "author" == name)
			{
				return new Author();
			}
			return null;
		}

		// Token: 0x06016A12 RID: 92690 RVA: 0x0032D614 File Offset: 0x0032B814
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Authors>(deep);
		}

		// Token: 0x0400994D RID: 39245
		private const string tagName = "authors";

		// Token: 0x0400994E RID: 39246
		private const byte tagNsId = 22;

		// Token: 0x0400994F RID: 39247
		internal const int ElementTypeIdConst = 11054;
	}
}
