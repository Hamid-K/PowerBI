using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D93 RID: 11667
	[GeneratedCode("DomGen", "2.0")]
	internal class Hidden : OnOffType
	{
		// Token: 0x17008755 RID: 34645
		// (get) Token: 0x06018D6A RID: 101738 RVA: 0x00344D50 File Offset: 0x00342F50
		public override string LocalName
		{
			get
			{
				return "hidden";
			}
		}

		// Token: 0x17008756 RID: 34646
		// (get) Token: 0x06018D6B RID: 101739 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008757 RID: 34647
		// (get) Token: 0x06018D6C RID: 101740 RVA: 0x00344D57 File Offset: 0x00342F57
		internal override int ElementTypeId
		{
			get
			{
				return 11666;
			}
		}

		// Token: 0x06018D6D RID: 101741 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018D6F RID: 101743 RVA: 0x00344D5E File Offset: 0x00342F5E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Hidden>(deep);
		}

		// Token: 0x0400A524 RID: 42276
		private const string tagName = "hidden";

		// Token: 0x0400A525 RID: 42277
		private const byte tagNsId = 23;

		// Token: 0x0400A526 RID: 42278
		internal const int ElementTypeIdConst = 11666;
	}
}
