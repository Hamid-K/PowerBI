using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D6F RID: 11631
	[GeneratedCode("DomGen", "2.0")]
	internal class SuppressAutoHyphens : OnOffType
	{
		// Token: 0x170086E9 RID: 34537
		// (get) Token: 0x06018C92 RID: 101522 RVA: 0x00344A53 File Offset: 0x00342C53
		public override string LocalName
		{
			get
			{
				return "suppressAutoHyphens";
			}
		}

		// Token: 0x170086EA RID: 34538
		// (get) Token: 0x06018C93 RID: 101523 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170086EB RID: 34539
		// (get) Token: 0x06018C94 RID: 101524 RVA: 0x00344A5A File Offset: 0x00342C5A
		internal override int ElementTypeId
		{
			get
			{
				return 11503;
			}
		}

		// Token: 0x06018C95 RID: 101525 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018C97 RID: 101527 RVA: 0x00344A61 File Offset: 0x00342C61
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SuppressAutoHyphens>(deep);
		}

		// Token: 0x0400A4B8 RID: 42168
		private const string tagName = "suppressAutoHyphens";

		// Token: 0x0400A4B9 RID: 42169
		private const byte tagNsId = 23;

		// Token: 0x0400A4BA RID: 42170
		internal const int ElementTypeIdConst = 11503;
	}
}
