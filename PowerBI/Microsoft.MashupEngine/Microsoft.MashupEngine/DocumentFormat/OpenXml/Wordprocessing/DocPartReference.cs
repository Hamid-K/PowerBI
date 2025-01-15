using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D55 RID: 11605
	[GeneratedCode("DomGen", "2.0")]
	internal class DocPartReference : StringType
	{
		// Token: 0x1700869B RID: 34459
		// (get) Token: 0x06018BF5 RID: 101365 RVA: 0x003447D0 File Offset: 0x003429D0
		public override string LocalName
		{
			get
			{
				return "docPart";
			}
		}

		// Token: 0x1700869C RID: 34460
		// (get) Token: 0x06018BF6 RID: 101366 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700869D RID: 34461
		// (get) Token: 0x06018BF7 RID: 101367 RVA: 0x003447D7 File Offset: 0x003429D7
		internal override int ElementTypeId
		{
			get
			{
				return 11768;
			}
		}

		// Token: 0x06018BF8 RID: 101368 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018BFA RID: 101370 RVA: 0x003447DE File Offset: 0x003429DE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DocPartReference>(deep);
		}

		// Token: 0x0400A46B RID: 42091
		private const string tagName = "docPart";

		// Token: 0x0400A46C RID: 42092
		private const byte tagNsId = 23;

		// Token: 0x0400A46D RID: 42093
		internal const int ElementTypeIdConst = 11768;
	}
}
