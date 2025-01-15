using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027F0 RID: 10224
	[GeneratedCode("DomGen", "2.0")]
	internal class TableStyle : TableStyleType
	{
		// Token: 0x170064D4 RID: 25812
		// (get) Token: 0x06013F71 RID: 81777 RVA: 0x0030DEB2 File Offset: 0x0030C0B2
		public override string LocalName
		{
			get
			{
				return "tableStyle";
			}
		}

		// Token: 0x170064D5 RID: 25813
		// (get) Token: 0x06013F72 RID: 81778 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170064D6 RID: 25814
		// (get) Token: 0x06013F73 RID: 81779 RVA: 0x0030DEB9 File Offset: 0x0030C0B9
		internal override int ElementTypeId
		{
			get
			{
				return 10261;
			}
		}

		// Token: 0x06013F74 RID: 81780 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013F75 RID: 81781 RVA: 0x0030DEC0 File Offset: 0x0030C0C0
		public TableStyle()
		{
		}

		// Token: 0x06013F76 RID: 81782 RVA: 0x0030DEC8 File Offset: 0x0030C0C8
		public TableStyle(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013F77 RID: 81783 RVA: 0x0030DED1 File Offset: 0x0030C0D1
		public TableStyle(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013F78 RID: 81784 RVA: 0x0030DEDA File Offset: 0x0030C0DA
		public TableStyle(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013F79 RID: 81785 RVA: 0x0030DEE3 File Offset: 0x0030C0E3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableStyle>(deep);
		}

		// Token: 0x0400886D RID: 34925
		private const string tagName = "tableStyle";

		// Token: 0x0400886E RID: 34926
		private const byte tagNsId = 10;

		// Token: 0x0400886F RID: 34927
		internal const int ElementTypeIdConst = 10261;
	}
}
