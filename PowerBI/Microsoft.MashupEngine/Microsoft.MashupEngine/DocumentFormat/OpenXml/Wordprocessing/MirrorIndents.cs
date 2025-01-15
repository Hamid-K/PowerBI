using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D7A RID: 11642
	[GeneratedCode("DomGen", "2.0")]
	internal class MirrorIndents : OnOffType
	{
		// Token: 0x1700870A RID: 34570
		// (get) Token: 0x06018CD4 RID: 101588 RVA: 0x00344B49 File Offset: 0x00342D49
		public override string LocalName
		{
			get
			{
				return "mirrorIndents";
			}
		}

		// Token: 0x1700870B RID: 34571
		// (get) Token: 0x06018CD5 RID: 101589 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700870C RID: 34572
		// (get) Token: 0x06018CD6 RID: 101590 RVA: 0x00344B50 File Offset: 0x00342D50
		internal override int ElementTypeId
		{
			get
			{
				return 11516;
			}
		}

		// Token: 0x06018CD7 RID: 101591 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018CD9 RID: 101593 RVA: 0x00344B57 File Offset: 0x00342D57
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MirrorIndents>(deep);
		}

		// Token: 0x0400A4D9 RID: 42201
		private const string tagName = "mirrorIndents";

		// Token: 0x0400A4DA RID: 42202
		private const byte tagNsId = 23;

		// Token: 0x0400A4DB RID: 42203
		internal const int ElementTypeIdConst = 11516;
	}
}
