using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x020029E8 RID: 10728
	[GeneratedCode("DomGen", "2.0")]
	internal class DissolveTransition : EmptyType
	{
		// Token: 0x17006E51 RID: 28241
		// (get) Token: 0x06015591 RID: 87441 RVA: 0x0031E1EE File Offset: 0x0031C3EE
		public override string LocalName
		{
			get
			{
				return "dissolve";
			}
		}

		// Token: 0x17006E52 RID: 28242
		// (get) Token: 0x06015592 RID: 87442 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006E53 RID: 28243
		// (get) Token: 0x06015593 RID: 87443 RVA: 0x0031E1F5 File Offset: 0x0031C3F5
		internal override int ElementTypeId
		{
			get
			{
				return 12378;
			}
		}

		// Token: 0x06015594 RID: 87444 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015596 RID: 87446 RVA: 0x0031E1FC File Offset: 0x0031C3FC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DissolveTransition>(deep);
		}

		// Token: 0x04009312 RID: 37650
		private const string tagName = "dissolve";

		// Token: 0x04009313 RID: 37651
		private const byte tagNsId = 24;

		// Token: 0x04009314 RID: 37652
		internal const int ElementTypeIdConst = 12378;
	}
}
