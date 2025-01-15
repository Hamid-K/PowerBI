using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026F0 RID: 9968
	[GeneratedCode("DomGen", "2.0")]
	internal class Gamma : OpenXmlLeafElement
	{
		// Token: 0x17005DF8 RID: 24056
		// (get) Token: 0x0601301C RID: 77852 RVA: 0x00301986 File Offset: 0x002FFB86
		public override string LocalName
		{
			get
			{
				return "gamma";
			}
		}

		// Token: 0x17005DF9 RID: 24057
		// (get) Token: 0x0601301D RID: 77853 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005DFA RID: 24058
		// (get) Token: 0x0601301E RID: 77854 RVA: 0x0030198D File Offset: 0x002FFB8D
		internal override int ElementTypeId
		{
			get
			{
				return 10032;
			}
		}

		// Token: 0x0601301F RID: 77855 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013021 RID: 77857 RVA: 0x00301994 File Offset: 0x002FFB94
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Gamma>(deep);
		}

		// Token: 0x0400843A RID: 33850
		private const string tagName = "gamma";

		// Token: 0x0400843B RID: 33851
		private const byte tagNsId = 10;

		// Token: 0x0400843C RID: 33852
		internal const int ElementTypeIdConst = 10032;
	}
}
