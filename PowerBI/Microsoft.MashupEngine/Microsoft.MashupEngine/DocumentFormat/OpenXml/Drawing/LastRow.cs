using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200280C RID: 10252
	[GeneratedCode("DomGen", "2.0")]
	internal class LastRow : TablePartStyleType
	{
		// Token: 0x17006553 RID: 25939
		// (get) Token: 0x060140C3 RID: 82115 RVA: 0x0030EB19 File Offset: 0x0030CD19
		public override string LocalName
		{
			get
			{
				return "lastRow";
			}
		}

		// Token: 0x17006554 RID: 25940
		// (get) Token: 0x060140C4 RID: 82116 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006555 RID: 25941
		// (get) Token: 0x060140C5 RID: 82117 RVA: 0x0030EB20 File Offset: 0x0030CD20
		internal override int ElementTypeId
		{
			get
			{
				return 10287;
			}
		}

		// Token: 0x060140C6 RID: 82118 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060140C7 RID: 82119 RVA: 0x0030EA63 File Offset: 0x0030CC63
		public LastRow()
		{
		}

		// Token: 0x060140C8 RID: 82120 RVA: 0x0030EA6B File Offset: 0x0030CC6B
		public LastRow(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060140C9 RID: 82121 RVA: 0x0030EA74 File Offset: 0x0030CC74
		public LastRow(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060140CA RID: 82122 RVA: 0x0030EA7D File Offset: 0x0030CC7D
		public LastRow(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060140CB RID: 82123 RVA: 0x0030EB27 File Offset: 0x0030CD27
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LastRow>(deep);
		}

		// Token: 0x040088CD RID: 35021
		private const string tagName = "lastRow";

		// Token: 0x040088CE RID: 35022
		private const byte tagNsId = 10;

		// Token: 0x040088CF RID: 35023
		internal const int ElementTypeIdConst = 10287;
	}
}
