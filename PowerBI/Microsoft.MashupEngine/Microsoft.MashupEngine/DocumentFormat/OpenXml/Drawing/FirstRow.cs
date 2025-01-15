using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200280F RID: 10255
	[GeneratedCode("DomGen", "2.0")]
	internal class FirstRow : TablePartStyleType
	{
		// Token: 0x1700655C RID: 25948
		// (get) Token: 0x060140DE RID: 82142 RVA: 0x0030EB5E File Offset: 0x0030CD5E
		public override string LocalName
		{
			get
			{
				return "firstRow";
			}
		}

		// Token: 0x1700655D RID: 25949
		// (get) Token: 0x060140DF RID: 82143 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700655E RID: 25950
		// (get) Token: 0x060140E0 RID: 82144 RVA: 0x0030EB65 File Offset: 0x0030CD65
		internal override int ElementTypeId
		{
			get
			{
				return 10290;
			}
		}

		// Token: 0x060140E1 RID: 82145 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060140E2 RID: 82146 RVA: 0x0030EA63 File Offset: 0x0030CC63
		public FirstRow()
		{
		}

		// Token: 0x060140E3 RID: 82147 RVA: 0x0030EA6B File Offset: 0x0030CC6B
		public FirstRow(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060140E4 RID: 82148 RVA: 0x0030EA74 File Offset: 0x0030CC74
		public FirstRow(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060140E5 RID: 82149 RVA: 0x0030EA7D File Offset: 0x0030CC7D
		public FirstRow(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060140E6 RID: 82150 RVA: 0x0030EB6C File Offset: 0x0030CD6C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FirstRow>(deep);
		}

		// Token: 0x040088D6 RID: 35030
		private const string tagName = "firstRow";

		// Token: 0x040088D7 RID: 35031
		private const byte tagNsId = 10;

		// Token: 0x040088D8 RID: 35032
		internal const int ElementTypeIdConst = 10290;
	}
}
