using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002656 RID: 9814
	[GeneratedCode("DomGen", "2.0")]
	internal class TextLineColorList : ColorsType
	{
		// Token: 0x17005B60 RID: 23392
		// (get) Token: 0x06012A65 RID: 76389 RVA: 0x002FDAAB File Offset: 0x002FBCAB
		public override string LocalName
		{
			get
			{
				return "txLinClrLst";
			}
		}

		// Token: 0x17005B61 RID: 23393
		// (get) Token: 0x06012A66 RID: 76390 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005B62 RID: 23394
		// (get) Token: 0x06012A67 RID: 76391 RVA: 0x002FDAB2 File Offset: 0x002FBCB2
		internal override int ElementTypeId
		{
			get
			{
				return 10631;
			}
		}

		// Token: 0x06012A68 RID: 76392 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012A69 RID: 76393 RVA: 0x002FDA51 File Offset: 0x002FBC51
		public TextLineColorList()
		{
		}

		// Token: 0x06012A6A RID: 76394 RVA: 0x002FDA59 File Offset: 0x002FBC59
		public TextLineColorList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012A6B RID: 76395 RVA: 0x002FDA62 File Offset: 0x002FBC62
		public TextLineColorList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012A6C RID: 76396 RVA: 0x002FDA6B File Offset: 0x002FBC6B
		public TextLineColorList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012A6D RID: 76397 RVA: 0x002FDAB9 File Offset: 0x002FBCB9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TextLineColorList>(deep);
		}

		// Token: 0x0400810C RID: 33036
		private const string tagName = "txLinClrLst";

		// Token: 0x0400810D RID: 33037
		private const byte tagNsId = 14;

		// Token: 0x0400810E RID: 33038
		internal const int ElementTypeIdConst = 10631;
	}
}
