using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200274E RID: 10062
	[GeneratedCode("DomGen", "2.0")]
	internal class ComplexScriptFont : TextFontType
	{
		// Token: 0x17006091 RID: 24721
		// (get) Token: 0x060135D3 RID: 79315 RVA: 0x00306574 File Offset: 0x00304774
		public override string LocalName
		{
			get
			{
				return "cs";
			}
		}

		// Token: 0x17006092 RID: 24722
		// (get) Token: 0x060135D4 RID: 79316 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006093 RID: 24723
		// (get) Token: 0x060135D5 RID: 79317 RVA: 0x0030657B File Offset: 0x0030477B
		internal override int ElementTypeId
		{
			get
			{
				return 10133;
			}
		}

		// Token: 0x060135D6 RID: 79318 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060135D8 RID: 79320 RVA: 0x00306582 File Offset: 0x00304782
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ComplexScriptFont>(deep);
		}

		// Token: 0x040085DB RID: 34267
		private const string tagName = "cs";

		// Token: 0x040085DC RID: 34268
		private const byte tagNsId = 10;

		// Token: 0x040085DD RID: 34269
		internal const int ElementTypeIdConst = 10133;
	}
}
