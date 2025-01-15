using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x020029E3 RID: 10723
	[GeneratedCode("DomGen", "2.0")]
	internal class EndSoundAction : EmptyType
	{
		// Token: 0x17006E42 RID: 28226
		// (get) Token: 0x06015573 RID: 87411 RVA: 0x0031E182 File Offset: 0x0031C382
		public override string LocalName
		{
			get
			{
				return "endSnd";
			}
		}

		// Token: 0x17006E43 RID: 28227
		// (get) Token: 0x06015574 RID: 87412 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006E44 RID: 28228
		// (get) Token: 0x06015575 RID: 87413 RVA: 0x0031E189 File Offset: 0x0031C389
		internal override int ElementTypeId
		{
			get
			{
				return 12190;
			}
		}

		// Token: 0x06015576 RID: 87414 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015578 RID: 87416 RVA: 0x0031E190 File Offset: 0x0031C390
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EndSoundAction>(deep);
		}

		// Token: 0x04009303 RID: 37635
		private const string tagName = "endSnd";

		// Token: 0x04009304 RID: 37636
		private const byte tagNsId = 24;

		// Token: 0x04009305 RID: 37637
		internal const int ElementTypeIdConst = 12190;
	}
}
