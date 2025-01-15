using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D80 RID: 11648
	[GeneratedCode("DomGen", "2.0")]
	internal class Bold : OnOffType
	{
		// Token: 0x1700871C RID: 34588
		// (get) Token: 0x06018CF8 RID: 101624 RVA: 0x0032F0BC File Offset: 0x0032D2BC
		public override string LocalName
		{
			get
			{
				return "b";
			}
		}

		// Token: 0x1700871D RID: 34589
		// (get) Token: 0x06018CF9 RID: 101625 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700871E RID: 34590
		// (get) Token: 0x06018CFA RID: 101626 RVA: 0x00344BD3 File Offset: 0x00342DD3
		internal override int ElementTypeId
		{
			get
			{
				return 11577;
			}
		}

		// Token: 0x06018CFB RID: 101627 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018CFD RID: 101629 RVA: 0x00344BDA File Offset: 0x00342DDA
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Bold>(deep);
		}

		// Token: 0x0400A4EB RID: 42219
		private const string tagName = "b";

		// Token: 0x0400A4EC RID: 42220
		private const byte tagNsId = 23;

		// Token: 0x0400A4ED RID: 42221
		internal const int ElementTypeIdConst = 11577;
	}
}
