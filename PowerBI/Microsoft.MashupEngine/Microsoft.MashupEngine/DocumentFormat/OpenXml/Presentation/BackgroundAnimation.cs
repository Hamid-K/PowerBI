using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x020029E6 RID: 10726
	[GeneratedCode("DomGen", "2.0")]
	internal class BackgroundAnimation : EmptyType
	{
		// Token: 0x17006E4B RID: 28235
		// (get) Token: 0x06015585 RID: 87429 RVA: 0x002EF0F4 File Offset: 0x002ED2F4
		public override string LocalName
		{
			get
			{
				return "bg";
			}
		}

		// Token: 0x17006E4C RID: 28236
		// (get) Token: 0x06015586 RID: 87430 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006E4D RID: 28237
		// (get) Token: 0x06015587 RID: 87431 RVA: 0x0031E1C7 File Offset: 0x0031C3C7
		internal override int ElementTypeId
		{
			get
			{
				return 12370;
			}
		}

		// Token: 0x06015588 RID: 87432 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601558A RID: 87434 RVA: 0x0031E1CE File Offset: 0x0031C3CE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BackgroundAnimation>(deep);
		}

		// Token: 0x0400930C RID: 37644
		private const string tagName = "bg";

		// Token: 0x0400930D RID: 37645
		private const byte tagNsId = 24;

		// Token: 0x0400930E RID: 37646
		internal const int ElementTypeIdConst = 12370;
	}
}
