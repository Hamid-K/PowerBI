using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D8B RID: 11659
	[GeneratedCode("DomGen", "2.0")]
	internal class Imprint : OnOffType
	{
		// Token: 0x1700873D RID: 34621
		// (get) Token: 0x06018D3A RID: 101690 RVA: 0x00344CAD File Offset: 0x00342EAD
		public override string LocalName
		{
			get
			{
				return "imprint";
			}
		}

		// Token: 0x1700873E RID: 34622
		// (get) Token: 0x06018D3B RID: 101691 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700873F RID: 34623
		// (get) Token: 0x06018D3C RID: 101692 RVA: 0x00344CB4 File Offset: 0x00342EB4
		internal override int ElementTypeId
		{
			get
			{
				return 11588;
			}
		}

		// Token: 0x06018D3D RID: 101693 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018D3F RID: 101695 RVA: 0x00344CBB File Offset: 0x00342EBB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Imprint>(deep);
		}

		// Token: 0x0400A50C RID: 42252
		private const string tagName = "imprint";

		// Token: 0x0400A50D RID: 42253
		private const byte tagNsId = 23;

		// Token: 0x0400A50E RID: 42254
		internal const int ElementTypeIdConst = 11588;
	}
}
