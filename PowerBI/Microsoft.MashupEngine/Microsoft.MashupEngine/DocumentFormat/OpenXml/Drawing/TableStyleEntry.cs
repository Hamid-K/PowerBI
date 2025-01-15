using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027F1 RID: 10225
	[GeneratedCode("DomGen", "2.0")]
	internal class TableStyleEntry : TableStyleType
	{
		// Token: 0x170064D7 RID: 25815
		// (get) Token: 0x06013F7A RID: 81786 RVA: 0x0030DEEC File Offset: 0x0030C0EC
		public override string LocalName
		{
			get
			{
				return "tblStyle";
			}
		}

		// Token: 0x170064D8 RID: 25816
		// (get) Token: 0x06013F7B RID: 81787 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170064D9 RID: 25817
		// (get) Token: 0x06013F7C RID: 81788 RVA: 0x0030DEF3 File Offset: 0x0030C0F3
		internal override int ElementTypeId
		{
			get
			{
				return 10293;
			}
		}

		// Token: 0x06013F7D RID: 81789 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013F7E RID: 81790 RVA: 0x0030DEC0 File Offset: 0x0030C0C0
		public TableStyleEntry()
		{
		}

		// Token: 0x06013F7F RID: 81791 RVA: 0x0030DEC8 File Offset: 0x0030C0C8
		public TableStyleEntry(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013F80 RID: 81792 RVA: 0x0030DED1 File Offset: 0x0030C0D1
		public TableStyleEntry(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013F81 RID: 81793 RVA: 0x0030DEDA File Offset: 0x0030C0DA
		public TableStyleEntry(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013F82 RID: 81794 RVA: 0x0030DEFA File Offset: 0x0030C0FA
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableStyleEntry>(deep);
		}

		// Token: 0x04008870 RID: 34928
		private const string tagName = "tblStyle";

		// Token: 0x04008871 RID: 34929
		private const byte tagNsId = 10;

		// Token: 0x04008872 RID: 34930
		internal const int ElementTypeIdConst = 10293;
	}
}
