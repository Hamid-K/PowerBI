using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200274F RID: 10063
	[GeneratedCode("DomGen", "2.0")]
	internal class SymbolFont : TextFontType
	{
		// Token: 0x17006094 RID: 24724
		// (get) Token: 0x060135D9 RID: 79321 RVA: 0x0030658B File Offset: 0x0030478B
		public override string LocalName
		{
			get
			{
				return "sym";
			}
		}

		// Token: 0x17006095 RID: 24725
		// (get) Token: 0x060135DA RID: 79322 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006096 RID: 24726
		// (get) Token: 0x060135DB RID: 79323 RVA: 0x00306592 File Offset: 0x00304792
		internal override int ElementTypeId
		{
			get
			{
				return 10325;
			}
		}

		// Token: 0x060135DC RID: 79324 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060135DE RID: 79326 RVA: 0x00306599 File Offset: 0x00304799
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SymbolFont>(deep);
		}

		// Token: 0x040085DE RID: 34270
		private const string tagName = "sym";

		// Token: 0x040085DF RID: 34271
		private const byte tagNsId = 10;

		// Token: 0x040085E0 RID: 34272
		internal const int ElementTypeIdConst = 10325;
	}
}
