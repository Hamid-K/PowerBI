using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DB6 RID: 11702
	[GeneratedCode("DomGen", "2.0")]
	internal class SaveSubsetFonts : OnOffType
	{
		// Token: 0x170087BE RID: 34750
		// (get) Token: 0x06018E3C RID: 101948 RVA: 0x00345067 File Offset: 0x00343267
		public override string LocalName
		{
			get
			{
				return "saveSubsetFonts";
			}
		}

		// Token: 0x170087BF RID: 34751
		// (get) Token: 0x06018E3D RID: 101949 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170087C0 RID: 34752
		// (get) Token: 0x06018E3E RID: 101950 RVA: 0x0034506E File Offset: 0x0034326E
		internal override int ElementTypeId
		{
			get
			{
				return 11970;
			}
		}

		// Token: 0x06018E3F RID: 101951 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018E41 RID: 101953 RVA: 0x00345075 File Offset: 0x00343275
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SaveSubsetFonts>(deep);
		}

		// Token: 0x0400A58D RID: 42381
		private const string tagName = "saveSubsetFonts";

		// Token: 0x0400A58E RID: 42382
		private const byte tagNsId = 23;

		// Token: 0x0400A58F RID: 42383
		internal const int ElementTypeIdConst = 11970;
	}
}
