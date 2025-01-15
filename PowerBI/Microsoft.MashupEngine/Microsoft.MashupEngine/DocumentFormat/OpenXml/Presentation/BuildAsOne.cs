using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x020029E4 RID: 10724
	[GeneratedCode("DomGen", "2.0")]
	internal class BuildAsOne : EmptyType
	{
		// Token: 0x17006E45 RID: 28229
		// (get) Token: 0x06015579 RID: 87417 RVA: 0x0031E199 File Offset: 0x0031C399
		public override string LocalName
		{
			get
			{
				return "bldAsOne";
			}
		}

		// Token: 0x17006E46 RID: 28230
		// (get) Token: 0x0601557A RID: 87418 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006E47 RID: 28231
		// (get) Token: 0x0601557B RID: 87419 RVA: 0x0031E1A0 File Offset: 0x0031C3A0
		internal override int ElementTypeId
		{
			get
			{
				return 12232;
			}
		}

		// Token: 0x0601557C RID: 87420 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601557E RID: 87422 RVA: 0x0031E1A7 File Offset: 0x0031C3A7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BuildAsOne>(deep);
		}

		// Token: 0x04009306 RID: 37638
		private const string tagName = "bldAsOne";

		// Token: 0x04009307 RID: 37639
		private const byte tagNsId = 24;

		// Token: 0x04009308 RID: 37640
		internal const int ElementTypeIdConst = 12232;
	}
}
