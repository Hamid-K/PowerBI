using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DAF RID: 11695
	[GeneratedCode("DomGen", "2.0")]
	internal class DoNotDisplayPageBoundaries : OnOffType
	{
		// Token: 0x170087A9 RID: 34729
		// (get) Token: 0x06018E12 RID: 101906 RVA: 0x00344FC6 File Offset: 0x003431C6
		public override string LocalName
		{
			get
			{
				return "doNotDisplayPageBoundaries";
			}
		}

		// Token: 0x170087AA RID: 34730
		// (get) Token: 0x06018E13 RID: 101907 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170087AB RID: 34731
		// (get) Token: 0x06018E14 RID: 101908 RVA: 0x00344FCD File Offset: 0x003431CD
		internal override int ElementTypeId
		{
			get
			{
				return 11963;
			}
		}

		// Token: 0x06018E15 RID: 101909 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018E17 RID: 101911 RVA: 0x00344FD4 File Offset: 0x003431D4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DoNotDisplayPageBoundaries>(deep);
		}

		// Token: 0x0400A578 RID: 42360
		private const string tagName = "doNotDisplayPageBoundaries";

		// Token: 0x0400A579 RID: 42361
		private const byte tagNsId = 23;

		// Token: 0x0400A57A RID: 42362
		internal const int ElementTypeIdConst = 11963;
	}
}
