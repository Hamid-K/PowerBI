using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002657 RID: 9815
	[GeneratedCode("DomGen", "2.0")]
	internal class TextFillColorList : ColorsType
	{
		// Token: 0x17005B63 RID: 23395
		// (get) Token: 0x06012A6E RID: 76398 RVA: 0x002FDAC2 File Offset: 0x002FBCC2
		public override string LocalName
		{
			get
			{
				return "txFillClrLst";
			}
		}

		// Token: 0x17005B64 RID: 23396
		// (get) Token: 0x06012A6F RID: 76399 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005B65 RID: 23397
		// (get) Token: 0x06012A70 RID: 76400 RVA: 0x002FDAC9 File Offset: 0x002FBCC9
		internal override int ElementTypeId
		{
			get
			{
				return 10632;
			}
		}

		// Token: 0x06012A71 RID: 76401 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012A72 RID: 76402 RVA: 0x002FDA51 File Offset: 0x002FBC51
		public TextFillColorList()
		{
		}

		// Token: 0x06012A73 RID: 76403 RVA: 0x002FDA59 File Offset: 0x002FBC59
		public TextFillColorList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012A74 RID: 76404 RVA: 0x002FDA62 File Offset: 0x002FBC62
		public TextFillColorList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012A75 RID: 76405 RVA: 0x002FDA6B File Offset: 0x002FBC6B
		public TextFillColorList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012A76 RID: 76406 RVA: 0x002FDAD0 File Offset: 0x002FBCD0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TextFillColorList>(deep);
		}

		// Token: 0x0400810F RID: 33039
		private const string tagName = "txFillClrLst";

		// Token: 0x04008110 RID: 33040
		private const byte tagNsId = 14;

		// Token: 0x04008111 RID: 33041
		internal const int ElementTypeIdConst = 10632;
	}
}
