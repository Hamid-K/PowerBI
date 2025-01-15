using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028F7 RID: 10487
	[GeneratedCode("DomGen", "2.0")]
	internal class Editor : NameType
	{
		// Token: 0x1700698E RID: 27022
		// (get) Token: 0x06014AB8 RID: 84664 RVA: 0x0031548C File Offset: 0x0031368C
		public override string LocalName
		{
			get
			{
				return "Editor";
			}
		}

		// Token: 0x1700698F RID: 27023
		// (get) Token: 0x06014AB9 RID: 84665 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17006990 RID: 27024
		// (get) Token: 0x06014ABA RID: 84666 RVA: 0x00315493 File Offset: 0x00313693
		internal override int ElementTypeId
		{
			get
			{
				return 10773;
			}
		}

		// Token: 0x06014ABB RID: 84667 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014ABC RID: 84668 RVA: 0x003153D6 File Offset: 0x003135D6
		public Editor()
		{
		}

		// Token: 0x06014ABD RID: 84669 RVA: 0x003153DE File Offset: 0x003135DE
		public Editor(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014ABE RID: 84670 RVA: 0x003153E7 File Offset: 0x003135E7
		public Editor(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014ABF RID: 84671 RVA: 0x003153F0 File Offset: 0x003135F0
		public Editor(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014AC0 RID: 84672 RVA: 0x0031549A File Offset: 0x0031369A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Editor>(deep);
		}

		// Token: 0x04008F70 RID: 36720
		private const string tagName = "Editor";

		// Token: 0x04008F71 RID: 36721
		private const byte tagNsId = 9;

		// Token: 0x04008F72 RID: 36722
		internal const int ElementTypeIdConst = 10773;
	}
}
