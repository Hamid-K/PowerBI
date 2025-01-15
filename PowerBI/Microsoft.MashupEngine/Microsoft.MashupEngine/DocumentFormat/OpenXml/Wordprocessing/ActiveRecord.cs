using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E35 RID: 11829
	[GeneratedCode("DomGen", "2.0")]
	internal class ActiveRecord : DecimalNumberType
	{
		// Token: 0x1700898B RID: 35211
		// (get) Token: 0x060191DF RID: 102879 RVA: 0x00346863 File Offset: 0x00344A63
		public override string LocalName
		{
			get
			{
				return "activeRecord";
			}
		}

		// Token: 0x1700898C RID: 35212
		// (get) Token: 0x060191E0 RID: 102880 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700898D RID: 35213
		// (get) Token: 0x060191E1 RID: 102881 RVA: 0x0034686A File Offset: 0x00344A6A
		internal override int ElementTypeId
		{
			get
			{
				return 11825;
			}
		}

		// Token: 0x060191E2 RID: 102882 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060191E4 RID: 102884 RVA: 0x00346871 File Offset: 0x00344A71
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ActiveRecord>(deep);
		}

		// Token: 0x0400A71D RID: 42781
		private const string tagName = "activeRecord";

		// Token: 0x0400A71E RID: 42782
		private const byte tagNsId = 23;

		// Token: 0x0400A71F RID: 42783
		internal const int ElementTypeIdConst = 11825;
	}
}
